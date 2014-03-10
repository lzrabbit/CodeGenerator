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
    public partial class AddConnection : Form
    {
        public AddConnection()
        {
            InitializeComponent();
        }
        private void AddConnection_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ddlDBType.SelectedIndex = 0;

            ddlDBType_SelectedIndexChanged(null, null);
        }

        private void ddlDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlDBType.Text == "SqlServer") this.txtPort.Text = "1433";
            else this.txtPort.Text = "3306";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dbconn = TestConnection();
            if (dbconn != null)
            {
                dbconn.Save();
                ((SimpleCoder)this.Owner).LoadConns();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestConnection();
        }

        DbConnection TestConnection()
        {
            DbConnection dbconn = null;
            try
            {
                dbconn = new DbConnection
                {
                    DbType = this.ddlDBType.Text == "MySql" ? EDbType.MySql : EDbType.SqlServer,
                    Server = this.txtServer.Text.Trim(),
                    Port = int.Parse(this.txtPort.Text.Trim()),
                    User = this.txtUserName.Text.Trim(),
                    Password = this.txtPassword.Text,

                };
                IDB db = dbconn.GetDB();
                db.TestConnection();

                MessageBox.Show("链接成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dbconn;
        }


    }
}
