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
    /// Receiving.xaml 的交互逻辑
    /// </summary>
    public partial class Receiving : Window
    {
        //全局变量
        DataRowView DRV;

        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();


        public Receiving(DataRowView dr)
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
        }

        //领取
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取ID
            int Then = Convert.ToInt32(DRV.Row["Thename"].ToString());

            //获取申请ID
            int ID = Convert.ToInt32(DRV.Row["ID"].ToString());

            //获取行物资总数，用于删减
            int IntC = Convert.ToInt32(DRV.Row["Inventory"].ToString());

            //根据需要申请的数删减总数
            string txtCbox = txt_applications.Text.ToString();
            int Box = Convert.ToInt32(txtCbox);
            int IntB = IntC - Box;

            int Con = myAss.btnRecipients(Then, IntB);            
            if (Con > 0)
            {
                //修改领药状态
                myAss.btnmedicine(ID);
                MessageBox.Show("领取成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("领取失败！");
            }
        }

        //取消
        private void Button_Clicb(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
