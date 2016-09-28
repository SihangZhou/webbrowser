using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebmindBrowser.CSegment
{
    class Utility
    {
        private Encoding GBKEncoder = Encoding.GetEncoding("gbk");

        public const int RESERVE_ITEMS = 64;  //58+5,大小写英文字母和其他一些符号
        public const String SEPERATOR_LINK  = "\t\n\r 　" ;

        public void GetChar(String cWord, ref int niter, ref String cWordArray)
        {
            cWordArray = cWord.Substring(niter, 1);
            niter++;
        }
        
        public int GetChar(String cWord)
        {
            int nWordLen = 0;
            try
            {
                String tt = cWord.Substring(0, 1);
                nWordLen = GBKEncoder.GetBytes(cWord.Substring(0, 1)).Length;
                return nWordLen;
            }
            catch (Exception e)
            {
                return 0;
            }

            
        }
        
        public int GetChar(char cWord)
        {
            int nWordLen;
            if (cWord < 0)		//双字节
            {
                nWordLen = 2;
            }
            else						//单字节
            {
                nWordLen = 1;
            }
            return nWordLen;
        }


        public int GetInnerCode(String sChar)
        {
            byte[] GB_Code = GBKEncoder.GetBytes(sChar.Substring(0, 1));
            

            int nLen = GB_Code.Length;

            if (sChar[0] > '0' - 1 && sChar[0] < '9' + 1)//num
                return 1;
            if (sChar[0] > 'a' - 1 && sChar[0] < 'z' + 1 || sChar[0] > 'A' - 1 && sChar[0] < 'Z' + 1)	//charcter-started word
                return ((int)sChar[0] - 59);

            if (sChar[0] == '\n' || sChar[0] == '\t' || sChar[0] == '\r' || sChar[0] == ' ')
                return 5;
            if (nLen == 1)		//其他单字节符号
                return 0;

            if ((sbyte)GB_Code[0] == -93 && (sbyte)GB_Code[1] > -81 && (sbyte)GB_Code[1] < -70)
                return 2;
            if ((sbyte)GB_Code[0] == -93 && ((sbyte)GB_Code[1] >= -63 && (sbyte)GB_Code[1] <= -38 || (sbyte)GB_Code[1] >= -31 && (sbyte)GB_Code[1] <= -16))
                return 3;
            if ((sbyte)GB_Code[0] == -95 || (sbyte)GB_Code[0] == -93)//Punctuation
                return 4;

            int Innercode = 0 ;
            if ((sbyte)GB_Code[0] > -81 && (sbyte)GB_Code[1] > -96)     //GB2312
                Innercode = (80 + (sbyte)GB_Code[0]) * 94 + ((sbyte)GB_Code[1] + 95) + RESERVE_ITEMS;
                
            else
            {
                if ((sbyte)GB_Code[0] < -86)		// GBK/3
                    Innercode = (127 + (sbyte)GB_Code[0]) * 191 + ((sbyte)GB_Code[1] + 192) + 6768 + RESERVE_ITEMS;
                else Innercode = (86 + (sbyte)GB_Code[0]) * 97 + ((sbyte)GB_Code[1] + 192) + 12880 + RESERVE_ITEMS;  // GBK/4
            }
            return Innercode;

        }

        public int GetInnerCode(String sChar, int nLen)
        {
            byte[] GB_Code = GBKEncoder.GetBytes(sChar.Substring(0, 1));//获取第一个字的字节（汉字双字节）

            if (sChar[0] > '0' - 1 && sChar[0] < '9' + 1)//num
                return 1;
            if (sChar[0] > 'a' - 1 && sChar[0] < 'z' + 1 || sChar[0] > 'A' - 1 && sChar[0] < 'Z' + 1)	//charcter-started word
                return ((int)sChar[0] - 59);

            if (sChar[0] == '\n' || sChar[0] == '\t' || sChar[0] == '\r' || sChar[0] == ' ')
                return 5;
            if (nLen == 1)		//其他单字节符号
                return 0;

            if ((sbyte)GB_Code[0] == -93 && (sbyte)GB_Code[1] > -81 && (sbyte)GB_Code[1] < -70)
                return 2;
            if ((sbyte)GB_Code[0] == -93 && ((sbyte)GB_Code[1] >= -63 && (sbyte)GB_Code[1] <= -38 || (sbyte)GB_Code[1] >= -31 && (sbyte)GB_Code[1] <= -16))
                return 3;
            if ((sbyte)GB_Code[0] == -95 || (sbyte)GB_Code[0] == -93)//Punctuation
                return 4;

            int Innercode = 0;
            if ((sbyte)GB_Code[0] > -81 && (sbyte)GB_Code[1] > -96)
            {//GB2312
                Innercode = (80 + (sbyte)GB_Code[0]) * 94 + ((sbyte)GB_Code[1] + 95) + RESERVE_ITEMS;
            }

            else
            {
                if ((sbyte)GB_Code[0] < -86)		// GBK/3
                    Innercode = (127 + (sbyte)GB_Code[0]) * 191 + ((sbyte)GB_Code[1] + 192) + 6768 + RESERVE_ITEMS;
                else Innercode = (86 + (sbyte)GB_Code[0]) * 97 + ((sbyte)GB_Code[1] + 192) + 12880 + RESERVE_ITEMS;  // GBK/4
            }
            return Innercode;

        }
       public int GetInnerCode(char ch,int nLen)//by kosko,这个地方有些问题，不知道这里
    {
	    //0：其他单字节符号
	    //1: digit
	    //2: double-bytes digit
	    //3: double-bytes charcter
	    //4: Punctuation
	    //5:换行符、回车符、制表符和空格
	    //6-63:single character
        String sChar = "";
        sChar += ch;
        byte[] GB_Code = GBKEncoder.GetBytes(sChar.Substring(0, 1));//获取第一个字的字节（汉字双字节）

        if (sChar[0] > '0' - 1 && sChar[0] < '9' + 1)//num
            return 1;
        if (sChar[0] > 'a' - 1 && sChar[0] < 'z' + 1 || sChar[0] > 'A' - 1 && sChar[0] < 'Z' + 1)	//charcter-started word
            return ((int)sChar[0] - 59);

        if (sChar[0] == '\n' || sChar[0] == '\t' || sChar[0] == '\r' || sChar[0] == ' ')
            return 5;
        if (nLen == 1)		//其他单字节符号
            return 0;

        if ((sbyte)GB_Code[0] == -93 && (sbyte)GB_Code[1] > -81 && (sbyte)GB_Code[1] < -70)
            return 2;
        if ((sbyte)GB_Code[0] == -93 && ((sbyte)GB_Code[1] >= -63 && (sbyte)GB_Code[1] <= -38 || (sbyte)GB_Code[1] >= -31 && (sbyte)GB_Code[1] <= -16))
            return 3;
        if ((sbyte)GB_Code[0] == -95 || (sbyte)GB_Code[0] == -93)//Punctuation
            return 4;

        int Innercode = 0;
        if ((sbyte)GB_Code[0] > -81 && (sbyte)GB_Code[1] > -96)
        {//GB2312
            Innercode = (80 + (sbyte)GB_Code[0]) * 94 + ((sbyte)GB_Code[1] + 95) + RESERVE_ITEMS;
        }

        else
        {
            if ((sbyte)GB_Code[0] < -86)		// GBK/3
                Innercode = (127 + (sbyte)GB_Code[0]) * 191 + ((sbyte)GB_Code[1] + 192) + 6768 + RESERVE_ITEMS;
            else Innercode = (86 + (sbyte)GB_Code[0]) * 97 + ((sbyte)GB_Code[1] + 192) + 12880 + RESERVE_ITEMS;  // GBK/4
        }
        return Innercode;
    }




        }
}
