using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebmindBrowser.UrlHistory
{
    public class UrlRecordEntity
    {
        private string m_curUrl = "about:blank";//当前活动网页的完整URL
        private string m_curDomainName = "";//当前活动网页对应的域名
        private string m_curTitle = "";//当前网页的标题
        private DateTime m_curOpenTime = DateTime.Now;//当前网页打开时间
        private string m_curClientIP = "127.0.0.1";//用户的IP地址
        private Int64 m_curStaySecond = default(Int64);//当前网页的停留时间

        public string CurUrl
        {
            get { return m_curUrl; }
            set { m_curUrl = value; }
        }
        public string CurDomainName
        {
            get { return m_curDomainName; }
            set { m_curDomainName = value; }
        }
        public string CurTitle
        {
            get { return m_curTitle; }
            set { m_curTitle = value; }
        }
        public DateTime CurOpenTime
        {
            get { return m_curOpenTime; }
            set { m_curOpenTime = value; }
        }
        public string CurClientIP
        {
            get { return m_curClientIP; }
            set { m_curClientIP = value; }
        }
        public Int64 CurStaySecond
        {
            get { return m_curStaySecond; }
            set { m_curStaySecond = value; }
        }
    }

    public class UrlMappingEntity
    {
        private string m_UrlMD5 = string.Empty;//Url的MD5的加密值
        private int m_Count = default(int);//页面访问次数
        private int m_ContentCategory = default(int);//页面内容分类结果
        private int m_EmotionCategory = default(int);//页面情感分类结果

        public string UrlMD5
        {
            get { return m_UrlMD5; }
            set { m_UrlMD5 = value; }
        }
        public int Count
        {
            get { return m_Count; }
            set { m_Count = value; }
        }
        public int ContentCategory
        {
            get { return m_ContentCategory; }
            set { m_ContentCategory = value; }
        }
        public int EmotionCategory
        {
            get { return m_EmotionCategory; }
            set { m_EmotionCategory = value; }
        }
    }
}
