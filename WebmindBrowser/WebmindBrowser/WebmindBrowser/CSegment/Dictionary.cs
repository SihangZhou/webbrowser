using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections ;
using System.IO;

namespace WebmindBrowser.CSegment
{
    class Dictionary
    {
        private Encoding gbk = Encoding.GetEncoding("gbk");
        private Encoding ut8 = Encoding.UTF8;

        private Utility util = new Utility();
        private Comp comp = new Comp();

        /*
         * 内码是指整机汉字系统中使用的二进制字符编码，
         * 是沟通输入、输出与系统平台之间的交换码，
         * 通过内码可以达到通用和高效率传输文本的目的。
         * 比如MS Word中所存储和调用的就是内码而非图形文字。
         * 英文ASCII 字符采用一个字节的内码表示，中文字符如国标字符集中，
         * GB2312、GB12345、GB13000皆用双字节内码，GB18030（27,533汉字）双字节内码汉字为20,902个，
         * 其余6,631个汉字用四字节内码。
         * 
         */
        public const int SINGLE_WORD_INNERCODE = 20902;
        public const int MaxWordSize = 30;  //词长
        public const int Maxsize = 5000;    //一个状态的转移状态总数

        public ArrayList m_vBase;//可动态扩容的
        public ArrayList m_vCheck;

        public uint m_nDictItemCount ;  //词典总词条数
        public uint m_nDictArraySize ;  //词典数组长度
        public uint m_nWordMaxSize ;    //词典最大词长度

        public bool m_IsDirty;            //数组是否被修改过

        //用于建索引
        private String[] m_pcWordArray ;
        private int[] m_piWordLenArray ;

        struct StateInfo {
            public int nStateSub;			//当前状态的base和check数组下标,代表着一个字
            public int nNextStateCount;	//所有下一状态数
            public int[] pNextID;			//下一状态ID数组,其值为当前状态延续字的内码
        } ;


