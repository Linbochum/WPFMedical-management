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
    /// Querycation.xaml 的交互逻辑
    /// </summary>
    public partial class Querycation : UserControl
    {
        //全局变量
        DataTable dt;


        //命名空间
        BLL.Assist.AssistClient myAs = new BLL.Assist.AssistClient();

        public Querycation()
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
            dt = myAs.btnapplication().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //搜索按钮
        private void txt_Select_KeyDowna(object sender, RoutedEventArgs e)
        {

        }

        //新增
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Surgeryfor mySur = new Surgeryfor();
            mySur.ShowDialog();
            GetData();
        }

        //修改
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                TUEuerycation myTu = new TUEuerycation(dr);
                myTu.ShowDialog();
                GetData();

            }
            else
            {
                MessageBox.Show("请选择需要修改的申请表！");
            }
        }

        //删除
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                //获取状态值，用于判断是否删除
                int auto = Convert.ToInt32(dr.Row["Review_results"]);

                //获取行ID用于删除
                int ID = Convert.ToInt32(dr.Row["ID"]);

                if (auto == 91)
                {
                    //执行删除
                    MessageBox.Show("申请还在审核中，无法删除！");
                }
                else if (auto == 92)
                {
                    MessageBox.Show("已经完成审核的数据删除后就无法进行手术！");
                    MessageBoxResult doa = MessageBox.Show("该申请已经完成审核，删除需谨慎！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);                  
                    if (doa == MessageBoxResult.OK)
                    {
                        //执行删除
                        int cont = myAs.btnDeleteation(ID);
                        if (cont > 0)
                        {
                            MessageBox.Show("删除成功");
                            GetData();
                        }
                        else
                        {
                            MessageBox.Show("删除失败，请联系管理员！");
                        }
                    }                    
                }
                else if (auto == 93)
                {
                    MessageBoxResult dc = MessageBox.Show("该申请资料还有用处，请慎重删除！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dc == MessageBoxResult.OK)
                    {
                        //执行删除
                        int cont = myAs.btnDeleteation(ID);
                        if (cont > 0)
                        {
                            MessageBox.Show("删除成功");
                            GetData();
                        }
                        else
                        {
                            MessageBox.Show("删除失败，请联系管理员！");
                        }
                    }                    
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的数据！");
            }
        }

        //打印
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        //导出
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void dgvStaff_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void dgvStaff_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }
        

        //手术审核
        private void btnmanagement_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                //获取选中行
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                int Revie = Convert.ToInt32(dr.Row["Review_results"]);
                if (Revie !=92)
                {
                    //跨页面传递数据
                    operationaudit myope = new operationaudit(dr);
                    myope.ShowDialog();
                    GetData();
                }
                else
                {
                    MessageBox.Show("病人已经通过审核，无需再次审核！");
                }
            }
            else
            {
                MessageBox.Show("选择需要审核的数据！");
            }
        }

        //紧急手术提交
        private void btnemergency_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
