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

namespace WpfApp.View._1
{
    /// <summary>
    /// Detailedquery.xaml 的交互逻辑
    /// </summary>
    public partial class Detailedquery : Window
    {
        DataRowView DRV;
        public Detailedquery(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
        }

        //页面加载事件
        private void InsertStaffInformation_Loaded(object sender, RoutedEventArgs e)
        {
            //病人姓名
            Patient.Text = (DRV.Row["Patientname"].ToString());

            //入院时间            
            string Doc = Convert.ToDateTime(DRV.Row["Admissiontime"]).ToString("yyyy/MM/dd");
            Thetime.Text = Doc;


            

            string SQ = Doc;
            string SZ = DateTime.Now.ToString("yyyy/MM/dd");
            Atpresenttime.Text = SZ;

            DateTime dt1 = DateTime.Parse(SQ);
            DateTime dt2 = DateTime.Parse(SZ);

            string Year = (dt2.Year - dt1.Year).ToString() + "年";
            Console.WriteLine(Year);

            TimeSpan ts1 = new TimeSpan(dt1.Ticks);
            TimeSpan ts2 = new TimeSpan(dt2.Ticks);

            TimeSpan ts = ts1.Subtract(ts2).Duration();

            string dateDiff = ts.Days.ToString();

            int DOK = Convert.ToInt32(dateDiff);
            MessageBox.Show("一共"+dateDiff+"天");

            //获取病房数据
            int BF = Convert.ToInt32(DRV.Row["as_departmentK"].ToString());
            
            if (BF != 105)
            {
                

            }
            else
            {
                
            }

            

            
        }

        //确定按钮
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        //取消按钮
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
