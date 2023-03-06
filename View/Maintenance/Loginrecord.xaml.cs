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

namespace WpfApp.View.Maintenance
{
    /// <summary>
    /// Loginrecord.xaml 的交互逻辑
    /// </summary>
    public partial class Loginrecord : UserControl
    {
        //声明全局变量
        DataTable dt;

        //引用服务
        BLL.Maintenance.MaintenanceClient myMain = new BLL.Maintenance.MaintenanceClient();

        public Loginrecord()
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
            dt = myMain.btnLoginrecord().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

       

        //删除数据
        private void btnInsert_opening(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                int ID = Convert.ToInt32(dr.Row["id"]);
                MessageBox.Show("是否要删除登录记录？");
                MessageBoxResult dc = MessageBox.Show("删除后无法恢复，请考虑清除！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (dc == MessageBoxResult.OK)
                {
                    //执行删除
                    int Cont = myMain.btnDeletingLoginRecords(ID);
                    //判断是否删除成功                    
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
                MessageBox.Show("请选择需要删除的数据！");
            }
        }

        //导出记录
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            //导出登录记录
            ExportToExcel.Export(dgvStaff, "登录记录");                       
        }

        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {
            try
            {
                String strStr = "";
                String strContent = txt_Select.Text.Trim();

                if (strContent != null)
                {
                    //模糊查询内容
                    strStr += "operatorAccounts like '%" + strContent + "%'" +
                    "or Account_owner like '%" + strContent + "%'" +
                    "or Account_type like '%" + strContent + "%'";

                }
                //查询筛选的数据
                //将数据提取出来
                DataTable dtAll = myMain.btnLoginrecord().Tables[0];
                //可以自定义视图、可以用来排序筛选
                DataView dv = new DataView(dtAll);

                //存放已经筛选完成的数据
                DataTable dt = new DataTable();

                if (strContent != "")
                {
                    //筛选数据
                    dv.RowFilter = strStr;
                    dt = dv.ToTable();//Totable转换
                }

                if (strContent == "")
                {
                    //查询全部数据
                    dt = dv.ToTable();
                }
                dgvStaff.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
