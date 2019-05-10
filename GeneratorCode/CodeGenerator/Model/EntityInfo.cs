using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Model
{
    public class EntityInfo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string columnName { set; get; }


        public string columnType { set; get; }


        public int columnLength { set; get; }


        public bool allowNull { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string columnComment { set; get; }


     
    }
}
