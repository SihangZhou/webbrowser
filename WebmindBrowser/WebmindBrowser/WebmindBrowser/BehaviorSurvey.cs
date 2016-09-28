using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebmindBrowser
{
    public partial class BehaviorSurvey : Form
    {
        public BehaviorSurvey()
        {
            InitializeComponent();
            InitializeQuestions();
        }

        private void InitializeQuestions()
        {
//#region 第一页 测试题 

//#region  第一题
//            this.panelFirst.Controls.Add(this.label3);
//            this.panelFirst.Controls.Add(this.radioWeekendA);
//            this.panelFirst.Controls.Add(this.radioWeekendB);
//            this.panelFirst.Controls.Add(this.radioWeekendC);
//            this.panelFirst.Controls.Add(this.radioWeekendD);
//            this.panelFirst.Controls.Add(this.radioWeekendE);
//            this.panelFirst.Controls.Add(this.radioWeekendF);
//#endregion

//#region  第二题 
//            this.panelFirst.Controls.Add(this.label4);
//            this.panelFirst.Controls.Add(this.radioWorkDay2);
//            this.panelFirst.Controls.Add(this.radioWorkDay2_6);
//            this.panelFirst.Controls.Add(this.radioWorkDay6_10);
//            this.panelFirst.Controls.Add(this.radioWorkDay10_12);
//            this.panelFirst.Controls.Add(this.radioWorkDay12);
//#endregion

//#region  第三题

//            this.panelFirst.Controls.Add(this.label5);
//            this.panelFirst.Controls.Add(this.radioStartTimeA);
//            this.panelFirst.Controls.Add(this.radioStartTimeB);
//            this.panelFirst.Controls.Add(this.radioStartTimeC);
//            this.panelFirst.Controls.Add(this.radioStartTimeD);
//            this.panelFirst.Controls.Add(this.radioStartTimeE);
//            this.panelFirst.Controls.Add(this.radioStartTimeF);
//            this.panelFirst.Controls.Add(this.radioStartTimeG);
//#endregion

//#region  第四题
//            this.panelFirst.Controls.Add(this.label6);
//            this.panelFirst.Controls.Add(this.radioStartBrowsingA);
//            this.panelFirst.Controls.Add(this.radioStartBrowsingB);
//            this.panelFirst.Controls.Add(this.radioStartBrowsingC);
//            this.panelFirst.Controls.Add(this.radioStartBrowsingD);
//#endregion

//#region  第五题 
//            this.panelFirst.Controls.Add(this.label7);
//            this.panelFirst.Controls.Add(this.radioStaySecondA);
//            this.panelFirst.Controls.Add(this.radioStaySecondB);
//            this.panelFirst.Controls.Add(this.radioStaySecondC);
//            this.panelFirst.Controls.Add(this.radioStaySecondD);
//            this.panelFirst.Controls.Add(this.radioStaySecondE);
//            this.panelFirst.Controls.Add(this.radioStaySecondF);
//#endregion
//            #region 第六题 
//            this.panelFirst.Controls.Add(this.label8);
//            this.panelFirst.Controls.Add(this.radioSearchCountA);
//            this.panelFirst.Controls.Add(this.radioSearchCountB);
//            this.panelFirst.Controls.Add(this.radioSearchCountC);
//            this.panelFirst.Controls.Add(this.radioSearchCountD);
//            this.panelFirst.Controls.Add(this.radioSearchCountE);
//            this.panelFirst.Controls.Add(this.radioSearchCountF);

//            #endregion

//#region  第七题 
//            this.panelFirst.Controls.Add(this.label9);
//            this.panelFirst.Controls.Add(this.checkSearchMethodA);
//            this.panelFirst.Controls.Add(this.checkSearchMethodB);
//            this.panelFirst.Controls.Add(this.checkSearchMethodC);
//            this.panelFirst.Controls.Add(this.checkSearchMethodD);
//            this.panelFirst.Controls.Add(this.checkSearchMethodE);
//#endregion

//#region  第八题 
//            this.panelFirst.Controls.Add(this.label10);
//            this.panelFirst.Controls.Add(this.radioWatchVideoA);
//            this.panelFirst.Controls.Add(this.radioWatchVideoB);
//            this.panelFirst.Controls.Add(this.radioWatchVideoC);
//            this.panelFirst.Controls.Add(this.radioWatchVideoD);
//            this.panelFirst.Controls.Add(this.radioWatchVideoE);
//            this.panelFirst.Controls.Add(this.radioWatchVideoF);
//#endregion
//#endregion

        }
        private void BehaviorSurvey_Load(object sender, EventArgs e)
        {

        }

        private void buttonPre_Click(object sender, EventArgs e)
        {
            this.panelFirst.Visible = false;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            this.panelFirst.Visible = true;
        }
    }
}
