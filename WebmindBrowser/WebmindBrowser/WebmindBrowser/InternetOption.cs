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
    public partial class InternetOption : Form
    {
        String adresa;
        public Font font;
        public Color forecolor, backcolor;
        public InternetOption(String adresa)
        {
            this.adresa = adresa;
            InitializeComponent();
        }

        private void button_use_Click(object sender, EventArgs e)
        {
            homePage_set.Text = adresa;
        }

        private void button_blank_Click(object sender, EventArgs e)
        {
            homePage_set.Text="about:blank";
        }

        private void button_foreColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                forecolor = c.Color;
        }

        private void button_backColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                backcolor = c.Color;
        }

        private void button_font_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                font = dlg.Font;
        }

        private void g(object sender, EventArgs e)
        {

        }
    }
}
