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
using WpfApp.View.Print;

namespace WpfApp.View._1
{
    /// <summary>
    /// cs.xaml 的交互逻辑
    /// </summary>
    public partial class cs : UserControl
    {
        public cs()
        {
            InitializeComponent();
        }

        //全局变量
        DataTable dt;

        //命名空间
        BLL.ChuRuYuan.ChuRuYuanClient myYuan = new BLL.ChuRuYuan.ChuRuYuanClient();

        //页面加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }
        public void GetData() 
        {
            dt = myYuan.btnQuerydata().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }


        //新增
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //根据查询的方式来获取第一条数据
            //取出第一条数据单号
            //通过列名来获取数据
            string str = dt.Rows[0]["Hospital"].ToString().Trim();

            //取出右边开始取4个字符（数字）
            int intNumber = Convert.ToInt32(str.Substring(str.Length - 4));
            UC_Newpatients myNewp = new UC_Newpatients(intNumber);
            myNewp.ShowDialog();
            GetData();
        }

        //修改数据
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //获取选中行
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;

                //跨页面传递数据
                
                UC_Monewpatiemts myMon = new UC_Monewpatiemts(dr);

                myMon.ShowDialog();
                GetData();
            }
            else
            {
                MessageBox.Show("请选择您要修改的数据！");
            }
        }

        //删除数据
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               if((DataRowView)dgvStaff.SelectedItem != null)
                {
                    //获取当前行
                    DataRowView drv = ((DataRowView)dgvStaff.SelectedItem);
                    //获取主键ID用于执行删除
                    int patientID = Convert.ToInt32(drv.Row["patientID"]);
                    MessageBoxResult dr = MessageBox.Show("删除数据后无法找回是否删除？", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr ==MessageBoxResult.OK)
                    {
                        //执行删除
                        int intcount = myYuan.Delete(patientID);
                        if (intcount > 0)
                        {
                            MessageBox.Show("数据删除成功！");
                            GetData();
                        }
                        else
                        {
                            MessageBox.Show("数据删除失败!");
                        }
                    }
                    

                }
                else
                {
                    MessageBox.Show("请选择要删除的数据!");

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        //打印表格
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
             if (dgvStaff.Items.Count > 0)
            {
                //2.弹出窗口
                WD_Formtoprint myPrint = new WD_Formtoprint();
                myPrint.ShowDialog();
            }
        }

        //导出
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
             MessageBoxResult dk = MessageBox.Show("病人信息将导出为Excel格式！！", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (dk == MessageBoxResult.OK)
                {
                    if (ExportToExcel.Export(dgvStaff, "在院病人信息") != false)
                    {
                        
                    }  
                }
                else
                {
                    MessageBox.Show("数据导出失败，请联系管理员！");
                }
            
        }

        //搜索框
        private void txt_Select_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string select = "";
                string strS = txt_Select.Text.Trim();
                if (strS != "")
                {
                    //模糊查询内容
                    select += "Patientname like '%" + strS + "%'";
                    //" or department like '%" + strS + "%'" +
                    //" or detailedAttributeName like '%" + strS + "%'" +
                    //" or staffAddress like '%" + strS + "%'";

                }
                System.Data.DataTable dtselect = myYuan.btnQuerydata().Tables[0];
                DataView dv = new DataView(dtselect);
                System.Data.DataTable dt = new System.Data.DataTable();

                if (select != "")
                {
                    //筛选数据
                    dv.RowFilter = select;
                    dt = dv.ToTable();
                }
                if (select == "")
                {
                    //查询全部数据
                    dt = dv.ToTable();
                    GetData();
                }
                dgvStaff.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //行数
        private void dgvStaff_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void dgvStaff_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        private void txt_Select_KeyDowna(object sender, RoutedEventArgs e)
        {
            string select = "";
            string strS = txt_Select.Text.Trim();
            if (strS != "")
            {
                //模糊查询内容
                select += "Patientname like '%" + strS + "%'";
                //" or department like '%" + strS + "%'" +
                //" or detailedAttributeName like '%" + strS + "%'" +
                //" or staffAddress like '%" + strS + "%'";

            }
            System.Data.DataTable dtselect = myYuan.btnQuerydata().Tables[0];
            DataView dv = new DataView(dtselect);
            System.Data.DataTable dt = new System.Data.DataTable();

            if (select != "")
            {
                //筛选数据
                dv.RowFilter = select;
                dt = dv.ToTable();
            }
            if (select == "")
            {
                //查询全部数据
                dt = dv.ToTable();
            }
            dgvStaff.ItemsSource = dt.DefaultView;
        }
    }
}
