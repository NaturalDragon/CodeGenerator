using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utils
{
  public  class DbEnginee
    {
        public static ObservableCollection<DbTypeInfo> GetDbTypeInfo()
        {
            return new ObservableCollection<DbTypeInfo>()
            {
                new DbTypeInfo(){ Name="SqlServer",Value=DbType.SqlServer.ToString()},
                new DbTypeInfo(){ Name="MySql",Value=DbType.MySql.ToString()},
            };
        }
    }
}
