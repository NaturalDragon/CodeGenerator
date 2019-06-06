using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;
using System.CodeDom.Compiler;
using System.Configuration;
using CodeGenerator.Utils;
using System.Windows.Controls;

namespace CodeGenerator.Model
{
   public class CreateCode
    {
        private static string InitTemplate(EntityClassInfo classInfo, string templatePath,string projectName=null)
        {
            CustomTextTemplatingEngineHost host = new CustomTextTemplatingEngineHost();
            host.TemplateFileValue = templatePath;
            string input = File.ReadAllText(templatePath);

            host.Session = new TextTemplatingSession();
            host.Session.Add("entity", classInfo);
            if (projectName != null)
            {
                host.Session.Add("projectName", projectName);
            }
            string output = new Engine().ProcessTemplate(input, host);

            StringBuilder errorWarn = new StringBuilder();
            foreach (CompilerError error in host.Errors)
            {
                errorWarn.Append(error.Line).Append(":").AppendLine(error.ErrorText);
            }
            if (!File.Exists("Error.log"))
            {
                File.Create("Error.log");
            }
            File.WriteAllText("Error.log", errorWarn.ToString());
            return output;
        }
        public static string CreateInitClass(EntityClassInfo classInfo)
        {
            string templatePath = AppConfigSetting.TemplateInit;
            string output = InitTemplate(classInfo, templatePath);

            return output;
        }
        public static string CreateEntityClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox=null)
        {
            string templatePath = AppConfigSetting.TemplateEntity;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            foreach (var classInfo in classInfos)
            {
                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(basePath + classInfo.ClassName + ".cs",
                  output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }

        private static void CodeVisible(RichTextBox richTextBox, StringBuilder sb)
        {
            if (richTextBox != null)
            {
                richTextBox.AppendText(sb.ToString());
            }
        }

        /// <summary>
        /// 创建Configuration
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateConfigurationClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateConfiguration;
            var projectName = FileHelper.GetProjectName(basePath);
            StringBuilder sb = new StringBuilder();
            foreach (var classInfo in classInfos)
            {
                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(basePath+ "Configuration/" + classInfo.ClassName + "Configuration.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }

        /// <summary>
        /// 创建IRespository
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateIRespositoryClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateIRespository;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastOneLevel(basePath);
            var iRespositoryPath =string.Format( $"{lastPath}{AppConfigSetting.IRespository}\\", projectName);

          
            foreach (var classInfo in classInfos)
            {
                
                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + "I" + classInfo.ClassName + "Respository.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }

        /// <summary>
        /// 创建Respository
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateRespositoryClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateRespository;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastOneLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.Respository}\\", projectName);


            foreach (var classInfo in classInfos)
            {
                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + classInfo.ClassName + "Respository.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }

        public static string CreateDataAccessClass(List<EntityClassInfo> classInfos,string basePath, RichTextBox richTextBox=null)
        {
            string templatePath = AppConfigSetting.TemplateDataAccess;
            StringBuilder sb = new StringBuilder();
            foreach (var classInfo in classInfos)
            {
                string output = InitTemplate(classInfo, templatePath);
                File.WriteAllText(basePath + classInfo.ClassName + "DAL.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }



        /// <summary>
        /// 创建IApplication
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateIApplicationClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateIApplication;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastTwoLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.IApplication}\\", projectName);
            FileHelper.CreateFoler(iRespositoryPath);
            foreach (var classInfo in classInfos)
            {

                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + "I" + classInfo.ClassName + "AppService.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }

        /// <summary>
        /// 创建Application
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateApplicationClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateApplication;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastTwoLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.Application}\\", projectName);
            FileHelper.CreateFoler(iRespositoryPath);
            foreach (var classInfo in classInfos)
            {

                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + classInfo.ClassName + "AppService.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }
        /// <summary>
        /// 创建Validators
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateValidatorsClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateValidators;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastTwoLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.Application}\\Validators\\", projectName);
            FileHelper.CreateFoler(iRespositoryPath);
            foreach (var classInfo in classInfos)
            {

                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + classInfo.ClassName + "Validator.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }
        public static string CreateValidatorsFiltersClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateValidatorsFilters;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastTwoLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.Application}\\ValidatorsFilters\\", projectName);
            FileHelper.CreateFoler(iRespositoryPath);
            foreach (var classInfo in classInfos)
            {

                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + classInfo.ClassName + "ValidatorsFilter.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }
        

        /// <summary>
        /// 创建IModules
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateIModulesClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateIModules;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastTwoLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.IModules}\\", projectName);
            FileHelper.CreateFoler(iRespositoryPath);
            foreach (var classInfo in classInfos)
            {

                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + "I" + classInfo.ClassName + "Service.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }

        /// <summary>
        /// 创建Modules
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        public static string CreateModulesClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            string templatePath = AppConfigSetting.TemplateModules;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastPath = FileHelper.GetLastTwoLevel(basePath);
            var iRespositoryPath = string.Format($"{lastPath}{AppConfigSetting.Modules}\\", projectName);
            FileHelper.CreateFoler(iRespositoryPath);
            foreach (var classInfo in classInfos)
            {

                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(iRespositoryPath + classInfo.ClassName + "Service.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }


        #region dto

        public static  string CreateQueryDtoClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            var templatePath = AppConfigSetting.TemplateQueryDto;
            return CreateDtoClass(classInfos, basePath, templatePath, "QueryDto", richTextBox);
        }
        public static string CreateRequestDtoClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            var templatePath = AppConfigSetting.TemplateRequestDto;
            return CreateDtoClass(classInfos, basePath, templatePath, "RequestDto", richTextBox);
        }
        public static string CreatePageRequestDtoClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            var templatePath = AppConfigSetting.TemplatePageRequestDto;
            return CreateDtoClass(classInfos, basePath, templatePath, "PageRequestDto", richTextBox);
        }
        public static string CreateBatchRequestDtoClass(List<EntityClassInfo> classInfos, string basePath, RichTextBox richTextBox = null)
        {
            var templatePath = AppConfigSetting.TemplateBatchRequestDto;
            return CreateDtoClass(classInfos, basePath, templatePath, "BatchRequestDto", richTextBox);
        }





        /// <summary>
        /// 创建Respository
        /// </summary>
        /// <param name="classInfos"></param>
        /// <param name="basePath"></param>
        /// <param name="richTextBox"></param>
        /// <returns></returns>
        private static string CreateDtoClass(List<EntityClassInfo> classInfos, string basePath,string templatePath,string dtoName, RichTextBox richTextBox = null)
        {
           // string templatePath = AppConfigSetting.TemplateRespository;
            StringBuilder sb = new StringBuilder();
            var projectName = FileHelper.GetProjectName(basePath);
            var lastTwoPath = FileHelper.GetLastTwoLevel(basePath);
            var iApplicationPath = string.Format($"{lastTwoPath}{AppConfigSetting.IApplication}\\Dto\\", projectName);


           

            foreach (var classInfo in classInfos)
            {
                var folder = iApplicationPath + $"{classInfo.ClassName}\\";
                FileHelper.CreateFoler(folder);
                string output = InitTemplate(classInfo, templatePath, projectName);
                File.WriteAllText(folder + classInfo.ClassName + $"{dtoName}.cs",
                output);
                sb.Append(output);
                sb.Append("\r\n");
            }
            CodeVisible(richTextBox, sb);
            return sb.ToString();
        }
        #endregion

    }
}
