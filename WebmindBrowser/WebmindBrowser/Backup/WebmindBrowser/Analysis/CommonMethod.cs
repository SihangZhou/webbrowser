using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;

namespace WebmindBrowser.Analysis
{
    public class CommonMethod
    {


        /// <summary>
        /// 加载XML文件
        /// </summary>
        /// <param name="xmlDoc">XML文件</param>
        /// <param name="strXmlPath">XML文件的路径</param>
        /// <param name="strRoot">根节点的值</param>
        public static void LoadXmlFile(XmlDocument xmlDoc, string strXmlPath, string strRoot)
        {
            if (strXmlPath == string.Empty)
            {
                return;
            }
            if (xmlDoc == null)
            {
                return;
            }
            if (System.IO.File.Exists(strXmlPath))
            {
                xmlDoc.Load(strXmlPath);
            }
            else
            {
                XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.AppendChild(xmlNode);

                XmlElement xmlElement = xmlDoc.CreateElement("", "Root", "");
                XmlText xmlText = xmlDoc.CreateTextNode(strRoot);
                xmlElement.AppendChild(xmlText);
                xmlDoc.AppendChild(xmlElement);
                try
                {
                    xmlDoc.Save(strXmlPath);
                    xmlDoc.Load(strXmlPath);
                }
                catch (System.Exception e)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 保存XML文档
        /// </summary>
        /// <param name="xmlDoc">XML文档</param>
        /// <param name="strXmlPath">保存路径</param>
        public static void SaveXmlFile(XmlDocument xmlDoc, string strXmlPath)
        {
            if (xmlDoc == null)
            {
                return;
            }
            if (strXmlPath == string.Empty)
            {
                return;
            }
            try
            {
                xmlDoc.Save(strXmlPath);
            }
            catch (System.Exception e)
            {
                return;
            }
        }
    }
}
