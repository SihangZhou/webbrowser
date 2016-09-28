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
    public partial class FeedBack : Form
    {
        public FeedBack()
        {
            InitializeComponent();
        }

        private void FeedBack_Load(object sender, EventArgs e)
        {
            this.richTextBox_feedBack.Text = "您可以将您使用WebMind系统的不满或者心得发送给\r\t\tginobilinie@gmail.com\n我们当前还没实现不满意见主页，所以只能发送给邮箱了\n谢谢您";
            this.richTextBox_feedBack.ReadOnly = true;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
