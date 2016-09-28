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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.richTextBox_about.Text = "欢迎使用WindMind系统，WebMind系统简介：我们建立了一个WebMind系统为实验平台。利用训练获得的网络使用行为与心理健康的相关性模型，WebMind系统实现了对用户的网络使用行为的跟踪记录、心理健康状态的实时预测以及干预调节建议的推荐。通过两周时间的用户实验表明，70.3%的干预建议对用户是有帮助的。证明我们的系统能够准确地预测用户心理健康状态并给出有效的干预建议。";
            this.richTextBox_about.ReadOnly = true;
        }
    }
}
