using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Materialinput.xaml 的交互逻辑
    /// </summary>
    public partial class Materialinput : Window
    {
        //定义一个用于循环条纹码的号码
        string A = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string code = "";

        //实例化一个随机数
        Random b = new Random();

        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();

        public Materialinput()
        {
            InitializeComponent();
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //循环条纹码
            for (int i = 0; i < 12; i++)
            {
                code = code + A.Substring(b.Next(0, A.Length), 1);
            }

            //回填条纹码
            Barcode.Text = code;

            //绑定科室
            DataTable dtGender = myAss.btnclassification().Tables[0];
            Suppliescategory.ItemsSource = dtGender.DefaultView;
            Suppliescategory.DisplayMemberPath = "detailedAttributeName";
            Suppliescategory.SelectedValuePath = "detailedAttributeID";
        }
        
        //保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Namegoods.Text.ToString() != string.Empty && Enternumber.Text.ToString() != string.Empty
                    && Theprice.Text.ToString() != string.Empty && Suppliescategory.SelectedValue.ToString() != string.Empty
                    && Generatethedate.Text.ToString() != string.Empty && Barcode.Text.ToString() != string.Empty 
                    && Expirationdatae.Text.ToString() != string.Empty)
                {
                    //获取数据
                    //物资名称
                    string Namegood = Namegoods.Text.ToString();

                    //录入数量
                    int Enternumbe = Convert.ToInt32(Enternumber.Text);

                    //零售价格
                    decimal Thepric = Convert.ToInt32(Theprice.Text);

                    //物资类型
                    int Suppliescategor = Convert.ToInt32(Suppliescategory.SelectedValue);
                    
                    //生产日期
                    string Generatethedat = Generatethedate.Text.ToString();

                    //过期时间
                    string Expirationdat = Expirationdate.Text.ToString();
                    
                    //条纹码
                    string Barcod = Barcode.Text.ToString();

                    //生产公司
                    string Expirationdata = Expirationdatae.Text.ToString();

                    //药品备注
                    string Drugnot = Drugnote.Text.ToString();

                    MessageBoxResult dc = MessageBox.Show("是否保存药品信息？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dc == MessageBoxResult.OK)
                    {
                        //允许数据为空，为空的时候默认返回0001-01-01的时间数据代表无期限！
                        if (Expirationdat == "")
                        {
                            Expirationdat = "0001-01-01";
                            //提交数据

                            int Con = myAss.btnMaterialinput(Namegood, Enternumbe, Thepric, Suppliescategor, Generatethedat, Expirationdat, Barcod, Expirationdata, Drugnot);
                            if (Con > 0)
                            {
                                MessageBox.Show("录入成功");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("录入失败！");
                            }
                        }
                        else
                        {
                            //提交数据

                            int Con = myAss.btnMaterialinput(Namegood, Enternumbe, Thepric, Suppliescategor, Generatethedat, Expirationdat, Barcod, Expirationdata, Drugnot);
                            if (Con > 0)
                            {
                                MessageBox.Show("录入成功");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("录入失败！");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请将数据填写完整");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Enternumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Theprice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
