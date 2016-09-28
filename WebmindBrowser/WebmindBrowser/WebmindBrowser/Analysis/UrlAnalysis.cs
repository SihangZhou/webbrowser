using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using WebmindBrowser.UrlHistory;

namespace WebmindBrowser.Analysis
{
    public class UrlAnalysis
    {
        //————————————————————————网页的历史记录————————————————
        public static Dictionary<string, string> dic_Url_DomainName = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_Url_Title = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_Url_FirstTime = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_Url_LastTime = new Dictionary<string, string>();
        public static Dictionary<string, string> dic_Url_ClientIP = new Dictionary<string, string>();
        public static Dictionary<string, Int64> dic_Url_StaySecond = new Dictionary<string, Int64>();



        //--------------------------------------用户行为特征————————————————
        //public static Dictionary<>

        /// <summary>
        /// 初始加载Url分析记录的XML文件
        /// </summary>
        /// <param name="strFileName">文件路径</param>
        public static void LoadUrlAnalysisHistory(string strFileName)
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
                if (!dic_Url_DomainName.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_Url_DomainName.Add(node.Attributes["UrlMD5"].InnerText.ToString(), node.SelectSingleNode("DomainName").InnerText);
                if (!dic_Url_Title.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_Url_Title.Add(node.Attributes["UrlMD5"].InnerText.ToString(), node.SelectSingleNode("Title").InnerText);
                if (!dic_Url_FirstTime.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_Url_FirstTime.Add(node.Attributes["UrlMD5"].InnerText.ToString(), node.SelectSingleNode("FirstTime").InnerText);
                if (!dic_Url_LastTime.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_Url_LastTime.Add(node.Attributes["UrlMD5"].InnerText.ToString(), node.SelectSingleNode("LastTime").InnerText);
                if (!dic_Url_ClientIP.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                    dic_Url_ClientIP.Add(node.Attributes["UrlMD5"].InnerText.ToString(), node.SelectSingleNode("ClientIP").InnerText);
                if (!dic_Url_StaySecond.ContainsKey(node.Attributes["UrlMD5"].InnerText.ToString()))
                dic_Url_StaySecond.Add(node.Attributes["UrlMD5"].InnerText.ToString(), Int64.Parse(node.SelectSingleNode("StaySecond").InnerText));
            }
        }


        /// <summary>
        /// 保存文本分析的结果
        /// </summary> 
        /// <param name="strFileName"></param>
        public static void SaveUrlAnalysisHistory(string strFileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string strRoot = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlNode);

            XmlNode xmlRoot = xmlDoc.CreateElement("Root");
            xmlRoot.AppendChild(xmlDoc.CreateTextNode(strRoot));
            xmlDoc.AppendChild(xmlRoot);
            foreach (KeyValuePair<string, long> item in dic_Url_StaySecond)
            {
                XmlNode node = xmlDoc.CreateElement("UrlRecord");
                XmlAttribute url = xmlDoc.CreateAttribute("UrlMD5");
                url.Value = item.Key;
                node.Attributes.Append(url);

                XmlNode DomainName = xmlDoc.CreateElement("DomainName");
                DomainName.AppendChild(xmlDoc.CreateTextNode(dic_Url_DomainName[item.Key].ToString()));

                XmlNode Title = xmlDoc.CreateElement("Title");
                Title.AppendChild(xmlDoc.CreateTextNode(dic_Url_Title[item.Key].ToString()));

                XmlNode FirstTime = xmlDoc.CreateElement("FirstTime");
                FirstTime.AppendChild(xmlDoc.CreateTextNode(dic_Url_FirstTime[item.Key].ToString()));

                XmlNode LastTime = xmlDoc.CreateElement("LastTime");
                LastTime.AppendChild(xmlDoc.CreateTextNode(dic_Url_LastTime[item.Key].ToString()));

                XmlNode ClientIP = xmlDoc.CreateElement("ClientIP");
                ClientIP.AppendChild(xmlDoc.CreateTextNode(dic_Url_ClientIP[item.Key].ToString()));

                XmlNode StaySecond = xmlDoc.CreateElement("StaySecond");
                StaySecond.AppendChild(xmlDoc.CreateTextNode(dic_Url_StaySecond[item.Key].ToString()));

                node.AppendChild(DomainName);
                node.AppendChild(Title);
                node.AppendChild(FirstTime);
                node.AppendChild(LastTime);
                node.AppendChild(ClientIP);
                node.AppendChild(StaySecond);
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
        /// 统计Url的停留时间
        /// </summary>
        /// <param name="strUrlMD5">Url地址</param>
        /// <param name="iStaySecond">停留时间</param>
        public static void UpdateUrlAnalysisHistory(UrlRecordEntity urlRecord )
        {
            if (urlRecord.CurUrl.Equals("about:blank"))
            {
                return;
            }
            if(dic_Url_StaySecond.ContainsKey(urlRecord.CurUrl))
            {
                dic_Url_StaySecond[urlRecord.CurUrl] += urlRecord.CurStaySecond;
                dic_Url_LastTime[urlRecord.CurUrl] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                dic_Url_DomainName.Add(urlRecord.CurUrl, urlRecord.CurDomainName);
                dic_Url_Title.Add(urlRecord.CurUrl, urlRecord.CurTitle);
                dic_Url_FirstTime.Add(urlRecord.CurUrl, urlRecord.CurOpenTime.ToString("yyyy-MM-dd HH:mm:ss"));
                dic_Url_LastTime.Add(urlRecord.CurUrl, urlRecord.CurOpenTime.ToString("yyyy-MM-dd HH:mm:ss"));
                dic_Url_ClientIP.Add(urlRecord.CurUrl, urlRecord.CurClientIP);
                dic_Url_StaySecond.Add(urlRecord.CurUrl, urlRecord.CurStaySecond);
            }
        }

        /// <summary>
        /// 行为特征提取
        /// 目标：更新心理健康预测结果数组
        ///     dic_SOM_Predictions 躯体化预测记录
        ///     dic_DEP_Predictions 抑郁预测记录
        ///     dic_ANX_Predictions 焦虑预测记录
        ///     dic_PSD_Predictions 病态人格预测记录
        ///     dic_HYP_Predictions 疑心预测记录
        ///     dic_UNR_Predictions 脱离现实预测记录
        ///     dic_HMA_Predictions 兴奋状态预测记录
        /// </summary>
        public void AnalysisCurrentHistory()
        {
            long timeLong = DateTime.Now.ToBinary();
            double dSOM = GetSOMPredictions();
            double dDEP = GetDEPPredictions();
            double dANX = GetANXPredictions();
            double dPSD = GetPSDPredictions();
            double dHYP = GetHYPPredictions();
            double dUNR = GetUNRPredictions();
            double dHMA = GetHMAPredictions();

            Recommendation.dic_ANX_Predictions.Add(timeLong, dANX);
            Recommendation.dic_DEP_Predictions.Add(timeLong, dDEP);
            Recommendation.dic_HMA_Predictions.Add(timeLong, dHMA);
            Recommendation.dic_HYP_Predictions.Add(timeLong, dHYP);
            Recommendation.dic_PSD_Predictions.Add(timeLong, dPSD);
            Recommendation.dic_SOM_Predictions.Add(timeLong, dSOM);
            Recommendation.dic_UNR_Predictions.Add(timeLong, dUNR);
        }

        /// <summary>
        /// 获得维度躯体化预测值
        /// </summary>
        /// <returns></returns>
        private double GetSOMPredictions()
        {
            double value = 0;
            return value;
        }
        /// <summary>
        /// 获得维度抑郁预测值
        /// </summary>
        /// <returns></returns>
        private double GetDEPPredictions()
        {
            double value = 0;
            return value;
        }
        /// <summary>
        /// 获得维度焦虑预测值
        /// </summary>
        /// <returns></returns>
        private double GetANXPredictions()
        {
            double value = 0;
            return value;
        }
        /// <summary>
        /// 获得维度病态人格预测值
        /// </summary>
        /// <returns></returns>
        private double GetPSDPredictions()
        {
            double value = 0;
            return value;
        }
        /// <summary>
        /// 获得维度疑心预测值
        /// </summary>
        /// <returns></returns>
        private double GetHYPPredictions()
        {
            double value = 0;
            return value;
        }
        /// <summary>
        /// 获得维度脱离现实预测值
        /// </summary>
        /// <returns></returns>
        private double GetUNRPredictions()
        {
            double value = 0;
            return value;
        }
        /// <summary>
        /// 获得维度兴奋状态预测值
        /// </summary>
        /// <returns></returns>
        private double GetHMAPredictions()
        {
            double value = 0;
            return value;
        }

        //浏览器关闭的时候进行统计分析
        //当天的网络浏览时间(第一条记录和最后一条记录的时间差)
        public static void AnalysisOnlineTime()
        {
            //计算今天的网络浏览时间
            string strMinDateTime = dic_Url_FirstTime.First().Value;//网络浏览的开启时间
            DateTime BeginningDate = DateTime.Parse(strMinDateTime);//转化为DateTime类型
            TimeSpan ts = DateTime.Now.Subtract(BeginningDate);//计算时间间隔
            int iHourSpan = ts.Hours + 1;
            Int16 iWeek = Convert.ToInt16(DateTime.Now.DayOfWeek);//计算礼拜
            if ((iWeek == 0) ||(iWeek == 6))//周六 周日 
            {
                if (iHourSpan <= 3)
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory.RadioSelected_A);
                }
                else if ((iHourSpan > 3) && (iHourSpan <= 4)) //3 < iHourSpan <= 4
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory.RadioSelected_B);
                }else if ((iHourSpan > 4) && (iHourSpan <= 6)) //4 < iHourSpan <= 6
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory.RadioSelected_C);
                }else if ((iHourSpan > 6) && (iHourSpan <= 9)) //6 < iHourSpan <= 9
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory.RadioSelected_D);
                }else if ((iHourSpan > 9) && (iHourSpan <= 12)) //9 < iHourSpan <= 12
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory.RadioSelected_E);
                }else if (iHourSpan > 12)
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory.RadioSelected_F);
                }
            }
            else  //周一 到 周五
            {
                if (iHourSpan <= 2)
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_WorkDay(RadioSelectionCategory.RadioSelected_A);
                }else if ((iHourSpan > 2) && (iHourSpan <= 6)) // 2 < iHourSpan <= 6
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_WorkDay(RadioSelectionCategory.RadioSelected_B);
                }else if ((iHourSpan > 6) && (iHourSpan <= 10)) //6 < iHourSpan <= 10
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_WorkDay(RadioSelectionCategory.RadioSelected_C);
                }else if ((iHourSpan > 10) && (iHourSpan <=12)) //10 < iHourSpan <= 12
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_WorkDay(RadioSelectionCategory.RadioSelected_D);
                }else if (iHourSpan > 12)
                {
                    BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_WorkDay(RadioSelectionCategory.RadioSelected_E);
                }
            }
        }

        //更新当天网页的平均停留时间（隔一定时间分析一次）
        public static void AnalysisONB_StaySecond()
        {
            long lStaySecond = default(long);
            foreach (KeyValuePair<string, long> item in dic_Url_StaySecond)
            {
                lStaySecond = +item.Value;
            }
            lStaySecond = (long)lStaySecond / dic_Url_StaySecond.Count;
            if (lStaySecond <= 10) //≤10秒
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory.RadioSelected_A);
            }
            else if ((lStaySecond > 10) && (lStaySecond <= 30)) //10秒—30秒
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory.RadioSelected_B);
            }
            else if ((lStaySecond > 30) && (lStaySecond <= 60)) //30秒—1分钟
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory.RadioSelected_C);
            }
            else if ((lStaySecond > 60) && (lStaySecond <= 120)) //1—2分钟
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory.RadioSelected_D);
            }
            else if ((lStaySecond > 120) && (lStaySecond <= 300)) //2—5分钟
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory.RadioSelected_E);
            }
            else if (lStaySecond > 300) //>5分钟
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory.RadioSelected_F);
            }
        }

        //分析每天的上网时段：每天的起始上网时间在哪个时段 (一天分析一次即可)
        public static void AnalysisONB_StartTime()
        {
            //计算当天的开始浏览网络的时段
            string strMinDateTime = dic_Url_FirstTime.First().Value;//网络浏览的开启时间
            DateTime BeginningDate = DateTime.Parse(strMinDateTime);//转化为DateTime类型
            int iHour = BeginningDate.Hour;
            if (iHour < 9)
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_A);
            }else if ((iHour >= 9) && (iHour < 12))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_B);
            }else if ((iHour >= 12) && (iHour < 14))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_C);
            }else if ((iHour >= 14) && (iHour < 18))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_D);
            }else if ((iHour >= 18) && (iHour < 19))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_E);
            }else if ((iHour >= 19) && (iHour < 23))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_F);
            }else if ((iHour >= 23) && (iHour < 24))
            {
                BehaviorFeatureAnalysis.UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory.RadioSelected_G);
            }
        }


    }
}
