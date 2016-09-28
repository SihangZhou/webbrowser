using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using WebmindBrowser.Analysis;

namespace WebmindBrowser
{
    public partial class WebView : Form
    {
        public WebView()
        {
            InitializeComponent();

          //  this.WebBrowserContent.StartNewWindow += new EventHandler<BrowserExtendedNavigatingEventArgs>(this.WebBrowserContent_StartNewWindow);
        }

        private void WebBrowserContent_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url.ToString().Equals("about:blank"))
            {
                return;
            }
            //加载新页面时,重新赋值LastUrlRecordEntity
            MainWindow.LastUrlRecordEntity.CurClientIP = StaticHelperClass.strClientIPAddress;
            MainWindow.LastUrlRecordEntity.CurDomainName = this.WebBrowserContent.Document.Url.Host;
            MainWindow.LastUrlRecordEntity.CurOpenTime = DateTime.Now;
            MainWindow.LastUrlRecordEntity.CurStaySecond = Stopwatch.GetTimestamp();
            MainWindow.LastUrlRecordEntity.CurTitle = this.WebBrowserContent.Document.Title;
            MainWindow.LastUrlRecordEntity.CurUrl = this.WebBrowserContent.Url.ToString();
            MainWindow.url = this.WebBrowserContent.Url;
        }

        //<summary>
        //这就是传说中的页面加载完成
        //然后直接调用对网页内容分析更新
        //然后我还加上了把该网页加载进历史记录里(addHistory)
        //</summary>
        //
        private void WebBrowserContent_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Equals("about:blank"))
            {
                return;
            }
            //addHistory(e.Url, DateTime.Now.ToString(MainWindow.currentCulture));
            this.Text = WebBrowserContent.Document.Title;
            ((MainWindow)this.MdiParent).UpdateAddressBox(this, WebBrowserContent.Url.ToString());
            ContentAnalysis.UpdateContentAnalysisHistory(e.Url.OriginalString, WebBrowserContent.Document.Body.InnerText);
        }

        //private void UpdateCurVisitedHistory(string strUpdateUrl)
        //{
        //    XmlNodeList nodeList 
        //        = MainWindow.ClientXmlDoc.SelectSingleNode("UrlHistory").ChildNodes;//获取所有子节点
        //    foreach (XmlNode xn in nodeList)//遍历
        //    {
        //        XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
        //        if (xe.GetAttribute("Url") == strUpdateUrl)
        //        {
        //            xe.SetAttributeNode("CloseTime", DateTime.Now.ToString());
        //        }
        //    }
        //}

        private void WriteCurUrlContent()//记录页面的内容
        {

        }

        private void WebBrowserContent_Navigating(object sender, WebBrowserNavigatingEventArgs e)//这里就已经转化成当前请求的网页了
        {
            //m_curUrl = e.Url.OriginalString;
            
        }

        private void WebBrowserContent_StartNewWindow(object sender, BrowserExtendedNavigatingEventArgs e)
        {
            // Here we do the pop-up blocker work

            // Note that in Windows 2000 or lower this event will fire, but the
            // event arguments will not contain any useful information
            // for blocking pop-ups.

            // There are 4 filter levels.
            // None: Allow all pop-ups
            // Low: Allow pop-ups from secure sites
            // Medium: Block most pop-ups
            // High: Block all pop-ups (Use Ctrl to override)

            // We need the instance of the main form, because this holds the instance
            // to the WindowManager.
            MainWindow mf = (MainWindow)this.MdiParent;
            //if (mf == null)
            //  return;

            // Allow a popup when there is no information available or when the Ctrl key is pressed
            bool allowPopup = (e.NavigationContext == UrlContext.None) || ((e.NavigationContext & UrlContext.OverrideKey) == UrlContext.OverrideKey);

            if (!allowPopup)
            {
                // Give None, Low & Medium still a chance.
                switch (SettingsHelper.Current.FilterLevel)
                {
                    case PopupBlockerFilterLevel.None:
                        allowPopup = true;
                        break;
                    case PopupBlockerFilterLevel.Low:
                        // See if this is a secure site
                        if (this.WebBrowserContent.EncryptionLevel != WebBrowserEncryptionLevel.Insecure)
                            allowPopup = true;
                        else
                            // Not a secure site, handle this like the medium filter
                            goto case PopupBlockerFilterLevel.Medium;
                        break;
                    case PopupBlockerFilterLevel.Medium:
                        // This is the most dificult one.
                        // Only when the user first inited and the new window is user inited
                        if ((e.NavigationContext & UrlContext.UserFirstInited) == UrlContext.UserFirstInited && (e.NavigationContext & UrlContext.UserInited) == UrlContext.UserInited)
                            allowPopup = true;
                        break;
                }
            }

            if (allowPopup)
            {
                // Check wheter it's a HTML dialog box. If so, allow the popup but do not open a new tab
                if (!((e.NavigationContext & UrlContext.HtmlDialog) == UrlContext.HtmlDialog))
                {

                    ExtendedWebBrowser ewb = mf.NewBlankBrowserWindow(false);
                    // The (in)famous application object
                    e.AutomationObject = ewb.Application;
                }

            }
            else
                // Here you could notify the user that the pop-up was blocked
                e.Cancel = true;

        }





    }
}
