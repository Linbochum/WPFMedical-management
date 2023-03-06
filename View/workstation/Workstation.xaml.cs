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

namespace WpfApp.View.workstation
{
    /// <summary>
    /// Workstation.xaml 的交互逻辑
    /// </summary>
    public partial class Workstation : UserControl
    {
        DataRowView DRV;

        public Workstation()
        {
            InitializeComponent();
            
        }

        //全局变量
        DataTable dt;

        //命名空间
        BLL.Doctortion.DoctortionClient myDoc = new BLL.Doctortion.DoctortionClient();

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

        //新增
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Advice myAdvic = new Advice();
            myAdvic.ShowDialog();
            GetData();
        }

        //修改
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem !=null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                //跨页面传递数据
                Modifythe myMod = new Modifythe(dr);
                myMod.ShowDialog();
                GetData();
            }
            else
            {
                MessageBox.Show("请选择要修改的病人数据！");
            }
        }

        //删除
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            
            if ((DataRowView)dgvStaff.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                DRV = dr;
                int state = Convert.ToInt32(DRV.Row["perfrom"]);
                int ID = Convert.ToInt32(DRV.Row["ID"]);//病人ID                
                if (state == 70)
                {
                    MessageBox.Show("病人已经进行开药操作无法删除！");
                }
                else
                {
                    MessageBoxResult dc = MessageBox.Show("确定要删除吗?,删除后不可恢复", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dc == MessageBoxResult.OK) 
                    {
                        MessageBox.Show("恭喜，白忙活了！");

                        //删除数据
                        int count = myDoc.btndeleteadvice(ID);
                        //刷新表格
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("大郎，该吃药了！");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的数据！");
            }
        }

        //打印表格
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        //导出
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ExportToExcel.Export(dgvStaff, "员工信息") == true)
            {
                MessageBox.Show("数据导出成功!", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("数据导出失败!", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Error);
            }
        }

        //搜索框
        private void txt_Select_KeyDowna(object sender, KeyEventArgs e)
        {

        }

        //搜索按钮
        private void txt_Select_KeyDowna(object sender, RoutedEventArgs e)
        {

        }


        private void dgvStaff_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void dgvStaff_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        //开药管理
        private void btnmanagement_Click(object sender, RoutedEventArgs e)
        {
            

            if ((DataRowView)dgvStaff.SelectedItem !=null)
            {
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                prescribing mypres = new prescribing(dr);
                mypres.ShowDialog();
                GetData();
            }
            else
            {
                MessageBox.Show("请选择需要开药的病人！");
            }
        }

        //追加医嘱
        private void btnInsert_opening(object sender, RoutedEventArgs e)
        {
            Aorders myAor = new Aorders();
            myAor.ShowDialog();
            GetData();
        }

        //打开手术申请页面
        private void btnInsert_Surgery(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgvStaff.SelectedItem !=null)
            {
                MessageBox.Show("手术申请请再三确认，一旦提交无法撤销！");
                DataRowView dr = (DataRowView)dgvStaff.SelectedItem;
                Surgery mySur = new Surgery(dr);
                mySur.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择需要申请手术的病人！");
            }
        }
    }
}
