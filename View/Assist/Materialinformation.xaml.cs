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
using WpfApp.View.Publicpage;

namespace WpfApp.View.Assist
{
    /// <summary>
    /// Materialinformation.xaml 的交互逻辑
    /// </summary>
    public partial class Materialinformation : Window
    {
        //声明全局变量
        DataRowView DRV;

        //命名空间
        BLL.Assist.AssistClient myAss = new BLL.Assist.AssistClient();

        public Materialinformation(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //下拉框绑定
            //绑定科室
            DataTable dtGender = myAss.btnclassification().Tables[0];
            Suppliescategory.ItemsSource = dtGender.DefaultView;
            Suppliescategory.DisplayMemberPath = "detailedAttributeName";
            Suppliescategory.SelectedValuePath = "detailedAttributeID";

            //信息回填
            //物资名称
            Namegoods.Text = (DRV.Row["Namequipment"].ToString());

            //库存数量
            Enternumber.Text = (DRV.Row["Inventory"].ToString());

            //零售价格
            Theprice.Text = (DRV.Row["Price"].ToString());

            //物资类型
            Suppliescategory.Text = (DRV.Row["category"].ToString());

            //生产日期
            Generatethedate.Text = (DRV.Row["Production"].ToString());

            string DOC = (DRV.Row["DueTotime"].ToString());
            //过期时间
            if (DOC != "0001/1/1 0:00:00")
            {
                Expirationdate.Text = (DRV.Row["DueTotime"].ToString());
            }
            else
            {
                Expirationdate.Text = "";
                //显示文本框
                //Expirationdatee.Visibility = Visibility.Visible;

                //隐藏日期选择框
                //Expirationdate.Visibility = Visibility.Hidden;

            }

            //条纹码
            Barcode.Text = (DRV.Row["Barcode"].ToString());

            //生产公司
            Expirationdatae.Text = (DRV.Row["manufacturer"].ToString());

            //物资备注
            Drugnote.Text = (DRV.Row["Note"].ToString());
            
        }


        //打印条纹码
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Stripeprint myStr = new Stripeprint(DRV);
            myStr.ShowDialog();
        }

        //关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //正则事件
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


        //保存修改
        private void btnSave_Cliack(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Namegoods.Text.ToString() != string.Empty && Enternumber.Text.ToString() != string.Empty
                    && Theprice.Text.ToString() != string.Empty && Suppliescategory.SelectedValue.ToString() != string.Empty
                    && Generatethedate.Text.ToString() != string.Empty && Barcode.Text.ToString() != string.Empty
                    && Expirationdatae.Text.ToString() != string.Empty)
                {
                    //获取数据
                    //获取ID
                    int ID = Convert.ToInt32(DRV.Row["ID"]);
                    //物资名称
                    string Namegood = Namegoods.Text.ToString();

                    //录入数量
                    int Enternumbe = Convert.ToInt32(Enternumber.Text);

                    //零售价格
                    Double Thepric = Convert.ToDouble(Theprice.Text);

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

                    int Inventor = Convert.ToInt32(DRV.Row["Inventory"]);

                    //获取当前行数据来做对比
                    string DOC = Convert.ToDateTime(DRV.Row["DueTotime"]).ToString("yyyy/MM/dd");
                    if (Expirationdat != DOC)
                    {
                        
                        //库存修改判定
                        if (Enternumbe != Inventor)
                        {
                            //提示
                            MessageBox.Show("随意修改库存录入数量可能导致无法对账的情况请确定录入的的物资是正确的数量！");
                            //保存修改
                            int Con = myAss.btnModifygoods(Namegood, Enternumbe, Thepric, Suppliescategor, Generatethedat, Expirationdat, Barcod, Expirationdata, Drugnot,ID);
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
                            if (Expirationdat == "")
                            {
                                Expirationdat = "0001-01-01";

                                int Con = myAss.btnModifygoods(Namegood, Enternumbe, Thepric, Suppliescategor, Generatethedat, Expirationdat, Barcod, Expirationdata, Drugnot, ID);
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
                        if (Enternumbe != Inventor)
                        {
                            //提示
                            MessageBox.Show("随意修改库存录入数量可能导致无法对账的情况请确定录入的的物资是正确的数量！");
                            if (Expirationdat == "")
                            {
                                Expirationdat = "0001-01-01";

                                int Con = myAss.btnModifygoods(Namegood, Enternumbe, Thepric, Suppliescategor, Generatethedat, Expirationdat, Barcod, Expirationdata, Drugnot, ID);
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
                        else
                        {
                            if (Expirationdat == "")
                            {
                                Expirationdat = "0001-01-01";

                                int Con = myAss.btnModifygoods(Namegood, Enternumbe, Thepric, Suppliescategor, Generatethedat, Expirationdat, Barcod, Expirationdata, Drugnot, ID);
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
                }
                else
                {
                    MessageBox.Show("数据不完整请重请仔细检查！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


      
    }
}
