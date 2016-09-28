using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MdiTabStrip;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using UrlHistoryLibrary;
using System.Diagnostics;
using System.Xml;
using System.Net;
using WebmindBrowser.UrlHistory;
using WebmindBrowser.Analysis;
using ZedGraph;
using System.Diagnostics;
using System.Globalization;

namespace WebmindBrowser
{

    public partial class MainWindow : Form
    {
        public static String favXml = "favorits.xml", linksXml = "links.xml";
        public static String settingsXml = "settings.xml", historyXml = "history.xml";
        public String lastSessionXml = "lastSession.xml";
        List<String> urls = new List<String>();
        XmlDocument settings = new XmlDocument();
        public static CultureInfo currentCulture;
        public String homePage = "about:blank";


        public static Uri url=null;//这个纯粹是为了把浏览网页加进历史记录而设置的
        protected static string mainTitle = "Webmind from WSI";
        public static UrlRecordEntity LastUrlRecordEntity = new UrlRecordEntity();//上一个访问记录,其CurUrl属性表示当前的url(当然我是指当前(或者是最近请求的)请求的那个)
        public EmotionTracking m_emotionTracking;//心理状况跟踪窗口
        public EmotionSuggestion m_emotionSuggestion;//心理建议窗口

        private EmotionSuggestion m_AutoSuggestion;//自动推荐建议窗口
        public static bool AutoSuggestionVisible = false;//自动推荐建议窗口是否在显示

        private System.Timers.Timer timerAutoSuggestion = new System.Timers.Timer(300000000);//推荐周期5分钟，由于老师要求，我不得不改成-1，即永远也无法自动跳出

        private System.Timers.Timer timerEmotionAnalysis = new System.Timers.Timer(1200000);//心理分析周期20分钟

        /// <summary>
        /// 
        /// </summary>
        delegate void SetVisibleCallback();

        delegate void UpdateEmotionTrackingCallback();

