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

namespace WpfApp.View.Management
{
    /// <summary>
    /// Newdrugs.xaml 的交互逻辑
    /// </summary>
    public partial class Newdrugs : Window
    {
        //声明全局变量
        public int intNumber;
        public int intNumbec;

        //命名空间
        BLL.Management.ManagementClient myMan = new BLL.Management.ManagementClient();

        public Newdrugs(int DOC,int DOK)
        {
            InitializeComponent();
            //目前最大的单号
            intNumber = DOC;//西药
            intNumbec = DOK;//中药

        }

        //页面加载
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //下拉框数据绑定
            DataTable dtGD = myMan.btnDrugtype().Tables[0];
            cbo_Suppliescategory.ItemsSource = dtGD.DefaultView;
            cbo_Suppliescategory.DisplayMemberPath = "detailedAttributeName";
            cbo_Suppliescategory.SelectedValuePath = "detailedAttributeID";
        }

        public void QinTex() 
        {
            txt_Theprice.Text = null; txt_Enternumber.Text = null;
            txt_Drugname.Text = null; txt_Generatethedate.Text = null;
            txt_Qualitytime.Text = null; txt_Productionaddress.Text = null;
            txt_specifications.Text = null;

        }
        //保存
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbo_Suppliescategory.SelectedValue.ToString() != string.Empty
                    && txt_Drugnumber.Text.ToString() != string.Empty
                    && txt_Theprice.Text.ToString() != string.Empty
                    && txt_Enternumber.Text.ToString() != string.Empty
                    && txt_Drugname.Text.ToString() != string.Empty
                    && txt_Generatethedate.Text.ToString() != string.Empty
                    && txt_Qualitytime.Text.ToString() != string.Empty
                    && txt_Productionaddress.Text.ToString() != string.Empty
                    && txt_specifications.Text.ToString() != string.Empty)
                {
                    //获取信息用于保存数据
                    //下拉框信息（同时用于判断）
                    int cbo_Sup = Convert.ToInt32(cbo_Suppliescategory.SelectedValue);

                    //药物编号
                    string txtDrugnumber = txt_Drugnumber.Text.ToString();

                    //价格
                    string txtTheprice = txt_Theprice.Text.ToString();

                    //录入数量
                    string txtEnternumber = txt_Enternumber.Text.ToString();

                    //药物名称
                    string txtDrugname = txt_Drugname.Text.ToString();

                    //生产日期
                    string txtGeneratethedate = txt_Generatethedate.Text.ToString();

                    //保质日期
                    string txtQualitytime = txt_Qualitytime.Text.ToString();

                    //生产地
                    string txtProductionaddress = txt_Productionaddress.Text.ToString();

                    //药物规格
                    string txtspecifications = txt_specifications.Text.ToString();



                    if (cbo_Sup != 65)
                    {

                        QinTex();
                        int Con = myMan.btn_Cedicine(cbo_Sup,txtDrugnumber,txtTheprice,txtEnternumber,txtDrugname,
                            txtGeneratethedate,txtQualitytime,txtProductionaddress,txtspecifications);
                        if (Con >0)
                        {
                            MessageBox.Show("保存成功!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("保存失败！");
                        }
                    }
                    else
                    {
                        QinTex();
                        int Con = myMan.btn_western(cbo_Sup, txtDrugnumber, txtTheprice, txtEnternumber, txtDrugname,
                            txtGeneratethedate, txtQualitytime, txtProductionaddress, txtspecifications);
                        if (Con > 0)
                        {
                            MessageBox.Show("保存成功!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("保存失败！");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请将信息填写完整！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //取消
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //下拉框触发事件
        private void cbo_Suppliescategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int YW = Convert.ToInt32(cbo_Suppliescategory.SelectedValue);
            //用于区分新增的药物是中药还是西药
            if (YW != 65)
            {
                //调用清空
                QinTex();

                //编号事件
                ZYDY();
            }
            else
            {
                //调用清空
                QinTex();

                //编号事件
                XYDY();
            }
        }

        private void Enternumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        //西药调用
        public void XYDY() 
        {
            txt_Drugnumber.Text = null;
            //西药
            int intNewNumber = intNumber + 1;
            string strNumber = intNewNumber.ToString();
            string strLeng = "";
            switch (strNumber.Length)
            {
                case 1:
                    strLeng = "000" + strNumber;
                    break;
                case 2:
                    strLeng = "00" + strNumber;
                    break;
                case 3:
                    strLeng = "0" + strNumber;
                    break;
                case 4:
                    strLeng = strNumber;
                    break;
                default:
                    break;
            }
            txt_Drugnumber.Text = "XY" + strLeng;
        }

        //中药调用
        public void ZYDY() 
        {
            

            txt_Drugnumber.Text = null;
            //中药
            int intNewNumbec = intNumbec + 1;
            string strNumbec = intNewNumbec.ToString();
            string strLeng = "";
            switch (strNumbec.Length)
            {
                case 1:
                    strLeng = "000" + strNumbec;
                    break;
                case 2:
                    strLeng = "00" + strNumbec;
                    break;
                case 3:
                    strLeng = "0" + strNumbec;
                    break;
                case 4:
                    strLeng = strNumbec;
                    break;
                default:
                    break;
            }
            txt_Drugnumber.Text = "ZY" + strLeng;
        }
    }
}
