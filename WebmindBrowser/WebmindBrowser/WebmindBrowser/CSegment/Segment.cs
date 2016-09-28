using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebmindBrowser.CSegment
{
    class Segment
    {
        public const int BUFFERSIZE = 5000;
        public const int FILTER_LEN = 50;

        private Dictionary m_pDict;   //词典对象
        private int m_nDictArraySize; //词典数组长度

        private StringBuilder m_cSegTxt;
        private Encoding gbk = Encoding.GetEncoding("GBK");

        public long m_lLen;			  //文件长度
        public String m_lpBaseAddress;	  //文件基址

        private Utility util = new Utility();

        public bool Init(Dictionary dict)
        {
            if (dict == null)
            {
                return false;
            }
            else
            {
                m_pDict = dict;
                m_cSegTxt = null;
                return true;
            }
        }

        //将待分词文件读入内存
        public bool LoadFile(String pszFileName)
        {
            try
            {
                FileStream TextFile = File.OpenRead(pszFileName);
                StreamReader TextReader = new StreamReader(TextFile, Encoding.GetEncoding("GBK"));

                m_lpBaseAddress = TextReader.ReadToEnd();//字符串形式的流的其余部分（从当前位置到末尾）。如果当前位置位于流的末尾，则返回空字符串 ("")。
                m_lLen = m_lpBaseAddress.Length;

                TextReader.Close();
                TextFile.Close();

                return true;

            }
            catch(Exception e)
            {
                System.Console.Out.WriteLine("text file is invalid!");
                return false;
            }
        }

        //<summary>
        //将待分词内容(String形式)读入内存,这是为了适应如果来的直接是内容，而不是以文件的形式
        //</summary>
        public bool LoadContent(String content)
        {
  
                m_lpBaseAddress = content;
                m_lLen = m_lpBaseAddress.Length;
                return true;      
        }


        //分词函数，返回切分好的字符串和字符串长度
        public String TextSegment(ref int iOutSize)
        {
            int nIter = 0;

            if (m_pDict == null || m_lLen == 0 || m_lpBaseAddress == null)
            {
                System.Console.Out.WriteLine("未读入词典数组文件或未读入待分词文件!");
                return null;
            }

            String lpszCur = m_lpBaseAddress;

            m_nDictArraySize = (int)m_pDict.m_nDictArraySize;
            
            if (m_nDictArraySize == 0)
            {
                System.Console.Out.WriteLine("词典数组长度为空，请重新生成!");
                return null;
            }

            m_cSegTxt = new StringBuilder() ;

            System.Console.Out.WriteLine(m_lLen);  //debug

           // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();//开始计时

            MMSegment(ref nIter);

          //  stopwatch.Stop();
            //TimeSpan timespan = stopwatch.Elapsed;
            //double minutes = timespan.TotalMinutes;  // 总分钟
            //double seconds = timespan.TotalSeconds;  //  总秒数
            //double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
            //Console.WriteLine("总秒数:" + seconds);

            iOutSize = nIter;

            System.Console.Out.WriteLine(iOutSize); //debug

            System.Console.Out.WriteLine("分词完成！");



            return m_cSegTxt.ToString();
        }



        private void MMSegment(ref int nIter)//这个是最核心的东西,原来的算法超时了
        {
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();//开始计时
            int nSub = 0;
            int nID;
            int nStringLen;
            int nWordLen = 0;
            int nCheckVal = 0;
            int nBaseVal = 0;
            int nPreSub;
            int nTestWordLen = 0;
            bool IsFirst = true;

            int lpszCur = 0;
            int pIter = 0;
            int cFileSize = gbk.GetByteCount(m_lpBaseAddress);
            String fileContent = m_lpBaseAddress;

            double totalmilliseconds = 0;
            byte[] bytes = gbk.GetBytes(m_lpBaseAddress);//modified by kosko
            while (pIter<cFileSize-1)//如果用pIter会使原文部分丢失的,lpszCur,这里我之所以用cFileSize-1作为阀值，是因为我担心到了cFileSize-1这个时候，不能去2个字节了
            {
               // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
               // stopwatch.Start();//开始计时
               // String temp = gbk.GetString(gbk.GetBytes(m_lpBaseAddress), pIter, gbk.GetByteCount(m_lpBaseAddress) - pIter);
                String temp = gbk.GetString(bytes, pIter, 2);//下一个最多为2个字节, modified by kosko
                //我想我找到了两个版本时间差别如此巨大的原因，gbk.GetString函数累计花费的时间约为0.828s!!!
               // stopwatch.Stop();
                //TimeSpan timespan = stopwatch.Elapsed;
                //double minutes = timespan.TotalMinutes;  // 总分钟
                //double seconds = timespan.TotalSeconds;  //  总秒数
                //double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数
                //totalmilliseconds += milliseconds;
                //Console.WriteLine("取temp毫秒数:" + milliseconds);
               // if (temp.Length > 0)
               // {
                   // String subS = fileContent.Substring(pIter, pIter + 1);
                    nStringLen = util.GetChar(temp);//查看这一段文件首个文字是双字节还是单字节,其实完全没必要把真个这段全传过去
                    nID = util.GetInnerCode(temp, nStringLen);//其实，这里也没必要传整个文本段，只是要了文本段的第一个字
                    pIter += nStringLen;


                    if (!IsFirst)
                    {
                        if (nID >= Utility.RESERVE_ITEMS)			//汉字或单词
                        {
                            nPreSub = nSub;
                            nSub = Math.Abs(nBaseVal) + nID;
                            if (nSub > m_nDictArraySize - 1)
                            {
                                m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                                m_cSegTxt.Append(" ");

                                nIter += (nWordLen + 1);
                                lpszCur += nWordLen;
                                pIter = lpszCur;
                                IsFirst = true;
                                nTestWordLen = 0;
                                nWordLen = 0;
                            }
                            else
                            {
                                nCheckVal = Convert.ToInt32(m_pDict.m_vCheck[nSub]);
                                nBaseVal = Convert.ToInt32(m_pDict.m_vBase[nSub]);

                                if (nCheckVal != nPreSub || nBaseVal == 0)			//状态不能继
                                {
                                    m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                                    m_cSegTxt.Append(" ");

                                    nIter += nWordLen + 1;
                                    lpszCur += nWordLen;
                                    pIter = lpszCur;
                                    IsFirst = true;
                                    nWordLen = 0;
                                    nTestWordLen = 0;
                                }

                                else if (nCheckVal == nPreSub && nBaseVal < 0)    //成词
                                {
                                    nTestWordLen += nStringLen;
                                    nWordLen = nTestWordLen;
                                    if (nBaseVal == (-1) * nSub)		//最大词
                                    {
                                        m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                                        m_cSegTxt.Append(" ");

                                        nIter += nWordLen + 1;
                                        lpszCur += nWordLen;
                                        pIter = lpszCur;
                                        IsFirst = true;
                                        nTestWordLen = 0;
                                        nWordLen = 0;
                                    }
                                }
                                else						//不成词但状态可继续
                                {
                                    nTestWordLen += nStringLen;
                                }
                            }

                        }

                        else							//非汉字
                        {
                            m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                            m_cSegTxt.Append(" ");
                            nIter += nWordLen + 1;
                            lpszCur += nWordLen;
                            pIter = lpszCur;
                            IsFirst = true;
                            nWordLen = 0;
                            nTestWordLen = 0;
                        }
                    }
                    else //	first character
                    {

                        if (nID >= Utility.RESERVE_ITEMS)								//汉字
                        {
                            if (nID > m_nDictArraySize - 1)
                            {
                               
                                m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nStringLen)/*gbk.GetByteCount(m_lpBaseAddress) - lpszCur*/);
                                m_cSegTxt.Append(" ");

                                nIter += (nStringLen + 1);
                                lpszCur += nStringLen;
                                pIter = lpszCur;
                                nTestWordLen = 0;
                                nWordLen = 0;
                            }
                            else
                            {
                                nTestWordLen += nStringLen;
                                nBaseVal = Convert.ToInt32(m_pDict.m_vBase[nID]);
                                nCheckVal = Convert.ToInt32(m_pDict.m_vCheck[nID]);
                                nWordLen = nTestWordLen;
                                if (nBaseVal == 0 || nCheckVal != 0 || nBaseVal == (-1) * nID)	//单字不为词典中的首字或者单字成最大词
                                {
                                    m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                                    m_cSegTxt.Append(" ");

                                    nIter += (nWordLen + 1);
                                    lpszCur = pIter;
                                    nWordLen = nTestWordLen = 0;
                                }
                                else
                                {
                                    nSub = nID;
                                    IsFirst = false;
                                }
                            }
                        }
                        else										//非汉字或单词
                        {
                            nWordLen = nStringLen;
                            String temp2 = gbk.GetString(bytes, pIter, (cFileSize - pIter));
                            if (temp2.Length > 0)
                            {
                                nStringLen = util.GetChar(temp2);
                                nID = util.GetInnerCode(temp2, nStringLen);
                                while (nID < Utility.RESERVE_ITEMS && nID != 5 && nID != 4 && pIter + nStringLen < cFileSize)							//非汉字、空格、回车和标点
                                {
                                    nWordLen += nStringLen;
                                    pIter += nStringLen;
                                    String temp3 = (gbk.GetString(bytes, pIter, cFileSize - pIter));//modified by kosko
                                    nStringLen = util.GetChar(temp3);
                                    nID = util.GetInnerCode(temp3, nStringLen);
                                }
                                if (nWordLen <= FILTER_LEN)
                                {
                                    if (nID != 5)
                                    {
                                        m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                                        m_cSegTxt.Append(" ");

                                        nWordLen++;
                                        nIter += nWordLen;
                                    }
                                    else
                                    {
                                        nWordLen += nStringLen;
                                        pIter += nStringLen;
                                        m_cSegTxt.Append(gbk.GetString(bytes, lpszCur, nWordLen));//modified by kosko
                                        nIter += nWordLen;
                                    }
                                }

                                lpszCur = pIter;
                            }

                        }// nID < RESERVE_ITEMS
                    }//temp.Length > 0
               // }// if (temp.Length > 0)
               // else
               // {
                  // return;
               // }



            }//end of while
            // Console.WriteLine("取temp所花总毫秒:" + totalmilliseconds);
           /* stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            double minutes = timespan.TotalMinutes;  // 总分钟
            double seconds = timespan.TotalSeconds;  //  总秒数
            double milliseconds = timespan.TotalMilliseconds;  //  总毫秒数:887.5ms,因此1.3-0.8，故差别就在这里
            Console.WriteLine("总秒数:" + seconds);
            * */
        }
    }
}
