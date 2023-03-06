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
    /// Modifythe.xaml 的交互逻辑
    /// </summary>
    public partial class Modifythe : Window
    {
        DataRowView DRV;
        //DataTable dt;
        //命名空间
        BLL.Doctortion.DoctortionClient myDoct = new BLL.Doctortion.DoctortionClient();
        public Modifythe(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            txtName.Text = (DRV.Row["Thedoctor"].ToString());

            //备注
            note.Text = (DRV.Row["D_Dote"].ToString());

            //项目名称
            txtNae.Text = (DRV.Row["Projectname"].ToString());

            //日期
            txt_dateof.Text = (DRV.Row["Toldtime"].ToString());

            DataTable dtGen = myDoct.btndrug().Tables[0];
            cbo_Prescribing.ItemsSource = dtGen.DefaultView;
            cbo_Prescribing.DisplayMemberPath = "detailedAttributeName";
            cbo_Prescribing.SelectedValuePath = "detailedAttributeID";

            DataTable dtGena = myDoct.btncycle().Tables[0];
            cbo_cycle.ItemsSource = dtGena.DefaultView;
            cbo_cycle.DisplayMemberPath = "detailedAttributeName";
            cbo_cycle.SelectedValuePath = "detailedAttributeID";
        }


        //确定
        private void btn_Save(object sender, RoutedEventArgs e)
        {
            int state = Convert.ToInt32(DRV.Row["perfrom"]);
            
            if (state != 70)
            {
                if (txtName.Text.ToString() != string.Empty && txtNae.Text.ToString() != string.Empty
                     && note.Text.ToString() != string.Empty)
                {
                    //获取数据
                    int ID = Convert.ToInt32(DRV.Row["patientID"]);//病人ID
                    int cboPrescribing = Convert.ToInt32(cbo_Prescribing.SelectedValue);//开药下拉框
                    int cbocycle = Convert.ToInt32(cbo_cycle.SelectedValue);//周期下拉框                    
                    string txt_Name = txtName.Text.ToString();//开嘱医生
                    string txt_Nae = txtNae.Text.ToString();//项目名称
                    string Note = note.Text.ToString();//备注
                    //先判断有没有选择类型和周期，否则无法保存
                    if (cboPrescribing > 0 && cbocycle > 0)
                    {
                        //修改数据
                        int coun = myDoct.btnadvice(ID, cboPrescribing, cbocycle, txt_Name, txt_Nae, Note);
                        if (coun > 0)
                        {
                            MessageBox.Show("修改成功！");
                            this.Close();
                            
                        }
                        else
                        {
                            MessageBox.Show("修改失败！");
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
                MessageBox.Show("病人已经进行开药处理无法修改！");
            }
        }


        //取消
        private void cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
