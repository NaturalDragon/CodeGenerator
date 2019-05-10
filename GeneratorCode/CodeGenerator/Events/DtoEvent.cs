using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CodeGenerator.Model;

namespace CodeGenerator.Events
{
    public class DtoEvent : IEvent
    {
        public string GeneratorCode(List<EntityClassInfo> entityClassInfos, string basePath, RichTextBox richTextBox = null)
        {
            var str1 = CreateCode.CreateRequestDtoClass(entityClassInfos, basePath, richTextBox);
            var str2 = CreateCode.CreateQueryDtoClass(entityClassInfos, basePath, richTextBox);
            var str3 = CreateCode.CreatePageRequestDtoClass(entityClassInfos, basePath, richTextBox);
            var str4 = CreateCode.CreateBatchRequestDtoClass(entityClassInfos, basePath, richTextBox);
            return str1 + str2 + str3 + str4;
        }
    }
}
