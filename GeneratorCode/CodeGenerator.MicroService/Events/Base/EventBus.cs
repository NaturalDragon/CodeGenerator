using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CodeGenerator.MicroService.Events
{
    public class EventBus
    {
        public EventBus()
        {
            EventReturn = new StringBuilder();
        }

        public StringBuilder EventReturn
        {
            get;private set;
        }

        public  event  Func<List<EntityClassInfo>, string, RichTextBox,string>  GeneratorCode;

        public string Run(List<EntityClassInfo> entityClassInfos , string basePath, RichTextBox richTextBox = null)
        {
           
            var str = "";
            str = GeneratorCode?.Invoke(entityClassInfos, basePath, richTextBox);
            EventReturn.Append(str);
            return str;//GeneratorCode?.Invoke(entityClassInfos, basePath, richTextBox);
        }
    }
}
