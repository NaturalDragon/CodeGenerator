﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CodeGenerator.Model;

namespace CodeGenerator.Events
{
    public class ApplicationEvent : IEvent
    {
        public string GeneratorCode(List<EntityClassInfo> entityClassInfos, string basePath, RichTextBox richTextBox = null)
        {
            return CreateCode.CreateApplicationClass(entityClassInfos, basePath, richTextBox);
        }
    }
}
