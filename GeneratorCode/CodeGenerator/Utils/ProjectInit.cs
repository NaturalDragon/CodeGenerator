using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Utils
{
    public class ProjectInit
    {

        public static ObservableCollection<ProjectModel> GetProjectModelList()
        {
            return new ObservableCollection<ProjectModel>()
            {
                new ProjectModel(){ Name="Entity", Key="EntityEvent" ,Checked=false},
                new ProjectModel(){ Name="Configruation", Key="ConfigruationEvent" ,Checked=false},
                new ProjectModel(){ Name="IRespository", Key="IRespositoryEvent" ,Checked=false},
                new ProjectModel(){ Name="Respository", Key="RespositoryEvent" ,Checked=false},
                new ProjectModel(){ Name="Dto", Key="DtoEvent" ,Checked=false},
                new ProjectModel(){ Name="IApplication", Key="IApplicationEvent" ,Checked=false},
                new ProjectModel(){ Name="Application", Key="ApplicationEvent" ,Checked=false},
                new ProjectModel(){ Name="Validators", Key="ValidatorsEvent" ,Checked=false},
                new ProjectModel(){ Name="IModules", Key="IModulesEvent" ,Checked=false},
                new ProjectModel(){ Name="Modules", Key="ModulesEvent" ,Checked=false},
                
                //new ProjectModel(){ Name="DAL", Key="DalEvent" ,Checked=false}
            };
        }
    }  
}
