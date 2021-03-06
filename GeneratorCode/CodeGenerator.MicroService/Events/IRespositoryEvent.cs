﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CodeGenerator.Model;
using CodeGenerator.MicroService.Model;
namespace CodeGenerator.MicroService.Events
{
    public class IRespositoryEvent : IEvent
    {
        public string GeneratorCode(List<EntityClassInfo> entityClassInfos, string basePath, RichTextBox richTextBox = null)
        {
          return  CreateCode.CreateIRespositoryClass(entityClassInfos, basePath, richTextBox);
        }
    }
}
