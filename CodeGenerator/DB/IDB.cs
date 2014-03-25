using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.DB
{
    public interface IDB
    {
        EDbType DbType { get; }

        bool TestConnection();

        List<DbColumn> GetDbColumns(string database, string table);


        List<DbIndex> GetDbIndexs(string database, string table);


        List<DbTable> GetDbTables(string database);

        List<string> GetDatabases();

    }
}
