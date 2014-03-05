using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Core.DB
{
    /// <summary>
    /// 表结构
    /// </summary>
    public sealed class DbTable
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表的架构
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// 表的记录数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 是否含有主键
        /// </summary>
        public bool HasPrimaryKey { get; set; }
    }
}
