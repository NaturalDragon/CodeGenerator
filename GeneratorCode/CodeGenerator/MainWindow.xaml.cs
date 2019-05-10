using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using CodeGenerator.Model;
using System.Collections.ObjectModel;
using CodeGenerator.Utils;
using CodeGenerator.Events;
using System.Reflection;
using CodeGenerator.Factory;
using System.Threading;

namespace CodeGenerator
{
    public delegate void MyCallBack(string str);
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
       
        ObservableCollection<TableModel> tableModels = new ObservableCollection<TableModel>();
        ObservableCollection<ProjectModel> projectModels = new ObservableCollection<ProjectModel>();

        ObservableCollection<DbTypeInfo> dbTypeInfos = new ObservableCollection<DbTypeInfo>();
        EventBus eventBus = new EventBus();
        List<EntityClassInfo> entityClassInfos;
        EntityClassInfo entityInfo;
        string connenction = "";
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            txtPath.Text = "";// @"F:\lznMicro\surgingDemo\SurgingDemo\02.Domain\MicroService.Entity.Order" + "\\";
            CodeGenerator.IsEnabled = false;
            btnInit.IsEnabled = false;
            InitDbTypeComboBox();
            projectModels = ProjectInit.GetProjectModelList();
            projectBox.ItemsSource = projectModels;
        } 

        private void InitDbTypeComboBox()
        {
            dbTypeInfos = DbEnginee.GetDbTypeInfo();
            dbTypeComboBox.ItemsSource = dbTypeInfos;
            dbTypeComboBox.SelectedValue = dbTypeInfos[0].Value;
            dbTypeComboBox.DisplayMemberPath = "Name";//显示出来的值
            dbTypeComboBox.SelectedValuePath = "Value";//实际选中后获取的结果的值
        }

        private void DbTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtConnStr.Text = "";
            btnInit.IsEnabled = false;
            tableModels = new ObservableCollection<TableModel>();
            tableBox.ItemsSource = tableModels;
            StatusOfConnection(false);
        }
    
        #region 非事件响应
        private void StatusOfConnection(bool isEnable)
        {

          
            txtConnStr.IsEnabled = !isEnable;
            btnConnection.IsEnabled = !isEnable;
            if (isEnable)
            {
              //  btnConnection.Content = "断开";
            }
            else
            {
                rtxtDAL.Document.Blocks.Clear();
            }
        }
        private void CbbTableNameBind()
        {
            string sql = @"SELECT TABLE_NAME FROM information_schema.`TABLES` 
                        WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME LIKE concat(?TableTage,'%') AND TABLE_SCHEMA = ?DataBase ;";
            DataTable dt = new DataTable();
            try
            {
                dt = new Factory.FactoryDb(dbTypeComboBox.SelectedValue.ToString()).GetDbTable(connenction);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("连接数据库失败！错误信息：" + ex.Message, "错误");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("不是有效的连接字符串！错误信息：" + ex.Message, "错误");
                return;
            }
            StatusOfConnection(true);
           
            foreach (DataRow item in dt.Rows)
            {
                tableModels.Add(new TableModel() { Name = item.ItemArray[0].ToString(), Checked = false });
            }

            tableBox.ItemsSource = tableModels;
        }
        #endregion


        private void btnConnection_Click(object sender, RoutedEventArgs e)  //连接数据库
        {
            if (string.IsNullOrEmpty(txtConnStr.Text))
            {
                MessageBox.Show("请填写连接字符串!", "提示");
                return;
            }
            else
            {
                btnInit.IsEnabled = true;
                connenction = txtConnStr.Text;
                CbbTableNameBind();
            }
        }

        private void btnSelectPath_Click(object sender, RoutedEventArgs e)  //选择保存文件的路径
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            using (FileStream fs = new FileStream($"{AppContext.BaseDirectory}\\FolderBrowser.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    folderBrowser.SelectedPath = sr.ReadToEnd();
                }
            }
          
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = folderBrowser.SelectedPath;
                if (!path.Contains("MicroService.Entity"))
                {
                    txtPath.Text = "";
                    MessageBox.Show("路径应包含MicroService.Entity.xxx");
                }
                else
                {
                    var selectPath = folderBrowser.SelectedPath + "\\";
                    using (FileStream fs = new FileStream($"{AppContext.BaseDirectory}\\FolderBrowser.txt", FileMode.OpenOrCreate))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.Write(selectPath);
                        }
                    }
                    txtPath.Text = selectPath;
                }

              
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("请选择Domain路径");
                return;
            }
          
            rtxtDAL.Document.Blocks.Clear();
            entityClassInfos = new List<EntityClassInfo>();
            try
            {
                foreach (var table in tableModels)
                {
                    DataTable dt = new DataTable();
                    DataTable dtComent = new DataTable();
                    if (table.Checked)
                    {
                        var factory = new FactoryDb(dbTypeComboBox.SelectedValue.ToString());
                        dt = factory.GetTableInfo(connenction, table.Name);
                        dtComent = factory.GetTableComment(connenction, table.Name);

                         var str = Newtonsoft.Json.JsonConvert.SerializeObject(dtComent);
                        List<Model.EntityInfo> entityInfos =
                            Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.EntityInfo>>(str);
                        entityInfo = new EntityClassInfo(dt, entityInfos);
                        entityClassInfos.Add(entityInfo);
                    }
                }
                if (entityClassInfos.Count > 0)
                {
                    CodeGenerator.IsEnabled = true;
                    CreateCode.CreateInitClass(entityClassInfos[0]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        private void CodeGenerator_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var basePath = txtPath.Text;
                eventBus.Run(entityClassInfos, basePath, rtxtDAL);
                MessageBox.Show("生成成功!");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MyCallBack d = new MyCallBack(ExecAction);
            //CodeGenerator.IsEnabled = false;
            //CodeGenerator.Content = "生成中...";
            //var basePath = txtPath.Text;
            //Thread thread = new Thread(new ParameterizedThreadStart((obj) =>
            //{

            //    eventBus.Run(entityClassInfos, basePath);
            //    string str = eventBus.EventReturn.ToString();
            //    MyCallBack call = (MyCallBack)obj;
            //    call(str);
            //}))
            //{
            //    IsBackground = true
            //};
            //thread.Start(d);

        }
        void ExecAction(string str)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                CodeGenerator.IsEnabled = true;
                CodeGenerator.Content = "生成代码";
                rtxtDAL.AppendText(str);

            }));
          
            MessageBox.Show("生成成功!");

        }
       private void CodeGeneratorFuc(object basePath)
        {
          
          
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            CodeGenerator.IsEnabled = false;
        }
        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = e.Source as CheckBox;

            foreach (var item in tableModels)
            {
                item.Checked = box.IsChecked.Value;
            }
            CodeGenerator.IsEnabled = false;
            tableBox.ItemsSource = null;
            tableBox.ItemsSource = tableModels;
        }

        private void CheckFan_Click(object sender, RoutedEventArgs e)
        {
            CodeGenerator.IsEnabled = false;
            foreach (var item in tableModels)
            {
                item.Checked = !item.Checked;
            }
            tableBox.ItemsSource = null;
            tableBox.ItemsSource = tableModels;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            RegisterEvent();
        }

        public void RegisterEvent()
        {
            eventBus = new EventBus();
            foreach (var item in projectModels)
            {
                if (item.Checked)
                {
                    var projectEvent1 = (IEvent)Assembly.GetExecutingAssembly().
                        CreateInstance($"CodeGenerator.Events.{item.Key}", false);
                    eventBus.GeneratorCode += projectEvent1.GeneratorCode;
                }
            }
        }

        private void ProjectCheckAll_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = e.Source as CheckBox;
            foreach (var item in projectModels)
            {
                item.Checked = box.IsChecked.Value;
            }
            projectBox.ItemsSource = null;
            projectBox.ItemsSource = projectModels;
            RegisterEvent();
        }

        private void ProjectCheckFan_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in projectModels)
            {
                item.Checked = !item.Checked;
            }
            projectBox.ItemsSource = null;
            projectBox.ItemsSource = projectModels;
            RegisterEvent();
        }

       
    }
}
