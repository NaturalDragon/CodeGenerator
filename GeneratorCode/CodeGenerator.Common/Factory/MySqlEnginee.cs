using CodeGenerator.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Factory
{
    public class MySqlEnginee : IDbHelper
    {
        public DataTable GetDbTable(string connectionString)
        {
            string sql = @"SELECT TABLE_NAME FROM information_schema.`TABLES` 
                        WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME LIKE concat(?TableTage,'%') AND TABLE_SCHEMA = ?DataBase ;";
            var dateTable = Utils.MySqlHelper.ExecuteDataTable(connectionString, sql,
                       new MySqlParameter("?TableTage", ""),
                       new MySqlParameter("?DataBase", Utils.MySqlHelper.GetDataBaseString(connectionString)));
            return dateTable;
        }
        public DataTable GetTableInfo(string connectionString, string tableName)
        {
          var   dt = Utils.MySqlHelper.ExecuteDataTable(connectionString, string.Format(@"
SELECT * FROM {0} LIMIT 0,0 ", tableName));
            return dt;
        }
        public DataTable GetTableComment(string connectionString, string tableName)
        {
           var  dtComent = Utils.MySqlHelper.ExecuteDataTable(connectionString, string.Format(@"
select column_name as columnName,
data_type as columnType ,
(case when IS_NULLABLE='NO' then 0 else 1 end) as allowNull,  
 (case when CHARACTER_MAXIMUM_LENGTH is null then -1 else CHARACTER_MAXIMUM_LENGTH end)  as columnLength, 
column_comment as columnComment
from information_schema.columns where table_name='{0}' ", tableName));
            return dtComent;
        }

   
    }
}