    #region 初始化函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWindow()
        {
            
            StartInitialThread();
            InitializeComponent();
            InitializeLeftTreeExplorer();
            this.Text = mainTitle;
            setVisibility();
            NewBlankBrowserWindow();
            InitializeTimers();
            InitalEmotionForm();
            UpdateEmotionTracking();
            currentCulture = CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitializeTimers()
        {
            //定时推荐
            this.timerAutoSuggestion.Elapsed += new System.Timers.ElapsedEventHandler(AutoRecommendSuggestion);
            this.timerAutoSuggestion.AutoReset = true;
            this.timerAutoSuggestion.Enabled = true;

            //定时心理分析
            this.timerEmotionAnalysis.Elapsed += new System.Timers.ElapsedEventHandler(TimingAnalysisEmotionIndicators);
            this.timerEmotionAnalysis.AutoReset = true;
            this.timerEmotionAnalysis.Enabled = true;
        }
        /// <summary>
        /// 初始化 这里就已经加载了收藏夹和历史记录
        /// </summary>
        private void InitializeLeftTreeExplorer()
        {
            //LeftTreeExplorer.Clear();

            LeftFormPanel.Visible = false;
        }

        /// <summary>
        /// 初始化子窗口
        /// </summary>
        private void InitalEmotionForm()
        {
            this.m_emotionSuggestion = new EmotionSuggestion(this);//推荐建议窗口
            this.m_emotionTracking = new EmotionTracking(this);//心理状况跟踪窗口
            this.m_AutoSuggestion = new EmotionSuggestion();//自动推荐窗口
            this.m_emotionTracking.Hide();
            this.m_emotionSuggestion.Hide();

            this.m_AutoSuggestion.Show();
            this.m_AutoSuggestion.Visible = false;
        }

        public void UpdateEmotionTracking()//这就是在浏览器右上角有个小图标的地方,得改这个地方
        {/*
            GraphPane trackingPane = this.zedGraphControl1.GraphPane;
            trackingPane.XAxis.Type = AxisType.Date;
            trackingPane.XAxis.IsVisible = false;
            trackingPane.YAxis.IsVisible = false;
            trackingPane.Title.IsVisible = false;
            trackingPane.GraphObjList.Clear();
            trackingPane.CurveList.Clear();
            LineItem line1 = trackingPane.AddCurve("", EmotionTracking.SOMValueList, Color.Red, SymbolType.None);
            //LineItem line2 = trackingPane.AddCurve("", EmotionTracking.DEPValueList, Color.Silver, SymbolType.None);
            LineItem line3 = trackingPane.AddCurve("", EmotionTracking.ANXValueList, Color.DarkBlue, SymbolType.None);
            LineItem line4 = trackingPane.AddCurve("", EmotionTracking.PSDValueList, Color.Blue, SymbolType.None);
            LineItem line5 = trackingPane.AddCurve("", EmotionTracking.HYPValueList, Color.Green, SymbolType.None);
            LineItem line6 = trackingPane.AddCurve("", EmotionTracking.UNRValueList, Color.DarkCyan, SymbolType.None);
            LineItem line7 = trackingPane.AddCurve("", EmotionTracking.HMAValueList, Color.DeepPink, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
          * */
            CreateMultiYChart(this.zedGraphControl1);
        }

        private void CreateMultiYChart(ZedGraphControl zgc)
        {
            // Get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the titles and axis labels
            myPane.Title.Text = "Demonstration of Multi Y Graph";
            myPane.XAxis.Title.Text = "Time, s";
            myPane.YAxis.Title.Text = "Velocity, m/s";
            myPane.Y2Axis.Title.Text = "Acceleration, m/s2";

            // Make up some data points based on the Sine function
            PointPairList vList = new PointPairList();
            PointPairList aList = new PointPairList();
            PointPairList dList = new PointPairList();
            PointPairList eList = new PointPairList();

            double[] x4 = {0, 1, 2, 3, 4, 5, 6, 7};
            /*
            // Fabricate some data values
            for (int i = 0; i < 7; i++)
            {
                double time = (double)i;
                double acceleration = 2.0;
                double velocity = acceleration * time;
                double distance = acceleration * time * time / 2.0;
                double energy = 100.0 * velocity * velocity / 2.0;
                aList.Add(time, acceleration);
            }


            for (int i = 0; i < 5; i++)
            {
                double time = x4[i];
                double acceleration = 2.0;
                double velocity = 13.0 + 7.0 * acceleration * time;
                double distance = 10.0 + 3.0 * acceleration * time * velocity;
                double energy = 8.0 + 6.0 * velocity * velocity / 2.0;
                vList.Add(x4[i], velocity);
                eList.Add(x4[i], energy);
                dList.Add(x4[i], distance);
            }
            */

            // Generate a red curve with diamond symbols, and "Velocity" in the legend
            LineItem myCurve = myPane.AddCurve("Velocity",
                // dList, Color.Red, SymbolType.Diamond);
                EmotionTracking.DEPValueList, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            
            myCurve.Line.Width = 2.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Symbol.Size = 24.0F;

            // Generate a green curve with square symbols, and "Distance" in the legend
            myCurve = myPane.AddCurve("Distance",
                // vList, Color.Green, SymbolType.Square);
            EmotionTracking.ANXValueList, Color.Green, SymbolType.Square);
            // Fill the symbols with white
           
            // Associate this curve with the second Y axis
            myCurve.YAxisIndex = 1;
            myCurve.Line.Width = 2.0F;

            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Symbol.Size = 24.0F;

            // Generate a Black curve with triangle symbols, and "Energy" in the legend
            myCurve = myPane.AddCurve("Energy",
                //   eList, Color.Black, SymbolType.Triangle);
            EmotionTracking.HMAValueList, Color.Black, SymbolType.Triangle);
            // Fill the symbols with white
            
            // Associate this curve with the Y2 axis
            myCurve.IsY2Axis = false;
            // Associate this curve with the second Y2 axis
            myCurve.YAxisIndex = 1;
            myCurve.Line.Width = 2.0F;

            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Symbol.Size = 24.0F;


            //double[] x4 = { 0, 1, 2, 3, 4, 5, 6, 7 };
            // double[] x5 = { 10,11,12,13,14,15,16,17};
            double[] y4 = { 30, 45, 53, 60, 45, 53, 24 };
            // normalize(y4, 7);
            BarItem bar = myPane.AddBar("PHI", x4, EmotionTracking.y5, Color.SteelBlue);
            // Fill the bars with a RosyBrown-White-RosyBrown gradient
            bar.Bar.Border.Width = 0F;
            bar.Bar.Fill = new Fill(Color.LightBlue, Color.White, Color.LightBlue);


            //// Generate a blue curve with circle symbols, and "Acceleration" in the legend
            //myCurve = myPane.AddCurve("Acceleration",
            //   aList, Color.Blue, SymbolType.Circle);
            //// Fill the symbols with white
            //myCurve.Symbol.Fill = new Fill(Color.White);
            //// Associate this curve with the Y2 axis
            //myCurve.IsY2Axis = true;


            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = false;

            myPane.Border.IsVisible = false;
            myPane.XAxis.IsVisible = false;

            myPane.XAxis.Scale.Min = -1;
            myPane.XAxis.Scale.Max = 7;

            myPane.Legend.IsVisible = false;//legend:图例、说明
            myPane.Title.IsVisible = false;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.Red;
            myPane.YAxis.Title.FontSpec.FontColor = Color.Red;
            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;
            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;
            myPane.YAxis.Scale.Max = 1.5;//我把这里的100改成了1.5
            myPane.YAxis.IsVisible = false;

            // Enable the Y2 axis display
            myPane.Y2Axis.IsVisible = false;
            // Make the Y2 axis scale blue
            myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue;
            myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            myPane.Y2Axis.MajorTic.IsOpposite = false;
            myPane.Y2Axis.MinorTic.IsOpposite = false;
            // Display the Y2 axis grid lines
            myPane.Y2Axis.MajorGrid.IsVisible = false;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;
            myPane.Y2Axis.Scale.Min = 0;//我把1.5改成了0
            myPane.Y2Axis.Scale.Max = 1.5;//我把3改成了1.5

            // Create a second Y Axis, green
            YAxis yAxis3 = new YAxis("Distance, m");
            myPane.YAxisList.Add(yAxis3);
            yAxis3.IsVisible = false;
            yAxis3.Scale.FontSpec.FontColor = Color.Green;
            yAxis3.Title.FontSpec.FontColor = Color.Green;
            yAxis3.Color = Color.Green;
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            yAxis3.MajorTic.IsInside = false;
            yAxis3.MinorTic.IsInside = false;
            yAxis3.MajorTic.IsOpposite = false;
            yAxis3.MinorTic.IsOpposite = false;
            // Align the Y2 axis labels so they are flush to the axis
            yAxis3.Scale.Align = AlignP.Inside;

            Y2Axis yAxis4 = new Y2Axis("Energy");
            yAxis4.IsVisible = false;
            myPane.Y2AxisList.Add(yAxis4);
            // turn off the opposite tics so the Y2 tics don't show up on the Y axis
            yAxis4.MajorTic.IsInside = false;
            yAxis4.MinorTic.IsInside = false;
            yAxis4.MajorTic.IsOpposite = false;
            yAxis4.MinorTic.IsOpposite = false;
            // Align the Y2 axis labels so they are flush to the axis
            yAxis4.Scale.Align = AlignP.Inside;
            yAxis4.Type = AxisType.Log;
            yAxis4.Scale.Min = 1.5;//我把100改成1.5

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

            zgc.AxisChange();
        }

    #endregion

    #region 启动线程
        private void StartInitialThread()
        {
            //ThreadManagement.StartContentClassificationThread();//加载内容分类模型
            //ThreadManagement.StartEmotionClassificationThread();//加载情感分类模型
            ThreadManagement.StartLoadUrlContentHistoryThread();//加载当前的网络浏览历史记录
            ThreadManagement.StartBehaviorFeaturesThread();//加载行为特征历史（行为分析结果  心理分析结果）
        }

        private void StartBahaviorAndRecommendThread()
        {
            ThreadManagement.StartRecommendationThread();
            //ThreadManagement.CallRecommendationThread();
        }
    #endregion

        //根据上次设置，来显示这次的浏览器情况
        private void setVisibility()
        {
            if (!File.Exists(settingsXml))
            {
                XmlElement r = settings.CreateElement("settings");
                settings.AppendChild(r);
                XmlElement el;

                el = settings.CreateElement("MainmenuStrip");
                el.SetAttribute("visible", "True");
                r.AppendChild(el);
             
                el = settings.CreateElement("homepage");
                el.InnerText = "about:blank";
                r.AppendChild(el);
            }
            else
            {
                settings.Load(settingsXml);
                XmlElement r = settings.DocumentElement;
                MainmenuStrip.Visible = (r.ChildNodes[0].Attributes[0].Value.Equals("True"));
                homePage = r.ChildNodes[1].InnerText;
            }
        }
        private void SetVisible()
        {
            if(this.m_AutoSuggestion.Visible)
            {
                this.m_AutoSuggestion.Visible = false;
            }
            else
            {
                this.m_AutoSuggestion.Visible = true;
            }
        }

    #region 事件


        #region  定时器事件： 定时推荐建议   定时心理分析
        /// <summary>
        /// 定时弹出建议窗口
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void AutoRecommendSuggestion(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.m_AutoSuggestion.Visible == false)
            {
                if (this.m_AutoSuggestion.InvokeRequired)
                {
                    SetVisibleCallback d = new SetVisibleCallback(SetVisible);
                    this.Invoke(d);
                }
                else
                {
                    SetVisible();
                }
            }
            this.m_AutoSuggestion.UpdateEmotionSuggestionContent();//更新数据
        }

        /// <summary>
        /// 定时进行心理分析
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void TimingAnalysisEmotionIndicators(object source, System.Timers.ElapsedEventArgs e)
        {
        #region  URL 分析

            UrlAnalysis.AnalysisOnlineTime();//统计当天的网络浏览时间

            UrlAnalysis.AnalysisONB_StaySecond();////更新当天网页的平均停留时间

            UrlAnalysis.AnalysisONB_StartTime();//分析每天的上网时段：每天的起始上网时间在哪个时段
        #endregion


        #region  Content 分析

            ContentAnalysis.AnalysisPAN_EmotionCategory();//分析网页的情感，以记录到BehaviorFeatureAnalysis里

            ContentAnalysis.AnalysisPTN_ContentCategory();//网页的内容分析,以记录到BehaviorFeatureAnalysis里

        #endregion

        #region   更新心理趋势预测值

            Recommendation.UpdateEmotionPredictionsHistory();//更新心理预测记录

        #endregion

        #region  更新曲线图

            if (this.m_emotionTracking.InvokeRequired)
            {
                UpdateEmotionTrackingCallback d = new UpdateEmotionTrackingCallback(UpdateEmotionTrackingForm);
                this.Invoke(d);
            }
            else
            {
                UpdateEmotionTrackingForm();
            }


        #endregion



        }

        #endregion

        private void UpdateEmotionTrackingForm()
        {
            this.m_emotionTracking.UpdateEmotionTracking();
        }


        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBlankBrowserWindow();
        }

        //<summary>
        //这里就应该是那个点击增加新tab的地方吧,我晕，验证结果不是这个方法
        //</summary>
        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            NewBlankBrowserWindow();
        }

        //<summary>
        //这里就应该是那个点击增加新tab(标签页)的地方吧,果然是这里，
        //</summary>
        private void OnMdiTabStript_Click(object sender, EventArgs e)
        {
            NewBlankBrowserWindow();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Enabled = false;
            (new Open(getCurrentBrowser(), this)).Show();
        }

        //<summary>
        //查看->浏览器栏->收藏夹
        //</summary>
        private void MenuItem_Favorite_Management_Click(object sender, EventArgs e)
        {
            this.MenuItem_View_Browser_History.Checked = !this.MenuItem_View_Browser_History.Checked;
           /* if (LeftFormPanel.Visible)
            {
                LeftFormPanel.Visible = false;
            }
            else
            {
                LeftFormPanel.Visible = true;
                leftTabControl.SelectedTab = favoritesPage;
            }
            * */
            LeftFormPanel.Visible = true;
            leftTabControl.SelectedTab = favoritesPage;
            
        }
        //<sumamry>
        //查看->浏览器栏->历史记录 by kosko
        //</summary>
        private void MenuItem_View_Browser_History_Click(object sender, EventArgs e)
        {
            this.MenuItem_View_Browser_History.Checked = !this.MenuItem_View_Browser_History.Checked;
            /*if (LeftFormPanel.Visible)
            {
                LeftFormPanel.Visible = false;
            }
            else
            {
                LeftFormPanel.Visible = true;
                leftTabControl.SelectedTab = historyPage;
            }
             * */
            LeftFormPanel.Visible = true;
            leftTabControl.SelectedTab = historyPage;
        }

