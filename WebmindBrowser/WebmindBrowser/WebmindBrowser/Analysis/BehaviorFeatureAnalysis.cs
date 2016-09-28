using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WebmindBrowser.Analysis
{
    public class BehaviorFeatureAnalysis
    {
#region   PSI  
        //社交网络网站中的主要行为
        public static int PSI_Behavior_A = default(int);//与一些固定的网友聊天
        public static int PSI_Behavior_B = default(int);//参与兴趣小组、话题小组
        public static int PSI_Behavior_C = default(int);//参与跟帖
        public static int PSI_Behavior_D = default(int);//与好友分享照片、心得体会
        public static int PSI_Behavior_E = default(int);//访问一些讨论区，参与热点话题
        public static int PSI_Behavior_F = default(int);//发表日志
        public static int PSI_Behavior_G = default(int);//玩网页游戏
        public static int PSI_Behavior_H = default(int);//访问好友个人页面
        public static int PSI_Behavior_I = default(int);//其他
        //每天会使用多长时间的即时通讯软件
        public static int PSI_IMActiveTimeA = default(int);//30分钟以内
        public static int PSI_IMActiveTimeB = default(int);//半小时到1小时
        public static int PSI_IMActiveTimeC = default(int);// l—2小时
        public static int PSI_IMActiveTimeD = default(int);//2—3小时
        public static int PSI_IMActiveTimeE = default(int);//3小时以上
        //即时通讯软件上的好友数

        public static int PSI_IMFriends10 = default(int);//10个以下
        public static int PSI_IMFriends10_50 = default(int);//10—50个
        public static int PSI_IMFriends50_100 = default(int);//50—100个
        public static int PSI_IMFriends100_200 = default(int);//100—200个
        public static int PSI_IMFriends200 = default(int);// 200个以上
        //每天联络的网友人数
        public static int PSI_ContactFriends2 = default(int);//2个以下
        public static int PSI_ContactFriends2_5 = default(int);//2—5个
        public static int PSI_ContactFriends5_10 = default(int);//5—10个
        public static int PSI_ContactFriends10_20 = default(int);//10—20个
        public static int PSI_ContactFriends20 = default(int);//20个以上
#endregion
        
#region   PAN
        //网页内容的情感分类
        public static int PAN_Happy = default(int);//喜悦/开心
        public static int PAN_Anger = default(int);//愤怒/暴力
        public static int PAN_Sadness = default(int);//哀伤/难过
        public static int PAN_Fear = default(int);//恐惧/恐怖
        public static int PAN_Peculiar = default(int);//新奇/奇特
        public static int PAN_Moving = default(int);//感动
        public static int PAN_Warm = default(int);//温馨
        public static int PAN_Sympathy = default(int);//同情
#endregion 

#region   PFN  
        //互联网使用功能分类
        public static int PFN_Email = default(int);//电子邮箱
        public static int PFN_BrowseNews = default(int);//浏览新闻
        public static int PFN_SearchEngine = default(int);//搜索引擎
        public static int PFN_Downloading = default(int);//软件下载
        public static int PFN_BrowseWebSite = default(int);//浏览网站/网页
        public static int PFN_Chatting = default(int);//网上聊天（聊天室、QQ、MSN等）
        public static int PFN_BBSForum = default(int);//BBS论坛、社区、讨论组等
        public static int PFN_HomeSpace = default(int);//个人主页空间（博客）
        public static int PFN_OnlineGames = default(int);//网络游戏
        public static int PFN_OnlineShopping = default(int);//网络购物/拍卖
        public static int PFN_OnlineEducation = default(int);//网上教育
        public static int PFN_SMS = default(int);//短信服务（彩信、彩铃）
        public static int PFN_Ezine = default(int);//电子杂志
        public static int PFN_Booking = default(int);//票务、旅店预定
        public static int PFN_Multimedia = default(int);//多媒体娱乐（MP3、FLASH等）
        public static int PFN_Recruitment = default(int);//网上招聘
        public static int PFN_Classmates = default(int);//同学录、校友录
        public static int PFN_SpanNoPurpose = default(int);//无目的漫游
#endregion

#region  PSM 
        //使用搜索引擎进行信息检索时，您一般会选择哪种检索手段
        public static int PSM_SingleKeyword = default(int);//单个关键词搜索
        public static int PSM_MultipleKeywords = default(int);//多个关键词搜索
        public static int PSM_Advanced = default(int);//高级搜索
        public static int PSM_CategorySearch = default(int);//分类搜索（新闻、网页等）或利用导航
        public static int PSM_Others = default(int);//其他
#endregion

#region  PTN 
        //对不同类别的网络内容的关注频度
        public static int PTN_IT = default(int);//计算机
        public static int PTN_OnlineGames = default(int);//网络游戏
        public static int PTN_Health = default(int);//健康
        public static int PTN_News = default(int);//新闻
        public static int PTN_Entertainment = default(int);//休闲娱乐
        public static int PTN_Research = default(int);//学术研究资料
        public static int PTN_Science = default(int);//科学动态与趋势
        public static int PTN_Shopping = default(int);//购物
        public static int PTN_Social = default(int);//社会
        public static int PTN_Sports = default(int);//体育
#endregion

#region  ONB 
        //周六和周日您平均每天上网的时间
        public static int ONB_Weekend3 = default(int);//≤3小时
        public static int ONB_Weekend3_4 = default(int);//3－4小时
        public static int ONB_Weekend4_6 = default(int);//4－6小时
        public static int ONB_Weekend6_9 = default(int);//6－9小时
        public static int ONB_Weekend9_12 = default(int);//9－12小时
        public static int ONB_Weekend12 = default(int);//>12小时
        //周一到周五您平均每天上网的时间
        public static int ONB_WorkDay2 = default(int);//≤2小时
        public static int ONB_WorkDay2_6 = default(int);// 2－6小时
        public static int ONB_WorkDay6_10 = default(int);//6－10小时
        public static int ONB_WorkDay10_12 = default(int);//10－12小时
        public static int ONB_WorkDay12 = default(int);//>12小时
        //每天使用社交网络的时间
        public static int ONB_SocialNetworkingA = default(int);// 30分钟以内
        public static int ONB_SocialNetworkingB = default(int);// 半小时到1小时
        public static int ONB_SocialNetworkingC = default(int);// l—2小时
        public static int ONB_SocialNetworkingD = default(int);// 2—3小时
        public static int ONB_SocialNetworkingE = default(int);// 3小时以上
        //多数情况下在什么时间上网?
        public static int ONB_StartTimeA = default(int);//9:00之前
        public static int ONB_StartTimeB = default(int);//9:00－12:00
        public static int ONB_StartTimeC = default(int);//12:00－14:00
        public static int ONB_StartTimeD = default(int);//14:00－18:00
        public static int ONB_StartTimeE = default(int);//18:00－19:00
        public static int ONB_StartTimeF = default(int);//19:00－23:00
        public static int ONB_StartTimeG = default(int);//23:00－24:00
        //每个网页上的平均停留时间
        public static int ONB_StaySecondA = default(int);//≤10秒
        public static int ONB_StaySecondB = default(int);//10秒—30秒
        public static int ONB_StaySecondC = default(int);//30秒—1分钟
        public static int ONB_StaySecondD = default(int);//1—2分钟
        public static int ONB_StaySecondE = default(int);//2—5分钟
        public static int ONB_StaySecondF = default(int);//>5分钟
        //自己每天的“主动网络行为”约占上网时间的百分比
        public static int ONB_Active1_20 = default(int);//
        public static int ONB_Active21_40 = default(int);
        public static int ONB_Active41_60 = default(int);
        public static int ONB_Active61_80 = default(int);
        public static int ONB_Active81_100 = default(int);
#endregion

#region   OBV 
        public static int OBV_Gender = default(int);//性别 男:0  女 :1 
        public static int OBV_Age = default(int);//年龄
        //最常使用的开始网络浏览的方式
        public static int OBV_StartBrowsingA = default(int);//直接输入网络地址
        public static int OBV_StartBrowsingB = default(int);//从书签中找
        public static int OBV_StartBrowsingC = default(int);//使用搜索引擎
        public static int OBV_StartBrowsingD = default(int);//其他
        //每周主动去浏览不良信息（色情/暴力等）网站的次数
        public static int OBV_UnhealthyWeb0 = default(int);//从不
        public static int OBV_UnhealthyWeb1 = default(int);//l次
        public static int OBV_UnhealthyWeb2 = default(int);//2次
        public static int OBV_UnhealthyWeb3_6 = default(int);//3—6次
        public static int OBV_UnhealthyWeb6 = default(int);//>6次
        //每天进行的搜索次数
        public static int OBV_SearchCount5 = default(int);//≤5次
        public static int OBV_SearchCount5_10 = default(int);//5—10次
        public static int OBV_SearchCount10_20 = default(int);//10—20次
        public static int OBV_SearchCount20_30 = default(int);//20—30次	
        public static int OBV_SearchCount30_50 = default(int);//30—50次
        public static int OBV_SearchCount50 = default(int);// >50次
        //每天花在网络游戏上的时间
        public static int OBV_OnlineGamesTimeA = default(int);//30分钟以内
        public static int OBV_OnlineGamesTimeB = default(int);//半小时到1小时
        public static int OBV_OnlineGamesTimeC = default(int);// l—2小时
        public static int OBV_OnlineGamesTimeD = default(int);//2—3小时
        public static int OBV_OnlineGamesTimeE = default(int);//3—5小时
        public static int OBV_OnlineGamesTimeF = default(int);//5小时以上
#endregion
        /// <summary>
        /// 加载上次行为分析的结果
        /// </summary>
        /// <param name="strFileName"></param>
        public static void LoadBehaviorFeatureFromXML(string strFileName)
        {
            if (System.IO.File.Exists(strFileName) == false)
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFileName);
            XmlElement root = xmlDoc.DocumentElement;//

            XmlNode nPSI = root.SelectSingleNode("PSI");            
            PSI_Behavior_A = int.Parse(nPSI.SelectSingleNode("Behavior_A").InnerText);
            PSI_Behavior_B = int.Parse(nPSI.SelectSingleNode("Behavior_B").InnerText);
            PSI_Behavior_C = int.Parse(nPSI.SelectSingleNode("Behavior_C").InnerText);
            PSI_Behavior_D = int.Parse(nPSI.SelectSingleNode("Behavior_D").InnerText);
            PSI_Behavior_E = int.Parse(nPSI.SelectSingleNode("Behavior_E").InnerText);
            PSI_Behavior_F = int.Parse(nPSI.SelectSingleNode("Behavior_F").InnerText);
            PSI_Behavior_G = int.Parse(nPSI.SelectSingleNode("Behavior_G").InnerText);
            PSI_Behavior_H = int.Parse(nPSI.SelectSingleNode("Behavior_H").InnerText);
            PSI_Behavior_I = int.Parse(nPSI.SelectSingleNode("Behavior_I").InnerText);
            PSI_IMActiveTimeA = int.Parse(nPSI.SelectSingleNode("IMActiveTimeA").InnerText);
            PSI_IMActiveTimeB = int.Parse(nPSI.SelectSingleNode("IMActiveTimeB").InnerText);
            PSI_IMActiveTimeC = int.Parse(nPSI.SelectSingleNode("IMActiveTimeC").InnerText);
            PSI_IMActiveTimeD = int.Parse(nPSI.SelectSingleNode("IMActiveTimeD").InnerText);
            PSI_IMActiveTimeE = int.Parse(nPSI.SelectSingleNode("IMActiveTimeE").InnerText);
            PSI_IMFriends10 = int.Parse(nPSI.SelectSingleNode("IMFriends10").InnerText);
            PSI_IMFriends10_50 = int.Parse(nPSI.SelectSingleNode("IMFriends10_50").InnerText);
            PSI_IMFriends50_100 = int.Parse(nPSI.SelectSingleNode("IMFriends50_100").InnerText);
            PSI_IMFriends100_200 = int.Parse(nPSI.SelectSingleNode("IMFriends100_200").InnerText);
            PSI_IMFriends200 = int.Parse(nPSI.SelectSingleNode("IMFriends200").InnerText);
            PSI_ContactFriends2 = int.Parse(nPSI.SelectSingleNode("ContactFriends2").InnerText);
            PSI_ContactFriends2_5 = int.Parse(nPSI.SelectSingleNode("ContactFriends2_5").InnerText);
            PSI_ContactFriends5_10 = int.Parse(nPSI.SelectSingleNode("ContactFriends5_10").InnerText);
            PSI_ContactFriends10_20 = int.Parse(nPSI.SelectSingleNode("ContactFriends10_20").InnerText);
            PSI_ContactFriends20 = int.Parse(nPSI.SelectSingleNode("ContactFriends20").InnerText);

            XmlNode nPAN = root.SelectSingleNode("PAN");
            PAN_Happy = int.Parse(nPAN.SelectSingleNode("Happy").InnerText);
            PAN_Anger = int.Parse(nPAN.SelectSingleNode("Anger").InnerText);
            PAN_Sadness = int.Parse(nPAN.SelectSingleNode("Sadness").InnerText);
            PAN_Fear = int.Parse(nPAN.SelectSingleNode("Fear").InnerText);
            PAN_Peculiar = int.Parse(nPAN.SelectSingleNode("Peculiar").InnerText);
            PAN_Moving = int.Parse(nPAN.SelectSingleNode("Moving").InnerText);
            PAN_Warm = int.Parse(nPAN.SelectSingleNode("Warm").InnerText);
            PAN_Sympathy = int.Parse(nPAN.SelectSingleNode("Sympathy").InnerText);

            XmlNode nPFN = root.SelectSingleNode("PFN");
            PFN_Email = int.Parse(nPFN.SelectSingleNode("Email").InnerText);
            PFN_BrowseNews = int.Parse(nPFN.SelectSingleNode("BrowseNews").InnerText);
            PFN_SearchEngine = int.Parse(nPFN.SelectSingleNode("SearchEngine").InnerText);
            PFN_Downloading = int.Parse(nPFN.SelectSingleNode("Downloading").InnerText);
            PFN_BrowseWebSite = int.Parse(nPFN.SelectSingleNode("BrowseWebSite").InnerText);
            PFN_Chatting = int.Parse(nPFN.SelectSingleNode("Chatting").InnerText);
            PFN_BBSForum = int.Parse(nPFN.SelectSingleNode("BBSForum").InnerText);
            PFN_HomeSpace = int.Parse(nPFN.SelectSingleNode("HomeSpace").InnerText);
            PFN_OnlineGames = int.Parse(nPFN.SelectSingleNode("OnlineGames").InnerText);
            PFN_OnlineShopping = int.Parse(nPFN.SelectSingleNode("OnlineShopping").InnerText);
            PFN_OnlineEducation = int.Parse(nPFN.SelectSingleNode("OnlineEducation").InnerText);
            PFN_SMS = int.Parse(nPFN.SelectSingleNode("SMS").InnerText);
            PFN_Ezine = int.Parse(nPFN.SelectSingleNode("E-zine").InnerText);
            PFN_Booking = int.Parse(nPFN.SelectSingleNode("Booking").InnerText);
            PFN_Multimedia = int.Parse(nPFN.SelectSingleNode("Multimedia").InnerText);
            PFN_Recruitment = int.Parse(nPFN.SelectSingleNode("Recruitment").InnerText);
            PFN_Classmates = int.Parse(nPFN.SelectSingleNode("Classmates").InnerText);
            PFN_SpanNoPurpose = int.Parse(nPFN.SelectSingleNode("SpanNoPurpose").InnerText);

            XmlNode nPSM = root.SelectSingleNode("PSM");
            PSM_SingleKeyword = int.Parse(nPSM.SelectSingleNode("SingleKeyword").InnerText);
            PSM_MultipleKeywords = int.Parse(nPSM.SelectSingleNode("MultipleKeywords").InnerText);
            PSM_Advanced = int.Parse(nPSM.SelectSingleNode("Advanced").InnerText);
            PSM_CategorySearch = int.Parse(nPSM.SelectSingleNode("CategorySearch").InnerText);
            PSM_Others = int.Parse(nPSM.SelectSingleNode("Others").InnerText);

            XmlNode nPTN = root.SelectSingleNode("PTN");
            PTN_IT = int.Parse(nPTN.SelectSingleNode("IT").InnerText);
            PTN_OnlineGames = int.Parse(nPTN.SelectSingleNode("OnlineGames").InnerText);
            PTN_Health = int.Parse(nPTN.SelectSingleNode("Health").InnerText);
            PTN_News = int.Parse(nPTN.SelectSingleNode("News").InnerText);
            PTN_Entertainment = int.Parse(nPTN.SelectSingleNode("Entertainment").InnerText);
            PTN_Research = int.Parse(nPTN.SelectSingleNode("Research").InnerText);
            PTN_Science = int.Parse(nPTN.SelectSingleNode("Science").InnerText);
            PTN_Shopping = int.Parse(nPTN.SelectSingleNode("Shopping").InnerText);
            PTN_Social = int.Parse(nPTN.SelectSingleNode("Social").InnerText);
            PTN_Sports = int.Parse(nPTN.SelectSingleNode("Sports").InnerText);

            XmlNode nONB = root.SelectSingleNode("ONB");
            ONB_Weekend3 = int.Parse(nONB.SelectSingleNode("Weekend3").InnerText);
            ONB_Weekend3_4 = int.Parse(nONB.SelectSingleNode("Weekend3_4").InnerText);
            ONB_Weekend4_6 = int.Parse(nONB.SelectSingleNode("Weekend4_6").InnerText);
            ONB_Weekend6_9 = int.Parse(nONB.SelectSingleNode("Weekend6_9").InnerText);
            ONB_Weekend9_12 = int.Parse(nONB.SelectSingleNode("Weekend9_12").InnerText);
            ONB_Weekend12 = int.Parse(nONB.SelectSingleNode("Weekend12").InnerText);
            ONB_WorkDay2 = int.Parse(nONB.SelectSingleNode("WorkDay2").InnerText);
            ONB_WorkDay2_6 = int.Parse(nONB.SelectSingleNode("WorkDay2_6").InnerText);
            ONB_WorkDay6_10 = int.Parse(nONB.SelectSingleNode("WorkDay6_10").InnerText);
            ONB_WorkDay10_12 = int.Parse(nONB.SelectSingleNode("WorkDay10_12").InnerText);
            ONB_WorkDay12 = int.Parse(nONB.SelectSingleNode("WorkDay12").InnerText);
            ONB_SocialNetworkingA = int.Parse(nONB.SelectSingleNode("SocialNetworkingA").InnerText);
            ONB_SocialNetworkingB = int.Parse(nONB.SelectSingleNode("SocialNetworkingB").InnerText);
            ONB_SocialNetworkingC = int.Parse(nONB.SelectSingleNode("SocialNetworkingC").InnerText);
            ONB_SocialNetworkingD = int.Parse(nONB.SelectSingleNode("SocialNetworkingD").InnerText);
            ONB_SocialNetworkingE = int.Parse(nONB.SelectSingleNode("SocialNetworkingE").InnerText);
            ONB_StartTimeA = int.Parse(nONB.SelectSingleNode("StartTimeA").InnerText);
            ONB_StartTimeB = int.Parse(nONB.SelectSingleNode("StartTimeB").InnerText);
            ONB_StartTimeC = int.Parse(nONB.SelectSingleNode("StartTimeC").InnerText);
            ONB_StartTimeD = int.Parse(nONB.SelectSingleNode("StartTimeD").InnerText);
            ONB_StartTimeE = int.Parse(nONB.SelectSingleNode("StartTimeE").InnerText);
            ONB_StartTimeF = int.Parse(nONB.SelectSingleNode("StartTimeF").InnerText);
            ONB_StartTimeG = int.Parse(nONB.SelectSingleNode("StartTimeG").InnerText);
            ONB_StaySecondA = int.Parse(nONB.SelectSingleNode("StaySecondA").InnerText);
            ONB_StaySecondB = int.Parse(nONB.SelectSingleNode("StaySecondB").InnerText);
            ONB_StaySecondC = int.Parse(nONB.SelectSingleNode("StaySecondC").InnerText);
            ONB_StaySecondD = int.Parse(nONB.SelectSingleNode("StaySecondD").InnerText);
            ONB_StaySecondE = int.Parse(nONB.SelectSingleNode("StaySecondE").InnerText);
            ONB_StaySecondF = int.Parse(nONB.SelectSingleNode("StaySecondF").InnerText);
            ONB_Active1_20 = int.Parse(nONB.SelectSingleNode("Active1_20").InnerText);
            ONB_Active21_40 = int.Parse(nONB.SelectSingleNode("Active21_40").InnerText);
            ONB_Active41_60 = int.Parse(nONB.SelectSingleNode("Active41_60").InnerText);
            ONB_Active61_80 = int.Parse(nONB.SelectSingleNode("Active61_80").InnerText);
            ONB_Active81_100 = int.Parse(nONB.SelectSingleNode("Active81_100").InnerText);

            XmlNode nOBV = root.SelectSingleNode("OBV");
            OBV_Gender = int.Parse(nOBV.SelectSingleNode("Gender").InnerText);
            OBV_Age = int.Parse(nOBV.SelectSingleNode("Age").InnerText);
            OBV_StartBrowsingA = int.Parse(nOBV.SelectSingleNode("StartBrowsingA").InnerText);
            OBV_StartBrowsingB = int.Parse(nOBV.SelectSingleNode("StartBrowsingB").InnerText);
            OBV_StartBrowsingC = int.Parse(nOBV.SelectSingleNode("StartBrowsingC").InnerText);
            OBV_StartBrowsingD = int.Parse(nOBV.SelectSingleNode("StartBrowsingD").InnerText);
            OBV_UnhealthyWeb0 = int.Parse(nOBV.SelectSingleNode("UnhealthyWeb0").InnerText);
            OBV_UnhealthyWeb1 = int.Parse(nOBV.SelectSingleNode("UnhealthyWeb1").InnerText);
            OBV_UnhealthyWeb2 = int.Parse(nOBV.SelectSingleNode("UnhealthyWeb2").InnerText);
            OBV_UnhealthyWeb3_6 = int.Parse(nOBV.SelectSingleNode("UnhealthyWeb3_6").InnerText);
            OBV_UnhealthyWeb6 = int.Parse(nOBV.SelectSingleNode("UnhealthyWeb6").InnerText);
            OBV_SearchCount5 = int.Parse(nOBV.SelectSingleNode("SearchCount5").InnerText);
            OBV_SearchCount5_10 = int.Parse(nOBV.SelectSingleNode("SearchCount5_10").InnerText);
            OBV_SearchCount10_20 = int.Parse(nOBV.SelectSingleNode("SearchCount10_20").InnerText);
            OBV_SearchCount20_30 = int.Parse(nOBV.SelectSingleNode("SearchCount20_30").InnerText);
            OBV_SearchCount30_50 = int.Parse(nOBV.SelectSingleNode("SearchCount30_50").InnerText);
            OBV_SearchCount50 = int.Parse(nOBV.SelectSingleNode("SearchCount50").InnerText);
            OBV_OnlineGamesTimeA = int.Parse(nOBV.SelectSingleNode("OnlineGamesTimeA").InnerText);
            OBV_OnlineGamesTimeB = int.Parse(nOBV.SelectSingleNode("OnlineGamesTimeB").InnerText);
            OBV_OnlineGamesTimeC = int.Parse(nOBV.SelectSingleNode("OnlineGamesTimeC").InnerText);
            OBV_OnlineGamesTimeD = int.Parse(nOBV.SelectSingleNode("OnlineGamesTimeD").InnerText);
            OBV_OnlineGamesTimeE = int.Parse(nOBV.SelectSingleNode("OnlineGamesTimeE").InnerText);
            OBV_OnlineGamesTimeF = int.Parse(nOBV.SelectSingleNode("OnlineGamesTimeF").InnerText);
        }

        /// <summary>
        /// 更新周六和周日上网的时间
        /// </summary>
        /// <param name="iSelected"></param>
        public static void UpdateBehaviorFeature_ONB_Weekend(RadioSelectionCategory iSelected)
        {
            ONB_Weekend3 = 0;
            ONB_Weekend3_4 = 0;
            ONB_Weekend4_6 = 0;
            ONB_Weekend6_9 = 0;
            ONB_Weekend9_12 = 0;
            ONB_Weekend12 = 0;
            switch(iSelected)
            {
                case RadioSelectionCategory.RadioSelected_A:
                    ONB_Weekend3 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_B:
                    ONB_Weekend3_4 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_C:
                    ONB_Weekend4_6 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_D:
                    ONB_Weekend6_9 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_E:
                    ONB_Weekend9_12 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_F:
                    ONB_Weekend12 = 1;
                    break;
            }
        }

        /// <summary>
        /// 周一到周五每天上网的时间
        /// </summary>
        /// <param name="iSelected"></param>
        public static void UpdateBehaviorFeature_ONB_WorkDay(RadioSelectionCategory iSelected)
        {
            ONB_WorkDay2 = 0;
            ONB_WorkDay2_6 = 0;
            ONB_WorkDay6_10 = 0;
            ONB_WorkDay10_12 = 0;
            ONB_WorkDay12 = 0;
            switch (iSelected)
            {
                case RadioSelectionCategory.RadioSelected_A:
                    ONB_WorkDay2 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_B:
                    ONB_WorkDay2_6 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_C:
                    ONB_WorkDay6_10 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_D:
                    ONB_WorkDay10_12 = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_E:
                    ONB_WorkDay12 = 1;
                    break;
            }
        }

        /// <summary>
        /// 网页的平均停留时间
        /// </summary>
        /// <param name="iSelected"></param>
        public static void UpdateBehaviorFeature_ONB_StaySecond(RadioSelectionCategory iSelected)
        {
            ONB_StaySecondA = 0;
            ONB_StaySecondB = 0;
            ONB_StaySecondC = 0;
            ONB_StaySecondD = 0;
            ONB_StaySecondE = 0;
            ONB_StaySecondF = 0;
            switch (iSelected)
            {
                case RadioSelectionCategory.RadioSelected_A:
                    ONB_StaySecondA = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_B:
                    ONB_StaySecondB = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_C:
                    ONB_StaySecondC = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_D:
                    ONB_StaySecondD = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_E:
                    ONB_StaySecondE = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_F:
                    ONB_StaySecondF = 1;
                    break;
            }
        }

        /// <summary>
        /// 网络浏览时段
        /// </summary>
        public static void UpdateBehaviorFeature_ONB_StartTime(RadioSelectionCategory iSelected)
        {
            ONB_StartTimeA = 0;
            ONB_StartTimeB = 0;
            ONB_StartTimeC = 0;
            ONB_StartTimeD = 0;
            ONB_StartTimeE = 0;
            ONB_StartTimeF = 0;
            ONB_StartTimeG = 0;
            switch (iSelected)
            {
                case RadioSelectionCategory.RadioSelected_A:
                    ONB_StartTimeA = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_B:
                    ONB_StartTimeB = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_C:
                    ONB_StartTimeC = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_D:
                    ONB_StartTimeD = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_E:
                    ONB_StartTimeE = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_F:
                    ONB_StartTimeF = 1;
                    break;
                case RadioSelectionCategory.RadioSelected_G:
                    ONB_StartTimeG = 1;
                    break;
            }
        }

        /// <summary>
        /// 网页的情感分类
        /// </summary>
        /// <param name="emotion"></param>
        /// <param name="iSelected"></param>
        public static void UpdateBehaviorFeature_PAN_EmotionClassify(EmotionClassify emotion,RadioSelectionCategory iSelected)
        {
            switch(emotion)
            {
                case EmotionClassify.AngerClassify:
                    PAN_Anger = (int)iSelected;
                    break;
                case EmotionClassify.BoredClassify:
                    break;
                case EmotionClassify.HappyClassify:
                    PAN_Happy = (int)iSelected;
                    break;
                case EmotionClassify.PeculiarClassify:
                    PAN_Peculiar = (int)iSelected;
                    break;
                case EmotionClassify.SadnessClassify:
                    PAN_Sadness = (int)iSelected;
                    break;
            }
        }


        public static void UpdateBehaviorFeature_PTN_ContentClassify(ContentClassify content, RadioSelectionCategory iSelected)
        {
            switch(content)
            {
                case ContentClassify.AutomotiveClassify://对应 ： 购物
                    PTN_Shopping = (int)iSelected;
                    break;
                case ContentClassify.CultureClassify://对应 ： 社会
                    PTN_Social = (int)iSelected;
                    break;
                case ContentClassify.EconomicsClassify://对应 ： 新闻
                    PTN_News = (int)iSelected;
                    break;
                case ContentClassify.EducationClassify:// 对应 ：学术研究资料
                    PTN_Research = (int)iSelected;
                    break;
                case ContentClassify.HealthClassify:// 对应 ：健康
                    PTN_Health = (int)iSelected;
                    break;
                case ContentClassify.ITClassify:// 对应 ：计算机
                    PTN_IT = (int)iSelected;
                    break;
                case ContentClassify.MilitaryClassify:// 对应 ： 网络游戏
                    PTN_OnlineGames = (int)iSelected;
                    break;
                case ContentClassify.RecruitmentClassify:// 对应 ：科学动态与趋势
                    PTN_Science = (int)iSelected;
                    break;
                case ContentClassify.SportsClassify://对应 ： 体育
                    PTN_Sports = (int)iSelected;
                    break;
                case ContentClassify.TravelClassify://对应 :  休闲娱乐
                    PTN_Entertainment = (int)iSelected;
                    break;
            }
        }





        /// <summary>
        /// 躯体化预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_SOM_Prediction_Value()
        {
            //PSI_Behavior_B : 参与话题小组
            //PSI_Behavior_F : 发表日志
            //PSI_Behavior_H : 访问好友页面
            //PAN_Fear : 恐惧/恐怖
            //PFN_Email : 电子邮箱
            //PFN_OnlineShopping : 网络购物拍卖
            double dSOMValue = 
                2.134 + 0.806 * PSI_Behavior_B + 0.735 * PSI_Behavior_F
                - 1.229 * PSI_Behavior_H + 0.447 * PAN_Fear - 0.789 * PFN_Email
                + 0.297 * PFN_OnlineShopping;
            return dSOMValue;
        }
        /// <summary>
        /// 抑郁预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_DEP_Prediction_Value()
        {
            //OBV_Gender:性别
            //ONB_StartTimeA:上网时段: 9点之前
            //OBV_UnhealthyWeb3_6:浏览不良网页次数：3-6次
            //ONB_WorkDay2_6:周一到周五平均上网时间：2—6小时
            //ONB_Active1_20:主动上网时间所占比例：0%
            //PAN_Sympathy:同情
            //PSI_Behavior_I:社交网络使用行为：其他
            //ONB_Weekend3_4:周六周日平均上网时间：3—4小时
            //OBV_StartBrowsingC:使用搜索引擎
            //ONB_WorkDay10_12:周一到周五平均上网时间：10—12小时
            double dDEPValue =
                25.019 + 1.53 * OBV_Gender + 2.63 * ONB_StartTimeA
                + 1.113 * OBV_UnhealthyWeb3_6 - 0.368 * ONB_WorkDay2_6
                + 1.4 * ONB_Active1_20 + 0.195 * PAN_Sympathy
                + 0.631 * PSI_Behavior_I - 0.382 * ONB_Weekend3_4
                - 0.188 * OBV_StartBrowsingC - 0.594 * ONB_WorkDay10_12;
            return dDEPValue;
        }
        /// <summary>
        /// 焦虑预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_ANX_Prediction_Value()
        {
            //PSM_Others : 信息检索手段：其它
            //PSI_Behavior_B : 参与话题小组
            //PSI_Behavior_H : 访问好友页面
            //PTN_Research : 关注学术资料：大于3小时
            //PAN_Fear : 恐惧恐怖
            //PFN_SearchEngine : 搜索引擎
            //PFN_SMS : 短信服务
            //PFN_SpanNoPurpose : 无目的漫游
            int iPTN_Research = default(int);
            if (PTN_Research == 4)
            {
                iPTN_Research = 1;
            }else
            {
                iPTN_Research = 0;
            }             
            double dANXValue = 
                1.812 + 2.375 * PSM_Others + 1.160 * PSI_Behavior_B - 1.213 * PSI_Behavior_H
                - 2.512 * iPTN_Research + 0.443 * PAN_Fear - 0.876 * PFN_SearchEngine 
                - 0.389 * PFN_SMS + 0.853 * PFN_SpanNoPurpose;
            return dANXValue;
        }
        /// <summary>
        /// 病态人格预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_PSD_Prediction_Value()
        {
            //ONB_StaySecondC : 网页平均逗留时间：30秒-1分钟
            //ONB_StaySecondF : 网页平均逗留时间 大于5分钟
            //PSM_Others : 信息检索手段：其它
            //PSI_Behavior_B : 参与话题小组
            //PAN_Warm : 温馨
            //PFN_Downloading : 软件下载
            //PFN_OnlineShopping : 网络购物拍卖
            //PFN_Classmates : 同学录
            //PFN_SpanNoPurpose : 无目的漫游
            double dPSDValue = 
                3.769 - 2.030 * ONB_StaySecondC - 2.911 * ONB_StaySecondF + 2.202 * PSM_Others 
                + 0.936 * PSI_Behavior_B - 0.416 * PAN_Warm - 0.964 * PFN_Downloading + 0.567 * PFN_OnlineShopping 
                - 0.332 * PFN_Classmates + 0.690 * PFN_SpanNoPurpose;
            return dPSDValue;
        }
        /// <summary>
        /// 疑心预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_HYP_Prediction_Value()
        {
            //OBV_UnhealthyWeb2 : 浏览不良信息网站次数：2次
            //PSI_Behavior_B : 参与话题小组
            //PAN_Anger : 愤怒暴力
            //PAN_Sympathy : 同情
            //PFN_SearchEngine : 搜索引擎
            //PFN_Classmates : 同学录
            //PFN_SpanNoPurpose : 无目的漫游
            double dHYPValue = 
                1.994 + 1.954 * OBV_UnhealthyWeb2 + 1.255 * PSI_Behavior_B + 0.879 * PAN_Anger 
                - 0.549 * PAN_Sympathy - 0.596 * PFN_SearchEngine - 0.481 * PFN_Classmates + 0.390 * PFN_SpanNoPurpose;
            return dHYPValue;
        }
        /// <summary>
        /// 脱离现实预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_UNR_Prediction_Value()
        {
            //ONB_SocialNetworkingB : 社交网络日均使用时间：0.5—1小时
            //ONB_SocialNetworkingC : 社交网络日均使用时间：1-2小时
            //PSI_Behavior_H : 访问好友页面
            //ONB_Active1_20 : 主动网络使用时间比例：1%-20%
            //ONB_Active41_60 : 主动网络行为所占时间比例：41%-60%
            //ONB_Active61_80 : 主动网络行为所占时间比例：61%-80%
            //ONB_Active81_100 : 主动网络行为所占时间比例：81%-100%
            //PAN_Anger : 愤怒暴力
            //PFN_BrowseNews : 浏览新闻
            //PFN_SearchEngine : 搜索引擎
            //PFN_SpanNoPurpose : 无目的漫游
            double dUNRValue =
                4.181 + 1.422 * ONB_SocialNetworkingB + 1.370 * ONB_SocialNetworkingC - 1.409 * PSI_Behavior_H
                - 4.452 * ONB_Active1_20 - 4.667 * ONB_Active41_60 - 4.627 * ONB_Active61_80
                - 6.761 * ONB_Active81_100 + 1.230 * PAN_Anger - 0.517 * PFN_BrowseNews - 0.834 * PFN_SearchEngine + 0.956 * PFN_SpanNoPurpose;
            return dUNRValue;
        }
        /// <summary>
        /// 兴奋状态预测结果
        /// </summary>
        /// <returns></returns>
        public static double Calc_HMA_Prediction_Value()
        {
            //OBV_Age : 年龄
            //PSI_IMFriends50_100 : 即时通讯软件好友数量：50—100个
            //PSI_IMFriends100_200 : 即时通讯软件好友数量：100-200个
            //PSI_ContactFriends5_10 : 日均联络网友人数：5-10个
            //PTN_News : 新闻：2-3小时
            //PFN_Email : 电子邮箱
            //PFN_Ezine : 电子杂志
            //PFN_Multimedia : 多媒体娱乐
            //PFN_SpanNoPurpose : 无目的漫游
            int iPTN_News = default(int);
            if (PTN_News == 3)
            {
                iPTN_News = 1;
            }else
            {
                iPTN_News = 0;
            }
            double dHMAValue = 
                12.741 - 0.484 * OBV_Age - 1.813 * PSI_IMFriends50_100 - 1.919 * PSI_IMFriends100_200 
                    + 1.944 * PSI_ContactFriends5_10 + 1.829 * iPTN_News - 0.590 * PFN_Email 
                    + 0.646 * PFN_Ezine - 0.392 * PFN_Multimedia + 0.452 * PFN_SpanNoPurpose;
            return dHMAValue;
        }



        public static void SaveBehaviorFeatureToXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("BehaviorFeaturesHistory.xml");

            //XmlNode xmlNode = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            //xmlDoc.AppendChild(xmlNode);

            //XmlNode xmlRoot = xmlDoc.CreateElement("EmotionTracking");
            //xmlDoc.AppendChild(xmlRoot);
            XmlNode xmlRecord = xmlDoc.CreateElement("Record");
            XmlAttribute timeAttri = xmlDoc.CreateAttribute("Time");
            timeAttri.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            xmlRecord.Attributes.Append(timeAttri);

            #region   PSI 
            XmlNode PSI_Node = xmlDoc.CreateElement("PSI");
            XmlNode Behavior_A_Node = xmlDoc.CreateElement("Behavior_A");
            Behavior_A_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_A.ToString()));
            XmlNode Behavior_B_Node = xmlDoc.CreateElement("Behavior_B");
            Behavior_B_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_B.ToString()));
            XmlNode Behavior_C_Node = xmlDoc.CreateElement("Behavior_C");
            Behavior_C_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_C.ToString()));
            XmlNode Behavior_D_Node = xmlDoc.CreateElement("Behavior_D");
            Behavior_D_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_D.ToString()));
            XmlNode Behavior_E_Node = xmlDoc.CreateElement("Behavior_E");
            Behavior_E_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_E.ToString()));
            XmlNode Behavior_F_Node = xmlDoc.CreateElement("Behavior_F");
            Behavior_F_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_F.ToString()));
            XmlNode Behavior_G_Node = xmlDoc.CreateElement("Behavior_G");
            Behavior_G_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_G.ToString()));
            XmlNode Behavior_H_Node = xmlDoc.CreateElement("Behavior_H");
            Behavior_H_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_H.ToString()));
            XmlNode Behavior_I_Node = xmlDoc.CreateElement("Behavior_I");
            Behavior_I_Node.AppendChild(xmlDoc.CreateTextNode(PSI_Behavior_I.ToString()));
            XmlNode IMActiveTimeA_Node = xmlDoc.CreateElement("IMActiveTimeA");
            IMActiveTimeA_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMActiveTimeA.ToString()));
            XmlNode IMActiveTimeB_Node = xmlDoc.CreateElement("IMActiveTimeB");
            IMActiveTimeB_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMActiveTimeB.ToString()));
            XmlNode IMActiveTimeC_Node = xmlDoc.CreateElement("IMActiveTimeC");
            IMActiveTimeC_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMActiveTimeC.ToString()));
            XmlNode IMActiveTimeD_Node = xmlDoc.CreateElement("IMActiveTimeD");
            IMActiveTimeD_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMActiveTimeD.ToString()));
            XmlNode IMActiveTimeE_Node = xmlDoc.CreateElement("IMActiveTimeE");
            IMActiveTimeE_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMActiveTimeE.ToString()));
            XmlNode IMFriends10_Node = xmlDoc.CreateElement("IMFriends10");
            IMFriends10_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMFriends10.ToString()));
            XmlNode IMFriends10_50_Node = xmlDoc.CreateElement("IMFriends10_50");
            IMFriends10_50_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMFriends10_50.ToString()));
            XmlNode IMFriends50_100_Node = xmlDoc.CreateElement("IMFriends50_100");
            IMFriends50_100_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMFriends50_100.ToString()));
            XmlNode IMFriends100_200_Node = xmlDoc.CreateElement("IMFriends100_200");
            IMFriends100_200_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMFriends100_200.ToString()));
            XmlNode IMFriends200_Node = xmlDoc.CreateElement("IMFriends200");
            IMFriends200_Node.AppendChild(xmlDoc.CreateTextNode(PSI_IMFriends200.ToString()));
            XmlNode ContactFriends2_Node = xmlDoc.CreateElement("ContactFriends2");
            ContactFriends2_Node.AppendChild(xmlDoc.CreateTextNode(PSI_ContactFriends2.ToString()));
            XmlNode ContactFriends2_5_Node = xmlDoc.CreateElement("ContactFriends2_5");
            ContactFriends2_5_Node.AppendChild(xmlDoc.CreateTextNode(PSI_ContactFriends2_5.ToString()));
            XmlNode ContactFriends5_10_Node = xmlDoc.CreateElement("ContactFriends5_10");
            ContactFriends5_10_Node.AppendChild(xmlDoc.CreateTextNode(PSI_ContactFriends5_10.ToString()));
            XmlNode ContactFriends10_20_Node = xmlDoc.CreateElement("ContactFriends10_20");
            ContactFriends10_20_Node.AppendChild(xmlDoc.CreateTextNode(PSI_ContactFriends10_20.ToString()));
            XmlNode ContactFriends20_Node = xmlDoc.CreateElement("ContactFriends20");
            ContactFriends20_Node.AppendChild(xmlDoc.CreateTextNode(PSI_ContactFriends20.ToString()));
            PSI_Node.AppendChild(Behavior_A_Node);
            PSI_Node.AppendChild(Behavior_B_Node);
            PSI_Node.AppendChild(Behavior_C_Node);
            PSI_Node.AppendChild(Behavior_D_Node);
            PSI_Node.AppendChild(Behavior_E_Node);
            PSI_Node.AppendChild(Behavior_F_Node);
            PSI_Node.AppendChild(Behavior_G_Node);
            PSI_Node.AppendChild(Behavior_H_Node);
            PSI_Node.AppendChild(Behavior_I_Node);
            PSI_Node.AppendChild(IMActiveTimeA_Node);
            PSI_Node.AppendChild(IMActiveTimeB_Node);
            PSI_Node.AppendChild(IMActiveTimeC_Node);
            PSI_Node.AppendChild(IMActiveTimeD_Node);
            PSI_Node.AppendChild(IMActiveTimeE_Node);
            PSI_Node.AppendChild(IMFriends10_Node);
            PSI_Node.AppendChild(IMFriends10_50_Node);
            PSI_Node.AppendChild(IMFriends50_100_Node);
            PSI_Node.AppendChild(IMFriends100_200_Node);
            PSI_Node.AppendChild(IMFriends200_Node);
            PSI_Node.AppendChild(ContactFriends2_Node);
            PSI_Node.AppendChild(ContactFriends2_5_Node);
            PSI_Node.AppendChild(ContactFriends5_10_Node);
            PSI_Node.AppendChild(ContactFriends10_20_Node);
            PSI_Node.AppendChild(ContactFriends20_Node);

            #endregion

            #region  PAN 
            XmlNode PAN_Node = xmlDoc.CreateElement("PAN");
            XmlNode Happy_Node = xmlDoc.CreateElement("Happy");
            Happy_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Happy.ToString()));
            XmlNode Anger_Node = xmlDoc.CreateElement("Anger");
            Anger_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Anger.ToString()));
            XmlNode Sadness_Node = xmlDoc.CreateElement("Sadness");
            Sadness_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Sadness.ToString()));
            XmlNode Fear_Node = xmlDoc.CreateElement("Fear");
            Fear_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Fear.ToString()));
            XmlNode Peculiar_Node = xmlDoc.CreateElement("Peculiar");
            Peculiar_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Peculiar.ToString()));
            XmlNode Moving_Node = xmlDoc.CreateElement("Moving");
            Moving_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Moving.ToString()));
            XmlNode Warm_Node = xmlDoc.CreateElement("Warm");
            Warm_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Warm.ToString()));
            XmlNode Sympathy_Node = xmlDoc.CreateElement("Sympathy");
            Sympathy_Node.AppendChild(xmlDoc.CreateTextNode(PAN_Sympathy.ToString()));
            PAN_Node.AppendChild(Happy_Node);
            PAN_Node.AppendChild(Anger_Node);
            PAN_Node.AppendChild(Sadness_Node);
            PAN_Node.AppendChild(Fear_Node);
            PAN_Node.AppendChild(Peculiar_Node);
            PAN_Node.AppendChild(Moving_Node);
            PAN_Node.AppendChild(Warm_Node);
            PAN_Node.AppendChild(Sympathy_Node);
            #endregion

            #region  PFN 
            XmlNode PFN_Node = xmlDoc.CreateElement("PFN");
            XmlNode Email_Node = xmlDoc.CreateElement("Email");
            Email_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Email.ToString()));
            XmlNode BrowseNews_Node = xmlDoc.CreateElement("BrowseNews");
            BrowseNews_Node.AppendChild(xmlDoc.CreateTextNode(PFN_BrowseNews.ToString()));
            XmlNode SearchEngine_Node = xmlDoc.CreateElement("SearchEngine");
            SearchEngine_Node.AppendChild(xmlDoc.CreateTextNode(PFN_SearchEngine.ToString()));
            XmlNode Downloading_Node = xmlDoc.CreateElement("Downloading");
            Downloading_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Downloading.ToString()));
            XmlNode BrowseWebSite_Node = xmlDoc.CreateElement("BrowseWebSite");
            BrowseWebSite_Node.AppendChild(xmlDoc.CreateTextNode(PFN_BrowseWebSite.ToString()));
            XmlNode Chatting_Node = xmlDoc.CreateElement("Chatting");
            Chatting_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Chatting.ToString()));
            XmlNode BBSForum_Node = xmlDoc.CreateElement("BBSForum");
            BBSForum_Node.AppendChild(xmlDoc.CreateTextNode(PFN_BBSForum.ToString()));
            XmlNode HomeSpace_Node = xmlDoc.CreateElement("HomeSpace");
            HomeSpace_Node.AppendChild(xmlDoc.CreateTextNode(PFN_HomeSpace.ToString()));
            XmlNode OnlineGames_Node = xmlDoc.CreateElement("OnlineGames");
            OnlineGames_Node.AppendChild(xmlDoc.CreateTextNode(PFN_OnlineGames.ToString()));
            XmlNode OnlineShopping_Node = xmlDoc.CreateElement("OnlineShopping");
            OnlineShopping_Node.AppendChild(xmlDoc.CreateTextNode(PFN_OnlineShopping.ToString()));
            XmlNode OnlineEducation_Node = xmlDoc.CreateElement("OnlineEducation");
            OnlineEducation_Node.AppendChild(xmlDoc.CreateTextNode(PFN_OnlineEducation.ToString()));
            XmlNode SMS_Node = xmlDoc.CreateElement("SMS");
            SMS_Node.AppendChild(xmlDoc.CreateTextNode(PFN_SMS.ToString()));
            XmlNode Ezine_Node = xmlDoc.CreateElement("E-zine");
            Ezine_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Ezine.ToString()));
            XmlNode Booking_Node = xmlDoc.CreateElement("Booking");
            Booking_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Booking.ToString()));
            XmlNode Multimedia_Node = xmlDoc.CreateElement("Multimedia");
            Multimedia_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Multimedia.ToString()));
            XmlNode Recruitment_Node = xmlDoc.CreateElement("Recruitment");
            Ezine_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Ezine.ToString()));
            XmlNode Classmates_Node = xmlDoc.CreateElement("Classmates");
            Classmates_Node.AppendChild(xmlDoc.CreateTextNode(PFN_Classmates.ToString()));
            XmlNode SpanNoPurpose_Node = xmlDoc.CreateElement("SpanNoPurpose");
            SpanNoPurpose_Node.AppendChild(xmlDoc.CreateTextNode(PFN_SpanNoPurpose.ToString()));

            PFN_Node.AppendChild(Email_Node);
            PFN_Node.AppendChild(BrowseNews_Node);
            PFN_Node.AppendChild(SearchEngine_Node);
            PFN_Node.AppendChild(Downloading_Node);
            PFN_Node.AppendChild(BrowseWebSite_Node);
            PFN_Node.AppendChild(Chatting_Node);
            PFN_Node.AppendChild(BBSForum_Node);
            PFN_Node.AppendChild(HomeSpace_Node);
            PFN_Node.AppendChild(OnlineGames_Node);
            PFN_Node.AppendChild(OnlineShopping_Node);
            PFN_Node.AppendChild(OnlineEducation_Node);
            PFN_Node.AppendChild(SMS_Node);
            PFN_Node.AppendChild(Ezine_Node);
            PFN_Node.AppendChild(Booking_Node);
            PFN_Node.AppendChild(Multimedia_Node);
            PFN_Node.AppendChild(Recruitment_Node);
            PFN_Node.AppendChild(Classmates_Node);
            PFN_Node.AppendChild(SpanNoPurpose_Node);

            #endregion

            #region   PSM 

            XmlNode PSM_Node = xmlDoc.CreateElement("PSM");
            XmlNode SingleKeyword_Node = xmlDoc.CreateElement("SingleKeyword");
            SingleKeyword_Node.AppendChild(xmlDoc.CreateTextNode(PSM_SingleKeyword.ToString()));
            XmlNode MultipleKeywords_Node = xmlDoc.CreateElement("MultipleKeywords");
            MultipleKeywords_Node.AppendChild(xmlDoc.CreateTextNode(PSM_MultipleKeywords.ToString()));
            XmlNode Advanced_Node = xmlDoc.CreateElement("Advanced");
            Advanced_Node.AppendChild(xmlDoc.CreateTextNode(PSM_Advanced.ToString()));
            XmlNode CategorySearch_Node = xmlDoc.CreateElement("CategorySearch");
            CategorySearch_Node.AppendChild(xmlDoc.CreateTextNode(PSM_CategorySearch.ToString()));
            XmlNode Others_Node = xmlDoc.CreateElement("Others");
            Others_Node.AppendChild(xmlDoc.CreateTextNode(PSM_Others.ToString()));

            PSM_Node.AppendChild(SingleKeyword_Node);
            PSM_Node.AppendChild(MultipleKeywords_Node);
            PSM_Node.AppendChild(Advanced_Node);
            PSM_Node.AppendChild(CategorySearch_Node);
            PSM_Node.AppendChild(Others_Node);

            #endregion

            #region   PTN 

            XmlNode PTN_Node = xmlDoc.CreateElement("PTN");
            XmlNode IT_Node = xmlDoc.CreateElement("IT");
            IT_Node.AppendChild(xmlDoc.CreateTextNode(PTN_IT.ToString()));
            XmlNode PTNOnlineGames_Node = xmlDoc.CreateElement("OnlineGames");
            PTNOnlineGames_Node.AppendChild(xmlDoc.CreateTextNode(PTN_OnlineGames.ToString()));
            XmlNode Health_Node = xmlDoc.CreateElement("Health");
            Health_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Health.ToString()));
            XmlNode News_Node = xmlDoc.CreateElement("News");
            News_Node.AppendChild(xmlDoc.CreateTextNode(PTN_News.ToString()));
            XmlNode Entertainment_Node = xmlDoc.CreateElement("Entertainment");
            Entertainment_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Entertainment.ToString()));
            XmlNode Research_Node = xmlDoc.CreateElement("Research");
            Research_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Research.ToString()));
            XmlNode Science_Node = xmlDoc.CreateElement("Science");
            Science_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Science.ToString()));
            XmlNode Shopping_Node = xmlDoc.CreateElement("Shopping");
            Shopping_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Shopping.ToString()));
            XmlNode Social_Node = xmlDoc.CreateElement("Social");
            Social_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Social.ToString()));
            XmlNode Sports_Node = xmlDoc.CreateElement("Sports");
            Sports_Node.AppendChild(xmlDoc.CreateTextNode(PTN_Sports.ToString()));

            PTN_Node.AppendChild(IT_Node);
            PTN_Node.AppendChild(PTNOnlineGames_Node);
            PTN_Node.AppendChild(Health_Node);
            PTN_Node.AppendChild(News_Node);
            PTN_Node.AppendChild(Entertainment_Node);
            PTN_Node.AppendChild(Research_Node);
            PTN_Node.AppendChild(Science_Node);
            PTN_Node.AppendChild(Shopping_Node);
            PTN_Node.AppendChild(Social_Node);
            PTN_Node.AppendChild(Sports_Node);

            #endregion

            #region  ONB 

            XmlNode ONB_Node = xmlDoc.CreateElement("ONB");
            XmlNode Weekend3_Node = xmlDoc.CreateElement("Weekend3");
            Weekend3_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Weekend3.ToString()));
            XmlNode Weekend3_4_Node = xmlDoc.CreateElement("Weekend3_4");
            Weekend3_4_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Weekend3_4.ToString()));
            XmlNode Weekend4_6_Node = xmlDoc.CreateElement("Weekend4_6");
            Weekend4_6_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Weekend4_6.ToString()));
            XmlNode Weekend6_9_Node = xmlDoc.CreateElement("Weekend6_9");
            Weekend6_9_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Weekend6_9.ToString()));
            XmlNode Weekend9_12_Node = xmlDoc.CreateElement("Weekend9_12");
            Weekend9_12_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Weekend9_12.ToString()));
            XmlNode Weekend12_Node = xmlDoc.CreateElement("Weekend12");
            Weekend12_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Weekend12.ToString()));

            XmlNode WorkDay2_Node = xmlDoc.CreateElement("WorkDay2");
            WorkDay2_Node.AppendChild(xmlDoc.CreateTextNode(ONB_WorkDay2.ToString()));
            XmlNode WorkDay2_6_Node = xmlDoc.CreateElement("WorkDay2_6");
            WorkDay2_6_Node.AppendChild(xmlDoc.CreateTextNode(ONB_WorkDay2_6.ToString()));
            XmlNode WorkDay6_10_Node = xmlDoc.CreateElement("WorkDay6_10");
            WorkDay6_10_Node.AppendChild(xmlDoc.CreateTextNode(ONB_WorkDay6_10.ToString()));
            XmlNode WorkDay10_12_Node = xmlDoc.CreateElement("WorkDay10_12");
            WorkDay10_12_Node.AppendChild(xmlDoc.CreateTextNode(ONB_WorkDay10_12.ToString()));
            XmlNode WorkDay12_Node = xmlDoc.CreateElement("WorkDay12");
            WorkDay12_Node.AppendChild(xmlDoc.CreateTextNode(ONB_WorkDay12.ToString()));

            XmlNode SocialNetworkingA_Node = xmlDoc.CreateElement("SocialNetworkingA");
            SocialNetworkingA_Node.AppendChild(xmlDoc.CreateTextNode(ONB_SocialNetworkingA.ToString()));
            XmlNode SocialNetworkingB_Node = xmlDoc.CreateElement("SocialNetworkingB");
            SocialNetworkingB_Node.AppendChild(xmlDoc.CreateTextNode(ONB_SocialNetworkingB.ToString()));
            XmlNode SocialNetworkingC_Node = xmlDoc.CreateElement("SocialNetworkingC");
            SocialNetworkingC_Node.AppendChild(xmlDoc.CreateTextNode(ONB_SocialNetworkingC.ToString()));
            XmlNode SocialNetworkingD_Node = xmlDoc.CreateElement("SocialNetworkingD");
            SocialNetworkingD_Node.AppendChild(xmlDoc.CreateTextNode(ONB_SocialNetworkingD.ToString()));
            XmlNode SocialNetworkingE_Node = xmlDoc.CreateElement("SocialNetworkingE");
            SocialNetworkingE_Node.AppendChild(xmlDoc.CreateTextNode(ONB_SocialNetworkingE.ToString()));

            XmlNode StartTimeA_Node = xmlDoc.CreateElement("StartTimeA");
            StartTimeA_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeA.ToString()));
            XmlNode StartTimeB_Node = xmlDoc.CreateElement("StartTimeB");
            StartTimeB_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeB.ToString()));
            XmlNode StartTimeC_Node = xmlDoc.CreateElement("StartTimeC");
            StartTimeC_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeC.ToString()));
            XmlNode StartTimeD_Node = xmlDoc.CreateElement("StartTimeD");
            StartTimeD_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeD.ToString()));
            XmlNode StartTimeE_Node = xmlDoc.CreateElement("StartTimeE");
            StartTimeE_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeE.ToString()));
            XmlNode StartTimeF_Node = xmlDoc.CreateElement("StartTimeF");
            StartTimeF_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeF.ToString()));
            XmlNode StartTimeG_Node = xmlDoc.CreateElement("StartTimeG");
            StartTimeG_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StartTimeG.ToString()));

            XmlNode StaySecondA_Node = xmlDoc.CreateElement("StaySecondA");
            StaySecondA_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StaySecondA.ToString()));
            XmlNode StaySecondB_Node = xmlDoc.CreateElement("StaySecondB");
            StaySecondB_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StaySecondB.ToString()));
            XmlNode StaySecondC_Node = xmlDoc.CreateElement("StaySecondC");
            StaySecondC_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StaySecondC.ToString()));
            XmlNode StaySecondD_Node = xmlDoc.CreateElement("StaySecondD");
            StaySecondD_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StaySecondD.ToString()));
            XmlNode StaySecondE_Node = xmlDoc.CreateElement("StaySecondE");
            StaySecondE_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StaySecondE.ToString()));
            XmlNode StaySecondF_Node = xmlDoc.CreateElement("StaySecondF");
            StaySecondF_Node.AppendChild(xmlDoc.CreateTextNode(ONB_StaySecondF.ToString()));

            XmlNode Active1_20_Node = xmlDoc.CreateElement("Active1_20");
            Active1_20_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Active1_20.ToString()));
            XmlNode Active21_40_Node = xmlDoc.CreateElement("Active21_40");
            Active21_40_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Active21_40.ToString()));
            XmlNode Active41_60_Node = xmlDoc.CreateElement("Active41_60");
            Active41_60_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Active41_60.ToString()));
            XmlNode Active61_80_Node = xmlDoc.CreateElement("Active61_80");
            Active61_80_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Active61_80.ToString()));
            XmlNode Active81_100_Node = xmlDoc.CreateElement("Active81_100");
            Active81_100_Node.AppendChild(xmlDoc.CreateTextNode(ONB_Active81_100.ToString()));

            ONB_Node.AppendChild(Weekend3_Node);
            ONB_Node.AppendChild(Weekend3_4_Node);
            ONB_Node.AppendChild(Weekend4_6_Node);
            ONB_Node.AppendChild(Weekend6_9_Node);
            ONB_Node.AppendChild(Weekend9_12_Node);
            ONB_Node.AppendChild(Weekend12_Node);
            ONB_Node.AppendChild(WorkDay2_Node);
            ONB_Node.AppendChild(WorkDay2_6_Node);
            ONB_Node.AppendChild(WorkDay6_10_Node);
            ONB_Node.AppendChild(WorkDay10_12_Node);
            ONB_Node.AppendChild(WorkDay12_Node);
            ONB_Node.AppendChild(SocialNetworkingA_Node);
            ONB_Node.AppendChild(SocialNetworkingB_Node);
            ONB_Node.AppendChild(SocialNetworkingC_Node);
            ONB_Node.AppendChild(SocialNetworkingD_Node);
            ONB_Node.AppendChild(SocialNetworkingE_Node);
            ONB_Node.AppendChild(StartTimeA_Node);
            ONB_Node.AppendChild(StartTimeB_Node);
            ONB_Node.AppendChild(StartTimeC_Node);
            ONB_Node.AppendChild(StartTimeD_Node);
            ONB_Node.AppendChild(StartTimeE_Node);
            ONB_Node.AppendChild(StartTimeF_Node);
            ONB_Node.AppendChild(StartTimeG_Node);
            ONB_Node.AppendChild(StaySecondA_Node);
            ONB_Node.AppendChild(StaySecondB_Node);
            ONB_Node.AppendChild(StaySecondC_Node);
            ONB_Node.AppendChild(StaySecondD_Node);
            ONB_Node.AppendChild(StaySecondE_Node);
            ONB_Node.AppendChild(StaySecondF_Node);
            ONB_Node.AppendChild(Active1_20_Node);
            ONB_Node.AppendChild(Active21_40_Node);
            ONB_Node.AppendChild(Active41_60_Node);
            ONB_Node.AppendChild(Active61_80_Node);
            ONB_Node.AppendChild(Active81_100_Node);

            #endregion

            #region  OBV

            XmlNode OBV_Node = xmlDoc.CreateElement("OBV"); 

            XmlNode Gender_Node = xmlDoc.CreateElement("Gender");
            Gender_Node.AppendChild(xmlDoc.CreateTextNode(OBV_Gender.ToString()));
            XmlNode Age_Node = xmlDoc.CreateElement("Age");
            Age_Node.AppendChild(xmlDoc.CreateTextNode(OBV_Age.ToString()));
            XmlNode StartBrowsingA_Node = xmlDoc.CreateElement("StartBrowsingA");
            StartBrowsingA_Node.AppendChild(xmlDoc.CreateTextNode(OBV_StartBrowsingA.ToString()));
            XmlNode StartBrowsingB_Node = xmlDoc.CreateElement("StartBrowsingB");
            StartBrowsingB_Node.AppendChild(xmlDoc.CreateTextNode(OBV_StartBrowsingB.ToString()));
            XmlNode StartBrowsingC_Node = xmlDoc.CreateElement("StartBrowsingC");
            StartBrowsingC_Node.AppendChild(xmlDoc.CreateTextNode(OBV_StartBrowsingC.ToString()));
            XmlNode StartBrowsingD_Node = xmlDoc.CreateElement("StartBrowsingD");
            StartBrowsingD_Node.AppendChild(xmlDoc.CreateTextNode(OBV_StartBrowsingD.ToString()));

            XmlNode UnhealthyWeb0_Node = xmlDoc.CreateElement("UnhealthyWeb0");
            UnhealthyWeb0_Node.AppendChild(xmlDoc.CreateTextNode(OBV_UnhealthyWeb0.ToString()));
            XmlNode UnhealthyWeb1_Node = xmlDoc.CreateElement("UnhealthyWeb1");
            UnhealthyWeb1_Node.AppendChild(xmlDoc.CreateTextNode(OBV_UnhealthyWeb1.ToString()));
            XmlNode UnhealthyWeb2_Node = xmlDoc.CreateElement("UnhealthyWeb2");
            UnhealthyWeb2_Node.AppendChild(xmlDoc.CreateTextNode(OBV_UnhealthyWeb2.ToString()));
            XmlNode UnhealthyWeb3_6_Node = xmlDoc.CreateElement("UnhealthyWeb3_6");
            UnhealthyWeb3_6_Node.AppendChild(xmlDoc.CreateTextNode(OBV_UnhealthyWeb3_6.ToString()));
            XmlNode UnhealthyWeb6_Node = xmlDoc.CreateElement("UnhealthyWeb6");
            UnhealthyWeb6_Node.AppendChild(xmlDoc.CreateTextNode(OBV_UnhealthyWeb6.ToString()));

            XmlNode SearchCount5_Node = xmlDoc.CreateElement("SearchCount5");
            SearchCount5_Node.AppendChild(xmlDoc.CreateTextNode(OBV_SearchCount5.ToString()));
            XmlNode SearchCount5_10_Node = xmlDoc.CreateElement("SearchCount5_10");
            SearchCount5_10_Node.AppendChild(xmlDoc.CreateTextNode(OBV_SearchCount5_10.ToString()));
            XmlNode SearchCount10_20_Node = xmlDoc.CreateElement("SearchCount10_20");
            SearchCount10_20_Node.AppendChild(xmlDoc.CreateTextNode(OBV_SearchCount10_20.ToString()));
            XmlNode SearchCount20_30_Node = xmlDoc.CreateElement("SearchCount20_30");
            SearchCount20_30_Node.AppendChild(xmlDoc.CreateTextNode(OBV_SearchCount20_30.ToString()));
            XmlNode SearchCount30_50_Node = xmlDoc.CreateElement("SearchCount30_50");
            SearchCount30_50_Node.AppendChild(xmlDoc.CreateTextNode(OBV_SearchCount30_50.ToString()));
            XmlNode SearchCount50_Node = xmlDoc.CreateElement("SearchCount50");
            SearchCount50_Node.AppendChild(xmlDoc.CreateTextNode(OBV_SearchCount50.ToString()));

            XmlNode OnlineGamesTimeA_Node = xmlDoc.CreateElement("OnlineGamesTimeA");
            OnlineGamesTimeA_Node.AppendChild(xmlDoc.CreateTextNode(OBV_OnlineGamesTimeA.ToString()));
            XmlNode OnlineGamesTimeB_Node = xmlDoc.CreateElement("OnlineGamesTimeB");
            OnlineGamesTimeB_Node.AppendChild(xmlDoc.CreateTextNode(OBV_OnlineGamesTimeB.ToString()));
            XmlNode OnlineGamesTimeC_Node = xmlDoc.CreateElement("OnlineGamesTimeC");
            OnlineGamesTimeC_Node.AppendChild(xmlDoc.CreateTextNode(OBV_OnlineGamesTimeC.ToString()));
            XmlNode OnlineGamesTimeD_Node = xmlDoc.CreateElement("OnlineGamesTimeD");
            OnlineGamesTimeD_Node.AppendChild(xmlDoc.CreateTextNode(OBV_OnlineGamesTimeD.ToString()));
            XmlNode OnlineGamesTimeE_Node = xmlDoc.CreateElement("OnlineGamesTimeE");
            OnlineGamesTimeE_Node.AppendChild(xmlDoc.CreateTextNode(OBV_OnlineGamesTimeE.ToString()));
            XmlNode OnlineGamesTimeF_Node = xmlDoc.CreateElement("OnlineGamesTimeF");
            OnlineGamesTimeF_Node.AppendChild(xmlDoc.CreateTextNode(OBV_OnlineGamesTimeF.ToString()));

            OBV_Node.AppendChild(Gender_Node);
            OBV_Node.AppendChild(Age_Node);
            OBV_Node.AppendChild(StartBrowsingA_Node);
            OBV_Node.AppendChild(StartBrowsingB_Node);
            OBV_Node.AppendChild(StartBrowsingC_Node);
            OBV_Node.AppendChild(StartBrowsingD_Node);
            OBV_Node.AppendChild(UnhealthyWeb0_Node);
            OBV_Node.AppendChild(UnhealthyWeb1_Node);
            OBV_Node.AppendChild(UnhealthyWeb2_Node);
            OBV_Node.AppendChild(UnhealthyWeb3_6_Node);
            OBV_Node.AppendChild(UnhealthyWeb6_Node);
            OBV_Node.AppendChild(SearchCount5_Node);
            OBV_Node.AppendChild(SearchCount5_10_Node);
            OBV_Node.AppendChild(SearchCount10_20_Node);
            OBV_Node.AppendChild(SearchCount20_30_Node);
            OBV_Node.AppendChild(SearchCount30_50_Node);
            OBV_Node.AppendChild(SearchCount50_Node);
            OBV_Node.AppendChild(OnlineGamesTimeA_Node);
            OBV_Node.AppendChild(OnlineGamesTimeB_Node);
            OBV_Node.AppendChild(OnlineGamesTimeC_Node);
            OBV_Node.AppendChild(OnlineGamesTimeD_Node);
            OBV_Node.AppendChild(OnlineGamesTimeE_Node);
            OBV_Node.AppendChild(OnlineGamesTimeF_Node);


            #endregion

            //将节点添加到文档中
            XmlElement root = xmlDoc.DocumentElement;
            xmlRecord.AppendChild(PSI_Node);
            xmlRecord.AppendChild(PAN_Node);
            xmlRecord.AppendChild(PFN_Node);
            xmlRecord.AppendChild(PSM_Node);
            xmlRecord.AppendChild(PTN_Node);
            xmlRecord.AppendChild(ONB_Node);
            xmlRecord.AppendChild(OBV_Node);
            root.AppendChild(xmlRecord);
            xmlDoc.Save("BehaviorFeaturesHistory.xml");

        }
    }
}
