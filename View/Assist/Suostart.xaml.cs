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

namespace WpfApp.View.Assist
{
    /// <summary>
    /// Suostart.xaml 的交互逻辑
    /// </summary>
    public partial class Suostart : UserControl
    {
        //声明全局变量
        DataTable dt;

        //命名空间
        BLL.Assist.AssistClient myAs = new BLL.Assist.AssistClient();
        public Suostart()
        {
            InitializeComponent();
        }

        //页面加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            dt = myAs.btnapproved().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //开始/结束
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;

                //用于判断跳转
                int opet = Convert.ToInt32(dr.Row["Opertyoe"]);
                if (opet == 100)
                {
                    Suotart mySou = new Suotart(dr);
                    mySou.ShowDialog();
                    GetData();
                }
                else
                {
                    Theenthe myThe = new Theenthe(dr);
                    myThe.ShowDialog();
                    GetData();
                }
            }
            else
            {
                MessageBox.Show("请选择需要开始手术的资料！");
            }
        }

        //查看报告
        private void btnmanagement_Click(object sender, RoutedEventArgs e)
        {

        }

        //删除
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        //打印表格
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        //导出数据
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgvStaff_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void dgvStaff_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }

        //修改
        private void btnUpdatae_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
