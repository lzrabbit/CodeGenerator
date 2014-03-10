namespace CodeGenerator
{
    partial class SimpleCoder
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuConn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCode = new System.Windows.Forms.ToolStripMenuItem();
            this.pOCOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOJOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myBatisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 73);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(180, 292);
            this.treeView.TabIndex = 3;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.SystemColors.WindowText;
            this.txtCode.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtCode.Location = new System.Drawing.Point(223, 83);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCode.Size = new System.Drawing.Size(730, 292);
            this.txtCode.TabIndex = 4;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(544, 401);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "另存为";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConn,
            this.menuCode});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1071, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuConn
            // 
            this.menuConn.Name = "menuConn";
            this.menuConn.Size = new System.Drawing.Size(68, 21);
            this.menuConn.Text = "新建链接";
            this.menuConn.Click += new System.EventHandler(this.menuConn_Click);
            // 
            // menuCode
            // 
            this.menuCode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOCOToolStripMenuItem,
            this.pOJOToolStripMenuItem,
            this.myBatisToolStripMenuItem});
            this.menuCode.Name = "menuCode";
            this.menuCode.Size = new System.Drawing.Size(44, 21);
            this.menuCode.Text = "代码";
            // 
            // pOCOToolStripMenuItem
            // 
            this.pOCOToolStripMenuItem.Name = "pOCOToolStripMenuItem";
            this.pOCOToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pOCOToolStripMenuItem.Text = "POCO";
            this.pOCOToolStripMenuItem.Click += new System.EventHandler(this.CodeMenuItem_Click);
            // 
            // pOJOToolStripMenuItem
            // 
            this.pOJOToolStripMenuItem.Name = "pOJOToolStripMenuItem";
            this.pOJOToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pOJOToolStripMenuItem.Text = "POJO";
            this.pOJOToolStripMenuItem.Click += new System.EventHandler(this.CodeMenuItem_Click);
            // 
            // myBatisToolStripMenuItem
            // 
            this.myBatisToolStripMenuItem.Name = "myBatisToolStripMenuItem";
            this.myBatisToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.myBatisToolStripMenuItem.Text = "MyBatis";
            this.myBatisToolStripMenuItem.Click += new System.EventHandler(this.CodeMenuItem_Click);
            // 
            // SimpleCoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 461);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimpleCoder";
            this.Text = "SimpleCode";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuConn;
        private System.Windows.Forms.ToolStripMenuItem menuCode;
        private System.Windows.Forms.ToolStripMenuItem pOCOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOJOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myBatisToolStripMenuItem;
    }
}

