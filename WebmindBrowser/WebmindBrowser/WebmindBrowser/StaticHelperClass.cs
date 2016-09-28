using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using System.Reflection;

namespace WebmindBrowser
{
    public class StaticHelperClass
    {
        #region Static Helper Methods

        public static string strClientIPAddress = GetClientIPAddress();

        private static string GetClientIPAddress()
        {
            IPHostEntry iHere = Dns.GetHostByName(Dns.GetHostName());
            return iHere.AddressList[0].ToString();
        }

        public static Stream GetResource(string resourceName)
        {
            Stream stream = null;
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                stream = asm.GetManifestResourceStream("WebmindBrowser.Resources." + resourceName);
            }
            catch (Exception)
            {
                stream = null;
            }
            return stream;
        }
        public static Image GetImage(string resouceName)
        {
            Image image = null;
            Stream stream = null;
            try
            {
                stream = GetResource("Images." + resouceName);
                image = Image.FromStream(stream);
            }
            catch (Exception)
            {
                image = null;
            }
            return image;
        }
        #endregion //Static Helper Methods
    }


    public enum EmotionSuggestionLevel
    {
        //躯体化
        SOM_Lower = 1,
        SOM_Middle = 2,
        SOM_Higher = 3,

        //抑郁
        DEP_Lower = 7,
        DEP_Middle = 8,
        DEP_Higher = 9,

        //焦虑
        AMX_Lower = 13,
        AMX_Middle = 14,
        AMX_Higher = 15,

        //病态人格
        PSD_Lower = 19,
        PSD_Middle = 20,
        PSD_Higher = 21,

        //疑心
        HYP_Lower = 25,
        HYP_Middle = 26,
        HYP_Higher = 27,

        //脱离现实
        UNR_Lower = 31,
        UNR_Middle = 32,
        UNR_Higher = 33,

        //兴奋状态
        HMA_Lower = 37,
        HMA_Middle = 38,
        HMA_Higher = 39,
    }


    public enum EmotionDimensions
    {
        //躯体化
        SOM = 1,
        //抑郁
        DEP = 7,
        //焦虑
        AMX = 13,
        //病态人格
        PSD = 19,
        //疑心
        HYP = 25,
        //脱离现实
        UNR = 31,
        //兴奋状态
        HMA = 37,
    }

    public enum RecommendedCategory
    {
        CompletelyRandom = 0,//完全随机推荐 0
        BehaviorScores,//行为得分推荐 1
        BehaviorChange//行为变化推荐 2
    }

    //选项枚举
    public enum RadioSelectionCategory
    {
        RadioSelected_A = 1,
        RadioSelected_B,
        RadioSelected_C,
        RadioSelected_D,
        RadioSelected_E,
        RadioSelected_F,
        RadioSelected_G,
        RadioSelected_H,
        RadioSelected_I
    }


    //网页情感类别
    public enum EmotionClassify
    {
        AngerClassify = 0,//愤怒
        HappyClassify,//搞笑
        SadnessClassify,//难过
        BoredClassify,//无聊
        PeculiarClassify//奇特
    }

    public enum ContentClassify
    {
        AutomotiveClassify = 0,//汽车
        EconomicsClassify,//财经
        ITClassify,//IT
        HealthClassify,//健康
        SportsClassify,//体育
        TravelClassify,//旅游
        EducationClassify,//教育
        RecruitmentClassify,//招聘
        CultureClassify,//文化
        MilitaryClassify//军事
    }
}
