using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Core.DB
{
    /// <summary>
    /// 表字段结构
    /// </summary>
    public sealed class DbColumn
    {

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 数据库类型对应的C#类型
        /// </summary>
        public string CSharpType
        {
            get
            {
                return MySqlDbMap.CsharpType(ColumnType);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string JavaType
        {
            get
            {
                return MySqlDbMap.JavaType(ColumnType);
            }
        }

        /// <summary>
        /// 字节长度
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 字符长度
        /// </summary>
        public int CharLength { get; set; }


        /// <summary>
        /// 整数位
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 小数位
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 是否自增列
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否允许空
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
