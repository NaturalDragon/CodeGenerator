using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utils
{
   public class SqlServerHelper
    {
        public static string GetDataBaseString(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                return conn.Database;
            }
        }
        public static DataTable ExecuteDataTable(string connectionString, string sql, params DbParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            da.FillSchema(dt, SchemaType.Source);   //从数据源中检索架构
                            return dt;
                        }
                    }
                }
            }
        }
    }
}
