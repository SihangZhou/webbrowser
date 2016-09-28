using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using WebmindBrowser.Analysis;

namespace WebmindBrowser
{
    public partial class EmotionTracking : Form
    {
        public static PointPairList SOMValueList = new PointPairList();//躯体化跟踪数值
        public static PointPairList DEPValueList = new PointPairList();//抑郁跟踪数值
        public static PointPairList ANXValueList = new PointPairList();//焦虑跟踪数值
        public static PointPairList PSDValueList = new PointPairList();//病态人格跟踪数值
        public static PointPairList HYPValueList = new PointPairList();//疑心跟踪数值
        public static PointPairList UNRValueList = new PointPairList();//脱离现实数值跟踪
        public static PointPairList HMAValueList = new PointPairList();//兴奋状态数值跟踪

        private MainWindow Parent;
        public static double[] y5=new double[7]{0,0,0,0,0,0,0};
        //private double[] x5 = new double[7]{0,1,2,3,4,5,6};
        double[] x5 = { 0, 1, 2, 3, 4, 5, 6, 7 };
        delegate void RefreshGraphEmotionCallback();

        public EmotionTracking()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }
        public EmotionTracking(MainWindow father)
        {
            this.Parent = father;
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;

            InitalEmotionTracking();

            UpdateEmotionTracking();
        }

        private void InitalEmotionTracking()
        {
           // Recommendation.dic_ANX_Predictions.Last().Value();

            //double xAxis = (double)new XDate(DateTime.FromBinary(item.Key));
            double yANX = Recommendation.dic_ANX_Predictions.Last().Value;
            double yDEP = Recommendation.dic_DEP_Predictions.Last().Value;
            double yHMA = Recommendation.dic_HMA_Predictions.Last().Value;
            double yHYP = Recommendation.dic_HYP_Predictions.Last().Value;
            double yPSD = Recommendation.dic_PSD_Predictions.Last().Value;
            double ySOM = Recommendation.dic_SOM_Predictions.Last().Value;
            double yUNR = Recommendation.dic_UNR_Predictions.Last().Value;
            y5[0] = yANX;
            y5[1] = yDEP;
            y5[2] = yHMA;
            y5[3] = yHYP;
            y5[4] = yPSD;
            y5[5] = ySOM;
            y5[6] = yUNR;

/*
            foreach (KeyValuePair<long, double> item in Recommendation.dic_ANX_Predictions)
            {
                double xAxis = (double)new XDate(DateTime.FromBinary(item.Key));
                double yANX = Recommendation.dic_ANX_Predictions[item.Key];
                double yDEP = Recommendation.dic_DEP_Predictions[item.Key];
                double yHMA = Recommendation.dic_HMA_Predictions[item.Key];
                double yHYP = Recommendation.dic_HYP_Predictions[item.Key];
                double yPSD = Recommendation.dic_PSD_Predictions[item.Key];
                double ySOM = Recommendation.dic_SOM_Predictions[item.Key];
                double yUNR = Recommendation.dic_UNR_Predictions[item.Key];

                
                //这里，之所以这么做，是因为模拟数据时发现数据太小，显现不出来效果，故先模拟着,bar图数据
                if (yANX>y5[0])
                    y5[0]=yANX;
                if (yDEP>y5[1])
                    y5[1]=yDEP;
                if (yHMA > y5[2])
                    y5[2]=yHMA;
                if (yHYP > y5[3])
                    y5[3]=yHYP;
                if (yPSD > y5[4])
                    y5[4]=yPSD;
                if (ySOM > y5[5])
                    y5[5] = ySOM;
                if (yUNR > y5[6])
                    y5[6] = yUNR;
                
            }
 * */
            normalize(y5,7);
            int cnts = 0;
            double[] test=new double[7];
            foreach (KeyValuePair<String, double> item in BehaviorFeatureHistoryAnalysis.somatization)
            {
                
                double xAxis = (double)new XDate(Convert.ToDateTime(item.Key));
                x5[cnts] = xAxis;
                double som = BehaviorFeatureHistoryAnalysis.somatization[item.Key];//躯体化
                double anx = BehaviorFeatureHistoryAnalysis.anxiety[item.Key];//焦虑
                double psy = BehaviorFeatureHistoryAnalysis.psychopathic[item.Key];//病态人格
                double sus = BehaviorFeatureHistoryAnalysis.suspicion[item.Key];//疑心
                double outof = BehaviorFeatureHistoryAnalysis.outofreality[item.Key];//脱离现实行为
                double exc = BehaviorFeatureHistoryAnalysis.exciting[item.Key];//兴奋状态模型
                double dep = BehaviorFeatureHistoryAnalysis.depression[item.Key];//抑郁状态模型

                /*Here is just to test the normalization,in fact,the data is not ok*/
                test[0] = som;
                test[1] = anx;
                test[2] = psy;
                test[3] = sus;
                test[4] = outof;
                test[5] = exc;
                test[6] = dep;
                normalize(test, 7);


                ANXValueList.Add(cnts, test[0]);
                DEPValueList.Add(cnts, test[1]);
                HMAValueList.Add(cnts, test[2]);
                HYPValueList.Add(cnts, test[3]);
                PSDValueList.Add(cnts, test[4]);
                SOMValueList.Add(cnts, test[5]);
                UNRValueList.Add(cnts, test[6]);
                //这里,可能是数据与phi的数据比较落差较大，因此合成不了,可以做归一化，使得数据全都在0到1之间

                cnts++;
                if (cnts == 7)//其实，这里都没必要要了，因为每次我都控制了dictionary里只有最近的7次行为特征值
                    break;

              

            }
        }
        
