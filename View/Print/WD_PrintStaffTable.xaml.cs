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

namespace WpfApp.View.Print
{
    /// <summary>
    /// WD_PrintStaffTable.xaml 的交互逻辑
    /// </summary>
    public partial class WD_PrintStaffTable : Window
    {
        //全局变量
        DataTable dt;
        //命名空间
        BLL.Assist.AssistClient myAs = new BLL.Assist.AssistClient();

        public WD_PrintStaffTable()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_Tiem.Content = "打印时间：" + DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");
            //执行服务端的方法获取表格 数据
            GetData();
        }
        public void GetData()
        {
            dt = myAs.btnQuerycation().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

        }

        private void mi_Print_Click(object sender, RoutedEventArgs e)
        {
            //实例化打印对话框
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                //打印visual（非文本）对象（打印区域，要打印的作业的说明）
                dialog.PrintVisual(printDockPanel, "打印表格");
            }
        }

        private void dgvStaff_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //获取行索引+1
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
