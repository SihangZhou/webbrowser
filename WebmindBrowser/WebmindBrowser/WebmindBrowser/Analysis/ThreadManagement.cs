using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//using WebmindBrowser.Classification;

namespace WebmindBrowser.Analysis
{
    public class ThreadManagement
    {
        //public static Thread ContentClassificationThread;//加载网页内容分类模型线程
        //public static Thread EmotionClassificationThread;//加载网页情感分类模型线程
        public static Thread BehaviorAnalysisThread;//行为特征提取分析线程
        public static Thread RecommendationThread;//推荐建议线程
        public static Thread LoadUrlContentHistoryThread;//加载网页记录的XML文件
        public static Thread LoadBehaviorFeaturesThread;//加载行为特征分析历史文件
        public static Thread LoadBehaviorFeatureHistoryThread;//加载行为特征值的文件
/*
        #region ContentClassificationThread
        public static void StartContentClassificationThread()
        {
            ContentClassification contentCf = new ContentClassification();
            ContentClassificationThread = new Thread(new ThreadStart(contentCf.ClassifyProcess));
            ContentClassificationThread.Start();
        }
        public static void AbortContentClassificationThread()
        {
            ContentClassificationThread.Abort();
        }
        #endregion
        */
        /*
        #region EmotionClassificationThread
        public static void StartEmotionClassificationThread()
        {
            EmotionClassification emotionCf = new EmotionClassification();
            EmotionClassificationThread = new Thread(new ThreadStart(emotionCf.ClassifyProcess));
            EmotionClassificationThread.Start();
        }
        public static void AbortEmotionClassificationThread()
        {
            EmotionClassificationThread.Abort();
        }
        #endregion
        */
        #region BehaviorAnalysisThread
        public static void StartBehaviorAnalysisThread()
        {
            UrlAnalysis url = new UrlAnalysis();
            BehaviorAnalysisThread = new Thread(new ThreadStart(url.AnalysisCurrentHistory));
        }
        public static void CallBehaviorAnalysisThread()
        {
            BehaviorAnalysisThread.Start();
            while (BehaviorAnalysisThread.IsAlive)
            {
                Thread.Sleep(1000);
            }
        }
        public static void AbortUrlAnalysisThread()
        {
            BehaviorAnalysisThread.Abort();
        }
        #endregion

        #region RecommendationThread
        public static void StartRecommendationThread()
        {
            //Recommendation recommend = new Recommendation();
            //RecommendationThread = new Thread(new ThreadStart(recommend.TimingAutoRecommendInfo));
            //RecommendationThread.Start();
        }
        //public static void CallRecommendationThread()
        //{
            
        //    //Thread.Sleep(10000);
        //    //while (RecommendationThread.IsAlive)
        //    //{
        //    //    Thread.Sleep(5000);
        //    //}
        //}
        public static void AbortRecommendationThread()
        {
            RecommendationThread.Abort();
        }
        #endregion
        #region LoadBehaviorFeatureHistoryThread
        public static void StartBehaviorFeatureHistoryThread()
        {
            BehaviorFeatureHistoryAnalysis bfha = new BehaviorFeatureHistoryAnalysis();
            LoadBehaviorFeatureHistoryThread = new Thread(new ThreadStart(bfha.TimingAutoUpdateBehaviorFeature));
            LoadBehaviorFeatureHistoryThread.Start();
        }
        public static void CallLoadBehaviorFeatureHistoryThread()
        {

            Thread.Sleep(10000);
            while (LoadBehaviorFeatureHistoryThread.IsAlive)
            {
                Thread.Sleep(5000);
            }
        }
        public static void AbortBehaviorFeatureHistoryThread()
        {
            LoadBehaviorFeatureHistoryThread.Abort();
        }
        #endregion

        #region LoadUrlContentHistoryThread  加载网络记录历史的XML文件

        private static void InitStatisticalAnalysis()
        {
            string strDateTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString()
                + "_" + DateTime.Now.Day.ToString();
            string strContentAnalysisFile = System.Environment.CurrentDirectory + "\\" + "ContentAnalysis_" + strDateTime;
            ContentAnalysis.LoadContentAnalysisHistory(strContentAnalysisFile);

            string strUrlAnalysisFile = System.Environment.CurrentDirectory + "\\" + "UrlAnalysis_" + strDateTime;
            UrlAnalysis.LoadUrlAnalysisHistory(strUrlAnalysisFile);
        }
        public static void StartLoadUrlContentHistoryThread()
        {
            LoadUrlContentHistoryThread = new Thread(new ThreadStart(InitStatisticalAnalysis));
            LoadUrlContentHistoryThread.Start();
        }
        public static void AbortLoadUrlContentHistoryThread()
        {
            LoadUrlContentHistoryThread.Abort();
        }
        #endregion


        #region  LoadBehaviorFeaturesThread 加载行为特征历史（行为分析结果  心理分析结果）

        private static void InitalBehaviorFeatures()
        {
            string strFileName = "BehaviorFeatures.xml"; //用户的行为问卷结果?
            BehaviorFeatureAnalysis.LoadBehaviorFeatureFromXML(strFileName);

            string strEmotionHistory = "EmotionPredictionsHistory";
            Recommendation.LoadEmotionPredictionsHistory(strEmotionHistory);//加载心理分析结果

            string fileName2="BehaviorFeaturesHistory.xml";
            BehaviorFeatureHistoryAnalysis.LoadBehaviorFeatureHistoryAnalysis(fileName2);
        }
        public static void StartBehaviorFeaturesThread()
        {
            LoadBehaviorFeaturesThread = new Thread(new ThreadStart(InitalBehaviorFeatures));
            LoadBehaviorFeaturesThread.Start();
        }
        #endregion




    }
}
