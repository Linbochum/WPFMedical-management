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

namespace WpfApp.View.workstation
{
    /// <summary>
    /// Advice.xaml 的交互逻辑
    /// </summary>
    public partial class Advice : Window
    {
        //声明全局变量
        DataTable dt;
        DataRowView dr;
        public DataRowView DRV;
        public Advice()
        {
            InitializeComponent();
           
        }

        

        
        //命名空间
        BLL.Doctortion.DoctortionClient myDoc = new BLL.Doctortion.DoctortionClient();


        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();

            DataTable dtGen = myDoc.btndrug().Tables[0];
            cbo_Prescribing.ItemsSource = dtGen.DefaultView;
            cbo_Prescribing.DisplayMemberPath = "detailedAttributeName";
            cbo_Prescribing.SelectedValuePath = "detailedAttributeID";

            DataTable dtGena = myDoc.btncycle().Tables[0];
            cbo_cycle.ItemsSource = dtGena.DefaultView;
            cbo_cycle.DisplayMemberPath = "detailedAttributeName";
            cbo_cycle.SelectedValuePath = "detailedAttributeID";

        }

        public void GetData() 
        {
            dt = myDoc.btnprescribing().Tables[0];

            dgStation.ItemsSource = dt.DefaultView;
            

            //dgPager.ShowPages(dgvStaff, dt);
        }

        //内容表格
        private void dgStation_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
        }

        private void dgStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            
           
        }


        //回填载入数据
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgStation.SelectedItem != null)
            {
                DataRowView dr = (DataRowView)dgStation.SelectedItem;

                DRV = dr;

                //住院号
                txt_Number.Text = (DRV.Row["Hospital"].ToString());

                //姓名
                txt_Name.Text = (DRV.Row["Patientname"].ToString());

                //性别
                txtdoctor.Text = (DRV.Row["gender"].ToString());

                //年龄
                txt_age.Text = (DRV.Row["staffAge"].ToString());

                //科室
                txt_department.Text = (DRV.Row["detailedAttributeName"].ToString());
              
            }
            else
            {
                MessageBox.Show("请选择需要开嘱的病人信息！");
            }
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        //保存
        private void btn_Save(object sender, RoutedEventArgs e)
        {

            if (txt_Number.Text.ToString() != string.Empty
                && txt_age.Text.ToString() != string.Empty
                && txtdoctor.Text.ToString() != string.Empty
                && txt_age.Text.ToString() != string.Empty
                && txt_department.ToString() != string.Empty)
            { 
                if (txtName.Text.ToString() != string.Empty && txtNae.Text.ToString() != string.Empty 
                    && note.Text.ToString() != string.Empty)
                {
                    //获取数据
                    int ID = Convert.ToInt32(DRV.Row["patientID"]);//病人ID
                    

                    int cboPrescribing = Convert.ToInt32(cbo_Prescribing.SelectedValue);//下拉框
                    int cbocycle = Convert.ToInt32(cbo_cycle.SelectedValue);//下拉框
                    DateTime dtmentry_date = Convert.ToDateTime(dtp_EnterDate.Text.ToString());//时间
                    string txt_Name = txtName.Text.ToString();//开嘱医生
                    string txt_Nae = txtNae.Text.ToString();//项目名称
                    string Note = note.Text.ToString();//备注
                    //先判断有没有选择类型和周期，否则无法保存
                    if (cboPrescribing >0 && cbocycle >0)
                    {
                        //保存数据
                        int count = myDoc.btnStoc(ID);//改变开药状态
                        
                        //保存开药数据
                        int coun = myDoc.btnAnformation(ID, cboPrescribing, cbocycle, dtmentry_date, txt_Name, txt_Nae, Note);
                        if (coun>0)
                        {
                            MessageBox.Show("新增成功！");
                            this.Close();
                            GetData();

                            //prescribing mypres = new prescribing();
                            
                            //prescribing mypres = new prescribing(dr);
                            //mypres.ShowDialog();
                            
                         
                        }
                        else
                        {
                            MessageBox.Show("新增失败！");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("请选择开药类型/周期！");
                    }
                }
                else
                {
                    MessageBox.Show("请填写完整医嘱等信息！");
                }
            }
            else
            {
                MessageBox.Show("请载入病人信息！");
            }
        }

    }
}
