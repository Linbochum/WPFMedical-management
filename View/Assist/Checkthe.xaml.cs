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
using System.Windows.Shapes;
using WpfApp.Tools.Utils;
using WpfApp.View.Print;

namespace WpfApp.View.Assist
{
    /// <summary>
    /// Checkthe.xaml 的交互逻辑
    /// </summary>
    public partial class Checkthe : Window
    {
        //全局变量
        DataTable dt;

        //命名空间
        BLL.Assist.AssistClient myAs = new BLL.Assist.AssistClient();
        public Checkthe()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            dt = myAs.btnQuerycation().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //打印表格
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (dgvStaff.Items.Count > 0)
            {
                //2.弹出窗口
                WD_PrintStaffTable myPrint = new WD_PrintStaffTable();
                myPrint.ShowDialog();
            }
        }

        //导出信息
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ExportToExcel.Export(dgvStaff, "申请信息") == true)
            {
                MessageBox.Show("数据导出成功！", "系统提示", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("数据导出失败！", "系统提示", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        //搜索
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }
    }
}
