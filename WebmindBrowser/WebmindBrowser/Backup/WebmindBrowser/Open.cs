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
    public partial class Open : Form
    {
        WebBrowser wb;
        Form parent;

        public Open(WebBrowser wb)
        {
            this.wb = wb;
            //wb.WebBrowserContent;
            InitializeComponent();
        }

        public Open(WebBrowser wb, Form father)
        {
            this.wb = wb;
            //wb.WebBrowserContent;
            InitializeComponent();
            parent = father;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wb.Navigate(textBox1.Text);
            this.Close();
            /*  parent.Enabled = true;
              parent.Show();
            */
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            /*   parent.Enabled = true;
              parent.Show();
            */
        }
    }
}
