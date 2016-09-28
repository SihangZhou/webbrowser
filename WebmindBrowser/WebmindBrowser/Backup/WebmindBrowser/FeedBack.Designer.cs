namespace WebmindBrowser
{
    partial class FeedBack
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
            this.close = new System.Windows.Forms.Button();
            this.richTextBox_feedBack = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(155, 191);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 0;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // richTextBox_feedBack
            // 
            this.richTextBox_feedBack.Location = new System.Drawing.Point(41, 12);
            this.richTextBox_feedBack.Name = "richTextBox_feedBack";
            this.richTextBox_feedBack.Size = new System.Drawing.Size(326, 150);
            this.richTextBox_feedBack.TabIndex = 1;
            this.richTextBox_feedBack.Text = "";
            // 
            // FeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 241);
            this.Controls.Add(this.richTextBox_feedBack);
            this.Controls.Add(this.close);
            this.Name = "FeedBack";
            this.Text = "FeedBack";
            this.Load += new System.EventHandler(this.FeedBack_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RichTextBox richTextBox_feedBack;
    }
}