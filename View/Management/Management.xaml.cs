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
using System.Windows.Threading;

namespace WpfApp.View.Management
{
    /// <summary>
    /// Management.xaml 的交互逻辑
    /// </summary>
    public partial class Management : UserControl
    {
        //全局变量
        DataTable dt;
        DataTable dc;

        //命名空间
        BLL.Management.ManagementClient myMan = new BLL.Management.ManagementClient();
        public Management()
        {
            InitializeComponent();
        }

        //页面加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
            GetDatB();
        }

        public void GetData()
        {
            dt = myMan.bynQuerydrugs().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);
        }
        public void GetDatB()
        {
            dc = myMan.btnCTraditional().Tables[0];

            dgvStaffc.ItemsSource = dc.DefaultView;

            dgPagec.ShowPages(dgvStaffc, dc);
        }

        //新增
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string str = dt.Rows[0]["XYID"].ToString().Trim();
            string stc = dc.Rows[0]["ZYID"].ToString().Trim();
            int intNumber = Convert.ToInt32(str.Substring(str.Length - 4));
            int intNumbec = Convert.ToInt32(stc.Substring(str.Length - 4));
            Newdrugs myNew = new Newdrugs(intNumber, intNumbec);
            myNew.ShowDialog();
            GetData();
            GetDatB();
        }

        //修改
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            

            

            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                //西药
                DataRowView dr = ((DataRowView)dgvStaff.SelectedItem);
                Modifythedrug myMod = new Modifythedrug(dr);
                myMod.ShowDialog();
                GetData();
            }
            else if((DataRowView)dgvStaffc.SelectedItem != null)
            {
                //中药
                DataRowView dc = ((DataRowView)dgvStaffc.SelectedItem);
                Modifythedrug myMod = new Modifythedrug(dc);
                myMod.ShowDialog();
                GetDatB();
            }
            else
            {
                MessageBox.Show("请选择需要修改的数据！");
            }
        }

        //删除
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //西药
            DataRowView dr = ((DataRowView)dgvStaff.SelectedItem);
           
            //中药
            DataRowView dc = ((DataRowView)dgvStaffc.SelectedItem);

            if (dr != null)
            {
                //执行删除西药
                MessageBox.Show("删除西药");

                //执行删除
                int IDC = Convert.ToInt32(dr.Row["ID"].ToString());
                int Cont = myMan.btnDeleteComd(IDC);
                if (Cont >0)
                {
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
                //刷新表单
                GetData();
            }
            else if(dc !=null)
            {
                //执行删除中药   
                MessageBox.Show("删除中药");

                //执行删除
                int ID = Convert.ToInt32(dc.Row["ID"].ToString());
                int Con = myMan.btnDeletCedcine(ID);
                if (Con > 0)
                {
                    MessageBox.Show("删除成功！");
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }

                //刷新表单
                GetDatB();
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

        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }

        //查看中药
        private void btnInsert_Chich(object sender, RoutedEventArgs e)
        {
            //隐藏
            dgvStaff.Visibility = Visibility.Hidden;
            dgPager.Visibility = Visibility.Hidden;

            //显示
            dgvStaffc.Visibility = Visibility.Visible;
            dgPagec.Visibility = Visibility.Visible;

            btnxhih.Visibility = Visibility.Visible;
            btnChih.Visibility = Visibility.Hidden;
            GetDatB(); GetData();

        }
        //查看西药
        private void btnInsert_xhich(object sender, RoutedEventArgs e)
        {
            //显示
            dgvStaff.Visibility = Visibility.Visible;
            dgPager.Visibility = Visibility.Visible;

            //隐藏
            dgvStaffc.Visibility = Visibility.Hidden;
            dgPagec.Visibility = Visibility.Hidden;

            btnxhih.Visibility = Visibility.Hidden;
            btnChih.Visibility = Visibility.Visible;
            GetData(); GetDatB();

        }

        
        
    }
}
