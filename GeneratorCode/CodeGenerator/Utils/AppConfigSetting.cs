using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utils
{
    public class AppConfigSetting
    {
        public static string TemplateInit
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateInit"].ToString();
            }
        }
        public static string TemplateEntity
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateEntity"].ToString();
            }
        }
        public static string TemplateDataAccess
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateDataAccess"].ToString();
            }
        }
        public static string TemplateConfiguration
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateConfiguration"].ToString();
            }
        }
        public static string TemplateIRespository
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateIRespository"].ToString();
            }
        }

        public static string TemplateRespository
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateRespository"].ToString();
            }
        }
        public static string TemplateBatchRequestDto
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateBatchRequestDto"].ToString();
            }
        }
        public static string TemplatePageRequestDto
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplatePageRequestDto"].ToString();
            }
        }
        public static string TemplateQueryDto
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateQueryDto"].ToString();
            }
        }

        public static string TemplateRequestDto
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateRequestDto"].ToString();
            }
        }
        public static string TemplateIApplication
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateIApplication"].ToString();
            }
        }
        public static string TemplateApplication
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateApplication"].ToString();
            }
        }

        public static string TemplateValidators
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateValidators"].ToString();
            }
        }
        public static string TemplateValidatorsFilters
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateValidatorsFilters"].ToString();
            }
        }
        public static string TemplateIModules
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateIModules"].ToString();
            }
        }
        public static string TemplateModules
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateModules"].ToString();
            }
        }

        public static string TemplateController
        {
            get
            {
                return ConfigurationManager.AppSettings["TemplateController"].ToString();
            }
        }

        public static string IRespository
        {
            get
            {
                return ConfigurationManager.AppSettings["IRespository"].ToString();
            }
        }
        public static string Respository
        {
            get
            {
                return ConfigurationManager.AppSettings["Respository"].ToString();
            }
        }
        public static string IApplication
        {
            get
            {
                return ConfigurationManager.AppSettings["IApplication"].ToString();
            }
        }
      
        public static string Application
        {
            get
            {
                return ConfigurationManager.AppSettings["Application"].ToString();
            }
        }

        public static string Modules
        {
            get
            {
                return ConfigurationManager.AppSettings["Modules"].ToString();
            }
        }

        public static string IModules
        {
            get
            {
                return ConfigurationManager.AppSettings["IModules"].ToString();
            }
        }
        public static string Controller
        {
            get
            {
                return ConfigurationManager.AppSettings["Controller"].ToString();
            }
        }
    }
}
