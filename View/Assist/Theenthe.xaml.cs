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
    /// Theenthe.xaml 的交互逻辑
    /// </summary>
    public partial class Theenthe : Window
    {
        //全局变量
        DataRowView DRV;
        DataTable dc;


        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();
        public Theenthe(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;

            //麻醉类型
            //DataTable dtGender = myAss.btnAnesthesia().Tables[0];
            //cbo_type.ItemsSource = dtGender.DefaultView;
            //cbo_type.DisplayMemberPath = "detailedAttributeName";
            //cbo_type.SelectedValuePath = "detailedAttributeID";

            //信息回填
            //申请单号
            txt_Order.Text = (DRV.Row["Ordera"].ToString());

            //项目名称
            txt_the.Text = (DRV.Row["operationame"].ToString());

            //病人类型
            txt_type.Text = (DRV.Row["Typeo"].ToString());

            //病人姓名
            txt_name.Text = (DRV.Row["Patientname"].ToString());

            //预做时间
            txt_time.Text = (DRV.Row["reservation"].ToString());

            //申请时间
            txt_apply.Text = (DRV.Row["Totime"].ToString());

            //申请科室
            txt_department.Text = (DRV.Row["depar"].ToString());
        }

        public void Todcot() 
        {
            //获取ID
            int ID = Convert.ToInt32(DRV.Row["ID"]);
            dc = myAss.btnsurgeon(ID).Tables[0];
            dgvStaff.ItemsSource = dc.DefaultView;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //手术状态
            int opet = Convert.ToInt32(DRV.Row["Opertyoe"]);
            if (opet == 99)
            {
                Todcot();
                Button1.Content = "已结束";
                Button1.Background = new SolidColorBrush(Colors.IndianRed);
                
            }
            else if (opet == 98)
            {
                Todcot();
                Button1.Content = "进行中";
                Button1.Background = new SolidColorBrush(Colors.OrangeRed);
                
            }
        }

        //确定按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //判断按钮状态
                //获取状态值
                int opet = Convert.ToInt32(DRV.Row["Opertyoe"]);

                //获取ID
                int ID = Convert.ToInt32(DRV.Row["ID"]);
                if (opet == 99)
                {
                    MessageBox.Show("该手术已结束");
                }
                else if (opet == 98)
                {
                    MessageBoxResult dc = MessageBox.Show("是否结束手术，在点击确定之后即可结束手术","提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dc == MessageBoxResult.OK)
                    {
                        //修改手术状态
                        int Con = myAss.btnended(ID);
                        if (Con > 0)
                        {
                            MessageBox.Show("手术结束");
                            this.Close();

                            //物资结算

                        }
                        else
                        {
                            MessageBox.Show("手术结束失败，请联系管理员！");
                        }
                        //打开物资结算

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消按钮
        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
