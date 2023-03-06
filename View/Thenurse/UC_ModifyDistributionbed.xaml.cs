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

namespace WpfApp.View.Thenurse
{
    /// <summary>
    /// UC_ModifyDistributionbed.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ModifyDistributionbed : Window
    {
        public UC_ModifyDistributionbed()
        {
            InitializeComponent();
        }

        //全局变量
        DataTable dt;


        BLL.Thenurse.ThenurseClient myThen = new BLL.Thenurse.ThenurseClient();

        //页面加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            dt = myThen.btnQueryall().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }

        //修改床位
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //获取选中行
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;

                //跨页面传递数据
                Uc_ModifySeparate myMod = new Uc_ModifySeparate(dr);
                myMod.ShowDialog();
                GetData();
            }
        }

        
        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }

        private void dgvStaff_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void dgvStaff_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }
    }
}
