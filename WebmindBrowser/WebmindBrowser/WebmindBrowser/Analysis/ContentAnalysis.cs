using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using WebmindBrowser.BayesClassifier;

namespace WebmindBrowser.Analysis
{
    public class ContentAnalysis
    {
        //记录当前已有的网页统计记录
        public static Dictionary<string, int> dic_UrlMD5Count = new Dictionary<string, int>();//网页访问次数
        public static Dictionary<string, int> dic_UrlMD5ContentCategory = new Dictionary<string, int>();//网页内容的分类
        public static Dictionary<string, int> dic_UrlMD5EmotionCategory = new Dictionary<string, int>();//网页内容的情感分类
        //public static Dictionary<string, string> dic_UrlMD5Content = new Dictionary<string, string>();//记录网页内容

        //public static ContentClassification contentClfStatic = new ContentClassification();
        //public static EmotionClassification emotionClfStatic = new EmotionClassification();

        /// <summary>
        /// 对给定字符串MD5加密
        /// </summary>
        /// <param name="strOriginal">原始字符串</param>
        /// <returns>加密后字符串</returns>
        public static string ConvertToMd5(string strOriginal)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] sResult = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(strOriginal));
            return BitConverter.ToString(sResult);
        }

        /// <summary>
        /// 初始加载内容分析的XML文件
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        public static void LoadContentAnalysisHistory(string strFileName)
        {
            if (System.IO.File.Exists(strFileName) == false)
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            XmlElement root = xmlDoc.DocumentElement;//
            XmlNodeList nlist = root.SelectNodes("UrlRecord");//获取根节点的所有子节点
            foreach (XmlNode node in nlist)
            {
                if (!dic_UrlMD5Count.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_UrlMD5Count.Add(node.Attributes["UrlMD5"].InnerText.ToString(), int.Parse(node.SelectSingleNode("Count").InnerText));
                if (!dic_UrlMD5ContentCategory.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_UrlMD5ContentCategory.Add(node.Attributes["UrlMD5"].InnerText.ToString(), int.Parse(node.SelectSingleNode("ContentCategory").InnerText));
                if (!dic_UrlMD5EmotionCategory.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_UrlMD5EmotionCategory.Add(node.Attributes["UrlMD5"].InnerText.ToString(), int.Parse(node.SelectSingleNode("EmotionCategory").InnerText));
                //dic_UrlMD5Content.Add(node.Attributes["UrlMD5"].InnerText.ToString(),"");
            }
        }

        /// <summary>
        /// 保存文本分析的结果
        /// </summary> 
        /// <param name="strFileName"></param>
        public static void SaveContentAnalysisHistory(string strFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string strRoot = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlNode);

            XmlNode xmlRoot = xmlDoc.CreateElement("Root");
            xmlRoot.AppendChild(xmlDoc.CreateTextNode(strRoot));
            xmlDoc.AppendChild(xmlRoot);
            //XmlNode xmlRoot = xmlDoc.SelectSingleNode("Root");
            foreach (KeyValuePair<string, int> item in dic_UrlMD5Count)
            {
                XmlNode node = xmlDoc.CreateElement("UrlRecord");
                XmlAttribute url = xmlDoc.CreateAttribute("UrlMD5");
                url.Value = item.Key;
                node.Attributes.Append(url);

                XmlNode Count = xmlDoc.CreateElement("Count");
                Count.AppendChild(xmlDoc.CreateTextNode(dic_UrlMD5Count[item.Key].ToString()));
                XmlNode ContentCategory = xmlDoc.CreateElement("ContentCategory");
                ContentCategory.AppendChild(xmlDoc.CreateTextNode(dic_UrlMD5ContentCategory[item.Key].ToString()));
                XmlNode EmotionCategory = xmlDoc.CreateElement("EmotionCategory");
                EmotionCategory.AppendChild(xmlDoc.CreateTextNode(dic_UrlMD5EmotionCategory[item.Key].ToString()));

                //XmlNode UrlContent = xmlDoc.CreateElement("dic_UrlMD5Content");
                //UrlContent.AppendChild(xmlDoc.CreateTextNode(dic_UrlMD5Content[item.Key].ToString()));

                node.AppendChild(Count);
                node.AppendChild(ContentCategory);
                node.AppendChild(EmotionCategory);
                //node.AppendChild(UrlContent);
                xmlRoot.AppendChild(node);
            }
            try
            {
                if (System.IO.File.Exists(strFileName))
                {
                    System.IO.File.Delete(strFileName);
                }
                xmlDoc.Save(strFileName);
            }
            catch (System.Exception e)
            {
                return;
            }
        }
        /// <summary>
        /// 分析当前的Url网页内容,用到了内容分类和情感分类,得出该网页在内容上属于哪个分类、在情感上属于哪个分类
        /// </summary>
        /// <param name="strUrlMD5">Url的MD5编码</param>
        public static void UpdateContentAnalysisHistory(string strUrlMD5, string strContent)
        {
            if (strUrlMD5.Equals("about:blank"))
            {
                return;
            }
            if (dic_UrlMD5Count.ContainsKey(strUrlMD5))//如果此前访问过该url
            {
                dic_UrlMD5Count[strUrlMD5]++;
            }
            else
            {
                int iFirst = 1;//这里训练材料里好像没有汽车
                //0 : 汽车 //1 : 财经 //2 : IT //3 : 健康 //4 : 体育
                //5 : 旅游 //6 : 教育 //7 : 招聘 //8 : 文化 //9 : 军事
                //int iContent = contentClfStatic.ClassifyMessage(strContent); //内容分类
                testClassifier tc = testClassifier.getSingleObject();
                int iContent=tc.contentClassifierAPI(strContent);
                //0 : 愤怒 //1 : 搞笑 //2 : 难过 //3 : 无聊 //4 : 新奇
                //int iEmotion = emotionClfStatic.ClassifyMessage(strContent);//情感分类
                int iEmotion = tc.EmotionClassifierAPI(strContent);
                
                dic_UrlMD5Count.Add(strUrlMD5,iFirst);//iFirst是 int型
                dic_UrlMD5ContentCategory.Add(strUrlMD5, iContent);//将某个url内容分类，然后做出映射，(strUrlMD5,iContent)
                dic_UrlMD5EmotionCategory.Add(strUrlMD5, iEmotion);//strUrlMD5其实就是"http://www.qq.com",iEmotion是int型
                //dic_UrlMD5Content.Add(strUrlMD5, strContent);
            }
        }


        /// <summary>
        /// 分析网页的情感
        /// </summary>
        public static void AnalysisPAN_EmotionCategory()
        {
            //int iTotalCount = default(int);//网页访问总次数
            int iAnger = default(int);//愤怒(总次数)
            int iHappy = default(int);//搞笑(总次数)
            int iSadness = default(int);//难过（总次数）
            int iBored = default(int);//无聊（总次数）
            int iPeculiar = default(int);//奇特（总次数）
            foreach (KeyValuePair<string, int> item in dic_UrlMD5EmotionCategory)//这里便是利用WebView里documentCompleted事件时的文本分析内容的结果字典
            {
                switch(item.Value)
                {
                    case (int)EmotionClassify.AngerClassify:
                        iAnger += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)EmotionClassify.BoredClassify:
                        iBored += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)EmotionClassify.HappyClassify:
                        iHappy += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)EmotionClassify.PeculiarClassify:
                        iPeculiar += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)EmotionClassify.SadnessClassify:
                        iSadness += dic_UrlMD5Count[item.Key];
                        break;
                }
            }
            int iTotalCount = iAnger + iHappy + iSadness + iBored + iPeculiar;
            double dAngerFrequency = (double)(iAnger) / (double)(iTotalCount);
            EmotionCategoryFrequencyRating(dAngerFrequency, EmotionClassify.AngerClassify);

            double dHappyFrequency = (double)(iAnger) / (double)(iTotalCount);
            EmotionCategoryFrequencyRating(dHappyFrequency, EmotionClassify.HappyClassify);

            double dSadnessFrequency = (double)(iAnger) / (double)(iTotalCount);
            EmotionCategoryFrequencyRating(dSadnessFrequency, EmotionClassify.SadnessClassify);

            double dBoredFrequency = (double)(iAnger) / (double)(iTotalCount);
            EmotionCategoryFrequencyRating(dBoredFrequency, EmotionClassify.BoredClassify);

            double dPeculiarFrequency = (double)(iAnger) / (double)(iTotalCount);
            EmotionCategoryFrequencyRating(dPeculiarFrequency, EmotionClassify.PeculiarClassify);
        }

        private static void EmotionCategoryFrequencyRating(double dFrequency, EmotionClassify emotion)
        {
            double dNeverVisited = 0.05;//从未访问过
            double dFewerVisited = 0.10;//很少访问
            double dSometimesVisited = 0.20;//有时候访问
            double dAlwaysVisited = 0.30;//经常访问
            if (dFrequency <= dNeverVisited)
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PAN_EmotionClassify(emotion, RadioSelectionCategory.RadioSelected_A);
            }
            else if ((dFrequency > dNeverVisited) && (dFrequency <= dFewerVisited))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PAN_EmotionClassify(emotion, RadioSelectionCategory.RadioSelected_B);
            }
            else if ((dFrequency > dFewerVisited) && (dFrequency <= dSometimesVisited))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PAN_EmotionClassify(emotion, RadioSelectionCategory.RadioSelected_C);
            }
            else if ((dFrequency > dSometimesVisited) && (dFrequency <= dAlwaysVisited))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PAN_EmotionClassify(emotion, RadioSelectionCategory.RadioSelected_D);
            }
            else if (dFrequency > dAlwaysVisited)
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PAN_EmotionClassify(emotion, RadioSelectionCategory.RadioSelected_E);
            }
        }

        /// <summary>
        /// 网页的内容分析
        /// </summary>
        public static  void AnalysisPTN_ContentCategory()
        {
            int iAutomotiveCount = default(int);//汽车
            int iEconomicsCount = default(int);//财经
            int iITCount = default(int);//IT
            int iHealthCount = default(int);//健康
            int iSportsCount = default(int);//体育
            int iTravelCount = default(int);//旅游
            int iEducationCount = default(int);//教育
            int iRecruitmentCount = default(int);//招聘
            int iCultureCount = default(int);//文化
            int iMilitaryCount = default(int);//军事
            
#region  统计次数
            foreach (KeyValuePair<string, int> item in dic_UrlMD5ContentCategory)
            {
                switch (item.Value)
                {
                    case (int)ContentClassify.AutomotiveClassify:
                        iAutomotiveCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.CultureClassify:
                        iCultureCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.EconomicsClassify:
                        iEconomicsCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.EducationClassify:
                        iEducationCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.HealthClassify:
                        iHealthCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.ITClassify:
                        iITCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.MilitaryClassify:
                        iMilitaryCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.RecruitmentClassify:
                        iRecruitmentCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.SportsClassify:
                        iSportsCount += dic_UrlMD5Count[item.Key];
                        break;
                    case (int)ContentClassify.TravelClassify:
                        iTravelCount += dic_UrlMD5Count[item.Key];
                        break;
                }
            }
#endregion
            int iTotalCount = iAutomotiveCount + iEconomicsCount + iITCount + iHealthCount + iSportsCount 
                + iTravelCount + iEducationCount + iRecruitmentCount + iCultureCount + iMilitaryCount;
            double dAutomotiveFrequency = (double)(iAutomotiveCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dAutomotiveFrequency, ContentClassify.AutomotiveClassify);

            double dEconomicsFrequency = (double)(iEconomicsCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dAutomotiveFrequency, ContentClassify.EconomicsClassify);

            double dITFrequency = (double)(iITCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dITFrequency, ContentClassify.ITClassify);

            double dHealthFrequency = (double)(iHealthCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dHealthFrequency, ContentClassify.HealthClassify);

            double dSportsFrequency = (double)(iSportsCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dSportsFrequency, ContentClassify.SportsClassify);

            double dTravelFrequency = (double)(iTravelCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dTravelFrequency, ContentClassify.TravelClassify);

            double dEducationFrequency = (double)(iEducationCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dEducationFrequency, ContentClassify.EducationClassify);

            double dRecruitmentFrequency = (double)(iRecruitmentCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dRecruitmentFrequency, ContentClassify.RecruitmentClassify);

            double dCultureFrequency = (double)(iCultureCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dCultureFrequency, ContentClassify.CultureClassify);

            double dMilitaryFrequency = (double)(iMilitaryCount) / (double)(iTotalCount);
            ContentCategoryFrequencyRating(dMilitaryFrequency, ContentClassify.MilitaryClassify);
        }

        private static void ContentCategoryFrequencyRating(double dFrequency, ContentClassify content)
        {
            double dFewerVisited = 0.10;//很少访问
            double dSometimesVisited = 0.20;//有时候访问
            double dAlwaysVisited = 0.30;//经常访问

            if (dFrequency <= dFewerVisited)
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PTN_ContentClassify(content, RadioSelectionCategory.RadioSelected_A);
            }
            else if ((dFrequency > dFewerVisited) && (dFrequency <= dSometimesVisited))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PTN_ContentClassify(content, RadioSelectionCategory.RadioSelected_B);
            }
            else if ((dFrequency > dSometimesVisited) && (dFrequency <= dAlwaysVisited))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PTN_ContentClassify(content, RadioSelectionCategory.RadioSelected_C);
            }
            else if (dFrequency > dAlwaysVisited)
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_PTN_ContentClassify(content, RadioSelectionCategory.RadioSelected_D);
            }
        }
    }
}
