namespace WebmindBrowser
{
    partial class FindInPage
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
            this.lastOne = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nextOne = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lastOne
            // 
            this.lastOne.Location = new System.Drawing.Point(66, 108);
            this.lastOne.Name = "lastOne";
            this.lastOne.Size = new System.Drawing.Size(75, 23);
            this.lastOne.TabIndex = 0;
            this.lastOne.Text = "上一个";
            this.lastOne.UseVisualStyleBackColor = true;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(81, 34);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(220, 21);
            this.search.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "查找:";
            // 
            // nextOne
            // 
            this.nextOne.Location = new System.Drawing.Point(188, 108);
            this.nextOne.Name = "nextOne";
            this.nextOne.Size = new System.Drawing.Size(75, 23);
            this.nextOne.TabIndex = 3;
            this.nextOne.Text = "下一个";
            this.nextOne.UseVisualStyleBackColor = true;
            this.nextOne.Click += new System.EventHandler(this.nextOne_Click);
            // 
            // FindInPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 201);
            this.Controls.Add(this.nextOne);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search);
            this.Controls.Add(this.lastOne);
            this.Name = "FindInPage";
            this.Text = "FindInPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button lastOne;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextOne;
    }
}