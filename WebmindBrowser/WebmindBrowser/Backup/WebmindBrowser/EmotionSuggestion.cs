using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using WebmindBrowser.Analysis;

namespace WebmindBrowser
{
    public partial class EmotionSuggestion : Form
    {
        //This delegate enables asynchronous calls for setting the text property on a Control
        delegate void SetTextCallback(string text);
        

        private MainWindow Parent;
        private int iWidth = 300;
        private int iHeight = 180;

        public int iSuggestionID;//当前显示的建议ID
        public RecommendedCategory enumCategory;//当前的推荐类别
        public EmotionSuggestion()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Parent = null;
            this.ShowInTaskbar = false;
            UpdateEmotionSuggestionContent();
        }

        public EmotionSuggestion(MainWindow father)
        {
            this.Parent = father;
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;
        }

        /// <summary>
        /// update the data of emotion window
        /// </summary>
        public void UpdateEmotionSuggestionContent()
        {
            string strSuggestion = string.Empty;
            Random rd = new Random(unchecked((int)DateTime.Now.Ticks));
            //int iSelect = rd.Next(1, 4);//随机选择模型
            int iSelect = rd.Next(2, 4);//随机选择模型 按老师的要求，把推荐方法设置为0和1之间，2暂时不需要(即behavior变化推荐)，而random.next函数是取下不取上的
            switch (iSelect)
            {
                case 1://根据数值高低进行推荐，behaviorScore推荐
                    strSuggestion = BehaviorScoresLoadSuggestion();//这个暂时不要
                    break;
                case 2://根据变化幅度推荐,也就是传说中的行为变化推荐
                    strSuggestion = BehaviorChangeLoadSuggestion();//这个现在选上
                    break;
                case 3://完全随机模型，完全随机推荐
                    strSuggestion = CompletelyRandomLoadSuggestion();
                    break;
            }
            if (this.SuggestionContent.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { strSuggestion });
            }
            else
            {
                this.SuggestionContent.Text = strSuggestion;                
            }
        }

        /// <summary>
        /// This method is passed in to the SetTextCallBack delegate
        /// to set the Text property of textBox.
        /// </summary>
        /// <param name="text"></param>
        private void SetText(string text)
        {
            this.SuggestionContent.Text = text;
        }

        /// <summary>
        /// 完全随机模型
        /// </summary>
        /// <param name="category"></param>
        private string CompletelyRandomLoadSuggestion()
        {
            //string strSuggestion = string.Empty;
            Random rd = new Random(unchecked((int)DateTime.Now.Ticks));
            int iSelect = rd.Next(1, 43);//随机选择建议
            //return GetEmotionSuggestionByID(RecommendedCategory.CompletelyRandom, iSelect) + "\r\n <!--- 随机推荐 --->";
            return GetEmotionSuggestionByID(RecommendedCategory.CompletelyRandom, iSelect);
        }
        /// <summary>
        /// 根据数值高低进行推荐
        /// </summary>
        /// <param name="category"></param>
        private string BehaviorScoresLoadSuggestion()
        {
            double dANX = Recommendation.dic_ANX_Predictions.Values.Last();
            double dDEP = Recommendation.dic_DEP_Predictions.Values.Last();
            double dHMA = Recommendation.dic_HMA_Predictions.Values.Last();
            double dHYP = Recommendation.dic_HYP_Predictions.Values.Last();
            double dPSD = Recommendation.dic_PSD_Predictions.Values.Last();
            double dSOM = Recommendation.dic_SOM_Predictions.Values.Last();
            double dUNR = Recommendation.dic_UNR_Predictions.Values.Last();
            double dMax = dANX;
            EmotionDimensions emotion = EmotionDimensions.AMX;
            int iSuggestion = 0;//推荐建议的ID
            //if (dDEP > dMax)
            //{
            //    dMax = dDEP;
            //    emotion = EmotionDimensions.DEP;
            //}
            if (dHMA > dMax)
            {
                dMax = dHMA;
                emotion = EmotionDimensions.HMA;
            }
            if (dHYP > dMax)
            {
                dMax = dHYP;
                emotion = EmotionDimensions.HYP;
            }
            if (dPSD > dMax)
            {
                dMax = dPSD;
                emotion = EmotionDimensions.PSD;
            }
            if (dSOM > dMax)
            {
                dMax = dSOM;
                emotion = EmotionDimensions.SOM;
            }
            if (dUNR > dMax)
            {
                dMax = dUNR;
                emotion = EmotionDimensions.UNR;
            }
            /////////////////////  分三级  /////////////////////////
            if (dMax > 1.0)
            {
                iSuggestion = (int)emotion + 2;
            }else if (dMax < -1.0)
            {
                iSuggestion = (int)emotion;
            }else
            {
                iSuggestion = (int)emotion + 1;
            }
            //return GetEmotionSuggestionByID(RecommendedCategory.BehaviorScores, iSuggestion) + "\r\n <!--- 根据分值高低推荐 --->";
            return GetEmotionSuggestionByID(RecommendedCategory.BehaviorScores, iSuggestion);
        }
        private string GetEmotionSuggestionByID(RecommendedCategory category, int iID)
        {
            string strSuggestion = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            string strFileName = "SuggestionsRandom.xml";
            xmlDoc.Load(strFileName);
            XmlElement root = xmlDoc.DocumentElement;//
            XmlNodeList nlist = root.SelectNodes("Record");//获取根节点的所有子节点
            foreach (XmlNode node in nlist)
            {
                if (int.Parse(node.Attributes["ID"].InnerText.ToString()) == iID)
                {
                    strSuggestion = node.InnerText.ToString();
                }
            }
            this.iSuggestionID = iID;//
            this.enumCategory = category;//
            return strSuggestion;
        }
        /// <summary>
        /// 根据变化幅度推荐
        /// </summary>
        private string BehaviorChangeLoadSuggestion()
        {
            int iCount = Recommendation.dic_ANX_Predictions.Count;//记录数
            if (iCount == 0)
            {
                return string.Empty;
            }
            double dANX = Recommendation.dic_ANX_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_ANX_Predictions.Values.ElementAt(iCount - 2);
            double dDEP = Recommendation.dic_DEP_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_DEP_Predictions.Values.ElementAt(iCount - 2);
            double dHMA = Recommendation.dic_HMA_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_HMA_Predictions.Values.ElementAt(iCount - 2);
            double dHYP = Recommendation.dic_HYP_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_HYP_Predictions.Values.ElementAt(iCount - 2);
            double dPSD = Recommendation.dic_PSD_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_PSD_Predictions.Values.ElementAt(iCount - 2);
            double dSOM = Recommendation.dic_SOM_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_SOM_Predictions.Values.ElementAt(iCount - 2);
            double dUNR = Recommendation.dic_UNR_Predictions.Values.ElementAt(iCount - 1) -
                Recommendation.dic_UNR_Predictions.Values.ElementAt(iCount - 2);
            double dMax = dANX;
            EmotionDimensions emotion = EmotionDimensions.AMX;
            double dScore = Recommendation.dic_ANX_Predictions.Values.ElementAt(iCount - 1);
            int iSuggestion = 0;//推荐建议的ID
            if (dDEP > dMax)
            {
                dMax = dDEP;
                emotion = EmotionDimensions.DEP;
                dScore = Recommendation.dic_DEP_Predictions.Values.ElementAt(iCount - 1);
            }
            if (dHMA > dMax)
            {
                dMax = dHMA;
                emotion = EmotionDimensions.HMA;
                dScore = Recommendation.dic_HMA_Predictions.Values.ElementAt(iCount - 1);
            }
            if (dHYP > dMax)
            {
                dMax = dHYP;
                emotion = EmotionDimensions.HYP;
                dScore = Recommendation.dic_HYP_Predictions.Values.ElementAt(iCount - 1);
            }
            if (dPSD > dMax)
            {
                dMax = dPSD;
                emotion = EmotionDimensions.PSD;
                dScore = Recommendation.dic_PSD_Predictions.Values.ElementAt(iCount - 1);
            }
            if (dSOM > dMax)
            {
                dMax = dSOM;
                emotion = EmotionDimensions.SOM;
                dScore = Recommendation.dic_SOM_Predictions.Values.ElementAt(iCount - 1);
            }
            if (dUNR > dMax)
            {
                dMax = dUNR;
                emotion = EmotionDimensions.UNR;
                dScore = Recommendation.dic_UNR_Predictions.Values.ElementAt(iCount - 1);
            }
            /////////////////////  分三级  /////////////////////////
            if (dScore > 1.0)
            {
                iSuggestion = (int)emotion + 2;
            }
            else if (dScore < -1.0)
            {
                iSuggestion = (int)emotion;
            }
            else
            {
                iSuggestion = (int)emotion + 1;
            }
            //return GetEmotionSuggestionByID(RecommendedCategory.BehaviorChange, iSuggestion) + "\r\n <!--- 根据变化幅度推荐 --->";

            return GetEmotionSuggestionByID(RecommendedCategory.BehaviorChange, iSuggestion);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            thisRefresh();
            TopMost = true;
        }

        public void thisRefresh()
        {
            if (this.Parent == null)
            {
                this.Width = this.iWidth;
                this.Height = this.iHeight;
                this.Top = Screen.PrimaryScreen.Bounds.Height - this.iHeight - 20;
                this.Left = Screen.PrimaryScreen.Bounds.Width - this.iWidth - 10;
                selfRefresh();
                return;
            }
            this.Width = Parent.Width / 3;
            this.Height = Parent.Height / 3;
            this.Top = Parent.Top + 2 * Parent.Height / 3 - 35;
            this.Left = Parent.Left + 2 * Parent.Width / 3 - 20;
            selfRefresh();
        }

        public void selfRefresh()
        {
            this.panel1.Height = 2 * this.Height / 3;
            this.panel2.Top = this.panel1.Top + this.panel1.Height;
            this.panel2.Height = this.Height - this.panel1.Height;
            this.panel2.Left = this.panel1.Left;
            this.panel2.Width = this.panel1.Width;
            if (this.Width < 420)
            {
                this.radioButton1.Top = (this.panel2.Height - 32) / 3;
                this.radioButton2.Top = (this.panel2.Height - 32) / 3;
                this.radioButton3.Top = (this.panel2.Height - 32) * 2 / 3 + 16;
                this.radioButton4.Top = (this.panel2.Height - 32) * 2 / 3 + 16;
                this.radioButton1.Left = 20;
                this.radioButton2.Left = this.panel2.Width - 120;
                this.radioButton3.Left = 20;
                this.radioButton4.Left = this.panel2.Width - 120;
                return;
            }
            this.radioButton1.Top = (this.panel2.Height - 16) / 2;
            this.radioButton2.Top = (this.panel2.Height - 16) / 2;
            this.radioButton3.Top = (this.panel2.Height - 16) / 2;
            this.radioButton4.Top = (this.panel2.Height - 16) / 2;
            this.radioButton1.Left = 20;
            this.radioButton2.Left = this.radioButton1.Left + (this.panel2.Width - 140) / 3;
            this.radioButton3.Left = this.radioButton2.Left + (this.panel2.Width - 140) / 3;
            this.radioButton4.Left = this.radioButton3.Left + (this.panel2.Width - 140) / 3;
            Refresh();     
        }


        /// <summary>
        /// 估计这就是传说中的推荐系统的click事件，并将结果记录到UserEvaluation文件里
        /// CompletelyRandom = 0,//完全随机推荐 0
        ///BehaviorScores,//行为得分推荐 1
        ///BehaviorChange//行为变化推荐 2
        /// </summary>
        /// <param name="category"></param>//category分为 CompleteRandom、BehaviorScores和BehaviorChange
        /// <param name="iSugg"></param> iSugg是建议的id
        /// <param name="iScore"></param>iScore是用户选中的那一个
        public void clicked(RecommendedCategory category, int iSugg, int iScore)
        {
            this.Hide();
            if (this.Parent != null)
            {
                this.Parent.m_emotionTracking.Show();
            }
            SaveUserEvaluationHistory(category,iSugg, iScore);
        }
        private void radioButton1_Click(object sender, EventArgs e)
        {
            clicked(this.enumCategory,this.iSuggestionID,0);
            radioButton1.Checked = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            clicked(this.enumCategory,this.iSuggestionID, 1);
            radioButton2.Checked = false;
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            clicked(this.enumCategory,this.iSuggestionID, 2);
            radioButton3.Checked = false;
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            clicked(this.enumCategory,this.iSuggestionID, 3);
            radioButton4.Checked = false;
        }


        private void SaveUserEvaluationHistory(RecommendedCategory category, int iSuggestion, int iScore)
        {
            string strFilePath = "UserEvaluation";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("Evaluations");

            XmlNode node = xmlDoc.CreateElement("Record");
            XmlAttribute timeNode = xmlDoc.CreateAttribute("Time");
            timeNode.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            node.Attributes.Append(timeNode);

            XmlNode categoryNode = xmlDoc.CreateElement("Category");
            categoryNode.AppendChild(xmlDoc.CreateTextNode(((int)category).ToString()));

            XmlNode suggestion = xmlDoc.CreateElement("Suggestion");
            suggestion.AppendChild(xmlDoc.CreateTextNode(iSuggestion.ToString()));

            XmlNode score = xmlDoc.CreateElement("Scoring");
            score.AppendChild(xmlDoc.CreateTextNode(iScore.ToString()));

            node.AppendChild(categoryNode);
            node.AppendChild(suggestion);
            node.AppendChild(score);
            
            //root.AppendChild(node);
            //我想后进来的在上面显示
            XmlNode firstNode=root.FirstChild;
            root.InsertBefore(node, firstNode);
            xmlDoc.Save(strFilePath);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }       
    }
}
