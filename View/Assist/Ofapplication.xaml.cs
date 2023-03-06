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
using WpfApp.Tools.Utils;

namespace WpfApp.View.Assist
{
    /// <summary>
    /// Ofapplication.xaml 的交互逻辑
    /// </summary>
    public partial class Ofapplication : UserControl
    {

        //声明全局变量
        DataTable dt;
        DataRowView DRV;

        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();

        public Ofapplication()
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
            dt = myAss.btnSuppliesquery().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff,dt);
        }

        //物资录入
        private void btnMaterial_Click(object sender, RoutedEventArgs e)
        {
            Materialinput myMa = new Materialinput();
            myMa.ShowDialog();
            GetData();
        }

        //查看
        private void btnreport_Click(object sender, RoutedEventArgs e)
        {
            Query myQuer = new Query();
            myQuer.ShowDialog();
        }

        //打印表格
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        //删除
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                int id = Convert.ToInt32(dr.Row["number"]);
                MessageBoxResult dc = MessageBox.Show("是否保存药品信息？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (dc == MessageBoxResult.OK)
                {
                    //执行删除
                    int Cont = myAss.btnSuppliesdelete(id);
                    if (Cont >0)
                    {
                        MessageBox.Show("删除成功！");
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的数据！");
            }
        }

        //导出
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ExportToExcel.Export(dgvStaff, "物资信息") == true)
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

        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }

        //查看完整的物资信息
        private void btnreport_Materialinformation(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem !=null)
            {
                //跨页面传递数据

                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                Materialinformation myMater = new Materialinformation(dr);
                myMater.ShowDialog();
                GetData();
            }
            else
            {
                MessageBox.Show("请选择需要查看完整信息的物资！");
            }
        }
    }
}
