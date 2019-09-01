using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CodeGenerator.MicroService.Model;
using CodeGenerator.Model;

namespace CodeGenerator.MicroService.Events
{
    public class ControllerEvent : IEvent
    {
        public string GeneratorCode(List<EntityClassInfo> entityClassInfos, string basePath, RichTextBox richTextBox = null)
        {
           return CreateCode.CreateControllerClass(entityClassInfos, basePath, richTextBox);
        }
    }
}
