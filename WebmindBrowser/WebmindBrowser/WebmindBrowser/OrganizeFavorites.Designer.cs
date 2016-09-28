namespace WebmindBrowser
{
    partial class OrganizeFavorites
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Links");
            this.organizeFavTreeView = new System.Windows.Forms.TreeView();
            this.rename_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.canel_button = new System.Windows.Forms.Button();
            this.organizeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // organizeFavTreeView
            // 
            this.organizeFavTreeView.Location = new System.Drawing.Point(12, 12);
            this.organizeFavTreeView.Name = "organizeFavTreeView";
            treeNode2.Name = "Links";
            treeNode2.Text = "Links";
            this.organizeFavTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.organizeFavTreeView.Size = new System.Drawing.Size(250, 275);
            this.organizeFavTreeView.TabIndex = 0;
            this.organizeFavTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.organizeFavTreeView_AfterSelect);
            this.organizeFavTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.organizeFavTreeView_NodeMouseClick);
            // 
            // rename_button
            // 
            this.rename_button.Location = new System.Drawing.Point(280, 40);
            this.rename_button.Name = "rename_button";
            this.rename_button.Size = new System.Drawing.Size(75, 23);
            this.rename_button.TabIndex = 1;
            this.rename_button.Text = "重命名";
            this.rename_button.UseVisualStyleBackColor = true;
            this.rename_button.Click += new System.EventHandler(this.rename_button_Click);
            // 
            // delete_button
            // 
            this.delete_button.Location = new System.Drawing.Point(280, 99);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(75, 23);
            this.delete_button.TabIndex = 2;
            this.delete_button.Text = "删除";
            this.delete_button.UseVisualStyleBackColor = true;
            this.delete_button.Click += new System.EventHandler(this.delete_button_Click);
            // 
            // canel_button
            // 
            this.canel_button.Location = new System.Drawing.Point(280, 241);
            this.canel_button.Name = "canel_button";
            this.canel_button.Size = new System.Drawing.Size(75, 23);
            this.canel_button.TabIndex = 3;
            this.canel_button.Text = "取消";
            this.canel_button.UseVisualStyleBackColor = true;
            this.canel_button.Click += new System.EventHandler(this.canel_button_Click);
            // 
            // organizeContextMenu
            // 
            this.organizeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.organizeContextMenu.Name = "organizeContextMenu";
            this.organizeContextMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameToolStripMenuItem.Text = "重命名";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "删除";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // OrganizeFavorites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 290);
            this.Controls.Add(this.canel_button);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.rename_button);
            this.Controls.Add(this.organizeFavTreeView);
            this.Name = "OrganizeFavorites";
            this.Text = "OrganizeFavorites";
            this.Load += new System.EventHandler(this.OrganizeFavorites_Load);
            this.organizeContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView organizeFavTreeView;
        private System.Windows.Forms.Button rename_button;
        private System.Windows.Forms.Button delete_button;
        private System.Windows.Forms.Button canel_button;
        private System.Windows.Forms.ContextMenuStrip organizeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}