        // to normalize a list of data using:(x-min)/(max-min);
        private void normalize(double[] arr,int len)
        {
            double max = 0, min = 0,con;
            for (int i=0;i<len;i++)
            {
                if (max < arr[i])
                    max = arr[i];
                else if (min > arr[i])
                    min = arr[i];
            }
            con = max - min;
            for (int i = 0; i < len; i++)
                arr[i] = (arr[i] - min) / con;
        }


        // Call this method from the Form_Load method, passing your ZedGraphControl instance
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

            double[] x4 = { 0, 1, 2, 3, 4, 5, 6, 7 };
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
                DEPValueList, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            //myCurve.Symbol.Size = 4.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Line.Width = 4.0F;

            // Generate a green curve with square symbols, and "Distance" in the legend
            myCurve = myPane.AddCurve("Distance",
              // vList, Color.Green, SymbolType.Square);
            ANXValueList, Color.Green, SymbolType.Square);
            // Fill the symbols with white
            //myCurve.Symbol.Size = 4.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            
            // Associate this curve with the second Y axis
            myCurve.YAxisIndex = 1;
            myCurve.Line.Width = 4.0F;

            // Generate a Black curve with triangle symbols, and "Energy" in the legend
            myCurve = myPane.AddCurve("Energy",
            //   eList, Color.Black, SymbolType.Triangle);
            HMAValueList, Color.Black, SymbolType.Triangle);
            // Fill the symbols with white
            //myCurve.Symbol.Size = 4.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            
            // Associate this curve with the Y2 axis
            myCurve.IsY2Axis = false;
            // Associate this curve with the second Y2 axis
            myCurve.YAxisIndex = 1;
            myCurve.Line.Width = 4.0F;




            //double[] x4 = { 0, 1, 2, 3, 4, 5, 6, 7 };
            // double[] x5 = { 10,11,12,13,14,15,16,17};
            double[] y4 = { 30, 45, 53, 60, 45, 53, 24 };
           // normalize(y4, 7);
            BarItem bar = myPane.AddBar("PHI", x4, y5, Color.SteelBlue);
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



        public static void UpdateEmotionTrackingPointPairList()
        {

        }

        private void RefreshGraphEmotion()
        {
            this.zedGraphEmotion.Refresh();
        }

        public void UpdateEmotionTracking()
        {
            CreateMultiYChart(this.zedGraphEmotion);//这里曲线图完全是模拟，因为行为特征值我还没法获取

           /*  
            GraphPane trackingPane = this.zedGraphEmotion.GraphPane;
           trackingPane.XAxis.Type = AxisType.Date;
            trackingPane.GraphObjList.Clear();
            trackingPane.CurveList.Clear();
            trackingPane.Title.Text = "心理状况跟踪";
            trackingPane.XAxis.Title.Text = "时间";
            trackingPane.YAxis.Title.Text = "指标值";

            LineItem line1 = trackingPane.AddCurve("躯体化", SOMValueList, Color.Red, SymbolType.Circle);
            //LineItem line2 = trackingPane.AddCurve("抑郁", DEPValueList, Color.Silver, SymbolType.Circle);
            LineItem line3 = trackingPane.AddCurve("焦虑", ANXValueList, Color.DarkBlue, SymbolType.Circle);
            LineItem line4 = trackingPane.AddCurve("病态人格", PSDValueList, Color.Blue, SymbolType.Circle);
            LineItem line5 = trackingPane.AddCurve("疑心", HYPValueList, Color.Green, SymbolType.Circle);
            LineItem line6 = trackingPane.AddCurve("脱离现实", UNRValueList, Color.DarkCyan, SymbolType.Circle);
            LineItem line7 = trackingPane.AddCurve("兴奋状态", HMAValueList, Color.DeepPink, SymbolType.Circle);
            zedGraphEmotion.AxisChange();
            //zedGraphEmotion.Refresh();

            if (this.zedGraphEmotion.InvokeRequired)
            {
                RefreshGraphEmotionCallback d = new RefreshGraphEmotionCallback(RefreshGraphEmotion);
                while (!this.IsHandleCreated)
                {
                    ;
                }

                this.Invoke(d);
            }
            else
            {
                RefreshGraphEmotion();
            }
              */
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Normal;
            this.Hide();
            timer1.Stop();
        }

        public void Show(int lasting)
        {
            this.timer1.Start();
            this.timer1.Interval = lasting;            
            thisRefresh();
            base.Show();
            this.Parent.m_emotionSuggestion.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thisRefresh();
        }

        public void thisRefresh()
        {
            this.Width = Parent.Width / 3;
            this.Height = Parent.Height / 3;
            this.Top = Parent.Top + 2 * Parent.Height / 3 - 35;
            this.Left = Parent.Left + 2 * Parent.Width / 3 - 20;
            selfRefresh();
        }

        public void selfRefresh()
        {
            this.zedGraphEmotion.Height = this.Height * 20/ 30;
            Refresh();        
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            selfRefresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            this.Parent.m_emotionSuggestion.Show();
            timer1.Stop();
        }
    }
}
