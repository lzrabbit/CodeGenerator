using CodeGenerator.Core;
using CodeGenerator.Core.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenerator
{
    public partial class SimpleCoder : Form
    {
        public SimpleCoder()
        {
            InitializeComponent();
        }

        IDB DB { get; set; }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConns();
            ((ToolStripMenuItem)this.menuCode.DropDownItems[0]).Checked = true;
        }

        public void LoadConns()
        {
            this.treeView.Nodes.Clear();
            var conns = DbConnection.Load();
            conns.ForEach(conn =>
            {
                this.treeView.Nodes.Add(new TreeNode(string.Format("{0} {1}", conn.DbType, conn.Server)) { ToolTipText = ENodeType.DbConnection.ToString(), Tag = conn });
            });
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DB = ((DbConnection)GetTopNode(e.Node).Tag).GetDB();
            e.Node.Nodes.Clear();

            ENodeType nodeType;
            Enum.TryParse<ENodeType>(e.Node.ToolTipText, out nodeType);
            switch (nodeType)
            {
                case ENodeType.DbConnection:
                    DB.GetDatabases().ForEach(db =>
                    {
                        e.Node.Nodes.Add(new TreeNode(db) { ToolTipText = ENodeType.DbDataBase.ToString(), Tag = db });
                    });
                    break;
                case ENodeType.DbDataBase:
                    e.Node.Nodes.Clear();
                    var tables = DB.GetDbTables(e.Node.Tag.ToString());
                    tables.ForEach(table =>
                    {
                        e.Node.Nodes.Add(new TreeNode(string.Format("{0}({1})", table.TableName, table.Rows)) { ToolTipText = ENodeType.DbTable.ToString(), Tag = table });
                    });
                    break;
                case ENodeType.DbTable:
                default:
                    TransformCode(e.Node, this.menuCode.DropDownItems.Cast<ToolStripMenuItem>().Single(item => item.Checked));
                    break;

            }
            e.Node.Expand();
        }

        TreeNode GetTopNode(TreeNode node)
        {
            if (node.Parent == null) return node;
            else return GetTopNode(node.Parent);
        }

        void TransformCode(TreeNode Node, ToolStripMenuItem menu)
        {
            if (Node.ToolTipText != ENodeType.DbTable.ToString()) return;

            var columns = DB.GetDbColumns(Node.Parent.Tag.ToString(), ((DbTable)Node.Tag).TableName);
            var tableName = ((DbTable)Node.Tag).TableName;
            var package = "com.cnblogs.lzrabbit";
            var ns = "com.cnblogs.lzrabbit";
            T4Base t4;

            string str = this.menuCode.DropDownItems.Cast<ToolStripMenuItem>().Single(item => item.Checked).Text;
            switch (menu.Text)
            {
                case "POCO":
                    t4 = new POCO
                    {
                        Package = package,
                        Namespace = ns,
                        DbColumns = columns,
                        TableName = tableName,
                    };
                    break;
                case "POJO":
                    t4 = new POJO
                    {
                        Package = package,
                        Namespace = ns,
                        DbColumns = columns,
                        TableName = tableName,
                    };
                    break;
                case "MyBatis":
                    t4 = new MyBatis
                    {
                        Package = package,
                        Namespace = ns,
                        DbColumns = columns,
                        TableName = tableName,
                    };
                    break;
                default:
                    t4 = new POCO
                    {
                        Package = package,
                        Namespace = ns,
                        DbColumns = columns,
                        TableName = tableName,
                    };
                    break;
            }
            this.txtCode.Text = t4.TransformText();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var node = this.treeView.SelectedNode;
            SaveFileDialog sfd = new SaveFileDialog();

            string filter = "";
            switch (filter)
            {
                case "POJO":
                    filter = "Java(*.java)|*.java";
                    break;
                case "MyBatis":
                    filter = "XML(*.xml)|*.xml";
                    break;
                case "POCO":
                default:
                    filter = "C#(*.cs)|*.cs";
                    break;

            }
            filter += "|所有文件(*.*)|*.*";

            //设置文件类型  
            sfd.Filter = filter;
            sfd.RestoreDirectory = true;
            if (node != null) sfd.FileName = node.Tag.ToString();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, this.txtCode.Text);
            }
        }

        #region 菜单

        private void menuConn_Click(object sender, EventArgs e)
        {
            AddConnection add = new AddConnection();
            add.ShowDialog(this);
        }

        private void CodeMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in this.menuCode.DropDownItems)
            {
                item.Checked = false;
            }
            var menu = sender as System.Windows.Forms.ToolStripMenuItem;
            menu.Checked = true;
            TransformCode(this.treeView.SelectedNode, menu);
        }
        #endregion

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.Node.ContextMenuStrip == null)
                {
                    e.Node.ContextMenuStrip = new ContextMenuStrip();
                    ToolStripMenuItem menu = new ToolStripMenuItem("删除");

                    menu.Click += menu_Click;
                    e.Node.ContextMenuStrip.Items.Add(menu);

                }
            }
        }

        void menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;//转换类型

            ContextMenuStrip strip = item.GetCurrentParent() as ContextMenuStrip;//检索作为当前ToolStripItem的容器
            TreeView tree = strip.SourceControl as TreeView;//获取使ContextMenuStrip此被显示的控件
            tree.SelectedNode.Remove();

            DbConnection.Save(this.treeView.Nodes.Cast<TreeNode>().Select(node => (DbConnection)node.Tag).ToList());
        }



    }
}
