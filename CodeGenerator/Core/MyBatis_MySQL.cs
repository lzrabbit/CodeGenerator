﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本: 12.0.0.0
//  
//     对此文件的更改可能会导致不正确的行为。此外，如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace CodeGenerator.Core
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class MyBatis_MySQL : T4Base
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<!DOCTYPE mapper\r\n  PUBLIC \"-//mybatis.or" +
                    "g//DTD Mapper 3.0//EN\"\r\n  \"http://mybatis.org/dtd/mybatis-3-mapper.dtd\">\r\n<mappe" +
                    "r namespace=\"");
            
            #line 10 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.Package));
            
            #line default
            #line hidden
            this.Write(".");
            
            #line 10 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TableName));
            
            #line default
            #line hidden
            this.Write("Dao\">\t\r\n\t<update id=\"batchUpdate\" parameterType=\"list\">\r\n\t\tINSERT INTO\r\n\t\t");
            
            #line 13 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(this.TableName));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 13 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
for(int i=0;i< this.DbColumns.Count;i++)
		{
		   if(i==0) this.Write(DbColumns[i].ColumnName);
		   else  this.Write(","+DbColumns[i].ColumnName);
		}
            
            #line default
            #line hidden
            this.Write(")\r\n\t\tVALUES\r\n\t\t<foreach collection=\"list\" item=\"item\" index=\"index\" separator=\",\"" +
                    ">\r\n\t\t(");
            
            #line 20 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
for(int i=0;i< this.DbColumnsWithoutIdentityKey.Count;i++)
		{
			if(i==0) this.Write("#{item."+DbColumnsWithoutIdentityKey[i].ColumnName+"}");
			else this.Write(",#{item."+DbColumnsWithoutIdentityKey[i].ColumnName+"}");
		}
            
            #line default
            #line hidden
            this.Write(")\r\n\t\t</foreach>\r\n\t\tON DUPLICATE KEY UPDATE\r\n\t\t");
            
            #line 27 "D:\GitHub\CodeGenerator\CodeGenerator\Core\MyBatis_MySQL.tt"
for(int i=0;i< this.DbColumnsWithoutPrimaryKey.Count;i++)
		{
			if(i==0) this.Write(DbColumnsWithoutPrimaryKey[i].ColumnName+"=VALUES("+DbColumnsWithoutPrimaryKey[i].ColumnName+")");
			else this.Write(","+DbColumnsWithoutPrimaryKey[i].ColumnName+"=VALUES("+DbColumnsWithoutPrimaryKey[i].ColumnName+")");
		}
            
            #line default
            #line hidden
            this.Write("\r\n\t</update>\r\n</mapper>");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}