        //<summary>
        //查看->浏览器栏->源 自己写显示源的处理函数 这个是在左侧显示的一个panel，和收藏夹、历史记录是一起的，不是那个
        //显示html源代码的那个 by kosko
        //</summary>

        private void MenuItem_Source_Management_Click(object sender, EventArgs e)
        {
            this.MenuItem_View_Browser_Source.Checked = !this.MenuItem_View_Browser_Source.Checked;
           /* if (LeftFormPanel.Visible)
            {
                LeftFormPanel.Visible = false;

            }
            else
            {
                LeftFormPanel.Visible = true;
                leftTabControl.SelectedTab = sourcePage;
            }
            * */
            LeftFormPanel.Visible = true;
            leftTabControl.SelectedTab = sourcePage;
        }

        private void btn_Favorite_Click(object sender, EventArgs e)
        {

        }

        //ENTER
        private void addressBar_Url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.mdiTabStrip1.ActiveTab == null)//没有标签页的情况下，新建一个窗口，然后请求网址
                {
                    WebView aForm = new WebView();
                    aForm.MdiParent = this;
                    aForm.WebBrowserContent.Navigate(addressBar_Url.Text.ToString());
                    if (url!=null)
                        addHistory(url, DateTime.Now.ToString(currentCulture));//这里我没有设置这个网页请求是否是有效的的验证
                    aForm.Show();
                }
                else//已有标签页的情况下，直接请求网址
                {
                    WebView webTab = (WebView)this.mdiTabStrip1.ActiveTab.Form;
                    webTab.WebBrowserContent.Navigate(addressBar_Url.Text.ToString());
                    if (url!=null)
                        addHistory(url, DateTime.Now.ToString(currentCulture));//这里我没有设置这个网页请求是否是有效的的验证
                }

            }
        }

        private void button_Favorite_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBar_Video_Blocking_Click(object sender, EventArgs e)
        {
            BehaviorSurvey survey = new BehaviorSurvey();
            survey.Show();
        }

        private void addressBar_Go_Click(object sender, EventArgs e)
        {
            if (this.mdiTabStrip1.ActiveTab == null)
            {
                WebView aForm = new WebView();
                aForm.MdiParent = this;
                aForm.WebBrowserContent.Navigate(addressBar_Url.Text.ToString());
                if (url != null)
                    addHistory(url, DateTime.Now.ToString(currentCulture));//这里我没有设置这个网页请求是否是有效的的验证
                aForm.Show();
            }
            else
            {
                WebView webTab = (WebView)this.mdiTabStrip1.ActiveTab.Form;
                webTab.WebBrowserContent.Navigate(addressBar_Url.Text.ToString());
                if (url != null)
                    addHistory(url, DateTime.Now.ToString(currentCulture));//这里我没有设置这个网页请求是否是有效的的验证
            }

        }

        private void addressBar_SizeChanged(object sender, EventArgs e)
        {
            ResizeAddress();
        }

        private void MenuItem_File_Print_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().ShowPrintDialog();
        }

        private void MenuItem_File_Preview_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().ShowPrintPreviewDialog();
        }

        private void MenuItem_File_Properties_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().ShowPropertiesDialog();
        }

        //<summary>
        //编辑->剪切，下面两个函数就是设计这个的，因为当没复制时，paste是无效的
        //</summary>
        private void MenuItem_Edit_Cut_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("Cut", false, null);
            this.MenuItem_Edit_Paste.Enabled=true;
        }
        private void MenuItem_Edit_Cut_CheckStateChanged(object sender, EventArgs e)
        {
            this.MenuItem_Edit_Paste.Enabled = !this.MenuItem_Edit_Paste.Enabled;
        }


        //<summary>
        //编辑->复制，下面两个函数就是设计这个的，因为当没复制时，paste是无效的
        //</summary>
        private void MenuItem_Edit_Copy_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("Copy", false, null);
            this.MenuItem_Edit_Paste.Enabled=true;
        }
        private void MenuItem_Edit_Copy_CheckStateChanged(object sender, EventArgs e)
        {
            this.MenuItem_Edit_Paste.Enabled = !this.MenuItem_Edit_Paste.Enabled;
        }
      
        private void MenuItem_Edit_Paste_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("Paste", false, null);
        }

        //<summary>
        //编辑->全选，下面两个函数就是设计这个的，因为当没全选时，复制、剪切是无效的,但发现后面两个函数基本没效果
        //</summary>
        private void MenuItem_Edit_SelectAll_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("SelectAll", true, null);
             this.MenuItem_Edit_Cut.Enabled = true;
            this.MenuItem_Edit_Copy.Enabled = true;
        }
        private void MenuItem_Edit_SelectAll_CheckStateChanged(object sender, EventArgs e)
        {
            this.MenuItem_Edit_Cut.Enabled = !this.MenuItem_Edit_Cut.Enabled;
            this.MenuItem_Edit_Copy.Enabled = !this.MenuItem_Edit_Copy.Enabled;
        }
        private void MenuItem_Edit_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.MenuItem_Edit_Cut.Enabled = !this.MenuItem_Edit_Cut.Enabled;
            this.MenuItem_Edit_Copy.Enabled = !this.MenuItem_Edit_Copy.Enabled;

        }
        private void MenuItem_File_Send_Click(object sender, EventArgs e)
        {
            Process.Start("msimn.exe");
        }
        /*
        public void Find()
        {
            IOleCommandTarget cmdt;
            Object o = new object();
            try
            {
                cmdt = (IOleCommandTarget)GetDocument();
                cmdt.Exec(ref cmdGuid, (uint)MiscCommandTarget.Find,
             (uint)SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, ref o, ref o);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        */


        //<summary>
        //在当前页上查找:txtkeyword是需要查找的关键字
        //</summary>
      
 
        private void MenuItem_Edit_FindInPage_Click(object sender, EventArgs e)
        {
            //getCurrentBrowser().Document.ExecCommand("find", true, null);
            FindInPage fp=new FindInPage(getCurrentBrowser());
        }

        //<summary>
        //查看->工具栏->菜单栏 让菜单栏显示或者隐藏
        //</summary>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainmenuStrip.Visible = !MainmenuStrip.Visible;

            this.toolStripMenuItem1.Checked = MainmenuStrip.Visible;
            this.MenuItem_View_Tools_Menu.Checked = MainmenuStrip.Visible;
        }

        //<summary>
        //查看->工具栏->菜单栏->状态栏 让状态栏显示或者隐藏
        //</summary>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StatusBar.Visible = !StatusBar.Visible;
            this.toolStripMenuItem2.Checked = StatusBar.Visible;
            this.MenuItem_View_Tools_Status.Checked = StatusBar.Visible;
        }
        //<summary>
        //直接调用ie的那个
        //</summary>
        private void MenuItem_Tools_Setting_Click(object sender, EventArgs e)
        {
            Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL inetcpl.cpl");
        }

        //<summary>
        //自己写的internet选项(紧紧作为尝试)
        //</summary>
        private void internetOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            InternetOption intOp = new InternetOption(getCurrentBrowser().Url.ToString());
            if (intOp.ShowDialog() == DialogResult.OK)
            {
                if (!intOp.homePage_set.Text.Equals(""))
                {
                    homePage = intOp.homePage_set.Text;
                    settings.DocumentElement.ChildNodes[1].InnerText = intOp.homePage_set.Text;
                }
                if (intOp.checkBox_history.Checked == true)
                {
                    File.Delete(historyXml);
                    historyTreeView.Nodes.Clear();
                }
                //settings.DocumentElement.ChildNodes[2].InnerText = intOp.num.Value.ToString();
                ActiveForm.ForeColor = intOp.forecolor;
                ActiveForm.BackColor = intOp.backcolor;
                //linkBar.BackColor = intOp.backcolor;
                addressBar.BackColor = intOp.backcolor;
                ActiveForm.Font = intOp.font;
                //linkBar.Font = intOp.font;
                MainmenuStrip.Font = intOp.font;
            }
            
        }



        //<summary>
        //全屏：实现方法是各个模块的visible设置为false或者true
        //</summary>
        private void MenuItem_View_FullScreen_Click(object sender, EventArgs e)
        {
            if (!(this.FormBorderStyle == FormBorderStyle.None && this.WindowState == FormWindowState.Maximized))
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                MainmenuStrip.Visible = false;
                addressBar.Visible = false;
                StatusBar.Visible = false;
                AddressBarPanel.Visible = false;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
                MainmenuStrip.Visible = true;
                addressBar.Visible = true;
                StatusBar.Visible = true;
                AddressBarPanel.Visible = true;
            }
        }

        //<summary>
        //查看->工具栏->地址栏 让地址栏显示或者隐藏
        //</summary>
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            addressBar.Visible = !addressBar.Visible;
            this.ToolStripMenuItem3.Checked = addressBar.Visible;
            this.MenuItem_View_Tools_address.Checked = addressBar.Visible;
        }

        //<summary>
        //查看->源代码  by kosko 个人设计时获取源代码后，用一个notepad弹出来,这就需要我对WebBrowser很了解
        //</summary>
        private void MenuItem_View_Source_Click(object sender, EventArgs e)
        {
            //getCurrentBrowser().DocumentText
            String source = ("source.txt");
            StreamWriter writer = File.CreateText(source);
            writer.Write(getCurrentBrowser().DocumentText);
            writer.Close();
            Process.Start("notepad.exe", source);
        }

        //<summary>
        //addFavorit method 添加到收藏夹
        //</summary>
        private void addFavorit(String url, string name)
        {
            XmlDocument myXml = new XmlDocument();
            XmlElement el = myXml.CreateElement("favorit");
            el.SetAttribute("url", url);
            el.InnerText = name;
            if (!File.Exists(favXml))
            {
                XmlElement root = myXml.CreateElement("favorites");
                myXml.AppendChild(root);
                root.AppendChild(el);
            }
            else
            {
                myXml.Load(favXml);
                XmlNodeList xnl=myXml.SelectNodes("favorit");
                bool found=false;
                //查看favXml是否已经有了url这个网站，如果有了，则覆盖掉,如果没有则添加进去
                foreach(XmlNode xn in xnl)
                {
                    if (xn.InnerText == url)
                    {
                        found = true;
                        break;
                    }
                        
                }
                if (!found)
                    myXml.DocumentElement.AppendChild(el);
            }
            if (LeftFormPanel.Visible == true)
            {
                TreeNode node = new TreeNode(el.InnerText, faviconIndex(el.GetAttribute("url")), faviconIndex(el.GetAttribute("url")));
                //TreeNode node = new TreeNode(el.InnerText);
                node.ToolTipText = el.GetAttribute("url");
                node.Name = el.GetAttribute("url");
                node.ContextMenuStrip = favContextMenu;//应该是个快捷菜单
                favTreeView.Nodes.Add(node);
            }
            myXml.Save(favXml);
        }

        //<summary>
        //delete favorit method
        //</summary>
        private void deleteFavorit()
        {
            favTreeView.SelectedNode.Remove();

            XmlDocument myXml = new XmlDocument();
            myXml.Load(favXml);
            XmlElement root = myXml.DocumentElement;
            foreach (XmlElement x in root.ChildNodes)
            {
                if (x.GetAttribute("url").Equals(adress))
                {
                    root.RemoveChild(x);
                    break;
                }
            }

            myXml.Save(favXml);

        }
        //<summary>
        //renameFavorit method
        //</summary>
        private void renameFavorit()
        {
            RenameLink rl = new RenameLink(name);
            if (rl.ShowDialog() == DialogResult.OK)
            {
                XmlDocument myXml = new XmlDocument();
                myXml.Load(favXml);
                foreach (XmlElement x in myXml.DocumentElement.ChildNodes)
                {
                    if (x.InnerText.Equals(name))
                    {
                        x.InnerText = rl.newName.Text;
                        break;
                    }
                }
                favTreeView.Nodes[adress].Text = rl.newName.Text;
                myXml.Save(favXml);
            }
            rl.Close();
        }




        //<summary>
        //addLink method 一个链接,比如在历史记录里，收藏夹里
        //</summary>
        private void addLink(String url, string name)
        {
            XmlDocument myXml = new XmlDocument();
            XmlElement el = myXml.CreateElement("link");
            el.SetAttribute("url", url);
            el.InnerText = name;

            if (!File.Exists(linksXml))
            {
                XmlElement root = myXml.CreateElement("links");
                myXml.AppendChild(root);
                root.AppendChild(el);
            }
            else
            {
                myXml.Load(linksXml);
                myXml.DocumentElement.AppendChild(el);
            }
           /* if (linkBar.Visible == true)
            {
                ToolStripButton b =
                          new ToolStripButton(el.InnerText, getFavicon(url), items_Click, el.GetAttribute("url"));
                b.ToolTipText = el.GetAttribute("url");
                b.MouseUp += new MouseEventHandler(b_MouseUp);
                linkBar.Items.Add(b);
            }
            */
            if (LeftFormPanel.Visible == true)
            {
                TreeNode node = new TreeNode(el.InnerText, faviconIndex(url), faviconIndex(el.GetAttribute("url")));
                node.Name = el.GetAttribute("url");
                node.ToolTipText = el.GetAttribute("url");
                node.ContextMenuStrip = linkContextMenu;
                favTreeView.Nodes[0].Nodes.Add(node);
            }
            myXml.Save(linksXml);
          

        }
        //<summary>
        //删除一个链接,比如在历史记录里，收藏夹里
        //</summary>
        private void deleteLink()
        {
            
            if (LeftFormPanel.Visible == true)
                favTreeView.Nodes[0].Nodes[adress].Remove();
            //if (linkBar.Visible == true)//这里linkBar就是传说中的收藏夹栏
              //  linkBar.Items.RemoveByKey(adress);
            XmlDocument myXml = new XmlDocument();
            myXml.Load(linksXml);
            XmlElement root = myXml.DocumentElement;
            foreach (XmlElement x in root.ChildNodes)
            {
                if (x.GetAttribute("url").Equals(adress))
                {
                    root.RemoveChild(x);
                    break;
                }
            }

            myXml.Save(linksXml);
             
        }
        
        //<summary>
        //renameLink 比如在历史记录和收藏夹里
        //</summary>
        private void renameLink()
        {
            RenameLink rl = new RenameLink(name);
            if (rl.ShowDialog() == DialogResult.OK)
            {
                XmlDocument myXml = new XmlDocument();
                myXml.Load(linksXml);
                foreach (XmlElement x in myXml.DocumentElement.ChildNodes)
                {
                    if (x.InnerText.Equals(name))
                    {
                        x.InnerText = rl.newName.Text;
                        break;
                    }
                }
                //if (linkBar.Visible == true)//收藏夹栏
                  //  linkBar.Items[adress].Text = rl.newName.Text;
                if (LeftFormPanel.Visible == true)
                    favTreeView.Nodes[0].Nodes[adress].Text = rl.newName.Text;
                myXml.Save(linksXml);
            }
            rl.Close();
        }


        private void MenuItem_Favorite_Add_Click(object sender, EventArgs e)
        {

            if (getCurrentBrowser().Url != null)
            {
                AddFavorites dlg = new AddFavorites(getCurrentBrowser().Url.ToString());
                DialogResult res = dlg.ShowDialog();

                if (res == DialogResult.OK)
                {
                    if (dlg.favFile == "Favorites")
                        addFavorit(getCurrentBrowser().Url.ToString(), dlg.favName);
                    else addLink(getCurrentBrowser().Url.ToString(), dlg.favName);
                }
                dlg.Close();
            }
        }

        //<summary>
        //显示浏览器左侧的收藏夹、历史记录、源等情况:主要是加载数据
        //</summary>
        #region FAVORITES WINDOW

        string adress, name;

        private void showFavorites()
        {
            XmlDocument myXml = new XmlDocument();
            TreeNode link = new TreeNode("Links", 0, 0);
            link.NodeFont = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            favTreeView.Nodes.Add(link);

            if (File.Exists(favXml))
            {
                myXml.Load(favXml);

                foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                {
                    TreeNode node =
                        new TreeNode(el.InnerText, faviconIndex(el.GetAttribute("url")), faviconIndex(el.GetAttribute("url")));
                    node.ToolTipText = el.GetAttribute("url");
                    node.Name = el.GetAttribute("url");
                    node.ContextMenuStrip = favContextMenu;
                    favTreeView.Nodes.Add(node);
                }

            }

            if (File.Exists(linksXml))
            {
                myXml.Load(linksXml);

                foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                {
                    TreeNode node =
                        new TreeNode(el.InnerText, faviconIndex(el.GetAttribute("url")), faviconIndex(el.GetAttribute("url")));
                    node.ToolTipText = el.GetAttribute("url");
                    node.Name = el.GetAttribute("url");
                    node.ContextMenuStrip = linkContextMenu;
                    favTreeView.Nodes[0].Nodes.Add(node);
                }

            }

        }

        //<summary>
        //点击右键后，放开鼠标mouseup事件
        //<summary>
        private void b_MouseUp(object sender, MouseEventArgs e)
        {
            ToolStripButton b = (ToolStripButton)sender;
            adress = b.ToolTipText;
            name = b.Text;

            if (e.Button == MouseButtons.Right)
                linkContextMenu.Show(MousePosition);
        }

        //<summary>
        //收藏夹面板上点击节点
        //</summary>
        private void favTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                favTreeView.SelectedNode = e.Node;
                adress = e.Node.ToolTipText;
                name = e.Node.Text;
            }
            else
                if (e.Node != favTreeView.Nodes[0])
                    getCurrentBrowser().Navigate(e.Node.ToolTipText);
        }
        /*
        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                favTreeView.SelectedNode = e.Node;
                adress = e.Node.ToolTipText;
                name = e.Node.Text;
            }
            else
                if (e.Node != favTreeView.Nodes[0])
                    getCurrentBrowser().Navigate(e.Node.ToolTipText);

        }
        */
         
        //show history in tree wiew
        
        private void showHistory()
        {
            historyTreeView.Nodes.Clear();
            XmlDocument myXml = new XmlDocument();

            if (File.Exists(historyXml))
            {
                myXml.Load(historyXml);
                DateTime now = DateTime.Now;
                if (comboBox1.Text.Equals("今天的浏览记录"))
                {
                    historyTreeView.ShowRootLines = false;
                    foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                    {
                        DateTime d = DateTime.Parse(el.GetAttribute("lastVisited"), currentCulture);

                        if (!(d.Date == now.Date)) return;

                        TreeNode node =
                            new TreeNode(el.GetAttribute("url"), 3, 3);
                        node.ToolTipText = el.GetAttribute("url") + "\nLast Visited: " + el.GetAttribute("lastVisited") + "\nTimes Visited: " + el.GetAttribute("times");
                        node.Name = el.GetAttribute("url");
                        node.ContextMenuStrip = histContextMenu;
                        historyTreeView.Nodes.Add(node);
                    }

                }

                if (comboBox1.Text.Equals("按网址查看"))
                {
                    historyTreeView.ShowRootLines = true;
                    foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                    {
                        Uri site = new Uri(el.GetAttribute("url"));

                        if (!historyTreeView.Nodes.ContainsKey(site.Host.ToString()))
                            historyTreeView.Nodes.Add(site.Host.ToString(), site.Host.ToString(), 0, 0);
                        TreeNode node = new TreeNode(el.GetAttribute("url"), 3, 3);
                        node.ToolTipText = el.GetAttribute("url") + "\nLast Visited: " + el.GetAttribute("lastVisited") + "\nTimes Visited: " + el.GetAttribute("times");
                        node.Name = el.GetAttribute("url");
                        node.ContextMenuStrip = histContextMenu;
                        historyTreeView.Nodes[site.Host.ToString()].Nodes.Add(node);
                    }

                }

                if (comboBox1.Text.Equals("按日期查看"))
                {
                    historyTreeView.ShowRootLines = true;
                    historyTreeView.Nodes.Add("两个星期前", "两个星期前", 2, 2);//nodes[0]
                    historyTreeView.Nodes.Add("上个星期", "上个星期", 2, 2);//nodes[1]
                    historyTreeView.Nodes.Add("这个星期", "这个星期", 2, 2);//nodes[2]
                    historyTreeView.Nodes.Add("昨天", "昨天", 2, 2);//nodes[3]
                    historyTreeView.Nodes.Add("今天", "今天", 2, 2);//nodes[4]
                    foreach (XmlElement el in myXml.DocumentElement.ChildNodes)
                    {
                        DateTime d = DateTime.Parse(el.GetAttribute("lastVisited"), currentCulture);//zai history.xml里记下有lastVisited属性

                        TreeNode node = new TreeNode(el.GetAttribute("url"), 3, 3);
                        node.ToolTipText = el.GetAttribute("url") + "\nLast Visited: " + el.GetAttribute("lastVisited") + "\nTimes Visited: " + el.GetAttribute("times");
                        node.Name = el.GetAttribute("url");
                        node.ContextMenuStrip = histContextMenu;

                        if (d.Date == now.Date)
                            historyTreeView.Nodes[4].Nodes.Add(node);
                        else
                            if (d.AddDays(1).ToShortDateString().Equals(now.ToShortDateString()))
                                historyTreeView.Nodes[3].Nodes.Add(node);
                            else
                                if (d.AddDays(7) > now)
                                    historyTreeView.Nodes[2].Nodes.Add(node);
                                else
                                    if (d.AddDays(14) > now)
                                        historyTreeView.Nodes[1].Nodes.Add(node);
                                    else
                                        if (d.AddDays(21) > now)
                                            historyTreeView.Nodes[0].Nodes.Add(node);
                                        else
                                            if (d.AddDays(22) > now)
                                                myXml.DocumentElement.RemoveChild(el);
                    }
                }
            }


        }

        //<summary>
        //addHistory method 将访问的网站写进历史记录
        //</summary>
        public void addHistory(Uri url, string data)
        {
            //写进xml文件
            XmlDocument myXml = new XmlDocument();
            int i = 1;
            XmlElement el = myXml.CreateElement("item");
            el.SetAttribute("url", url.ToString());
            el.SetAttribute("lastVisited", data);

            if (!File.Exists(historyXml))
            {
                XmlElement root = myXml.CreateElement("history");
                myXml.AppendChild(root);
                el.SetAttribute("times", "1");
                root.AppendChild(el);
            }
            else
            {
                myXml.Load(historyXml);

                foreach (XmlElement x in myXml.DocumentElement.ChildNodes)
                {
                    if (x.GetAttribute("url").Equals(url.ToString()))
                    {
                        i = int.Parse(x.GetAttribute("times")) + 1;
                        myXml.DocumentElement.RemoveChild(x);
                        break;
                    }
                }

                el.SetAttribute("times", i.ToString());
                myXml.DocumentElement.InsertBefore(el, myXml.DocumentElement.FirstChild);//最近的记录放在最前面

                //加入treeview
                if (LeftFormPanel.Visible == true)
                {
                    /*ordered visited today*/
                    if (comboBox1.Text.Equals("今天的浏览记录"))
                    {
                        if (!historyTreeView.Nodes.ContainsKey(url.ToString()))
                        {
                            TreeNode node =
                                 new TreeNode(url.ToString(), 3, 3);
                            node.ToolTipText = url.ToString() + "\nLast Visited: " + data + "\nTimes visited :" + i.ToString();
                            node.Name = url.ToString();
                            node.ContextMenuStrip = histContextMenu;
                            historyTreeView.Nodes.Insert(0, node);
                        }
                        else
                            historyTreeView.Nodes[url.ToString()].ToolTipText
                              = url.ToString() + "\nLast Visited: " + data + "\nTimes visited: " + i.ToString();
                    }
                    /*view by site*/
                    if (comboBox1.Text.Equals("按网址查看"))
                    {
                        if (!historyTreeView.Nodes.ContainsKey(url.Host.ToString()))
                        {
                            historyTreeView.Nodes.Add(url.Host.ToString(), url.Host.ToString(), 0, 0);

                            TreeNode node =
                                   new TreeNode(url.ToString(), 3, 3);
                            node.ToolTipText = url.ToString() + "\nLast Visited: " + data + "\nTimes visited: " + i.ToString();
                            node.Name = url.ToString();
                            node.ContextMenuStrip = histContextMenu;
                            historyTreeView.Nodes[url.Host.ToString()].Nodes.Add(node);
                        }

                        else
                            if (!historyTreeView.Nodes[url.Host.ToString()].Nodes.ContainsKey(url.ToString()))
                            {
                                TreeNode node =
                                    new TreeNode(url.ToString(), 3, 3);
                                node.ToolTipText = url.ToString() + "\nLast Visited: " + data + "\nTimes visited: " + i.ToString();
                                node.Name = url.ToString();
                                node.ContextMenuStrip = histContextMenu;
                                historyTreeView.Nodes[url.Host.ToString()].Nodes.Add(node);
                            }
                            else
                                historyTreeView.Nodes[url.Host.ToString()].Nodes[url.ToString()].ToolTipText
                                        = url.ToString() + "\nLast Visited: " + data + "\nTimes visited" + i.ToString();

                    }
                    /* view by date*/
                    if (comboBox1.Text.Equals("按日期查看"))
                    {
                        if (historyTreeView.Nodes[4].Nodes.ContainsKey(url.ToString()) && historyTreeView.Nodes[url.ToString()]!=null)
                            historyTreeView.Nodes[url.ToString()].ToolTipText
                                    = url.ToString() + "\nLast Visited: " + data + "\nTimes visited: " + i.ToString();
                        else
                        {
                            TreeNode node =
                                new TreeNode(url.ToString(), 3, 3);
                            node.ToolTipText = url.ToString() + "\nLast Visited: " + data + "\nTimes visited :" + i.ToString();
                            node.Name = url.ToString();
                            node.ContextMenuStrip = histContextMenu;
                            historyTreeView.Nodes[4].Nodes.Add(node);
                        }
                    }
                }

            }
            myXml.Save(historyXml);
        }
        //delete history
        private  void deleteHistory()
        {
            XmlDocument myXml = new XmlDocument();
            myXml.Load(historyXml);
            XmlElement root = myXml.DocumentElement;
            foreach (XmlElement x in root.ChildNodes)
            {
                if (x.GetAttribute("url").Equals(adress))
                {
                    root.RemoveChild(x);
                    break;
                }
            }
            historyTreeView.SelectedNode.Remove();
            myXml.Save(historyXml);
        }


        //history nodes click
        
        private void historyTreeView_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                historyTreeView.SelectedNode = e.Node;
                adress = e.Node.Text;
            }
            else
                if (!comboBox1.Text.Equals("Ordered Visited Today"))
                {
                    if (!historyTreeView.Nodes.Contains(e.Node))
                        getCurrentBrowser().Navigate(e.Node.Text);
                }
                else
                    getCurrentBrowser().Navigate(e.Node.Text);
        }
        


        //leftForm panel visible change 即浏览器左侧那个panel
       
        private void LeftFormPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (LeftFormPanel.Visible == true)
            {
                showFavorites();
                showHistory();
            }
            else
            {
                favTreeView.Nodes.Clear();
                historyTreeView.Nodes.Clear();
            }
        }
        //<summary>
        //删除历史记录
        //</summary>
        private void MenuItem_History_Delete1_Click(object sender, EventArgs e)
        {

            DeleteBrowsingHistory b = new DeleteBrowsingHistory();
            if (b.ShowDialog() == DialogResult.OK)
            {
                if (b.History.Checked == true)
                {
                    File.Delete(historyXml);
                    historyTreeView.Nodes.Clear();
                }
                if (b.TempFiles.Checked == true)
                {
                    urls.Clear();
                    //while (imgList.Images.Count > 4)
                    //  imgList.Images.RemoveAt(imgList.Images.Count - 1);
                    File.Delete("source.txt");

                }
            }
        }


        /*
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            showHistory();
        }
        */
        #endregion


        //<sumamry>
        //下面三个函数是处理网站中的图标
        //</summary>
        #region FAVICON
        
        // favicon
        public static Image favicon(String u, string file)
        {
            Uri url = new Uri(u);
            String iconurl = "http://" + url.Host + "/favicon.ico";

            if (u == "about:blank")
                return null;
            WebRequest request = WebRequest.Create(iconurl);
            try
            {
                WebResponse response = request.GetResponse();

                Stream s = response.GetResponseStream();
                return Image.FromStream(s);
            }
            catch (Exception ex)
            {
                return Image.FromFile(file);
            }


        }
        //favicon index
        private int faviconIndex(string url)
        {
            
            Uri key = new Uri(url);
            if (!imageList1.Images.ContainsKey(key.Host.ToString()) && url != "about:blank")
                imageList1.Images.Add(key.Host.ToString(), favicon(url, "link.png"));
            return imageList1.Images.IndexOfKey(key.Host.ToString());
        }
        //getFavicon from key
        private Image getFavicon(string key)
        {
            Uri url = new Uri(key);
            if (!imageList1.Images.ContainsKey(url.Host.ToString()))
                imageList1.Images.Add(url.Host.ToString(), favicon(key
                    , "link.png"));
            return imageList1.Images[url.Host.ToString()];
        }
         
        #endregion

        //<summary>
        //查看->转到->主页
        //</summary>
        private void MenuItem_View_Go_Home_Click(object sender, EventArgs e)
        {
            this.goHome();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //setVisibility();
            comboBox1.SelectedItem = comboBox1.Items[0];
            ResizeAddress();
        }


        private void zedGraphControl1_Click(object sender, EventArgs e)//这里就是点击跟踪趋势图的那个地方
        {
            if (this.m_emotionTracking.Visible == true)
            {
                return;
            }
            else
            {
                //这个地方是李一琳要求不用那种显示方式的，而是在statusBar上放一个心理贴士的label的click事件来实现的
              //  this.m_emotionSuggestion.Show();
              //  this.m_emotionSuggestion.UpdateEmotionSuggestionContent();
                this.m_emotionTracking.Show(3000);//这就是点击出现大图的地方

            }
        }

        private void zedGraphControl1_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void addressBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        //<summary>
        //将上一次会话的网页地址存储到xml里
        //</summary>
        void saveLastSession(String url)
        {
            XmlDocument myXml = new XmlDocument();
            XmlNode docNode = myXml.CreateXmlDeclaration("1.0", "UTF-8", null);
            myXml.AppendChild(docNode);
            XmlElement el = myXml.CreateElement("item");
            if (!File.Exists(lastSessionXml))
            {
                XmlElement root = myXml.CreateElement("LastSession");
                myXml.AppendChild(root);
                el.InnerText = url;
                root.AppendChild(el);
            }
            else
            {
                myXml.Load(lastSessionXml);
                XmlNode xe = myXml.SelectSingleNode("LastSession");
                xe.SelectSingleNode("item").InnerText = url;
            }
            myXml.Save(lastSessionXml);
        }


        //<sumamry>
        //这就是传说中的重新打开上次会话内容，不过我还没找出保存多个url的方法，在此只能处理一个网页
        //</summary>
        private void MenuItem_Tools_ReOpen_Click(object sender, EventArgs e)
        {
            XmlDocument myXml = new XmlDocument();
            if (!File.Exists(lastSessionXml))
            {
                return;
            }
            else
            {
                myXml.Load(lastSessionXml);
                XmlNode xe = myXml.SelectSingleNode("LastSession");
                String url = xe.SelectSingleNode("item").InnerText.ToString();
                WebView aForm = new WebView();
                aForm.MdiParent = this;
                aForm.WebBrowserContent.Navigate(url);
                aForm.Show();
            }
        }


        //<summary>
        //主窗口关闭:记录下该次浏览的行为，同时，我还加了一个“重新打开上次会话”的url记录进xml的部分
        //</summary>
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*
            foreach (TabControl tab in this.mdiTabStrip1.Tabs)
            {
                Console.WriteLine((WebView)tab.FindForm().WebBrowserContent.url.ToString());
            }
             * */
            settings.Save(settingsXml);
            if ((this.mdiTabStrip1.ActiveTab)!=null)
            {
                String url = ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.Url.Host.ToString();
                saveLastSession(url);
            }
            
            LastUrlRecordEntity.CurStaySecond = (Stopwatch.GetTimestamp() - LastUrlRecordEntity.CurStaySecond) / Stopwatch.Frequency + 1;//更新停留时间
            UrlAnalysis.UpdateUrlAnalysisHistory(LastUrlRecordEntity);//更新记录
            UpdateCurrentUrlRecord();

            string strDateTime = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString()
                + "_" + DateTime.Now.Day.ToString();

            string strContentAnalysisFile = System.Environment.CurrentDirectory + "\\" + "ContentAnalysis_" + strDateTime;
            ContentAnalysis.SaveContentAnalysisHistory(strContentAnalysisFile);

            string strUrlAnalysisFile = System.Environment.CurrentDirectory + "\\" + "UrlAnalysis_" + strDateTime;
            UrlAnalysis.SaveUrlAnalysisHistory(strUrlAnalysisFile);

            string strPredictionsHistory = System.Environment.CurrentDirectory + "\\" + "EmotionPredictionsHistory";
            Recommendation.SaveEmotionPredictionsHistory(strPredictionsHistory);

            BehaviorFeatureAnalysis.SaveBehaviorFeatureToXML();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (this.m_emotionSuggestion!=null)
                this.m_emotionSuggestion.thisRefresh();
            if (this.m_emotionTracking!=null)
                this.m_emotionTracking.thisRefresh();
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {

        }

        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (this.m_emotionSuggestion!=null)
                this.m_emotionSuggestion.thisRefresh();
            if (this.m_emotionTracking!=null)
                this.m_emotionTracking.thisRefresh();
        }

        private void MenuItem_File_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuItem_File_Save_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_File_NewWindow_Click(object sender, EventArgs e)
        {
            (new MainWindow()).Show();
        }

        //<summary>
        //查看->转到->后退
        //</summary>
        private void addressBar_Back_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().GoBack();
        }

        //<summary>
        //查看->转到->前进
        //</summary>
        private void addressBar_Fwd_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().GoForward();
        }

        private void addressBar_Refresh_Click(object sender, EventArgs e)
        {
            if (this.mdiTabStrip1.ActiveTab != null)
            {
                ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.Refresh();
                RefreshAddressBar();
            }
        }

        //<summary>
        //查看->停止
        //</summary>C:\Documents and Settings\Administrator\桌面\浏览器+分词+bayes\WebMindSystem_kosko集成版\WebmindBrowser\WebmindBrowser\WebmindBrowser\EmotionSuggestion.cs
        private void addressBar_Stop_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Stop();
        }

        private void MenuItem_File_SaveAs_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().ShowSaveAsDialog();
        }

        private void MenuItem_File_CloseTab_Click(object sender, EventArgs e)
        {
            if (this.mdiTabStrip1.ActiveTab!=null)
                this.mdiTabStrip1.ActiveTab.Form.Close();
        }

        private void MenuItem_File_PageSetting_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().ShowPageSetupDialog();
        }

        // 当tab变化时，需要获取当前活动的tab的url，并修改地址栏中的url地址
        private void mdiTabStrip1_CurrentMdiTabChanged(object sender, EventArgs e)
        {
            if (LastUrlRecordEntity == null)
            {
                return;
            }
            //记录访问历史
            //LastUrlRecordEntity.CurStaySecond = (Stopwatch.GetTimestamp() - LastUrlRecordEntity.CurStaySecond) / Stopwatch.Frequency + 1;//更新停留时间
            RefreshAddressBar();
            //UrlAnalysis.UpdateUrlAnalysisHistory(LastUrlRecordEntity);//更新浏览历史
            //UpdateCurrentUrlRecord();
        }

        private void StatusBar_Image_Blocking_Click(object sender, EventArgs e)
        {
            string strTitle = "拦截图片记录数目";
            string strContent = "具体拦截内容！\n 具体拦截内容！\n具体拦截内容！\n具体拦截内容！\n";
            ShowMsg(strTitle, strContent, 1);
        }

    #endregion

        /// <summary>
        /// 更新当前浏览的Url记录
        /// </summary>
        private void UpdateCurrentUrlRecord()
        {
            if (this.mdiTabStrip1.ActiveTab == null)
            {
                return;
            }
            WebView currentView = (WebView)this.mdiTabStrip1.ActiveTab.Form;
            if (this.mdiTabStrip1.ActiveTab == null)
            {
                return;
            }
            if (currentView.WebBrowserContent.Document == null)
            {
                LastUrlRecordEntity.CurClientIP = StaticHelperClass.strClientIPAddress;
                LastUrlRecordEntity.CurDomainName = "";
                LastUrlRecordEntity.CurOpenTime = DateTime.Now;
                LastUrlRecordEntity.CurStaySecond = Stopwatch.GetTimestamp();
                LastUrlRecordEntity.CurTitle = "";
                LastUrlRecordEntity.CurUrl = "about:blank";
            }
            else
            {
                LastUrlRecordEntity.CurClientIP = StaticHelperClass.strClientIPAddress;
                LastUrlRecordEntity.CurDomainName = currentView.WebBrowserContent.Document.Url.Host;
                LastUrlRecordEntity.CurOpenTime = DateTime.Now;
                LastUrlRecordEntity.CurStaySecond = Stopwatch.GetTimestamp();
                LastUrlRecordEntity.CurTitle = currentView.WebBrowserContent.Document.Title;
                if (currentView.WebBrowserContent.Url == null)
                {
                    LastUrlRecordEntity.CurUrl = "about:blank";
                }
                else
                {
                    LastUrlRecordEntity.CurUrl = currentView.WebBrowserContent.Url.ToString();
                }
            }
        }
        private WebBrowser getCurrentBrowser()
        {
            if (this.mdiTabStrip1.Tabs.Count == 0)
                NewBlankBrowserWindow();
            return ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent;
        }

        private void NewHomeBrowser()
        {
            WebView aForm = new WebView();
            aForm.MdiParent = this;
            aForm.WebBrowserContent.GoHome();
            aForm.Show();
        }

        //<summary>
        //新的空浏览器窗口
        //</summary>
        private void NewBlankBrowserWindow()
        {
            WebView aForm = new WebView();
            aForm.MdiParent = this;//让这个mdiTabStrip以MainWindow为依靠
            aForm.WebBrowserContent.Navigate(homePage);
            aForm.Show();
            if (url != null)
                addHistory(url, DateTime.Now.ToString(currentCulture));//这里我没有设置这个网页请求是否是有效的的验证
            //aForm.WebBrowserContent.GoHome();
        }

        public ExtendedWebBrowser NewBlankBrowserWindow(bool ab)
        {
            WebView aForm = new WebView();
            aForm.MdiParent = this;
            aForm.WebBrowserContent.Navigate(homePage);
            aForm.Show();
            return aForm.WebBrowserContent;
        }

        public void UpdateAddressBox(WebView currForm, string urlString)
        {

            if ((WebView)this.mdiTabStrip1.ActiveTab.Form == currForm &&
                !urlString.Equals(this.addressBar_Url.Text, StringComparison.InvariantCultureIgnoreCase))
            {
                this.addressBar_Url.Text = urlString;
            }
            RefreshAddressBar();
        }

        private void LoadHistoryItems(TreeView tsHistoryMnu)
        {
            UrlHistoryWrapperClass urlHistory = new UrlHistoryWrapperClass();
            UrlHistoryWrapperClass.STATURLEnumerator enumerator = urlHistory.GetEnumerator();

            while (enumerator.MoveNext())
            {
                tsHistoryMnu.Nodes.Add(enumerator.Current.URL, enumerator.Current.Title, 19);
            }

            enumerator.Reset();
            urlHistory.Dispose();
        }

        private void LoadFavoriteMenuItems(TreeView tsFavoritesMnu)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DirectoryInfo objDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
                //Recurse, starting from main dir
                LoadFavoriteMenuItems(objDir, tsFavoritesMnu, null);

                this.Cursor = Cursors.Default;
            }
            catch (Exception )
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Recursive function
        /// </summary>
        /// <param name="objDir"></param>
        private void LoadFavoriteMenuItems(DirectoryInfo objDir, TreeView tsFavoritesMnu, TreeNode currNode)
        {
            try
            {
                string strName = string.Empty;
                string strUrl = string.Empty;

                DirectoryInfo[] dirs = objDir.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    TreeNode cNode = new TreeNode();
                    cNode.Text = dir.Name;
                    cNode.ImageIndex = 16;
                    cNode.SelectedImageIndex = 1;

                    if (currNode == null)
                    {
                       tsFavoritesMnu.Nodes.Add(cNode);
                    }
                    else
                    {
                        currNode.Nodes.Add(cNode);
                    }

                    LoadFavoriteMenuItems(dir, tsFavoritesMnu, cNode);
                }

                bool addlinks = (objDir.Name.Equals("links", StringComparison.CurrentCultureIgnoreCase)) ? true : false;
                FileInfo[] urls = objDir.GetFiles("*.url");
                foreach (FileInfo url in urls)
                {
                    strName = Path.GetFileNameWithoutExtension(url.Name);
                    strUrl = url.FullName;

                    if (currNode == null)
                    {
                        tsFavoritesMnu.Nodes.Add(strName, strName, 19);
                    }
                    else
                    {
                        currNode.Nodes.Add(strName, strName, 19);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #region Popup Message aka Notifier


        private int mShowSecs = 5;
        private int mFadeInMSecs = 300;
        private int mFadeOutMSecs = 300;
        private int mUseFading = 0;

        private ToastCollection m_ColToast = new ToastCollection();

        public void ShowMsg(string title, string msg, int level)
        {
            this.ShowMsg(title, msg, level, mShowSecs);
        }

        public void ShowMsg(string title, string msg, int level, int delay)
        {
            this.Invoke(new NotifierHandler(NotifierSub), new object[] { title, msg, level, delay });
        }

        delegate void NotifierHandler(string title, string msg, int level, int delay);

        public void NotifierSub(string title, string msg, int level, int delay)
        {
            TaskbarNotifier notifier = InitTaskBarNotifier(new TaskbarNotifier());
            switch (level)
            {
                case 1:
                    notifier.SetBackgroundBitmap(StaticHelperClass.GetImage("skini.png"), Color.FromArgb(255, 0, 255));
                    //notifier.SetBackgroundBitmap(new Bitmap(GetType(), "skinw.bmp"), Color.FromArgb(255, 0, 255));
                    break;
                case 2:
                    notifier.SetBackgroundBitmap(StaticHelperClass.GetImage("skini.png"), Color.FromArgb(255, 0, 255));
                    //notifier.SetBackgroundBitmap(new Bitmap(GetType(), "skine.bmp"), Color.FromArgb(255, 0, 255));
                    break;
                default:
                    notifier.SetBackgroundBitmap(StaticHelperClass.GetImage("skini.png"), Color.FromArgb(255, 0, 255));
                    //notifier.SetBackgroundBitmap(new Bitmap(GetType(), "skini.bmp"), Color.FromArgb(255, 0, 255));
                    break;
            }

            notifier.AppearBySliding = (mUseFading == 0);
            notifier.Show(title, msg, mFadeInMSecs, delay * 1000, mFadeOutMSecs);
        }

        private TaskbarNotifier InitTaskBarNotifier(TaskbarNotifier notifier)
        {
            notifier.Base = m_ColToast;
            notifier.SetCloseBitmap(StaticHelperClass.GetImage("close.bmp"), Color.FromArgb(255, 0, 255), new Point(230, 8));
            notifier.TitleRectangle = new Rectangle(45, 9, 200, 25);
            notifier.ContentRectangle = new Rectangle(45, 40, 220, 68);
            notifier.ContentTextAlignement = ContentAlignment.TopLeft;
            notifier.CloseClickable = true;
            notifier.TitleClickable = false;
            notifier.ContentClickable = false;
            notifier.EnableSelectionRectangle = false;
            notifier.KeepVisibleOnMousOver = true;
            notifier.ReShowOnMouseOver = true;
            notifier.Padding = 0;
            return notifier;
        }
        #endregion

        protected void ResizeAddress()
        {
            addressBar_Url.Width = addressBar.Width - addressBar_Back.Width - addressBar_Fwd.Width - addressBar_Refresh.Width - addressBar_Stop.Width - addressBar_Suggest.Width - addressBar_Psycare.Width - 14;
            addressBar.Refresh();
        }

        private void goHome()
        {
            if (this.mdiTabStrip1.ActiveTab != null)
            {
                ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.GoHome();
                RefreshAddressBar();
            }
        }

        private void RefreshAddressBar()
        {
            try
            {
                //记录访问历史
                LastUrlRecordEntity.CurStaySecond = (Stopwatch.GetTimestamp() - LastUrlRecordEntity.CurStaySecond) / Stopwatch.Frequency + 1;//更新停留时间
                UrlAnalysis.UpdateUrlAnalysisHistory(LastUrlRecordEntity);//更新浏览历史
                UpdateCurrentUrlRecord();
                if (this.mdiTabStrip1.ActiveTab != null)
                {
                    if (((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.Document == null)
                    {
                        this.addressBar_Url.Text = string.Empty; ;
                        addressBar_Back.Enabled = false;
                        addressBar_Fwd.Enabled = false;
                        addressBar_Refresh.Enabled = false;
                        addressBar_Stop.Enabled = false;
                        return;
                    }
                    this.addressBar_Url.Text = ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.Document.Url.ToString();

                    addressBar_Back.Enabled = ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.CanGoBack;
                    addressBar_Fwd.Enabled = ((WebView)this.mdiTabStrip1.ActiveTab.Form).WebBrowserContent.CanGoForward;
                    addressBar_Refresh.Enabled = true;
                    addressBar_Stop.Enabled = true;
                }
                else
                {
                    this.addressBar_Url.Text = string.Empty; ;
                    addressBar_Back.Enabled = false;
                    addressBar_Fwd.Enabled = false;
                    addressBar_Refresh.Enabled = false;
                    addressBar_Stop.Enabled = false;
                }
            }
            catch (System.Exception e)
            {
            	
            }
        }

       
        private void leftTabControl_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void addressBar_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            showHistory();
        }

        private void mdiTabStrip1_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_View_Go_Current_Click(object sender, EventArgs e)
        {
            
        }

        private void MenuItem_History_View_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_Help_Content_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }

        private void MenuItem_Help_Introduction_Click(object sender, EventArgs e)
        {

        }

        private void MenuItem_Tools_Calculator_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void MenuItem_Tools_MailService_Click(object sender, EventArgs e)
        {
             getCurrentBrowser().Navigate("https://www.google.com/accounts/ServiceLogin?service=mail&passive=true&rm=false&continue=http%3A%2F%2Fmail.google.com%2Fmail%2F%3Fui%3Dhtml%26zy%3Dl&bsv=llya694le36z&scc=1&ltmpl=default&ltmplcache=2");
        }

        private void MenuItem_Help_About_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void MenuItem_Help_Feedback_Click(object sender, EventArgs e)
        {
            FeedBack fb = new FeedBack();
            fb.Show();
        }

        private void MenuItem_View_Font_Largest_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("FontSize",false,5);
        }

        private void MenuItem_View_Font_Smallest_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("FontSize", false, 1);
        }

        private void MenuItem_View_Font_Smaller_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("FontSize", false, 2);
        }

        private void MenuItem_View_Font_Middle_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("FontSize", false, 3);
        }

        private void MenuItem_View_Font_Larger_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Document.ExecCommand("FontSize", false, 4);
        }

        private void MenuItem_Tools_InPrivate_Click(object sender, EventArgs e)
        {

        }

        //<summary>
        //以下五个函数是右击收藏夹时的处理函数
        //</summary>
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Navigate(adress);
        }
        private void openInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebView aForm = new WebView();
            aForm.MdiParent = this;
            aForm.WebBrowserContent.Navigate(adress);
            aForm.Show();
        }

        private void openInNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow new_form = new MainWindow();
            new_form.Show();
            new_form.getCurrentBrowser().Navigate(adress);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteLink();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renameLink();
        }

        //<summary>
        //以下五个函数是历史记录里右键点击的反应
        //</summary>

        private void openToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Navigate(adress);
        }

        private void openInNewTabToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            WebView aForm = new WebView();
            aForm.MdiParent = this;
            aForm.WebBrowserContent.Navigate(adress);
            aForm.Show();
        }

        private void openInNewWindowToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MainWindow new_form = new MainWindow();
            new_form.Show();
            new_form.getCurrentBrowser().Navigate(adress);
        }

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFavorites dlg = new AddFavorites(historyTreeView.SelectedNode.Text);
            DialogResult res = dlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                if (dlg.favFile == "Favorites")
                    addFavorit(getCurrentBrowser().Url.ToString(), dlg.favName);
                else addLink(getCurrentBrowser().Url.ToString(), dlg.favName);

                deleteHistory();
            }
            dlg.Close();
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            deleteHistory();
        }


        //<summary>
        //下面五个函数表示处理收藏夹（面板）的右键点击事件处理
        //</summary>
        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            getCurrentBrowser().Navigate(adress);
        }

        private void openInNewTabToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WebView aForm = new WebView();
            aForm.MdiParent = this;
            aForm.WebBrowserContent.Navigate(adress);
            aForm.Show();
        }

        private void openInNewWindowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainWindow new_form = new MainWindow();
            new_form.Show();
            new_form.getCurrentBrowser().Navigate(adress);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            deleteFavorit();
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            renameFavorit();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.LeftFormPanel.Visible = false;
        }



        /// <summary>
        /// 这里就是小师姐要求的心理贴士模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip_psyTip_Click(object sender, EventArgs e)
        {
            if (this.m_AutoSuggestion.Visible == false)
            {
                if (this.m_AutoSuggestion.InvokeRequired)
                {
                    SetVisibleCallback d = new SetVisibleCallback(SetVisible);
                    this.Invoke(d);
                }
                else
                {
                    SetVisible();
                }
            }
            this.m_AutoSuggestion.UpdateEmotionSuggestionContent();//更新数据
        }

        
        


       

      
        


       /* private void historyTreeView_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
        */
       
       
    }
}
