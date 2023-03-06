using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Forms;
using TianWenMi;
using PrintDialog = System.Windows.Controls.PrintDialog;

namespace WpfApp.View.Publicpage
{
    /// <summary>
    /// Stripeprint.xaml 的交互逻辑
    /// </summary>
    public partial class Stripeprint : Window
    {
        DataRowView dr;
        public Stripeprint(DataRowView DRV)
        {
            InitializeComponent();
            dr = DRV;
        }
         
        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //物资名称
            Namegoods.Text = (dr.Row["Namequipment"].ToString());

            //生产公司
            Expirationdatae.Text = (dr.Row["manufacturer"].ToString());

            //有效日期，利用拼接方法
            string DOC = Convert.ToDateTime(dr.Row["Production"]).ToString("yyyy/MM/dd"); //生产日期
            string DOB = Convert.ToDateTime(dr.Row["DueTotime"]).ToString("yyyy/MM/dd");//截止日期
            //判断，如果DOC(截止时间)是等于无限期的时候就发生文字改变
            if (DOB == "0001/01/01")
            {
                Wordsreplace.Text = "生产日期：";
                Theeffectivedate.Text = DOC;
            }
            else
            {
                Theeffectivedate.Text = DOC + "-" + DOB;//拼接
            }
            

            //条纹码
            //Barcode.Text = (DRV.Row["Barcode"].ToString());
            string str = (dr.Row["Barcode"].ToString());
            if (str == "")
            {
                pictureBox1.Image = null;
                return;
            }
            C_Code128 _Code = new C_Code128();
            _Code.ValueFont = new Font("宋体", 13);
            System.Drawing.Bitmap imgTemp = _Code.GetCodeImage(str,
             C_Code128.Encode.Code128A);
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + str + "_" +
              DateTime.Now.Millisecond.ToString() + ".png";
            imgTemp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            pictureBox1.Image = System.Drawing.Image.FromFile(path);           

        }

       

        private void btn_Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                //打印visual（非文本）对象(打印区域,要打印的作业的说明)
                dialog.PrintVisual(printArea, "打印");

            }
        }
    }
}
