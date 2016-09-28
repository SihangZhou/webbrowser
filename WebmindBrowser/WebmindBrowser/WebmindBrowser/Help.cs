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
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            this.richTextBox_help.Text = "欢迎使用WebMind系统，帮助系统暂时还没开发出来，请您耐心等待\n目前处于测试阶段，我先给出下面这些功能我还没能实现:\n  编辑->在此页上查找(这个我代码已经写出，只是我认为实现的很垃圾，想改写)\n  查看->浏览器栏->源  查看->缩放  查看->编码  查看->语言\n  收藏夹->添加到收藏夹栏(这个实现起来很简单，只是我主观认为浏览器当前趋势是简洁，所以我个人没有做收藏夹栏)\n  工具->Inprivate系列  工具->弹出窗口程序  工具->管理加载项\n\t\t谢谢大家):";
            this.richTextBox_help.ReadOnly = true;
        }
    }
}
