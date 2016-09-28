using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using ZedGraph;


namespace WebmindBrowser.Analysis
{
    class BehaviorFeatureHistoryAnalysis
    {
        
        const int N = 7;
        //记录心理分析结果
        public static Dictionary<long, double> dic_SOM_Predictions = new Dictionary<long, double>();//躯体化预测记录，躯体化行为模型
        public static Dictionary<long, double> dic_DEP_Predictions = new Dictionary<long, double>();//抑郁预测记录
        public static Dictionary<long, double> dic_ANX_Predictions = new Dictionary<long, double>();//焦虑预测记录
        public static Dictionary<long, double> dic_PSD_Predictions = new Dictionary<long, double>();//病态人格预测记录
        public static Dictionary<long, double> dic_HYP_Predictions = new Dictionary<long, double>();//疑心预测记录
        public static Dictionary<long, double> dic_UNR_Predictions = new Dictionary<long, double>();//脱离现实预测记录
        public static Dictionary<long, double> dic_HMA_Predictions = new Dictionary<long, double>();//兴奋状态预测记录

        //somatization:躯体化行为
        public static Dictionary<String, double> somatization = new Dictionary<String, double>();//躯体化预测记录
       //anxiety:焦虑行为模型
        public static Dictionary<String, double> anxiety = new Dictionary<String, double>();//躯体化预测记录
        //psychopathic:病态人格行为模型
        public static Dictionary<String, double> psychopathic = new Dictionary<String, double>();//躯体化预测记录
        //suspicion:疑心行为模型
        public static Dictionary<String, double> suspicion = new Dictionary<String, double>();//躯体化预测记录
        //outofreality:脱离现实行为模型
        public static Dictionary<String, double> outofreality = new Dictionary<String, double>();//躯体化预测记录
        //exciting:兴奋行为模型
        public static Dictionary<String, double> exciting = new Dictionary<String, double>();//躯体化预测记录
        //depression:抑郁行为模型
        public static Dictionary<String, double> depression = new Dictionary<String, double>();//躯体化预测记录




