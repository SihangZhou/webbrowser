namespace WebmindBrowser
{
    partial class EmotionTracking
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.zedGraphEmotion = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // zedGraphEmotion
            // 
            this.zedGraphEmotion.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.zedGraphEmotion.AutoSize = true;
            this.zedGraphEmotion.BackColor = System.Drawing.SystemColors.ControlDark;
            this.zedGraphEmotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphEmotion.Location = new System.Drawing.Point(0, 0);
            this.zedGraphEmotion.Name = "zedGraphEmotion";
            this.zedGraphEmotion.ScrollGrace = 0;
            this.zedGraphEmotion.ScrollMaxX = 0;
            this.zedGraphEmotion.ScrollMaxY = 0;
            this.zedGraphEmotion.ScrollMaxY2 = 0;
            this.zedGraphEmotion.ScrollMinX = 0;
            this.zedGraphEmotion.ScrollMinY = 0;
            this.zedGraphEmotion.ScrollMinY2 = 0;
            this.zedGraphEmotion.Size = new System.Drawing.Size(527, 267);
            this.zedGraphEmotion.TabIndex = 2;
            this.zedGraphEmotion.ZoomButtons = System.Windows.Forms.MouseButtons.Right;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zedGraphEmotion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 267);
            this.panel1.TabIndex = 0;
            // 
            // EmotionTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(527, 267);
            this.Controls.Add(this.panel1);
            this.Name = "EmotionTracking";
            this.Text = "   ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private ZedGraph.ZedGraphControl zedGraphEmotion;
        private System.Windows.Forms.Panel panel1;


    }
}