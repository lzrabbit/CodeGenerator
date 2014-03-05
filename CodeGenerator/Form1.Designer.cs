namespace CodeGenerator
{
    partial class Form1
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
            this.txtConnString = new System.Windows.Forms.TextBox();
            this.btnMySql = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.ddlCodeType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConnString
            // 
            this.txtConnString.Location = new System.Drawing.Point(65, 16);
            this.txtConnString.Name = "txtConnString";
            this.txtConnString.Size = new System.Drawing.Size(652, 21);
            this.txtConnString.TabIndex = 0;
            // 
            // btnMySql
            // 
            this.btnMySql.Location = new System.Drawing.Point(737, 16);
            this.btnMySql.Name = "btnMySql";
            this.btnMySql.Size = new System.Drawing.Size(75, 23);
            this.btnMySql.TabIndex = 1;
            this.btnMySql.Text = "MySQL";
            this.btnMySql.UseVisualStyleBackColor = true;
            this.btnMySql.Click += new System.EventHandler(this.btnMySql_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据库:";
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 73);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(180, 292);
            this.treeView.TabIndex = 3;
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
            // ddlCodeType
            // 
            this.ddlCodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCodeType.FormattingEnabled = true;
            this.ddlCodeType.Items.AddRange(new object[] {
            "POCO",
            "POJO",
            "MyBatis"});
            this.ddlCodeType.Location = new System.Drawing.Point(832, 16);
            this.ddlCodeType.Name = "ddlCodeType";
            this.ddlCodeType.Size = new System.Drawing.Size(121, 20);
            this.ddlCodeType.TabIndex = 5;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 461);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ddlCodeType);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMySql);
            this.Controls.Add(this.txtConnString);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnString;
        private System.Windows.Forms.Button btnMySql;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ComboBox ddlCodeType;
        private System.Windows.Forms.Button btnSave;
    }
}

