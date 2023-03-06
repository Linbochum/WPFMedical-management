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
    /// UC_Separate.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Separate : Window
    {
        public DataRowView DRV;
        public UC_Separate(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
        }
        

        BLL.Thenurse.ThenurseClient myThen = new BLL.Thenurse.ThenurseClient();
        //页面加载事件
        private void InsertStaffInformation_Loaded(object sender, RoutedEventArgs e)
        {
            
            //绑定科室
            DataTable dtGender = myThen.btnuerydepartment().Tables[0];
            cbo_Post.ItemsSource = dtGender.DefaultView;
            cbo_Post.DisplayMemberPath = "detailedAttributeName";
            cbo_Post.SelectedValuePath = "detailedAttributeID";
            cbo_Post.SelectedValue = Convert.ToInt32(DRV.Row["as_departmentK"]);

            //住院编号
            txt_Number.Text = (DRV.Row["Hospital"].ToString());
            //姓名
            txt_Name.Text = (DRV.Row["Patientname"].ToString());
            //科室
            //cbo_Post.Text = (DRV.Row["as_departmentK"].ToString());

             
        }

        //科室
        private void cbo_Post_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int intFid = Convert.ToInt32(cbo_Post.SelectedValue);
                DataTable dt = myThen.btnempty(intFid).Tables[0];
                cbo_gender.ItemsSource = dt.DefaultView;
                cbo_gender.DisplayMemberPath = "BedsNo";
                cbo_gender.SelectedValuePath = "BedsNo";
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_Number.Text.ToString() !=string.Empty &&
                    txt_Name.Text.ToString() !=string.Empty &&
                    cbo_Post.SelectedValue.ToString() != string.Empty)
                {

                    //获取页面上的数据
                    //根据用户ID来修改，其次用来保存到床位表里
                    int ID = Convert.ToInt32(DRV.Row["patientID"]);
                    //住院号
                    string txtNumber = txt_Name.Text.ToString();
                    
                    //病人姓名
                    string txtName = txt_Number.Text.ToString();

                    //科室
                    int cboPost = Convert.ToInt32(cbo_Post.SelectedValue);

                    //床位号
                    int cbogender = Convert.ToInt32(cbo_gender.SelectedValue);
                   
                    
                    //判断病人的床位号是否为空
                    if (cbogender >0)
                    {
                        //执行保存方法，
                        //床位表
                        int count = myThen.btnbeds(ID, cbogender);
                        //病人信息表
                        int counto = myThen.btnsavebeds(ID, cbogender);

                        if (count >0 && counto >0)
                        {
                            MessageBox.Show("分配成功！");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("分配失败！");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("请选择病人的床位号！");
                    }

                    
                }
                else
                {
                    MessageBox.Show("请检查数据是否完整！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cbo_Post_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
