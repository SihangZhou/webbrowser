using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WebmindBrowser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //String filename = @"..\..\testSegment.txt";
            //SegmentApi.SegmentAPI(ref filename);//测试过了是好使的
            //String filecontent = "我是一个王子的人们好好学习天天向上";
            //String fileresult=SegmentApi.SegmentAPI_content(ref filecontent);//测试过了，也是好使的
           // testClassifier tc = new testClassifier();
            //int res=tc.contentClassifierAPI(fileresult);//这个测试过了，是好使的
            //int res1 = tc.EmotionClassifierAPI(fileresult);//这个也测试过了，是好使的
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
