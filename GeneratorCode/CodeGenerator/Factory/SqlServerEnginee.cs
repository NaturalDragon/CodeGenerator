using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Factory
{
    public class SqlServerEnginee : IDbHelper
    {
        public DataTable GetDbTable(string connectionString)
        {
            string sql = @"SELECT Name FROM SysObjects Where XType='U' ORDER BY Name";
            var dateTable = Utils.SqlServerHelper.ExecuteDataTable(connectionString, sql);
            return dateTable;
        }

        public DataTable GetTableInfo(string connectionString, string tableName)
        {
            var dt = Utils.SqlServerHelper.ExecuteDataTable(connectionString, string.Format(@"
SELECT top 0 * FROM {0} ", tableName));
            return dt;
        }
        public DataTable GetTableComment(string connectionString, string tableName)
        {
            var dtComent = Utils.SqlServerHelper.ExecuteDataTable(connectionString, string.Format(@"
SELECT  
    columnName     = a.name,
    columnType       = b.name,
    columnLength       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    allowNull     =  a.isnullable,
    columnComment   = isnull(g.[value],'')
FROM syscolumns a left join systypes b on  a.xusertype=b.xusertype
inner join  sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join  syscomments e on   a.cdefault=e.id
left join sys.extended_properties   g on  a.id=G.major_id and a.colid=g.minor_id  
left join sys.extended_properties f on  d.id=f.major_id and f.minor_id=0
where  d.name='{0}'   order by  a.id,a.colorder", tableName));
            return dtComent;
        }

    }

}
