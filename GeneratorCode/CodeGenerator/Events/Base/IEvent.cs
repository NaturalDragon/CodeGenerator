using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CodeGenerator.Events
{
   public interface IEvent
    {
        string GeneratorCode(List<EntityClassInfo> entityClassInfos,string basePath, RichTextBox richTextBox=null);
    }
}
