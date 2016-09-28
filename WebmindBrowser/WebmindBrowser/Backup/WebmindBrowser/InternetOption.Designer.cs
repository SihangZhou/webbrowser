namespace WebmindBrowser
{
    partial class InternetOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternetOption));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.num = new System.Windows.Forms.NumericUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deleteHistory = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.homepage = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox_homePage = new System.Windows.Forms.GroupBox();
            this.homePage_set = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_blank = new System.Windows.Forms.Button();
            this.button_use = new System.Windows.Forms.Button();
            this.groupBox_history = new System.Windows.Forms.GroupBox();
            this.checkBox_history = new System.Windows.Forms.CheckBox();
            this.groupBox_appearance = new System.Windows.Forms.GroupBox();
            this.button_backColor = new System.Windows.Forms.Button();
            this.button_font = new System.Windows.Forms.Button();
            this.button_foreColor = new System.Windows.Forms.Button();
            this.groupBox_urls = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numOfUrls = new System.Windows.Forms.NumericUpDown();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox_homePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox_history.SuspendLayout();
            this.groupBox_appearance.SuspendLayout();
            this.groupBox_urls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOfUrls)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(277, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button_cancel);
            this.tabPage1.Controls.Add(this.button_ok);
            this.tabPage1.Controls.Add(this.groupBox_urls);
            this.tabPage1.Controls.Add(this.groupBox_appearance);
            this.tabPage1.Controls.Add(this.groupBox_history);
            this.tabPage1.Controls.Add(this.groupBox_homePage);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(277, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(285, 313);
            this.tabControl1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.num);
            this.groupBox4.Location = new System.Drawing.Point(6, 219);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 45);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Drop Down urls";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of urls in adress bar";
            // 
            // num
            // 
            this.num.Location = new System.Drawing.Point(159, 18);
            this.num.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.num.Name = "num";
            this.num.Size = new System.Drawing.Size(102, 21);
            this.num.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button4.Location = new System.Drawing.Point(115, 270);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 21);
            this.button4.TabIndex = 11;
            this.button4.Text = "OK";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(198, 270);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 21);
            this.button3.TabIndex = 10;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Location = new System.Drawing.Point(8, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 65);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Appearance";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(98, 23);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 21);
            this.button7.TabIndex = 14;
            this.button7.Text = "Back Color";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(184, 23);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 21);
            this.button5.TabIndex = 12;
            this.button5.Text = "Fonts";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(6, 23);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 21);
            this.button6.TabIndex = 13;
            this.button6.Text = "Fore Color";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deleteHistory);
            this.groupBox2.Location = new System.Drawing.Point(6, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 42);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Browsing History";
            // 
            // deleteHistory
            // 
            this.deleteHistory.AutoSize = true;
            this.deleteHistory.Location = new System.Drawing.Point(6, 18);
            this.deleteHistory.Name = "deleteHistory";
            this.deleteHistory.Size = new System.Drawing.Size(210, 16);
            this.deleteHistory.TabIndex = 0;
            this.deleteHistory.Text = "Delete Browsing History on Exit";
            this.deleteHistory.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.homepage);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 80);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Home Page";
            // 
            // homepage
            // 
            this.homepage.Location = new System.Drawing.Point(46, 18);
            this.homepage.Name = "homepage";
            this.homepage.Size = new System.Drawing.Size(219, 21);
            this.homepage.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(175, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 4;
            this.button2.Text = "Use Blank";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "Use Current";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 28);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox_homePage
            // 
            this.groupBox_homePage.Controls.Add(this.homePage_set);
            this.groupBox_homePage.Controls.Add(this.pictureBox2);
            this.groupBox_homePage.Controls.Add(this.button_blank);
            this.groupBox_homePage.Controls.Add(this.button_use);
            this.groupBox_homePage.Location = new System.Drawing.Point(3, 0);
            this.groupBox_homePage.Name = "groupBox_homePage";
            this.groupBox_homePage.Size = new System.Drawing.Size(271, 80);
            this.groupBox_homePage.TabIndex = 8;
            this.groupBox_homePage.TabStop = false;
            this.groupBox_homePage.Text = "主页";
            // 
            // homePage_set
            // 
            this.homePage_set.Location = new System.Drawing.Point(46, 18);
            this.homePage_set.Name = "homePage_set";
            this.homePage_set.Size = new System.Drawing.Size(219, 21);
            this.homePage_set.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 28);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // button_blank
            // 
            this.button_blank.Location = new System.Drawing.Point(175, 42);
            this.button_blank.Name = "button_blank";
            this.button_blank.Size = new System.Drawing.Size(75, 21);
            this.button_blank.TabIndex = 4;
            this.button_blank.Text = "使用空白页";
            this.button_blank.UseVisualStyleBackColor = true;
            this.button_blank.Click += new System.EventHandler(this.button_blank_Click);
            // 
            // button_use
            // 
            this.button_use.Location = new System.Drawing.Point(94, 42);
            this.button_use.Name = "button_use";
            this.button_use.Size = new System.Drawing.Size(75, 21);
            this.button_use.TabIndex = 3;
            this.button_use.Text = "使用当前页";
            this.button_use.UseVisualStyleBackColor = true;
            this.button_use.Click += new System.EventHandler(this.button_use_Click);
            // 
            // groupBox_history
            // 
            this.groupBox_history.Controls.Add(this.checkBox_history);
            this.groupBox_history.Location = new System.Drawing.Point(3, 89);
            this.groupBox_history.Name = "groupBox_history";
            this.groupBox_history.Size = new System.Drawing.Size(265, 42);
            this.groupBox_history.TabIndex = 9;
            this.groupBox_history.TabStop = false;
            this.groupBox_history.Text = "浏览历史";
            // 
            // checkBox_history
            // 
            this.checkBox_history.AutoSize = true;
            this.checkBox_history.Location = new System.Drawing.Point(6, 18);
            this.checkBox_history.Name = "checkBox_history";
            this.checkBox_history.Size = new System.Drawing.Size(120, 16);
            this.checkBox_history.TabIndex = 0;
            this.checkBox_history.Text = "删除浏览历史记录";
            this.checkBox_history.UseVisualStyleBackColor = true;
            // 
            // groupBox_appearance
            // 
            this.groupBox_appearance.Controls.Add(this.button_backColor);
            this.groupBox_appearance.Controls.Add(this.button_font);
            this.groupBox_appearance.Controls.Add(this.button_foreColor);
            this.groupBox_appearance.Location = new System.Drawing.Point(3, 137);
            this.groupBox_appearance.Name = "groupBox_appearance";
            this.groupBox_appearance.Size = new System.Drawing.Size(265, 65);
            this.groupBox_appearance.TabIndex = 10;
            this.groupBox_appearance.TabStop = false;
            this.groupBox_appearance.Text = "外观";
            // 
            // button_backColor
            // 
            this.button_backColor.Location = new System.Drawing.Point(98, 23);
            this.button_backColor.Name = "button_backColor";
            this.button_backColor.Size = new System.Drawing.Size(75, 21);
            this.button_backColor.TabIndex = 14;
            this.button_backColor.Text = "背景色";
            this.button_backColor.UseVisualStyleBackColor = true;
            this.button_backColor.Click += new System.EventHandler(this.button_backColor_Click);
            // 
            // button_font
            // 
            this.button_font.Location = new System.Drawing.Point(184, 23);
            this.button_font.Name = "button_font";
            this.button_font.Size = new System.Drawing.Size(75, 21);
            this.button_font.TabIndex = 12;
            this.button_font.Text = "字体";
            this.button_font.UseVisualStyleBackColor = true;
            this.button_font.Click += new System.EventHandler(this.button_font_Click);
            // 
            // button_foreColor
            // 
            this.button_foreColor.Location = new System.Drawing.Point(6, 23);
            this.button_foreColor.Name = "button_foreColor";
            this.button_foreColor.Size = new System.Drawing.Size(75, 21);
            this.button_foreColor.TabIndex = 13;
            this.button_foreColor.Text = "前景色";
            this.button_foreColor.UseVisualStyleBackColor = true;
            this.button_foreColor.Click += new System.EventHandler(this.button_foreColor_Click);
            // 
            // groupBox_urls
            // 
            this.groupBox_urls.Controls.Add(this.label2);
            this.groupBox_urls.Controls.Add(this.numOfUrls);
            this.groupBox_urls.Location = new System.Drawing.Point(3, 208);
            this.groupBox_urls.Name = "groupBox_urls";
            this.groupBox_urls.Size = new System.Drawing.Size(267, 45);
            this.groupBox_urls.TabIndex = 13;
            this.groupBox_urls.TabStop = false;
            this.groupBox_urls.Text = "下拉urls";
            this.groupBox_urls.Enter += new System.EventHandler(this.g);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "地址栏的urls";
            // 
            // numOfUrls
            // 
            this.numOfUrls.Location = new System.Drawing.Point(159, 18);
            this.numOfUrls.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numOfUrls.Name = "numOfUrls";
            this.numOfUrls.Size = new System.Drawing.Size(102, 21);
            this.numOfUrls.TabIndex = 0;
            // 
            // button_ok
            // 
            this.button_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_ok.Location = new System.Drawing.Point(84, 259);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 21);
            this.button_ok.TabIndex = 14;
            this.button_ok.Text = "确定";
            this.button_ok.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(176, 260);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 21);
            this.button_cancel.TabIndex = 15;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // InternetOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 318);
            this.Controls.Add(this.tabControl1);
            this.Name = "InternetOption";
            this.Text = "Internet选项";
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox_homePage.ResumeLayout(false);
            this.groupBox_homePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox_history.ResumeLayout(false);
            this.groupBox_history.PerformLayout();
            this.groupBox_appearance.ResumeLayout(false);
            this.groupBox_urls.ResumeLayout(false);
            this.groupBox_urls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOfUrls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown num;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox deleteHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox homepage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.GroupBox groupBox_urls;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numOfUrls;
        private System.Windows.Forms.GroupBox groupBox_appearance;
        private System.Windows.Forms.Button button_backColor;
        private System.Windows.Forms.Button button_font;
        private System.Windows.Forms.Button button_foreColor;
        private System.Windows.Forms.GroupBox groupBox_history;
        public System.Windows.Forms.CheckBox checkBox_history;
        private System.Windows.Forms.GroupBox groupBox_homePage;
        public System.Windows.Forms.TextBox homePage_set;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button_blank;
        private System.Windows.Forms.Button button_use;
        private System.Windows.Forms.Button button_cancel;
    }
}