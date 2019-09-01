using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CodeGenerator.MicroService.Model;
namespace CodeGenerator.MicroService.Events
{
    public class EntityEvent : IEvent
    {
        public string GeneratorCode(List<EntityClassInfo> entityClassInfos, string basePath, RichTextBox richTextBox = null)
        {
            string codeEntity = CreateCode.CreateEntityClass(entityClassInfos, basePath, richTextBox);
            return codeEntity;
            //throw new NotImplementedException();
        }
    }
}
