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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Toquery : Window
    {
        //声明全局变量
        DataRowView DRV;
        
        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();

        public Toquery(DataRowView dr) 
        {
            InitializeComponent();
            DRV = dr;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //申请号
            txt_Application.Text = (DRV.Row["Application"].ToString());

            //申请人
            txt_Theapplicant.Text = (DRV.Row["Applicant"].ToString());

            //申请物资
            txt_supplies.Text = (DRV.Row["Namequipment"].ToString());

            //申请数量
            txt_applications.Text = (DRV.Row["Thenumber"].ToString());

            //申请科室
            txt_department.Text = (DRV.Row["Department"].ToString());

            //申请备注
            txt_Note.Text = (DRV.Row["Applicationote"].ToString());

            //下拉框绑定
            DataTable dt = myAss.btnApp().Tables[0];
            cbo_Application.ItemsSource = dt.DefaultView;
            cbo_Application.DisplayMemberPath = "detailedAttributeName";
            cbo_Application.SelectedValuePath = "detailedAttributeID";

        }

        //提交
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(DRV.Row["ID"].ToString());

            

            //审核状态
            int cboApplication = Convert.ToInt32(cbo_Application.SelectedValue);

            

            if (cbo_Application.SelectedValue != null)
            {
                int con = myAss.btnauditing(ID, cboApplication);
                if (con > 0)
                {
                    MessageBox.Show("审核完成");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("审核出错！");
                }
            }
            else
            {
                MessageBox.Show("请选择审核状态");
            }

        }

        //取消
        private void Button_Clicb(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
