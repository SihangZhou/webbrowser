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
    public partial class AddFavorites : Form
    {

        String url;
        public String favName, favFile;

        public AddFavorites(String url)
        {
            this.url = url;
            InitializeComponent();
        }
  
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void addButton_Click(object sender, EventArgs e)
        {
            favName = textBox1.Text;
            favFile = comboBox1.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
           // this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        private void AddFavorites_Load(object sender, EventArgs e)
        {
            textBox1.Text = url;
            comboBox1.Text = comboBox1.Items[0].ToString();
        }
    }
}
