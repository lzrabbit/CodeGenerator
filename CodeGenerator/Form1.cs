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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMySql_Click(object sender, EventArgs e)
        {
            DBHelper.ConnString = this.txtConnString.Text;
            var dbs = DBHelper.GetDatabases();
            dbs.ForEach(db =>
            {
                this.treeView.Nodes.Add(new TreeNode(db.DataBaseName) { ToolTipText = "DbDataBase", Tag = db.DataBaseName });
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtConnString.Text = "Server=192.168.9.51;Database=information_schema;Uid=liuzhao;Pwd=nopass.2;charset=utf8;";
            this.ddlCodeType.SelectedIndex = 0;

        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.ToolTipText == "DbDataBase")
            {
                e.Node.Nodes.Clear();
                var tables = DBHelper.GetDbTables(e.Node.Tag.ToString());
                tables.ForEach(table =>
                {
                    e.Node.Nodes.Add(new TreeNode(string.Format("{0}({1})", table.TableName, table.Rows)) { ToolTipText = "DbTable", Tag = table.TableName });
                });
                e.Node.Expand();
            }
            else
            {

                T4Base t4;
                switch (this.ddlCodeType.Text)
                {
                    case "POCO":
                        t4 = new POCO
                        {
                            DbColumns = DBHelper.GetgDbColumns(e.Node.Parent.Tag.ToString(), e.Node.Tag.ToString()),
                            Package = "lzrabbit",
                            TableName = e.Node.Tag.ToString(),
                        };
                        break;
                    case "POJO":
                        t4 = new POJO
                                {
                                    DbColumns = DBHelper.GetgDbColumns(e.Node.Parent.Tag.ToString(), e.Node.Tag.ToString()),
                                    Package = "lzrabbit",
                                    TableName = e.Node.Tag.ToString(),
                                };
                        break;
                    default:
                        t4 = new POCO
                        {
                            DbColumns = DBHelper.GetgDbColumns(e.Node.Parent.Tag.ToString(), e.Node.Tag.ToString()),
                            Package = "lzrabbit",
                            TableName = e.Node.Tag.ToString(),
                            Namespace = "xx",
                        };
                        break;
                }

                this.txtCode.Text = t4.TransformText();
            }


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
            switch (this.ddlCodeType.Text)
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



    }
}
