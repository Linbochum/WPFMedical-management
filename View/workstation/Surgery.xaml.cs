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
    /// Surgery.xaml 的交互逻辑
    /// </summary>
    public partial class Surgery : Window
    {
        //全局变量
        DataRowView DRV;

        //命名空间
        BLL.Doctortion.DoctortionClient myDoc = new BLL.Doctortion.DoctortionClient();

        public Surgery(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;

            //绑定手术方式下拉框
            DataTable dtGender = myDoc.btnoperationtype().Tables[0];
            cbo_type.ItemsSource = dtGender.DefaultView;
            cbo_type.DisplayMemberPath = "detailedAttributeName";
            cbo_type.SelectedValuePath = "detailedAttributeID";
            
            //判断类型
            DataTable dk = myDoc.btnDeterminetype().Tables[0];
            cbo_disease.ItemsSource = dk.DefaultView;
            cbo_disease.DisplayMemberPath = "detailedAttributeName";
            cbo_disease.SelectedValuePath = "detailedAttributeID"; 
            
            //病人类型（成人、孩童）
            DataTable doa = myDoc.btnPatientstype().Tables[0];
            cbo_Patients.ItemsSource = doa.DefaultView;
            cbo_Patients.DisplayMemberPath = "detailedAttributeName";
            cbo_Patients.SelectedValuePath = "detailedAttributeID";
            
            //查询所有空闲手术室
            DataTable dp = myDoc.btnoperating().Tables[0];
            cbo_between.ItemsSource = dp.DefaultView;
            cbo_between.DisplayMemberPath = "Roomon";
            cbo_between.SelectedValuePath = "ID";

            //查询科室
            DataTable dpa = myDoc.btndepartment().Tables[0];
            cbo_department.ItemsSource = dpa.DefaultView;
            cbo_department.DisplayMemberPath = "detailedAttributeName";
            cbo_department.SelectedValuePath = "detailedAttributeID";
            
            //加急状态
            DataTable DCO = myDoc.btnurgent().Tables[0];
            cbo_urgent.ItemsSource = DCO.DefaultView;
            cbo_urgent.DisplayMemberPath = "detailedAttributeName";
            cbo_urgent.SelectedValuePath = "detailedAttributeID";



            //回填信息
            //姓名
            txt_name.Text = (DRV.Row["Patientname"].ToString());

            //血型
            txt_Blood.Text = (DRV.Row["Blood"].ToString());

            //年龄
            txt_age.Text = (DRV.Row["staffAge"].ToString());
            
            //性别
            txt_state.Text = (DRV.Row["detailedAttributeName"].ToString());

            //入院时间
            txt_time.Text = (DRV.Row["Admissiontime"].ToString());

            //申请科室
            cbo_department.Text = (DRV.Row["department"].ToString());
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("手术申请需要进行审核，提交后将发送到辅助科处理！");

            //生成单号
            string danh = DateTime.Now.ToString("yyyyMMddhhmmss");
            txt_Order.Text = danh;

            string Totime = DateTime.Now.ToString("yyyy/MM/dd");
            txt_Totime.Text = Totime;

        }

        //提交
        private void btn_save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbo_department.SelectedItem != null
                    && cbo_type.SelectedItem != null
                    && txt_operation.Text.ToString() != string.Empty 
                    && dtp_EnterDate.Text.ToString() != string.Empty
                    && cbo_between.SelectedItem != null
                    && cbo_disease.SelectedItem != null
                    && txt_report.Text.ToString() != string.Empty 
                    && txt_Allergic.Text.ToString() != string.Empty
                    && txt_applicant.Text.ToString() != string.Empty 
                    && txt_Totime.Text.ToString() != string.Empty
                    && cbo_Patients.Text.ToString() != string.Empty
                    && cbo_urgent.SelectedItem != null)
                {
                    //获取数据
                    
                    

                    //病人ID1
                    int ID = Convert.ToInt32(DRV.Row["patientID"]);

                    //申请单号2
                    string UID = txt_Order.Text.ToString();

                    //加急状态3
                    int cbourgent = Convert.ToInt32(cbo_urgent.SelectedValue);

                    //申请科室4
                    int cbodepartment = Convert.ToInt32(cbo_department.SelectedValue);

                    //申请手术间5
                    int cbobetween = Convert.ToInt32(cbo_between.SelectedValue);

                    ////手术病人
                    //string txtname = txt_name.Text.ToString();

                    //手术名称6
                    string txtoperation = txt_operation.Text.ToString();

                    //申请时间7
                    DateTime txtTotime = Convert.ToDateTime(txt_Totime.Text);

                    //先天疾病8
                    int cbodisease = Convert.ToInt32(cbo_disease.SelectedValue);

                    //配血报告9
                    string txtreport = txt_report.Text.ToString();

                    //过敏报告10
                    string txtAllergic = txt_Allergic.Text.ToString();

                    //申请医生（申请人）11
                    string txtapplicant = txt_applicant.Text.ToString();

                    //手术类型13
                    int cbotype = Convert.ToInt32(cbo_type.SelectedValue);
                    

                    //预做时间13
                    DateTime dtpEnterDate = Convert.ToDateTime(dtp_EnterDate.SelectedDate);


                    //病人类型12
                    int cboPatients = Convert.ToInt32(cbo_Patients.SelectedValue);
                    
                    
                    
                    //用于判断
                    //病人年龄
                    int txtage = Convert.ToInt32(txt_age.Text);                   
                    if (txtage >=18)
                    {
                        if (cboPatients ==86)
                        {
                            //保存数据                            
                            if (txtage >=70)
                            {
                                MessageBoxResult dc = MessageBox.Show("病人已经属于高龄状态，请慎重考虑！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                                if (dc == MessageBoxResult.OK)
                                {
                                    int cont = myDoc.btnapplication(ID, UID, cbourgent, cbodepartment, cbobetween, txtoperation,
                                    txtTotime, cbodisease, txtreport, txtAllergic, txtapplicant, cboPatients, dtpEnterDate, cbotype);
                                    if (cont > 0)
                                    {
                                        MessageBox.Show("提交成功！");
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("提交失败！");
                                    }
                                }                                
                            }
                            else
                            {
                                int cont = myDoc.btnapplication(ID, UID, cbourgent, cbodepartment, cbobetween, txtoperation,
                               txtTotime, cbodisease, txtreport, txtAllergic, txtapplicant, cboPatients, dtpEnterDate,cbotype);
                                if (cont > 0)
                                {
                                    MessageBox.Show("提交成功！");
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("提交失败！");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("病人已经成年，请重新选择类型！");
                            //返回为初次状态
                            cbo_Patients.SelectedItem = null;
                        }
                    }
                    else if (txtage < 18)
                    {
                        if (cboPatients == 87)
                        {
                            if (txtage <= 8)
                            {
                                MessageBoxResult dc = MessageBox.Show("病人年龄较小，手术申请请慎重！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                                if (dc == MessageBoxResult.OK)
                                {
                                    int conta = myDoc.btnapplication(ID, UID, cbourgent, cbodepartment, cbobetween, txtoperation,
                                    txtTotime, cbodisease, txtreport, txtAllergic, txtapplicant, cboPatients, dtpEnterDate,cbotype);
                                    if (conta > 0)
                                    {
                                        MessageBox.Show("提交成功！");
                                    }
                                    else
                                    {
                                        MessageBox.Show("提交失败！");
                                    }
                                }
                            }
                            else
                            {
                                int cont = myDoc.btnapplication(ID, UID, cbourgent, cbodepartment, cbobetween, txtoperation,
                                    txtTotime, cbodisease, txtreport, txtAllergic, txtapplicant, cboPatients, dtpEnterDate, cbotype);
                                if (cont > 0)
                                {
                                    MessageBox.Show("提交成功！");
                                }
                                else
                                {
                                    MessageBox.Show("提交失败！");
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("病人还未成年，请重新选择类型！");
                            //返回为初次状态
                            cbo_Patients.SelectedItem = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请填写好完整的数据！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消
        private void btn_so(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
