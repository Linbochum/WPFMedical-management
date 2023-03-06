using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace WpfApp.View.workstation
{
    /// <summary>
    /// prescribing.xaml 的交互逻辑
    /// </summary>
    public partial class prescribing : Window
    {

        //声明全局变量
        DataTable dt;
        DataRowView DRV;
        DataRowView DBK;
        
        string a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        //命名空间
        BLL.Doctortion.DoctortionClient myDoct = new BLL.Doctortion.DoctortionClient();

        public prescribing(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;

        }
        public void GetData()
        {
            int ID = Convert.ToInt32(DRV.Row["ID"]);
            dt = myDoct.btndruggo(ID).Tables[0];

            dgStation.ItemsSource = dt.DefaultView;

            //dgPager.ShowPages(dgvStaff, dt);
        }


        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
            //住院号
            txt_Number.Text = (DRV.Row["Hospital"].ToString());

            //姓名
            txt_Name.Text = (DRV.Row["Patientname"].ToString());

            //性别
            txtdoctor.Text = (DRV.Row["detailedAttributeName"].ToString());

            //年龄
            txt_age.Text = (DRV.Row["staffAge"].ToString());

            //科室
            txt_department.Text = (DRV.Row["detailedAttributeName"].ToString());


            //开嘱医生
            txt_epartment.Text = (DRV.Row["Thedoctor"].ToString());

            

            int intFid = Convert.ToInt32(cbo_Type.SelectedValue);
            DataTable dt = myDoct.btnmedicine().Tables[0];
            cbo_Type.ItemsSource = dt.DefaultView;
            cbo_Type.DisplayMemberPath = "detailedAttributeName";
            cbo_Type.SelectedValuePath = "detailedAttributeID";
            
            //DataTable da = myDoct.btncycle().Tables[0];
            //cbo_cycle.ItemsSource = da.DefaultView;
            //cbo_cycle.DisplayMemberPath = "detailedAttributeName";
            //cbo_cycle.SelectedValuePath = "detailedAttributeID";

            //服用方式
            DataTable dd = myDoct.btnakeway().Tables[0];
            txt_way.ItemsSource = dd.DefaultView;
            txt_way.DisplayMemberPath = "detailedAttributeName";
            txt_way.SelectedValuePath = "detailedAttributeID";

            //开药类型
            cbo_Type.Text = (DRV.Row["cycle"].ToString());

        }

        //继续
        private void btn_Continue(object sender, RoutedEventArgs e)
        {
            //判断数据是否为空
            if (cbo_Type.SelectedItem != null && cbo_name.SelectedItem != null &&                
                txt_Uumber.Text.ToString() != string.Empty && note.Text.ToString() != string.Empty)
            {
                //获取数据保存
                int ID = Convert.ToInt32(DRV.Row["ID"]);
               

                //药品类型 中药、西药
                int cboType = Convert.ToInt32(cbo_Type.SelectedValue);

                //药品名称
                //string cboname = cbo_name.Text.ToString();
                int cboname = Convert.ToInt32(cbo_name.SelectedValue);

                //服用方式
                int cbocycle = Convert.ToInt32(txt_way.SelectedValue);

                //价格
                decimal txtprice = Convert.ToInt32(txt_price.SelectedValue);

                

                //药品备注
                string Note = note.Text.ToString();

                //开药数量
                int txtUumber = Convert.ToInt32(txt_Uumber.Text);

                int txtaUumber = Convert.ToInt32(txt_Uumber.SelectionStart);

                //保存数据
                int count = myDoct.btninformation(ID, cboType, cboname, cbocycle, txtprice, txtUumber, Note);

                //修改开药状态
                int Count = myDoct.btnperfrom(ID);

                //保存数据后恢复到null（空）的状态
                cbo_Type.SelectedItem = null;
                cbo_name.SelectedItem = null;
                //cbo_cycle.SelectedItem = null;
                note.Text = null;

                //然后刷新表格
                GetData();

                //Regex dk = new Regex("^[A-Za-z]+");

                ////判断用户输入的信息是否与正则表达式的限制一样
                //if (txt_Uumber.Text != a)
                //{
                //    MessageBox.Show("请输入正确的数量单位，如1-9！");
                //    txt_Uumber.Text = null;

                //}
                //else
                //{

                //}

            }
            else
            {
                MessageBox.Show("请补充完整病人的药品信息！");
            }
  
         
        }

        //提交
        private void btn_Save(object sender, RoutedEventArgs e)
        {

        }

        //取消
        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgStation_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        //药物类型
        private void cbo_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int intF = Convert.ToInt32(cbo_Type.SelectedValue);

            if (intF != 65)
            {
                //中药cbo_name
                DataTable dt = myDoct.btnChine().Tables[0];
                cbo_name.ItemsSource = dt.DefaultView;
                cbo_name.DisplayMemberPath = "Medicine_nane";
                cbo_name.SelectedValuePath = "ID";

               
                
            }
            else
            {
                //西药cbo_name
                DataTable dt = myDoct.btnWmedicine().Tables[0];
                cbo_name.ItemsSource = dt.DefaultView;
                cbo_name.DisplayMemberPath = "Western_medicine";
                cbo_name.SelectedValuePath = "ID";
            }
        }

        //药品名称
        private void cbo_Chine_SekectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int intF = Convert.ToInt32(cbo_Type.SelectedValue);
            if (intF == 64)
            {
                //查询中药价格                
                string intJG = Convert.ToString(cbo_name.SelectedValue);
                DataTable dt = myDoct.btnCtheprice(intJG).Tables[0];
                txt_price.ItemsSource = dt.DefaultView;
                txt_price.DisplayMemberPath = "The_price";
                txt_price.SelectedValuePath = "The_price";




            }
            else
            {
                //查询西药价格
                string intJG = Convert.ToString(cbo_name.SelectedValue);
                DataTable dt = myDoct.btnXdicine(intJG).Tables[0];
                txt_price.ItemsSource = dt.DefaultView;
                txt_price.DisplayMemberPath = "price";
                txt_price.SelectedValuePath = "price";
            }
        }

        //限制输入
        private void txt_Uumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
            //利用正则表达式的方法限制0-9的输入
            Regex RE = new Regex("[^0-9]+");
            e.Handled = RE.IsMatch(e.Text);

           

        }

        //删除
        private void btn_delete(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgStation.SelectedItem != null)
            {
                DataRowView dc = (DataRowView)dgStation.SelectedItem;
                DBK = dc;
                int UID = Convert.ToInt32(DBK.Row["ID"]);//查询病人药品的ID用于删除
                MessageBoxResult dr = MessageBox.Show("确定要删除吗?,删除后不可恢复", "提示",MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (dr == MessageBoxResult.OK)
                {
                    int coun = myDoct.btndelete(UID);
                    if (coun >0)
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
                MessageBox.Show("请选择要删除的数据！");
            }
        }
    }
}
