using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.View.Maintenance
{
    /// <summary>
    /// Accountmangent.xaml 的交互逻辑
    /// </summary>
    public partial class Accountmangent : UserControl
    {
        //声明全局变量
        DataTable dt;

        //引用服务
        BLL.Maintenance.MaintenanceClient myMain = new BLL.Maintenance.MaintenanceClient();

        public Accountmangent(string Xm,string zh,string Sf)
        {
            InitializeComponent();

            //姓名
            txt_name.Text = Xm;

            //当前账号
            txt_current.Text = zh;

            //管理身份
            txt_management.Text = Sf;
        }
        //页面加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            dt = myMain.btnQueryaccount().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;
        }

        //新增账号
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Thenewaccount myThen = new Thenewaccount();
            myThen.ShowDialog();
        }

        //修改账号
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        //删除账号
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        //查看用户完整信息
        private void btnInsert_information(object sender, RoutedEventArgs e)
        {

        }
    }
}
