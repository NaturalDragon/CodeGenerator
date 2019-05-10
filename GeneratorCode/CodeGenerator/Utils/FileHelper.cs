using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utils
{
   public class FileHelper
    {
       public static void CreateFoler(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        /// <summary>
        /// 上一级
        /// </summary>
        public static string GetLastOneLevel(string basePath)
        {
            string[] folders = basePath.Split(new char[] { '\\' });

            return string.Join("\\", folders.Take(folders.Length - 2));

        }
        /// <summary>
        /// 上两级
        /// </summary>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetLastTwoLevel(string basePath)
        {
            string[] folders = basePath.Split(new char[] { '\\' });

            return string.Join("\\", folders.Take(folders.Length - 3));

        }
        /// <summary>
        /// 
        /// </summary>
        public static string GetProjectName(string basePath)
        {
            string[] folders = basePath.Split(new char[] { '\\' });
            string [] lastStr= folders[folders.Length - 2].Split('.');
            return lastStr[lastStr.Length - 1];

        }
    }
}
