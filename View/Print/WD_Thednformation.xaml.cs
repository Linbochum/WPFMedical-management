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

namespace WpfApp.View.Print
{
    /// <summary>
    /// WD_Thednformation.xaml 的交互逻辑
    /// </summary>
    public partial class WD_Thednformation : Window
    {
        //声明全局变量
        DataRowView DRV;

        public WD_Thednformation(DataRowView dc)
        {
            InitializeComponent();
            DRV = dc;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //开嘱时间
            txt_Opentoldtime.Text = Convert.ToDateTime(DRV.Row["Toldtime"]).ToString("yyyy-MM-dd");

            //住院编号
            txt_Thenumber.Text = (DRV.Row["Hospital"].ToString());

            //住院科室
            txt_Thedepartments.Text = (DRV.Row["department"].ToString());

            //医嘱名称
            txt_Nameadvice.Text = (DRV.Row["Projectname"].ToString());

            //病人姓名
            txt_patientsname.Text = (DRV.Row["Patientname"].ToString());

            //病人性别
            txt_Patientsex.Text = (DRV.Row["detailedAttributeName"].ToString());

            //病人年龄
            txt_patientage.Text = (DRV.Row["staffAge"].ToString());

            //开嘱医生
            txt_doctor.Text = (DRV.Row["Thedoctor"].ToString());

            //医嘱备注
            txt_note.Text = (DRV.Row["D_Dote"].ToString());
        }

        //打印
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                //打印visual（非文本）对象(打印区域,要打印的作业的说明)
                dialog.PrintVisual(printArea, "打印");

            }
        }

        //取消
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
