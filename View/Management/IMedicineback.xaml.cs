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

namespace WpfApp.View.Management
{
    /// <summary>
    /// IMedicineback.xaml 的交互逻辑
    /// </summary>
    public partial class IMedicineback : UserControl
    {
        //全局变量
        DataTable dt;
        

        //命名空间
        BLL.Doctortion.DoctortionClient myDoc = new BLL.Doctortion.DoctortionClient();

        public IMedicineback()
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
            dt = myDoc.btndoctor().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //领药管理
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                Drugsmanagement myDrg = new Drugsmanagement(dr);
                myDrg.ShowDialog();
                GetData();
            }
            else
            {
                MessageBox.Show("请选择需要领药的医嘱！");
            }
        }

        //退药管理
        private void btnInsert_opening(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dc = (DataRowView)dgvStaff.SelectedItem;
                Refundmanagement myRe = new Refundmanagement(dc);
                myRe.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择需要退药的病人！");
            }
        }

        //打印
        private void btnInsert_Surgery(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dc = (DataRowView)dgvStaff.SelectedItem;
                WD_Thednformation myTh = new WD_Thednformation(dc);
                myTh.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择需要打印的数据！");
            }
        }

        //导出
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ExportToExcel.Export(dgvStaff, "全部医嘱信息") == true)
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
            try
            {
                String strStr = "";
                String strContent = txt_Select.Text.Trim();

                if (strContent != null)
                {
                    //模糊查询内容
                    strStr += "Hospital like '%" + strContent + "%'" +
                    "or Patientname like '%" + strContent + "%'" +
                    "or prescribing like '%" + strContent + "%'";
                    
                }
                //查询筛选的数据
                //将数据提取出来
                DataTable dtAll = myDoc.btndoctor().Tables[0];
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
