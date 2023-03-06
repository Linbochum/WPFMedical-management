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
    /// operationaudit.xaml 的交互逻辑
    /// </summary>
    public partial class operationaudit : Window
    {
        //全局变量
        DataRowView DRV;
        public operationaudit(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //申请单号
            txt_Order.Text = (DRV.Row["Ordera"].ToString());

            //申请科室
            cbo_department.Text = (DRV.Row["depar"].ToString());

            //手术类型
            cbo_type.Text = (DRV.Row["otyp"].ToString());

            //手术名称
            txt_operation.Text = (DRV.Row["operationame"].ToString());

            //预做时间
            dtp_EnterDate.Text = (DRV.Row["reservation"].ToString());

            //手术间号
            cbo_between.Text = (DRV.Row["Roomon"].ToString());

            //病人资料
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

            //备注资料
            //先天疾病
            cbo_disease.Text = (DRV.Row["disease"].ToString());

            //配血报告
            txt_report.Text = (DRV.Row["bloodreport"].ToString());

            //过敏药物
            txt_Allergic.Text = (DRV.Row["Allergicreport"].ToString());

            //申请人
            txt_applicant.Text = (DRV.Row["applicant"].ToString());

            //申请时间
            txt_Totime.Text = (DRV.Row["Totime"].ToString());

            //病人类型
            cbo_Patients.Text = (DRV.Row["Typeo"].ToString());

            //加急状态
            cbo_urgent.Text = (DRV.Row["Astate"].ToString());
        }

        //提交
        private void btn_save(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(DRV.Row["ID"]);
            string txtname = txt_name.Text.ToString();
            string txtOrder = txt_Order.Text.ToString();
            string txtstate = txt_state.Text.ToString();
            Reviewstatus myEv = new Reviewstatus(txtname,txtOrder, txtstate,ID);
            myEv.ShowDialog();
        }

        //取消
        private void btn_so(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