        public static void LoadBehaviorFeatureHistoryAnalysis(string strFileName)//由于每次只需要最近7次的record来计算网络行为特征值，我在这里就只取BeahviorFeaturesHistory.xml里的最新7次值
        {//在具体设计时，由于XmlNodeList里，是个单向list类型的东西，而且c#没指针，也不能直接从list的末尾往前取值，因此，我只有采取O(n)的算法，先求出record的总个数cnts，然后等到cnts-7时才开始录入dictionary里
            if (System.IO.File.Exists(strFileName) == false)
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            XmlElement root = xmlDoc.DocumentElement;//
            XmlNodeList nlist = root.SelectNodes("Record");//获取根节点的所有子节点
            XmlNode psi, psm, onb, obv;
            int cnts = nlist.Count;
            bool isOkRecord = false;
            if (cnts <= 7) isOkRecord = true;
            foreach (XmlNode node in nlist)// 这里，我只是计算了最近的7次值，以后每次更新，都是用FIFO来替换
            {
                if (isOkRecord){
                    psi = node.SelectSingleNode("PSI");
                    psm = node.SelectSingleNode("PSM");
                    onb = node.SelectSingleNode("ONB");
                    obv = node.SelectSingleNode("OBV");

                    String time = node.Attributes["Time"].InnerText.ToString(), con = psi.SelectSingleNode("Behavior_H").InnerText.ToString();
                    if (!somatization.ContainsKey(time))
                        somatization.Add((time), double.Parse(con));
                    if (!anxiety.ContainsKey((node.Attributes["Time"].InnerText.ToString())))
                    anxiety.Add((node.Attributes["Time"].InnerText.ToString()), double.Parse(psm.SelectSingleNode("Others").InnerText));
                    double staysecond = double.Parse(onb.SelectSingleNode("StaySecondA").InnerText)*1 + double.Parse(onb.SelectSingleNode("StaySecondB").InnerText)*2
                        + double.Parse(onb.SelectSingleNode("StaySecondC").InnerText)*3 + double.Parse(onb.SelectSingleNode("StaySecondD").InnerText.ToString())*4
                        + double.Parse(onb.SelectSingleNode("StaySecondE").InnerText)*5 + double.Parse(onb.SelectSingleNode("StaySecondF").InnerText.ToString())*6;
                    if (!psychopathic.ContainsKey((node.Attributes["Time"].InnerText.ToString())))
                    psychopathic.Add((node.Attributes["Time"].InnerText.ToString()), staysecond);
                   
                    double unhealthy=double.Parse(obv.SelectSingleNode("UnhealthyWeb0").InnerText)*1
                      + double.Parse(obv.SelectSingleNode("UnhealthyWeb1").InnerText) * 2 + double.Parse(obv.SelectSingleNode("UnhealthyWeb2").InnerText) * 3
                        + double.Parse(obv.SelectSingleNode("UnhealthyWeb3_6").InnerText) * 4 + double.Parse(obv.SelectSingleNode("UnhealthyWeb6").InnerText) * 5;
                    if (!suspicion.ContainsKey((node.Attributes["Time"].InnerText.ToString())))
                        suspicion.Add((node.Attributes["Time"].InnerText.ToString()),unhealthy);

                    double ac = double.Parse(onb.SelectSingleNode("Active1_20").InnerText) * 1
                      + double.Parse(onb.SelectSingleNode("Active21_40").InnerText) * 2 + double.Parse(onb.SelectSingleNode("Active41_60").InnerText) * 3
                        + double.Parse(onb.SelectSingleNode("Active61_80").InnerText) * 4 + double.Parse(onb.SelectSingleNode("Active81_100").InnerText) * 5;
                    if (!outofreality.ContainsKey((node.Attributes["Time"].InnerText.ToString())))
                    outofreality.Add((node.Attributes["Time"].InnerText.ToString()),ac);
                    

                    double ex = double.Parse(psi.SelectSingleNode("ContactFriends2").InnerText) * 1
                      + double.Parse(psi.SelectSingleNode("ContactFriends2_5").InnerText) * 2 + double.Parse(psi.SelectSingleNode("ContactFriends5_10").InnerText) * 3
                        + double.Parse(psi.SelectSingleNode("ContactFriends10_20").InnerText) * 4 + double.Parse(psi.SelectSingleNode("ContactFriends20").InnerText) * 5;
                    if (!exciting.ContainsKey(node.Attributes["Time"].InnerText.ToString()))
                        exciting.Add((node.Attributes["Time"].InnerText.ToString()), ex);

                    double de = double.Parse(onb.SelectSingleNode("StartTimeA").InnerText) * 1
                     + double.Parse(onb.SelectSingleNode("StartTimeB").InnerText) * 2 + double.Parse(onb.SelectSingleNode("StartTimeC").InnerText) * 3
                       + double.Parse(onb.SelectSingleNode("StartTimeD").InnerText) * 4 + double.Parse(onb.SelectSingleNode("StartTimeE").InnerText) * 5
                       + double.Parse(onb.SelectSingleNode("StartTimeF").InnerText) * 6 + double.Parse(onb.SelectSingleNode("StartTimeG").InnerText) * 7;
                    if (!depression.ContainsKey(node.Attributes["Time"].InnerText.ToString()))
                    depression.Add((node.Attributes["Time"].InnerText.ToString()), de);
                }else
                {
                    cnts--;
                    if (cnts <= 7)
                        isOkRecord = true;
                }

            }
        }

