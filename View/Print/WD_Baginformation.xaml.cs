using System.Windows;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;
using System.Data;

namespace WpfApp.View.Print
{
    /// <summary>
    /// WD_Baginformation.xaml 的交互逻辑
    /// </summary>
    public partial class WD_Baginformation : Window
    {
        DataRowView dt;
        
        string YD;
        public WD_Baginformation(DataRowView DRV,string YDX)
        {
            InitializeComponent();
            YD = YDX;
            dt = DRV;
            
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //页面信息回填
            //患者姓名
            txt_patientname.Text = (dt.Row["Patientname"].ToString());

            //药物类型
            txt_Drugtype.Text = (dt.Row["cycle"].ToString());

            //患者性别
            txt_Patientsgender.Text = (dt.Row["detailedAttributeName"].ToString());

            //患者年龄
            txt_patientage.Text = (dt.Row["staffAge"].ToString());

            imageQRCode.Source = null;
            try
            {
                GeneratorQRCode(YD);
                btn_dow.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                YD = ex.Message;//异常
            }
        }
        // 二维码生成函数    
        private System.Drawing.Image GeneratorQRCode(string txt)
        {
            //BarcodeWriter一个智能类来编码一些内容的二维码、条形码图像 
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE//设置二维码的格式
            };
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8"); // 编码格式    
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
            int codeSizeInPixels = 260;      // 二维码长宽       
            writer.Options.Height = codeSizeInPixels;
            writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;       // 设置边框         
            BitMatrix bm = writer.Encode(txt);
            Bitmap img = writer.Write(bm);//对指定内容进行编码，并返回该码的呈现实例(渲染属性、实例使用，在方法之前调用)
            imageQRCode.Source = BitmapToBitmapImage(img);//将图片控件的数据源设为生成后的二维码
            return img;
        }
        // Bitmap --> BitmapImage       

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0; BitmapImage result = new BitmapImage();
                result.BeginInit(); result.CacheOption = BitmapCacheOption.OnLoad; result.StreamSource = stream; result.EndInit();
                result.Freeze();
                return result;
            }
        }

        //打印药袋
        private void Button_Click_1(object sender, RoutedEventArgs e)
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
