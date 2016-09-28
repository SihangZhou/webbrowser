using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebmindBrowser.CSegment
{
    public class SegmentApi
    {

        //<summary>
        //该函数传进一个文件，然后利用该函数进行中文分词,返回分词后的内容
        //</summary>
       public static String SegmentAPI(ref String filename)
        {
            /* 这里，如果已经生成了双数组词典wc.txt，则不必要运行下面两行代码(即tt.DictLoad和tt.SaveArray)，
            * 如果没有生成wc.txt，则运行下面的两行代码
            * 下面两行的目的就是为了生成双数组字典wc.txt,
            * by kosko 2011-3-17
            */
            /*
            tt.DictLoad(@"..\..\CoreDict.txt");
            tt.SaveArray(@"..\..\CoreDict.txt",@"..\..\wc.txt", false);
            */
            Dictionary tt = new Dictionary();
            String dict = @"..\..\wc.txt";
            tt.Load(dict);//这里加载的是wc.txt，而不是以前所谓的qq.txt,我把这里改成了相对路径，方便大家使用,modified by kosko,2011-3-17
           //当是从BayesClassifierDemo那里启动时，是以BayesClassifierDemo下的目录为基准的
            //System.Console.Out.WriteLine(tt.SearchWord("故宫"));

            Segment seg = new Segment();
            seg.Init(tt);
            seg.LoadFile(filename);

            int iSegOutSize = 0;

            String result = seg.TextSegment(ref iSegOutSize);

            if (result == null)
            {
                System.Console.Out.WriteLine("error");
                return null;
            }
          /*  
           string strTarget = filename;//是不是可以直接覆盖掉原文
            FileStream ResultFile = File.OpenWrite(strTarget);
            StreamWriter ResultWriter = new StreamWriter(ResultFile, Encoding.GetEncoding("UTF-8"));

            ResultWriter.AutoFlush = true;

            ResultWriter.Write(result);

            ResultWriter.Close();
            ResultFile.Close();

            return strTarget;
             */
          return result;
        }
        //<summary>
        //这是为了传进的直接是内容时的情况做准备的，上面那个API上面是为了传进文件名
        //</summary>
       public static String SegmentAPI_content(ref String content)
       {
           /* 这里，如果已经生成了双数组词典wc.txt，则不必要运行下面两行代码(即tt.DictLoad和tt.SaveArray)，
           * 如果没有生成wc.txt，则运行下面的两行代码
           * 下面两行的目的就是为了生成双数组字典wc.txt,
           * by kosko 2011-3-17
           */
           /*
           tt.DictLoad(@"..\..\CoreDict.txt");
           tt.SaveArray(@"..\..\CoreDict.txt",@"..\..\wc.txt", false);
           */
           Dictionary tt = new Dictionary();
           String dict = @"..\..\wc.txt";
           tt.Load(dict);//这里加载的是wc.txt，而不是以前所谓的qq.txt,我把这里改成了相对路径，方便大家使用,modified by kosko,2011-3-17
           //当是从BayesClassifierDemo那里启动时，是以BayesClassifierDemo下的目录为基准的
           //System.Console.Out.WriteLine(tt.SearchWord("故宫"));

           Segment seg = new Segment();
           seg.Init(tt);
           seg.LoadContent(content);

           int iSegOutSize = 0;

           String result = seg.TextSegment(ref iSegOutSize);

           if (result == null)
           {
               System.Console.Out.WriteLine("error");
               return null;
           }
           /*  
            string strTarget = filename;//是不是可以直接覆盖掉原文
             FileStream ResultFile = File.OpenWrite(strTarget);
             StreamWriter ResultWriter = new StreamWriter(ResultFile, Encoding.GetEncoding("UTF-8"));

             ResultWriter.AutoFlush = true;

             ResultWriter.Write(result);

             ResultWriter.Close();
             ResultFile.Close();

             return strTarget;
              */
           return result;
       }
    }
}
