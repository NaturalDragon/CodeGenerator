using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Factory
{
   public class FactoryDb
    {
        private IDbHelper _dbHelper;

        public FactoryDb(string type)
        {
            Model.DbType enType =(Model.DbType) Enum.Parse(typeof(Model.DbType), type);
            switch (enType)
            {
                case Model.DbType.SqlServer:
                    _dbHelper = new SqlServerEnginee();
                    break;
                case Model.DbType.MySql:
                    _dbHelper = new MySqlEnginee();
                    break;
                default:
                    break;
            }
        }
        public DataTable GetDbTable(string connectionString)
        {
            return _dbHelper.GetDbTable(connectionString);
        }



        /// <summary>
        /// 数据表信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataTable GetTableInfo(string connectionString, string tableName)
        {
            return _dbHelper.GetTableInfo(connectionString,tableName);
        }
        public DataTable GetTableComment(string connectionString, string tableName)
        {
            return _dbHelper.GetTableComment(connectionString,tableName);
        }

    }
}
