using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
namespace WpfApp.View.Maintenance
{
    /// <summary>
    /// Thenewaccount.xaml 的交互逻辑
    /// </summary>
    public partial class Thenewaccount : Window
    {
        //引用
        BLL.Maintenance.MaintenanceClient myMain = new BLL.Maintenance.MaintenanceClient();
        public Thenewaccount()
        {
            InitializeComponent();

            //绑定科室
            DataTable dtGender = myMain.btnlevel().Tables[0];
            cbo_grade.ItemsSource = dtGender.DefaultView;
            cbo_grade.DisplayMemberPath = "detailedAttributeName";
            cbo_grade.SelectedValuePath = "detailedAttributeID";
            
            //绑定性别
            DataTable dtG = myMain.btngender().Tables[0];
            cbo_gender.ItemsSource = dtG.DefaultView;
            cbo_gender.DisplayMemberPath = "detailedAttributeName";
            cbo_gender.SelectedValuePath = "detailedAttributeID"; 
            
            //绑定启用状态
            DataTable dtGa = myMain.btnstate().Tables[0];
            cbo_state.ItemsSource = dtGa.DefaultView;
            cbo_state.DisplayMemberPath = "detailedAttributeName";
            cbo_state.SelectedValuePath = "detailedAttributeID";
        }

        List<byte[]> lstBytes = new List<byte[]>();
        //浏览图片按钮
        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //声明两个变量
                Stream phpto = null;

                //打开对话框(选择图片)
                OpenFileDialog ofdWenJian = new OpenFileDialog();

                //允许用户选择多个文件
                ofdWenJian.Multiselect = false;//多选图片
                ofdWenJian.Filter = "Image Files (*.bmp, *.png, *.jpg)|*.bmp;*.png;*.jpg | All Files | *.*";

                //显示对话框
                if ((bool)ofdWenJian.ShowDialog())
                {
                    //选定的文件（选定的文件打开只读流）
                    if ((phpto = ofdWenJian.OpenFile()) != null)
                    {
                        //获取文件长度(用字节标识的流长度)
                        int length = (int)phpto.Length;
                        //声明数组变量
                        byte[] bytes = new byte[length];
                        //读取文件(字节数组，从零开始的字节偏移量，读取的字节数)
                        phpto.Read(bytes, 0, length);
                        lstBytes.Add(bytes);
                        BitmapImage image = new BitmapImage(new Uri(ofdWenJian.FileName));

                        //绑定图片
                        img_photo.Source = image;
                        txt_Load.Text = ofdWenJian.FileName;

                    }
                    else
                    {
                        MessageBox.Show("对话框没有显示，没办法选择图片！", "系统提示",
                            MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("对话框没有显示，没办法选择图片！", "系统提示",
                            MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }

        }

        //清空图片
        private void btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            //清理元素
            lstBytes.Count();
            //清空图片框
            img_photo.Source = null;
            //去除路径
            txt_Load.Text = string.Empty;
        }

        //提交
        private void button_submit(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_account.Text.ToString() != string.Empty && cbo_grade.SelectedValuePath.ToString() != string.Empty
                    && txt_name.Text.ToString() != string.Empty && cbo_gender.SelectedValuePath.ToString() != string.Empty
                    && txt_certificate.Text.ToString() != string.Empty && txt_residence.Text.ToString() != string.Empty
                    && txt_Load.Text.ToString() != string.Empty && cbo_state.SelectedValuePath.ToString() != string.Empty
                    && txt_password.Text.ToString() != string.Empty)
                {
                    //获取数据
                    //账号
                    string txtaccount = txt_account.Text.ToString();

                    //医护等级
                    int cbograde = Convert.ToInt32(cbo_grade.SelectedValue);

                    //用户姓名
                    string txtname = txt_name.Text.ToString();

                    //用户性别
                    int cbogender = Convert.ToInt32(cbo_gender.SelectedValue);

                    //密码
                    string txtpassword = txt_password.Text.ToString();

                    //证件号
                    string txtcertificate = txt_certificate.Text.ToString();

                    //户口地址
                    string txtresidence = txt_residence.Text.ToString();

                    //图片路径
                    string txtLoad = txt_Load.Text.ToString();

                    //开始执行保存

                    //判断保存是否成功
                }
                else
                {
                    MessageBox.Show("请将数据填写完整！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消
        private void buttpm_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //医护等级
        private void cbo_Suppliescategory_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int YW = Convert.ToInt32(cbo_grade.SelectedValue);

        }
    }
}