        /// <summary>
        /// 更新心理预测记录
        /// </summary>
        public static void UpdateBehaviorFeatureHistory()//更新EmotionTracking里面的行为特征值(也就是曲线数据),其实就是要从BehaviorFeaturesHistory.xml里取出最新的record，然后加载到EmotionTracking里的pointlist里
        {
            long lPredictionTime = DateTime.Now.ToBinary();
            double xAxis = (double)new XDate(DateTime.FromBinary(lPredictionTime));

            double dANX = BehaviorFeatureAnalysis.Calc_ANX_Prediction_Value();
            dic_ANX_Predictions.Add(lPredictionTime, dANX);
            EmotionTracking.ANXValueList.Add(xAxis, dANX);

            double dDEP = BehaviorFeatureAnalysis.Calc_DEP_Prediction_Value();
            dic_DEP_Predictions.Add(lPredictionTime, dDEP);
            EmotionTracking.DEPValueList.Add(xAxis, dDEP);

            double dHMA = BehaviorFeatureAnalysis.Calc_HMA_Prediction_Value();
            dic_HMA_Predictions.Add(lPredictionTime, dHMA);
            EmotionTracking.HMAValueList.Add(xAxis, dHMA);

            double dHYP = BehaviorFeatureAnalysis.Calc_HYP_Prediction_Value();
            dic_HYP_Predictions.Add(lPredictionTime, dHYP);
            EmotionTracking.HYPValueList.Add(xAxis, dHYP);

            double dPSD = BehaviorFeatureAnalysis.Calc_PSD_Prediction_Value();
            dic_PSD_Predictions.Add(lPredictionTime, dPSD);
            EmotionTracking.PSDValueList.Add(xAxis, dPSD);

            double dSOM = BehaviorFeatureAnalysis.Calc_SOM_Prediction_Value();
            dic_SOM_Predictions.Add(lPredictionTime, dSOM);
            EmotionTracking.SOMValueList.Add(xAxis, dSOM);

            double dUNR = BehaviorFeatureAnalysis.Calc_UNR_Prediction_Value();
            dic_UNR_Predictions.Add(lPredictionTime, dUNR);
            EmotionTracking.UNRValueList.Add(xAxis, dUNR);
        }

        /// <summary>
        /// 保存心理预测的结果
        /// </summary> 
        /// <param name="strFileName"></param>
        public  void SaveEmotionPredictionsHistory(string strFileName)//感觉这里就不需要了su
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(xmlNode);

            XmlNode xmlRoot = xmlDoc.CreateElement("Predictions");
            xmlDoc.AppendChild(xmlRoot);
            foreach (KeyValuePair<long, double> item in dic_SOM_Predictions)
            {
                XmlNode node = xmlDoc.CreateElement("Record");
                XmlAttribute timeNode = xmlDoc.CreateAttribute("Time");
                timeNode.Value = item.Key.ToString();
                node.Attributes.Append(timeNode);

                XmlNode SOMNode = xmlDoc.CreateElement("SOM");
                SOMNode.AppendChild(xmlDoc.CreateTextNode(dic_SOM_Predictions[item.Key].ToString()));

                XmlNode DEPNode = xmlDoc.CreateElement("DEP");
                DEPNode.AppendChild(xmlDoc.CreateTextNode(dic_DEP_Predictions[item.Key].ToString()));

                XmlNode AMXNode = xmlDoc.CreateElement("ANX");
                AMXNode.AppendChild(xmlDoc.CreateTextNode(dic_ANX_Predictions[item.Key].ToString()));

                XmlNode PSDNode = xmlDoc.CreateElement("PSD");
                PSDNode.AppendChild(xmlDoc.CreateTextNode(dic_PSD_Predictions[item.Key].ToString()));

                XmlNode HYPNode = xmlDoc.CreateElement("HYP");
                HYPNode.AppendChild(xmlDoc.CreateTextNode(dic_HYP_Predictions[item.Key].ToString()));

                XmlNode UNRNode = xmlDoc.CreateElement("UNR");
                UNRNode.AppendChild(xmlDoc.CreateTextNode(dic_UNR_Predictions[item.Key].ToString()));

                XmlNode HMANode = xmlDoc.CreateElement("HMA");
                HMANode.AppendChild(xmlDoc.CreateTextNode(dic_HMA_Predictions[item.Key].ToString()));

                node.AppendChild(SOMNode);
                node.AppendChild(DEPNode);
                node.AppendChild(AMXNode);
                node.AppendChild(PSDNode);
                node.AppendChild(HYPNode);
                node.AppendChild(UNRNode);
                node.AppendChild(HMANode);
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
        public  void TimingAutoUpdateBehaviorFeature()//定时自动更新EmotionTracking里的行为特征曲线值
        {

        }
    }
}
