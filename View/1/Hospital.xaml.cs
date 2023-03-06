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

namespace WpfApp.View._1
{
    /// <summary>
    /// Hospital.xaml 的交互逻辑
    /// </summary>
    public partial class Hospital : UserControl
    {
        //全局变量
        DataTable dt;

        //命名空间
        BLL.ChuRuYuan.ChuRuYuanClient myYuan = new BLL.ChuRuYuan.ChuRuYuanClient();

        public Hospital()
        {
            InitializeComponent();
        }

        //页面加载
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        //明细查询
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                Detailedquery myDat = new Detailedquery(dr);
                myDat.ShowDialog();
                GetData();
            }
            else
            {
                MessageBox.Show("请选择需要查看明细的数据");
            }
        }
        public void GetData()
        {
            dt = myYuan.btnQuerydata().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //打印信息
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        //打印表格
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        //导出
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }
    }
}
