using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Factory
{
    public interface IDbHelper
    {
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        DataTable GetDbTable(string connectionString);

        /// <summary>
        /// 数据表信息
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        DataTable GetTableInfo(string connectionString,string tableName);

        DataTable GetTableComment(string connectionString, string tableName);
    }
}
