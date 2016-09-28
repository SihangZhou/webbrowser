
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebmindBrowser
{

    partial class WebView
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
            this.WebBrowserContent = new WebmindBrowser.ExtendedWebBrowser();
            this.SuspendLayout();
            // 
            // WebBrowserContent
            // 
            this.WebBrowserContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowserContent.Location = new System.Drawing.Point(0, 0);
            this.WebBrowserContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.WebBrowserContent.Name = "WebBrowserContent";
            this.WebBrowserContent.Size = new System.Drawing.Size(840, 555);
            this.WebBrowserContent.TabIndex = 0;
            this.WebBrowserContent.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowserContent_Navigating);
            this.WebBrowserContent.StartNewWindow += new System.EventHandler<WebmindBrowser.BrowserExtendedNavigatingEventArgs>(this.WebBrowserContent_StartNewWindow);//每次选中那个新窗口的tab(或者直接点击某个link)，就会引起这个调用
          //  this.WebBrowserContent.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowserContent_NewWindow);
            this.WebBrowserContent.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowserContent_DocumentCompleted);
            this.WebBrowserContent.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowserContent_Navigated);
            // 
            // WebView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(840, 555);
            this.Controls.Add(this.WebBrowserContent);
            this.Name = "WebView";
            this.Text = "WebBrowserContent";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public ExtendedWebBrowser WebBrowserContent;

    }
}