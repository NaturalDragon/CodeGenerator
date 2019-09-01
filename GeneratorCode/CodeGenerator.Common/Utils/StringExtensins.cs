using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utils
{
   public static class StringExtensins
    {
        /// <summary>
        /// 将字符串转换为小写字符
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ToTitileLower(this string content)
        {
            if (string.IsNullOrEmpty(content))
                return content;

            return content.Substring(0, 1).ToLower() + content.Substring(1);
        }
    }
}
