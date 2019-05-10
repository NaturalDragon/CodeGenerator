using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CodeGenerator.Utils;

namespace CodeGenerator.Model
{
    [Serializable]
    public class EntityClassPropertyInfo
    {
        public EntityClassPropertyInfo(EntityInfo entityInfo)
        {


            this.PropertyName = entityInfo.columnName;
            this.PropertyType = SqlServerDbTypeMap.MapCsharpType(entityInfo.columnType);//dcol.DataType.Name;
            this.ProptertyComment = entityInfo.columnComment;
            this.ProptertyMaxLength = entityInfo.columnLength;
            this.ProptertyIsNull = entityInfo.allowNull;
            this.IsValueType = false;

            if (entityInfo.allowNull&&this.PropertyType!="string")
            {
                this.PropertyType = this.PropertyType + "?";
            }
            else
            {
                this.IsValueType = true;
            }

        }

        public bool ProptertyIsNull
        {
            get; private set;
        }
        public int ProptertyMaxLength
        {
            get;private set;
        }
        public string ProptertyComment
        {
            get;private set;
        }
        public string PropertyName
        {
            get;
            private set;
        }

        public string PropertyType
        {
            get;
            private set;
        }

        public bool IsValueType
        {
            get;
            private set;
        }

        public override bool Equals(object obj)
        {
            EntityClassPropertyInfo temp = obj as EntityClassPropertyInfo;
            if (this.PropertyName == temp.PropertyName && this.PropertyType == temp.PropertyType)
            {
                return true;
            }
            return false;
        }
        
    }
}
