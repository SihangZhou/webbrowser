using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using ZedGraph;

namespace WebmindBrowser.Analysis
{

    public class Recommendation
    {
        //记录心理分析结果
        public static Dictionary<long, double> dic_SOM_Predictions = new Dictionary<long, double>();//躯体化预测记录
        public static Dictionary<long, double> dic_DEP_Predictions = new Dictionary<long, double>();//抑郁预测记录
        public static Dictionary<long, double> dic_ANX_Predictions = new Dictionary<long, double>();//焦虑预测记录
        public static Dictionary<long, double> dic_PSD_Predictions = new Dictionary<long, double>();//病态人格预测记录
        public static Dictionary<long, double> dic_HYP_Predictions = new Dictionary<long, double>();//疑心预测记录
        public static Dictionary<long, double> dic_UNR_Predictions = new Dictionary<long, double>();//脱离现实预测记录
        public static Dictionary<long, double> dic_HMA_Predictions = new Dictionary<long, double>();//兴奋状态预测记录

        

        public static void LoadEmotionPredictionsHistory(string strFileName)
        {
            if (System.IO.File.Exists(strFileName) == false)
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            XmlElement root = xmlDoc.DocumentElement;//
            XmlNodeList nlist = root.SelectNodes("Record");//获取根节点的所有子节点
            foreach (XmlNode node in nlist)
            {
                if (!dic_SOM_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_SOM_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("SOM").InnerText));//这里能执行到
                if (!dic_DEP_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_DEP_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("DEP").InnerText));
                if (!dic_ANX_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_ANX_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("ANX").InnerText));
                if (!dic_PSD_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_PSD_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("PSD").InnerText));
                if (!dic_HYP_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_HYP_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("HYP").InnerText));
                if (!dic_UNR_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_UNR_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("UNR").InnerText));
                if (!dic_HMA_Predictions.ContainsKey(long.Parse(node.Attributes["Time"].InnerText.ToString())))
                    dic_HMA_Predictions.Add(long.Parse(node.Attributes["Time"].InnerText.ToString()), double.Parse(node.SelectSingleNode("HMA").InnerText));
            }
        }

        /// <summary>
        /// 更新心理预测记录
        /// </summary>
        public static void UpdateEmotionPredictionsHistory()//这个函数是用来更新EmotionTracking里pointlist，从来来更新图标的内容。现在我把phi值换成了bar图(原来是曲线图)。
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
        public static void SaveEmotionPredictionsHistory(string strFileName)
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

    }
}
