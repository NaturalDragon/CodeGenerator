using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CodeGenerator.Utils;

namespace CodeGenerator.Model
{
    [Serializable]
    public class EntityClassInfo
    {
        /// <summary>
        /// 忽略字段
        /// </summary>
        private  List<string> IgnoreColumns
        {
            get
            {
                return new List<string>()
                {
                    "Id","IsDelete","CreateDate","CreateUserId","CreateUserName",
                    "ModifyUserId","ModifyDate","ModifyUserName"
                };
            }
        }

        public EntityClassInfo(DataTable dt,List<CodeGenerator.Model.EntityInfo> entityInfos)
        {
            this.ClassName = dt.TableName;
            this.ClassNameLower = dt.TableName.ToTitileLower();
            List<EntityClassPropertyInfo> ropertyListTemp = new List<EntityClassPropertyInfo>();

            foreach (var item in entityInfos)
            {
                if (IgnoreColumns.Contains(item.columnName)) continue;
                ropertyListTemp.Add(new EntityClassPropertyInfo(item));
            }

            //foreach (DataColumn dcol in dt.Columns)
            //{
            //    if (IgnoreColumns.Contains(dcol.ColumnName)) continue;
            //    var entity = entityInfos.Where(e => e.columnName == dcol.ColumnName).FirstOrDefault();
            //    ropertyListTemp.Add(new EntityClassPropertyInfo(dcol, entity));
            //}
            this.RopertyList = ropertyListTemp;





            //List<EntityClassPropertyInfo> primaryKeyListTemp = new List<EntityClassPropertyInfo>();
            //List<EntityClassPropertyInfo> notPrimaryKeyListTemp = new List<EntityClassPropertyInfo>(ropertyListTemp);
            //foreach (DataColumn dcol in dt.PrimaryKey)
            //{
            //    if (IgnoreColumns.Contains(dcol.ColumnName)) continue;
            //    var entity = entityInfos.Where(e => e.columnName == dcol.ColumnName).FirstOrDefault();
            //    primaryKeyListTemp.Add(new EntityClassPropertyInfo(dcol, entity));
            //    notPrimaryKeyListTemp.Remove(new EntityClassPropertyInfo(dcol, entity));
            //}
            //this.PrimaryKeyList = primaryKeyListTemp;
            //this.NotPrimaryKeyList = notPrimaryKeyListTemp;
        }
        public string ClassName
        {
            get;
            private set;
        }

        public string ClassNameLower
        {
            get;
            private set;
        }
        public List<EntityClassPropertyInfo> RopertyList
        {
            get;
            private set;
        }
        public List<EntityClassPropertyInfo> PrimaryKeyList
        {
            get;
            private set;
        }
        public List<EntityClassPropertyInfo> NotPrimaryKeyList
        {
            get;
            private set;
        }
    }
}