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
    /// Suotart.xaml 的交互逻辑
    /// </summary>
    public partial class Suotart : Window
    {
        //全局变量
        DataRowView DRV;
        
       
        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();


        public Suotart(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;


            //麻醉类型
            DataTable dtGender = myAss.btnAnesthesia().Tables[0];
            cbo_type.ItemsSource = dtGender.DefaultView;
            cbo_type.DisplayMemberPath = "detailedAttributeName";
            cbo_type.SelectedValuePath = "detailedAttributeID";

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

       

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        

        //开始
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取当前时间
            string Totime = DateTime.Now.ToString("yyyy/MM/dd");

            //预做时间            
            string txtime = Convert.ToString(txt_time.Text);

            //手术状态
            int opet = Convert.ToInt32(DRV.Row["Opertyoe"]);

            
            try
            {
                //判断数据是否完整
                if (cbo_type.SelectedValue != null &&
                    txt_surgeon.Text.ToString() != string.Empty &&
                    txt_assistant.Text.ToString() != string.Empty &&
                    txt_assistant2.Text.ToString() != string.Empty &&
                    txt_assistant3.Text.ToString() != string.Empty &&
                    txt_assistant4.Text.ToString() != string.Empty)
                {
                    //获取数据
                    //麻醉类型
                    int cbotype = Convert.ToInt32(cbo_type.SelectedValue);

                    //获取ID
                    int ID = Convert.ToInt32(DRV.Row["ID"]);

                    //主刀医生
                    string txtsurgeon = txt_surgeon.Text.ToString();

                    //助手
                    string txtassistant = txt_assistant.Text.ToString();

                    //助手2
                    string txtassistant2 = txt_assistant2.Text.ToString();

                    //助手3
                    string txtassistant3 = txt_assistant3.Text.ToString();

                    //助手4
                    string txtassistant4 = txt_assistant4.Text.ToString();

                    
                    if (opet == 100)
                    {
                       
                        //保存数据
                        MessageBoxResult dc = MessageBox.Show("手术即将开始请仔细核对信息，确定无错误信息点击确定即可开始手术", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                        if (dc == MessageBoxResult.OK)
                        {
                            int cont = myAss.btninformation(cbotype,ID,txtsurgeon,txtassistant,txtassistant2, txtassistant3, txtassistant4);
                            if (cont > 0)
                            {
                                //修改状态
                                myAss.btnongoing(ID);

                                MessageBox.Show("开始手术");

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("手术开始失败，请紧急联系管理员或者是开启紧急手术！");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("手术无法启动，请联系管理员！");
                    }
                }
                else
                {
                    MessageBox.Show("请把数据填写完整！");
                }                
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消
        private void Button_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
