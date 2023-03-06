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
    /// Reviewstatus.xaml 的交互逻辑
    /// </summary>
    public partial class Reviewstatus : Window
    {
        //全局变量
        int DRV;

        //命名空间
        BLL.Assist.AssistClient myAs = new BLL.Assist.AssistClient();

        public Reviewstatus(string txtname, string txtOrder,string txtstate,int ID)
        {
            InitializeComponent();
            //申请单号
            txt_Order.Text = txtname;

            //姓名
            txt_name.Text = txtOrder;

            //性别
            txt_state.Text = txtstate;

            DRV = ID;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //审核状态cbo_audit            
            DataTable dtGender = myAs.btnaudit().Tables[0];
            cbo_audit.ItemsSource = dtGender.DefaultView;
            cbo_audit.DisplayMemberPath = "detailedAttributeName";
            cbo_audit.SelectedValuePath = "detailedAttributeID";
            


        }

        //取消
        private void btn_so(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //提交
        private void btn_save(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(DRV.ToString());
            int cboaudit = Convert.ToInt32(cbo_audit.SelectedValue);
            string Note = note.Text.ToString();

            if (cboaudit != 0)
            {
                if (cboaudit == 92)
                {
                    int cont = myAs.btnReview(ID, cboaudit,Note);
                    if (cont > 0)
                    {
                        MessageBox.Show("审核完成!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("审核失败，请联系管理员!");
                    }
                }
                else if (cboaudit == 93)
                {                    
                    if (Note != "")
                    {
                       //保存数据
                        int cont = myAs.btnReview(ID, cboaudit, Note);
                        if (cont >0)
                        {
                            MessageBox.Show("审核完成!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("审核失败，请联系管理员!");
                        }                        
                    }
                    else
                    {
                        //提示
                        MessageBox.Show("请输入申请失败的原因！");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("请选择审核状态！");
            }


        }

        private void cbo_audit_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            //状态
            int cboaudit = Convert.ToInt32(cbo_audit.SelectedValue);
            if (cboaudit == 92)
            {
                note.IsReadOnly = true;
                note.Text = null;
            }
            else if(cboaudit == 93)
            {
                note.IsReadOnly = false;
                note.Text = null;
            }
        }
    }
}
