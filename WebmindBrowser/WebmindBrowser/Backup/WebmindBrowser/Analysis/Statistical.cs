using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WebmindBrowser.Analysis
{
    public class Statistical
    {
        /// <summary>
        /// 对Url访问历史进行统计
        /// </summary>
        /// <param name="strOriginal">Url历史文件的路径</param>
        public static void StatisticalUrlHistory(string strOriginal, string strResultFile)
        {
            Dictionary<string, string> dic_FirstTime = new Dictionary<string,string>();
            Dictionary<string, int> dic_Count = new Dictionary<string,int>();
            Dictionary<string, string> dic_LastTime = new Dictionary<string,string>();
            Dictionary<string, string> dic_HostName = new Dictionary<string,string>();
            try
            {
                ParseXmlFile(strOriginal, ref dic_FirstTime, ref dic_Count, ref dic_LastTime, ref dic_HostName);
                WriteStatisticalResult(strResultFile, dic_FirstTime, dic_Count, dic_LastTime, dic_HostName);
            }
            catch (System.Exception e)
            {
                return;
            }
        }


        public static void WriteStatisticalResult(string strXmlFile, Dictionary<string, string> dic_FirstTime,Dictionary<string, int> dic_Count,
            Dictionary<string, string> dic_LastTime, Dictionary<string, string> dic_HostName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string strRoot = "";
            CommonMethod.LoadXmlFile(xmlDoc, strXmlFile, strRoot);
            XmlNode xmlRoot = xmlDoc.SelectSingleNode("Root");
            foreach (KeyValuePair<string, int> item in dic_Count)
            {
                XmlNode node = xmlDoc.CreateElement("UrlRecord");
                XmlAttribute url = xmlDoc.CreateAttribute("Url");
                url.Value = item.Key;
                node.Attributes.Append(url);

                XmlNode firstTime = xmlDoc.CreateElement("FirstTime");
                firstTime.AppendChild(xmlDoc.CreateTextNode(dic_FirstTime[item.Key]));

                XmlNode lastTime = xmlDoc.CreateElement("LastTime");
                lastTime.AppendChild(xmlDoc.CreateTextNode(dic_LastTime[item.Key]));

                XmlNode domainName = xmlDoc.CreateElement("DomainName");
                domainName.AppendChild(xmlDoc.CreateTextNode(dic_HostName[item.Key]));

                XmlNode staySecond = xmlDoc.CreateElement("StaySecond");
                staySecond.AppendChild(xmlDoc.CreateTextNode(item.Value.ToString()));

                node.AppendChild(firstTime);
                node.AppendChild(lastTime);
                node.AppendChild(domainName);
                node.AppendChild(staySecond);

                xmlRoot.AppendChild(node);
            }

            CommonMethod.SaveXmlFile(xmlDoc, strXmlFile);
        }

        public static void ParseXmlFile(string strOriginal, ref Dictionary<string, string> dic_FirstTime, ref Dictionary<string, int> dic_Count,
            ref Dictionary<string, string> dic_LastTime, ref Dictionary<string, string> dic_HostName)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(strOriginal);
                XmlElement root = xdoc.DocumentElement;//
                XmlNodeList nlist = root.SelectNodes("UrlRecord");//获取根节点的所有子节点
                foreach (XmlNode node in nlist)
                {
                    if (dic_Count.ContainsKey(node.Attributes["Url"].InnerText.ToString()))
                    {
                        dic_Count[node.Attributes["Url"].InnerText.ToString()] += int.Parse(node.SelectSingleNode("StaySecond").InnerText);
                        dic_LastTime[node.Attributes["Url"].InnerText.ToString()] = node.SelectSingleNode("OpenTime").InnerText.ToString();
                    }
                    else
                    {
                        dic_Count.Add(node.Attributes["Url"].InnerText.ToString(), int.Parse(node.SelectSingleNode("StaySecond").InnerText));
                        dic_FirstTime.Add(node.Attributes["Url"].InnerText.ToString(), node.SelectSingleNode("OpenTime").InnerText.ToString());
                        dic_LastTime.Add(node.Attributes["Url"].InnerText.ToString(), node.SelectSingleNode("OpenTime").InnerText.ToString());
                        dic_HostName.Add(node.Attributes["Url"].InnerText.ToString(), node.SelectSingleNode("DomainName").InnerText.ToString());
                    }
                }
            }
            catch (System.Exception e)
            {

            }
        }



        //public static void 

    }
}
