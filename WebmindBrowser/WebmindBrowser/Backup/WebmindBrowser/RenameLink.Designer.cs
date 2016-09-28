namespace WebmindBrowser
{
    partial class RenameLink
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
            this.label1 = new System.Windows.Forms.Label();
            this.newName = new System.Windows.Forms.TextBox();
            this.rename_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "新名字:";
            // 
            // newName
            // 
            this.newName.Location = new System.Drawing.Point(104, 12);
            this.newName.Name = "newName";
            this.newName.Size = new System.Drawing.Size(201, 21);
            this.newName.TabIndex = 1;
            // 
            // rename_button
            // 
            this.rename_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.rename_button.Location = new System.Drawing.Point(64, 49);
            this.rename_button.Name = "rename_button";
            this.rename_button.Size = new System.Drawing.Size(75, 23);
            this.rename_button.TabIndex = 2;
            this.rename_button.Text = "确定";
            this.rename_button.UseVisualStyleBackColor = true;
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(183, 49);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 3;
            this.cancel_button.Text = "取消";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // RenameLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 94);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.rename_button);
            this.Controls.Add(this.newName);
            this.Controls.Add(this.label1);
            this.Name = "RenameLink";
            this.Text = "链接重命名";
            this.Load += new System.EventHandler(this.RenameLink_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox newName;
        public System.Windows.Forms.Button rename_button;
        public System.Windows.Forms.Button cancel_button;
    }
}