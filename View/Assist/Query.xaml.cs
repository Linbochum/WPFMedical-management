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

namespace WpfApp.View.Assist
{
    /// <summary>
    /// Query.xaml 的交互逻辑
    /// </summary>
    public partial class Query : Window
    {
        //全局变量
        DataTable dt;

        //命名空间
        BLL.Assist.AssistClient myAs = new BLL.Assist.AssistClient();
        public Query()
        {
            InitializeComponent();
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        public void GetData() 
        {
            dt = myAs.btnRec().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //查看申请
        private void btnreport_Click(object sender, RoutedEventArgs e)
        {
            Checkthe myChe = new Checkthe();
            myChe.ShowDialog();
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
                DataRowView dt = (DataRowView)dgvStaff.SelectedItem;
                //获取ID
                int ID = Convert.ToInt32(dt.Row["ID"].ToString());
                //获取状态
                int Rev = Convert.ToInt32(dt.Row["Reviewstatus"].ToString());
                if (Rev != 104)
                {
                    MessageBoxResult dc = MessageBox.Show("是否要删除该申请？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dc == MessageBoxResult.OK)
                    {
                        //执行删除
                        int Cont = myAs.btnSdelete(ID);
                        if (Cont > 0)
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
                    MessageBox.Show("该申请还未审核无法删除！");
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

        }

        //查询
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }

        //申请
        private void btnUpdatae_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem !=null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                int Revie = Convert.ToInt32(dr.Row["Reviewstatus"].ToString());
                if (Revie != 92)
                {
                    //跨页面传递数据
                    Toquery myTo = new Toquery(dr);
                    myTo.ShowDialog();
                    GetData();
                }
                else
                {
                    MessageBox.Show("审核已通过无需再次审核！");
                }               
            }
            else
            {
                MessageBox.Show("请选择需要审核的数据！");
            }
        }

        //物资领取
        private void btnreport_Receive(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                int Revie = Convert.ToInt32(dr.Row["Reviewstatus"].ToString());
                if (Revie != 104)
                {
                    //跨页面传递数据
                    Receiving myRe = new Receiving(dr);
                    myRe.ShowDialog();
                    GetData();
                }
                else
                {
                    MessageBox.Show("该申请还未申请，无法进行领药操作！");
                }
            }
            else
            {
                MessageBox.Show("请选择需要审核的数据！");
            }
        }    
    }
}
