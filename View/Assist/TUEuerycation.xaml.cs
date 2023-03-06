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
    /// TUEuerycation.xaml 的交互逻辑
    /// </summary>
    public partial class TUEuerycation : Window
    {
        DataRowView DRV;

        //命名空间
        BLL.Doctortion.DoctortionClient myDoc = new BLL.Doctortion.DoctortionClient();
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();
        public TUEuerycation(DataRowView dr)
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

            //申请单号
            txt_Order.Text = (DRV.Row["Ordera"].ToString());

            //手术名称
            txt_operation.Text = (DRV.Row["operationame"].ToString());

            //预做时间
            dtp_EnterDate.Text = (DRV.Row["reservation"].ToString());

            //配血报告
            txt_report.Text = (DRV.Row["bloodreport"].ToString());

            //过敏报告
            txt_Allergic.Text = (DRV.Row["Allergicreport"].ToString());

            //申请人
            txt_applicant.Text = (DRV.Row["applicant"].ToString());

            //更新时间
            string Totime = DateTime.Now.ToString("yyyy/MM/dd");
            txt_Totime.Text = Totime;
            
            

        }
        
        
        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //下拉框数据
            //申请科室
            cbo_department.Text = (DRV.Row["depar"].ToString());

            //手术类型
            cbo_type.Text = (DRV.Row["otyp"].ToString());

            //手术间号
            cbo_between.Text = (DRV.Row["Roomon"].ToString());

            //先天疾病
            cbo_disease.Text = (DRV.Row["disease"].ToString());

            //病人类型
            cbo_Patients.Text = (DRV.Row["Typeo"].ToString());

            //加急状态
            cbo_urgent.Text = (DRV.Row["Astate"].ToString());
        }

        //保存
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
                    MessageBoxResult dc = MessageBox.Show("确定需要修改病人的申请单吗？，请慎重考虑！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dc == MessageBoxResult.OK)
                    {
                        //获取数据

                        //病人数据ID
                        int ID = Convert.ToInt32(DRV.Row["ID"]);

                        //预做时间
                        DateTime dtpEnterDate = Convert.ToDateTime(dtp_EnterDate.SelectedDate);

                        //手术名称
                        string txtoperation = txt_operation.Text.ToString();

                        //先天疾病
                        int cbodisease = Convert.ToInt32(cbo_disease.SelectedValue);

                        //配血报告
                        string txtreport = txt_report.Text.ToString();

                        //过敏报告
                        string txtAllergic = txt_Allergic.Text.ToString();

                        //申请人
                        string txtapplicant = txt_applicant.Text.ToString();

                        //申请时间
                        DateTime txtTotime = Convert.ToDateTime(txt_Totime.Text);

                        //病人类型
                        int cboPatients = Convert.ToInt32(cbo_Patients.SelectedValue);

                        //加急状态
                        int cbourgent = Convert.ToInt32(cbo_urgent.SelectedValue);

                        //手术间号
                        int cbobetween = Convert.ToInt32(cbo_between.SelectedValue);

                        //保存数据
                        int Count = myAss.btnmodifyingfor(ID, dtpEnterDate, txtoperation, cbodisease, txtreport, txtAllergic,
                            txtapplicant, txtTotime, cboPatients, cbourgent, cbobetween);

                        //判断数据是否保存成功
                        if (Count > 0)
                        {
                            MessageBox.Show("修改成功！");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("修改失败!");
                        }
                    }                  
                }
                else
                {
                    MessageBox.Show("请检查数据是否填写完整！");
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