        class Comp : IComparer
        {
            public int Compare(Object x, Object y)
            {
                StateInfo xx = (StateInfo)x;
                StateInfo yy = (StateInfo)y;

                if (xx.nNextStateCount > yy.nNextStateCount)
                {
                    return -1;
                }
                else if (xx.nNextStateCount == yy.nNextStateCount)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public Dictionary()
        {
            m_nDictItemCount = 0;
            m_nWordMaxSize = 0;
            m_piWordLenArray = null;
            m_pcWordArray = null;
            m_IsDirty = false;
            m_vBase = null;
            m_vCheck = null;

        }

        // 保存数组文件
        public bool SaveArray(String cFileName, String cArrayFileName, bool IsDirty)//字典文本，双数组字典(即结果),isDirty为false
        {
            if (!Construct_Arrays(cFileName))
            {
                return false;
            }

            try
            {
                FileStream ArrayFile = File.OpenWrite(cArrayFileName);
                StreamWriter ArrayWriter = new StreamWriter(ArrayFile, System.Text.Encoding.GetEncoding("GBK"));

                m_nDictArraySize = GetArraySize();

                ArrayWriter.WriteLine(m_nDictItemCount);
                ArrayWriter.WriteLine(m_nWordMaxSize);
                ArrayWriter.WriteLine(m_nDictArraySize);

                
                int i;
                for (i = 0; i < m_nDictArraySize; i++)
                {
                    ArrayWriter.WriteLine(m_vBase[i]);
                    ArrayWriter.WriteLine(m_vCheck[i]);
                }
                ArrayWriter.WriteLine("");
                ArrayWriter.Close();
                ArrayFile.Close();
                
                
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("cArrayFileName is invalid!");
                return false;
            }

            return true;
        }

        //获得数组大小
        public uint GetArraySize()
        {
            return (uint)(m_vBase.Count) ;
        }

        //删除词语
        public bool AddWord(String cWord)
        {
            if (SearchWord(cWord) != -1)
            {
                System.Console.Out.WriteLine("要添加的词已经在词典中！");
                return true;
            }

            int nLen = gbk.GetByteCount(cWord);
            m_nDictItemCount++;
            bool bSucc = AddChar(cWord, 0);
            if (bSucc)
            {
                m_nDictItemCount++;
                m_IsDirty = true;
                if((int)Math.Ceiling((float)nLen/2) > m_nWordMaxSize)
                    m_nWordMaxSize = (uint)Math.Ceiling((float)(nLen / 2));
            }

            return bSucc;
        }

        //增加词语
        public bool DeleteWord(String cWord)
        {
            if (SearchWord(cWord) == -1)
            {
                System.Console.Out.WriteLine("要删除的词在词典中不存在");
                return false;
            }

            int nLen = gbk.GetByteCount(cWord);
            int nIter = 0;
            int nID, nWordLen, nNextCount;

            ArrayList nBaseVal = new ArrayList() ;
            ArrayList nCheckVal = new ArrayList();
            ArrayList nSubArray = new ArrayList();


            String temp = gbk.GetString(gbk.GetBytes(cWord), nIter, gbk.GetByteCount(cWord) - nIter);

            nWordLen = util.GetChar(temp);
            nID = util.GetInnerCode(temp, nWordLen);
            nIter += nWordLen;
            nSubArray.Add(nID);

            while (nIter < nLen)		//统计相关信息
            {
                String temp1 = gbk.GetString(gbk.GetBytes(cWord), nIter, gbk.GetByteCount(cWord) - nIter);
                nWordLen = util.GetChar(temp1);
                nID = util.GetInnerCode(temp1, nWordLen);
                nIter += nWordLen;
                int nSub = Math.Abs(Convert.ToInt32(m_vBase[Convert.ToInt32(nSubArray[nSubArray.Count-1])])) + nID;
                nSubArray.Add(nSub);
            }

            int nSize = nSubArray.Count;

            if (Convert.ToInt32(m_vBase[Convert.ToInt32(nSubArray[nSize - 1])]) == (-1) * Convert.ToInt32(nSubArray[nSize - 1]))	//要删除的词为最大词
            {
                nNextCount = Find(Convert.ToInt32(nSubArray[nSize - 2]));
                m_vBase[Convert.ToInt32(nSubArray[nSize - 1])] = 0;
                m_vCheck[Convert.ToInt32(nSubArray[nSize - 1])] = 0;
                nNextCount--;
                int i;
                for (i = nSize - 2; i > -1; i--)
                {
                    if (Convert.ToInt32(m_vBase[Convert.ToInt32(nSubArray[i])]) > 0 && nNextCount == 0)
                    {
                        nNextCount = Find(Convert.ToInt32(nSubArray[i - 1]));
                        m_vBase[Convert.ToInt32(nSubArray[i])] = 0;
                        m_vCheck[Convert.ToInt32(nSubArray[i])] = 0;
                        nNextCount--;
                    }
                    else if (Convert.ToInt32(m_vBase[Convert.ToInt32(nSubArray[i])]) > 0 && nNextCount > 0)
                    {
                        m_vBase[Convert.ToInt32(nSubArray[i])] = 0;
                        m_vCheck[Convert.ToInt32(nSubArray[i])] = 0;
                        break;
                    }
                    else
                        break;
                }
            }
            else					//并非最大词
            {
                m_vBase[Convert.ToInt32(nSubArray[nSize - 1])] = Math.Abs(Convert.ToInt32(m_vBase[Convert.ToInt32(nSubArray[nSize - 1])]));
            }

            m_nDictItemCount--;
            m_IsDirty = true;

            return true;
        }

        //查找词语
        public int SearchWord(String cWord)
        {
            int iSize = (int)GetArraySize();
            int nLen = cWord.Length;
            int nIter = 0;
            String cSingWord = "";

            util.GetChar(cWord, ref nIter, ref cSingWord);
            int ID = util.GetInnerCode(cSingWord);

            if (ID < 0 || ID > iSize)
                return -1;
            int iSub = ID;
            int k = iSub;

            while (nIter < nLen)
            {
                util.GetChar(cWord, ref nIter, ref cSingWord);
                iSub = Math.Abs(Convert.ToInt32(m_vBase[iSub])) + util.GetInnerCode(cSingWord);
                if (iSub > iSize)
                {
                    return -1;
                }
                if (Convert.ToInt32(m_vCheck[iSub]) != k)
                {
                    return -1;
                }
                k = iSub;
            }

            if (Convert.ToInt32(m_vBase[iSub]) < 0)
            {
                return 0;
            }


            return -1;
        }

        //读入词典数组
        public bool Load(String pszFileName)
        {
            try
            {
                FileStream ArrayFile = File.OpenRead(pszFileName);
                StreamReader ArrayReader = new StreamReader(ArrayFile);
                String temp = null;

                temp = ArrayReader.ReadLine();
                m_nDictItemCount = (uint)Convert.ToInt32(temp);
                temp = ArrayReader.ReadLine();
                m_nWordMaxSize = (uint)Convert.ToInt32(temp);
                temp = ArrayReader.ReadLine();
                m_nDictArraySize = (uint)Convert.ToInt32(temp);


                m_vBase = new ArrayList();
                m_vCheck = new ArrayList();

                for (int i = 0; i < m_nDictArraySize; i++)
                {
                    m_vBase.Add(Convert.ToInt32(ArrayReader.ReadLine()));
                    m_vCheck.Add(Convert.ToInt32(ArrayReader.ReadLine()));
                }

                ArrayReader.Close();
                ArrayFile.Close();

                return true;

            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("array file is invalid!");
                return false;
            }

        }


        //读入词典文本文件到m_pcWordArray
        public bool DictLoad(String pszFileName)
        {
            try
            {
                FileStream DicFile = File.OpenRead(pszFileName);
                StreamReader DicReader = new StreamReader(DicFile, System.Text.Encoding.GetEncoding("GBK"));//这里原来都是GBK的,是不是可以UTF-8

                String Cur_Line = null;
                String Value = null;

                if ((Cur_Line = DicReader.ReadLine()) != null)
                {
                    if (Cur_Line.Contains("DictItemCount = "))
                    {
                        Value = Cur_Line.Substring(Cur_Line.LastIndexOf(" "));
                        m_nDictItemCount = Convert.ToUInt32(Value);//获取字典里单词的总个数
                        //System.Console.Out.WriteLine(m_nDictItemCount);
                    }
                }

                if ((Cur_Line = DicReader.ReadLine()) != null)
                {
                    if (Cur_Line.Contains("MaxWordSize = "))//MaxWordSize是单词的size
                    {
                        Value = Cur_Line.Substring(Cur_Line.LastIndexOf(" "));
                        m_nWordMaxSize = Convert.ToUInt32(Value);
                        //System.Console.Out.WriteLine(m_nWordMaxSize);
                    }
                }


                int i = 0;
                m_pcWordArray = new String[m_nDictItemCount] ;//存字典的单词
                m_piWordLenArray = new int[m_nDictItemCount] ;//存字典里词每一个单词的size

                while ((Cur_Line = DicReader.ReadLine()) != null)
                {
                    if (i > m_pcWordArray.Length)//超出字典的长度了
                    {
                        System.Console.Out.WriteLine("i > m_nDictItemCount");
                        return false;
                    }

                    m_pcWordArray[i] = Cur_Line;//把数据读入m_pcWordArray
                    m_piWordLenArray[i] = Cur_Line.Length;
                    
                    i++;
                }

                DicReader.Close();
                DicFile.Close();

                //System.Console.Out.WriteLine(m_pcWordArray.Length);
                //System.Console.Out.WriteLine(m_piWordLenArray.Length);

                return true;

            }
            catch(Exception e)
            {
                return false;
            }
            

        }

         //遍历词表
        private bool Dict_Traversal(StateInfo[] pStateInfo, ref int nStateCount, int n)
        {
            //char[] cWordId = new char[MaxWordSize];			
	        
            String cState = "";
	        int i ;
            for ( i= 0; i < m_nDictItemCount; i++)//m_nDictItemCount是指字典里单词的总个数
            {
                
                AddInfo(m_pcWordArray[i], ref cState, n, m_piWordLenArray[i], ref pStateInfo, ref nStateCount);
            }

            //System.Console.Out.WriteLine(i);
            return true;
        }

        //添加状态信息到结构数组中
        private void AddInfo(String cWord, ref String cState, int n, int nLen, ref StateInfo[] pStateInfo, ref int nStateCount)
        {//nLen指cWord的长度,n是指当前在处理第几个字,cState表示当前状态,cWord就是当前单词,
            int nIter = 0;
            StringBuilder temp = new StringBuilder();
            String buff = "";

            util.GetChar(cWord, ref nIter, ref buff);
            temp.Append(buff);

            for (int i = 1; i < n; i++)	//获得cWord的前n个字
            {
                if (nIter == nLen)
                    return;
                else
                {
                    util.GetChar(cWord, ref nIter, ref buff);
                    temp.Append(buff);
                }
            }

            String cWordFir = temp.ToString();

            //如果当前状态后面还有输入变量
            if (nLen > nIter)
            {
                String cWordSec = "";
                util.GetChar(cWord, ref nIter, ref cWordSec);
                if (!cState.Equals(cWordFir))  //当前状态与数组中前一状态不同
                {
                    //将当前状态的信息加入状态信息数组
                    pStateInfo[nStateCount].nStateSub = GetStatArraySub(cWordFir);
                    pStateInfo[nStateCount].pNextID = new int[Maxsize];
                    pStateInfo[nStateCount].pNextID[0] = util.GetInnerCode(cWordSec);
                    pStateInfo[nStateCount].nNextStateCount = 1;
                    nStateCount++; ;
                    cState = cWordFir;
                }
                else  //相同
                {
                    int nCount = pStateInfo[nStateCount - 1].nNextStateCount;
                    if (nCount == 0)//如果当前状态还没有延续字，
                    {
                        pStateInfo[nStateCount - 1].pNextID = new int[Maxsize];
                        pStateInfo[nStateCount - 1].pNextID[0] = util.GetInnerCode(cWordSec);
                        pStateInfo[nStateCount - 1].nNextStateCount = 1;
                    }
                    else//如果当前状态已经有了延续字,再加下现在这个延续字
                    {
                        if (pStateInfo[nStateCount - 1].pNextID[nCount - 1] != util.GetInnerCode(cWordSec))//如果现在这个延续字与上一个当前状态的延续字不相同
                        {
                            pStateInfo[nStateCount - 1].pNextID[nCount] = util.GetInnerCode(cWordSec);
                            pStateInfo[nStateCount - 1].nNextStateCount++;
                        }
                    }
                }

            }
            //如果当前状态已经是一个完整的词条
            else
            {
                if (!cState.Equals(cWordFir))//当前状态与数组中前一状态不同
                {
                    pStateInfo[nStateCount].nStateSub = GetStatArraySub(cWordFir);
                    pStateInfo[nStateCount].pNextID = null;
                    pStateInfo[nStateCount].nNextStateCount = 0;

                    ConsidSize(pStateInfo[nStateCount].nStateSub);

                    m_vBase[pStateInfo[nStateCount].nStateSub] = (-1) * pStateInfo[nStateCount].nStateSub;//代表是一个词语了,下面没有后延了

                    nStateCount++;
                    cState = cWordFir;

                }
            }


        }

        //<summary>
        //构造双数组的核心函数
        //构造数组，生成数组文件
        //</summary>
        private bool Construct_Arrays(String pszFileName)//字典文件->数组文件
        {
            bool bSuccess = true;   //是否构造成功
            m_vBase = new ArrayList();//双数组
	        m_vCheck = new ArrayList();//双数组

            if (m_vBase == null || m_vCheck == null)
            {
                bSuccess = false;
                return bSuccess;
            }
            if (DictLoad(pszFileName) == false)//将字典文本装入数组m_pcWordArray和m_pcLenWordArray
                return false;
            if (m_nDictItemCount == 0)        //词典为空或读入失败 m_nDictItemCount为字典里单词的总个数
                bSuccess = false;
            else
            {
                StateInfo[] pStateInfo;
                int nStateArrayCount = 0;
                //int &nStateCount = nStateArrayCount;


                //initialize the check and base
                for (int n = 0; n < SINGLE_WORD_INNERCODE + Utility.RESERVE_ITEMS; n++)//单字节内码
                {
                    m_vBase.Add(0);
                    m_vCheck.Add(-1); //把前SINGLE_WORD_INNERCODE+RESERVE_ITEMS个位置留给首字
                }

                int i;
                for (i = 1; i < (m_nWordMaxSize) + 1; i++)          //遍历词表   m_nWordMaxSize代表一个单词的最大长度
                {//以单词里第i个字为标准,计算以该字为首的下面的字的个数
                    System.Console.Out.WriteLine("第" + i + "轮");
                    pStateInfo = new StateInfo[m_nDictItemCount];//建立状态 m_nDictItemCount代表字典里词的个数
                    nStateArrayCount = 0;

                    /*
                     *  struct StateInfo {
                            public int nStateSub;			//当前状态的base和check数组下标
                            public int nNextStateCount;	//所有下一状态数
                            public int[] pNextID;			//下一状态ID数组
                        } ;

                    */
                    Dict_Traversal(pStateInfo, ref nStateArrayCount, i);//每一轮考虑每个单词的当前第i个字(nStateInfo)，以及该字下面是否有延续字，如果有，那么记录下来()
                    
                    Sort(pStateInfo, nStateArrayCount);//按每个字的状态数(就是该字的延续字个数)排序
                    
                    //然后把该轮(第i轮)的状况直接
                    int k;
                    System.Console.Out.WriteLine(nStateArrayCount);//第i轮首字(即第i个字为首字,different)的个数
                    for (k = 0; k < nStateArrayCount; k++)      //从下一状态较多的词数开赋值
                    {
                        

                        bSuccess = FindVal(pStateInfo[k]);//在这里面为m_Base赋值
              
                        if (pStateInfo[k].nNextStateCount > 0)//为什么要这样做，是为了以后清零
                        {
                            pStateInfo[k].pNextID = null;
                            pStateInfo[k].nNextStateCount = 0;
                        }
                        //System.Console.Out.WriteLine(k);

                    }

                 

                    pStateInfo = null;

                    for (int j = 0; j < SINGLE_WORD_INNERCODE + Utility.RESERVE_ITEMS; j++)
                    {
                        m_vCheck[j] = 0;
                    }
                }
            }
               

            return bSuccess;
        }

        // 确定base值和check值
        /*
         *  struct StateInfo {
                public int nStateSub;			//当前状态的base和check数组下标,代表着一个字
                public int nNextStateCount;	//所有下一状态数
                public int[] pNextID;			//下一状态ID数组
            } ;
         * 
         */
        //<summary>
        //FindVal函数是对当前状态sInfo找到其对应的base值，并为所有延续字的check附上这个base值
        //</summary>
        private bool FindVal(StateInfo sInfo)
        {
            bool bFound = true;
            if (sInfo.nNextStateCount != 0)      //后面还有字 sInfo.nNextStateCount是指当前状态下延续字的个数(不算重复字)
            {
                bFound = false;
                int i = 1;//这里i应该是当前状态的base值的candidate值
                while (!bFound)
                {
                    int k;
                    //下面这个for循环是用来选i值的
                    for (k = 0; k < sInfo.nNextStateCount; k++)//如果是自动遍历完(中间没有停止)，那么说明i值是ok的
                    {
                        if (i + sInfo.pNextID[k] < SINGLE_WORD_INNERCODE + Utility.RESERVE_ITEMS)//说明sInfo.pNextID[k]合法
                            break;//i值需要改变
                        if (ConsidSize(i + sInfo.pNextID[k]))//确定m_vBase和m_vCheck和是否需要扩容
                        {

                            if (i == sInfo.nStateSub)//如果i为当前的状态值，i值需要改变
                                break;
                            if ((Convert.ToInt32(m_vBase[i + sInfo.pNextID[k]]) != 0) || (Convert.ToInt32(m_vCheck[i + sInfo.pNextID[k]]) != 0))
                                break;//意思是base和check有一个不为0(即该位置不空)，i值(当前状态的base值)就得改变
                            
                        }
                    }
                    //当i值使得所有m_vBase和m_vCheck均为0时,便可以取该i为当前state的base值,如果这个i值在没遍历完的情况下九跳出了上面的循环，说明该i值不行,得更换掉
                    if (k == sInfo.nNextStateCount) //找到i(也就是需要确定的当前的base值的candidate值)，确定相应的base和check值 这里意思是把当前状态的所有延续字都遍历了
                    {
                        if (!ConsidSize(sInfo.nStateSub))//如果当前状态超出了范围
                            m_vBase[sInfo.nStateSub] = i;
                        else
                        {
                            int nbaseval = Convert.ToInt32(m_vBase[sInfo.nStateSub]);
                            if (nbaseval < 0)  //如果本身已经成词
                                m_vBase[sInfo.nStateSub] = (-1) * i;//这里貌似少了一种状态，就是如果本身已经成词，但是下面还可以有延续字怎么办
                            else m_vBase[sInfo.nStateSub] = i;
                        }
                        for (k = 0; k < sInfo.nNextStateCount; k++)  //确定check值
                        {
                            m_vCheck[i + sInfo.pNextID[k]] = sInfo.nStateSub;//因为m_vBase[sInfo.nStateSub]=i
                        }
                        bFound = true;
                    }
                    else i++;
                }
            }
            return bFound;
        }

        private bool FindVal(int nCurrSub, int nNextID)
        {
            bool bFound = false;
            int i = 1;
            while (!bFound)
            {
                ConsidSize(i + nNextID);
                if (i == (-1) * nNextID)
                    i++;
                else if (Convert.ToInt32(m_vBase[i + nNextID]) != 0 || Convert.ToInt32(m_vCheck[i + nNextID]) != 0 ||
                    (i + nNextID) < SINGLE_WORD_INNERCODE + Utility.RESERVE_ITEMS)
                    i++;
                else
                    bFound = true;
            }
            if (!ConsidSize(nCurrSub))
                m_vBase[nCurrSub] = i;
            else
            {
                if (Convert.ToInt32(m_vBase[nCurrSub]) < 0)			//如果本身已经成词
                    m_vBase[nCurrSub] = (-1) * i;
                else
                    m_vBase[nCurrSub] = i;
            }
            m_vCheck[i + nNextID] = nCurrSub;

            return bFound;
        }

        // 获得当前状态在base数组中的下标
        private int GetStatArraySub(String cWord)
        {
            int nLen = cWord.Length;
	        int nIter = 0;
	        String charac = "";
	        util.GetChar(cWord, ref nIter, ref charac);
            int ID = util.GetInnerCode(charac);
	        int nSub;
	        nSub = ID;
	        while(nIter < nLen)
	        {
                util.GetChar(cWord, ref nIter, ref charac);
                ID = util.GetInnerCode(charac);
		        nSub = Math.Abs(Convert.ToInt32(m_vBase[nSub])) + ID;
	        }
	        return nSub;
        }

        //对状态数组排序
        private void Sort(StateInfo[] pStateInfo, int nStateCount)
        {
            Array.Sort(pStateInfo,comp);
            //Array.Reverse(pStateInfo);
        }

        //判断容器下标是否超出当前大小,如果超过则增长到下标大小
        private bool ConsidSize(int nSub)
        {
            bool less = true;
            int nSize = (int)GetArraySize();
            if (nSub >= nSize)//超标了
            {
                less = false;
                for (int i = 0; i < (nSub - nSize + 1); i++)
                {
                    m_vBase.Add(0);
                    m_vCheck.Add(0);
                }
            }
            return less;
        }

        //在Check数组中查找值为nSub的项，返回项数
        private int Find(int nSub, int[] nSubArray, int[] nIDArray, int[] nValArray)
        {
            int i = 0;
            int nCount = 0;
            for (i = (SINGLE_WORD_INNERCODE + Utility.RESERVE_ITEMS); i < m_nDictArraySize; i++)
            {
                if (Convert.ToInt32(m_vCheck[i]) == nSub)
                {
                    nSubArray[nCount] = i;
                    nIDArray[nCount] = i - Math.Abs(Convert.ToInt32(m_vBase[nSub]));
                    nValArray[nCount] = Convert.ToInt32(m_vBase[i]);
                    nCount++;
                }
            }
            return nCount;
        }

        private int Find(int nSub, int[] nSubArray)
        {
            int i;
            int nCount = 0;
            for (i = (SINGLE_WORD_INNERCODE + Utility.RESERVE_ITEMS); i < m_nDictArraySize; i++)
            {
                if (Convert.ToInt32(m_vCheck[i]) == nSub)
                {
                    nSubArray[nCount] = i;
                    nCount++;
                }
            }
            return nCount;
        }

        private int Find(int nSub)
        {
            int i;
            int nCount = 0;
            for (i = 0; i < m_nDictArraySize; i++)
            {
                if (Convert.ToInt32(m_vCheck[i]) == nSub)
                {
                    nCount++;
                }
            }
            return nCount;
        }


        //增加字串
        bool AddChar(String cWord, int nSub)
        {
            int nLen, nID;
            int nBaseVal, nCheckVal, nCurrentSub = 0;
            //int nCurrSub = nCurrentSub;

            nLen = util.GetChar(cWord);
            nID = util.GetInnerCode(cWord, nLen);
            nCurrentSub = Math.Abs(Convert.ToInt32(m_vBase[nSub])) + nID;
            bool bIsDirty;
            ConsidSize(nCurrentSub);

            if (nLen == gbk.GetByteCount(cWord))
            {
                if (nSub != 0)
                {
                    m_nDictItemCount++;
                    bIsDirty = ReLocate(nSub, nID, ref nCurrentSub);
                    if (!bIsDirty || Convert.ToInt32(m_vBase[nCurrentSub]) > 0)
                    {
                        m_vBase[nCurrentSub] = (-1) * Convert.ToInt32(m_vBase[nCurrentSub]);
                    }
                    else if (bIsDirty)
                        m_vBase[nCurrentSub] = (-1) * nCurrentSub;
                }
                else
                {
                    nBaseVal = Convert.ToInt32(m_vBase[nCurrentSub]);
                    nCheckVal = Convert.ToInt32(m_vCheck[nCurrentSub]);
                    if (nBaseVal == 0 && nCheckVal == 0)
                    {
                        m_vBase[nCurrentSub] = (-1) * nCurrentSub;
                        m_vCheck[nCurrentSub] = nSub;
                    }
                }
            }
            else
            {
                if (nSub != 0)
                {
                    bIsDirty = ReLocate(nSub, nID, ref nCurrentSub);
                    if (bIsDirty)
                    {
                        while (nLen < gbk.GetByteCount(cWord))
                        {
                            String temp = gbk.GetString(gbk.GetBytes(cWord), nLen, gbk.GetByteCount(cWord) - nLen);
                            int n = util.GetChar(temp);
                            nID = util.GetInnerCode(temp, n);
                            FindVal(nCurrentSub, nID);
                            nCurrentSub = Convert.ToInt32(m_vBase[nCurrentSub]) + nID;
                            nLen += n;
                        }

                        m_vBase[nCurrentSub] = (-1) * nCurrentSub;
                        return true;
                    }
                    else
                    {
                        String temp = gbk.GetString(gbk.GetBytes(cWord), nLen, gbk.GetByteCount(cWord) - nLen) ;
                        AddChar(temp, nCurrentSub);
                    }
                }
                else
                {
                    nSub = nCurrentSub;
                    String temp = gbk.GetString(gbk.GetBytes(cWord), nLen, gbk.GetByteCount(cWord) - nLen);
                    int nTempLen = util.GetChar(temp);
                    nID = util.GetInnerCode(temp, nTempLen);
                    nCurrentSub = Math.Abs(Convert.ToInt32(m_vBase[nSub])) + nID;
                    ConsidSize(nCurrentSub);
                    bIsDirty = ReLocate(nSub, nID, ref nCurrentSub);
                    nLen += nTempLen;
                    if (nLen == gbk.GetByteCount(cWord))
                    {
                        if (!bIsDirty || Convert.ToInt32(m_vBase[nCurrentSub]) > 0)
                        {
                            m_vBase[nCurrentSub] = (-1) * Convert.ToInt32(m_vBase[nCurrentSub]);
                        }
                        else if (bIsDirty)
                            m_vBase[nCurrentSub] = (-1) * nCurrentSub;
                    }
                    else
                    {
                        if (bIsDirty)
                        {
                            while (nLen < gbk.GetByteCount(cWord))
                            {
                                String temp1 = gbk.GetString(gbk.GetBytes(cWord), nLen, gbk.GetByteCount(cWord) - nLen);
                                int n = util.GetChar(temp1);
                                nID = util.GetInnerCode(temp1, n);
                                FindVal(nCurrentSub, nID);
                                nCurrentSub = Convert.ToInt32(m_vBase[nCurrentSub]) + nID;
                                nLen += n;
                            }
                            m_vBase[nCurrentSub] = (-1) * nCurrentSub;
                            return true;
                        }
                        else
                        {
                            String temp1 = gbk.GetString(gbk.GetBytes(cWord), nLen, gbk.GetByteCount(cWord) - nLen);
                            AddChar(temp1, nCurrentSub);
                        }
                    }
                }

            }

            return true;
        }

        //重新确定下标为nSub的数组元素值
	    bool ReLocate(int nSub,int nID, ref int nNextSub)
        {
            if (nNextSub > 6767 + Utility.RESERVE_ITEMS && Convert.ToInt32(m_vCheck[nNextSub]) == 0)		//空位置
            {
                m_vCheck[nNextSub] = nSub;
                m_IsDirty = true;
                return true;
            }
            else if (Convert.ToInt32(m_vCheck[nNextSub]) != nSub) //位置非空
            {
                int[] nSubArray = new int[Maxsize];
                int[] nValArray = new int[Maxsize];
                int[] nIDArray = new int[Maxsize];

                int nCount = Find(nSub, nSubArray, nIDArray, nValArray);

                if (nCount == 0)
                    FindVal(nSub, nID);
                else
                {
                    StateInfo reInfo;
                    reInfo.nNextStateCount = nCount + 1;
                    reInfo.nStateSub = nSub;
                    reInfo.pNextID = new int[Maxsize];

                    for (int i = 0; i < nCount; i++)
                    {
                        reInfo.pNextID[i] = nIDArray[i];
                    }

                    reInfo.pNextID[nCount] = nID;
                    FindVal(reInfo);

                    int[] nArray = new int[Maxsize];

                    for (int k = 0; k < nCount; k++)
                    {
                        //把原来的值复制到新位置
                        int nOtherNextSub = Math.Abs(Convert.ToInt32(m_vBase[nSub])) + nIDArray[k];
                        m_vBase[nOtherNextSub] = nValArray[k];

                        int nThirCount = Find(nSubArray[k], nArray);

                        for (int j = 0; j < nThirCount; j++)
                        {
                            m_vCheck[nArray[j]] = nOtherNextSub;
                        }

                        //原位置腾空
                        m_vBase[nSubArray[k]] = 0;
                        m_vCheck[nSubArray[k]] = 0;

                    }
                    nArray = null;
                    reInfo.pNextID = null;
                }
                nNextSub = Math.Abs(Convert.ToInt32(m_vBase[nSub])) + nID;

                nIDArray = null;
                nSubArray = null;
                nValArray = null;


                return true;
            }
            return false;
        }





        

    }
}
