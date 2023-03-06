using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfApp.View.Thenurse
{
    /// <summary>
    /// Toapplyfor.xaml 的交互逻辑
    /// </summary>
    public partial class Toapplyfor : UserControl
    {
        //全局变量
        DataTable dt;

        //命名空间
        BLL.Thenurse.ThenurseClient myThen = new BLL.Thenurse.ThenurseClient();

        public Toapplyfor()
        {
            InitializeComponent();
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
            //绑定等级
            DataTable dtGender = myThen.btnQuerylevels().Tables[0];
            cbo_Supplies.ItemsSource = dtGender.DefaultView;
            cbo_Supplies.DisplayMemberPath = "detailedAttributeName";
            cbo_Supplies.SelectedValuePath = "detailedAttributeID";
            
           
            
            //查询所有科室
            DataTable dtFauQ = myThen.btnuerydepartment().Tables[0];
            cbo_department.ItemsSource = dtFauQ.DefaultView;
            cbo_department.DisplayMemberPath = "detailedAttributeName";
            cbo_department.SelectedValuePath = "detailedAttributeID";

            //获取当天日期            
            string danh = DateTime.Now.ToString("yyyy-MM-dd");
            txt_Applicationdate.Text = danh;
            
            //调用编号生成
            BanGon();


        }

        public void BanGon()
        {
            //自动生成编号 
            string str = dt.Rows[0]["Application"].ToString().Trim();
            int intNum = Convert.ToInt32(str.Substring(str.Length - 5));
            int intNewNumber = intNum + 1;
            string strNumber = intNewNumber.ToString();
            string strLeng = "";
            switch (strNumber.Length)
            {
                case 1:
                    strLeng = "0000" + strNumber;
                    break;
                case 2:
                    strLeng = "000" + strNumber;
                    break;
                case 3:
                    strLeng = "00" + strNumber;
                    break;
                case 4:
                    strLeng = "0" + strNumber;
                    break;
                case 5:
                    strLeng = strNumber;
                    break;
                default:
                    break;
            }
            Application.Text = "SQ" + strLeng;
        }

        public void GetData()
        {
            dt = myThen.btnQuerycation().Tables[0];

            dgvStaff.ItemsSource = dt.DefaultView;

            dgPager.ShowPages(dgvStaff, dt);

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0,0,5);//时间刷新间隔为1分钟
            timer.Start();
        }

        //清空
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cbo_Supplies.SelectedItem = null;
            Namegoods.SelectedItem = null;           
            txt_applicant.Text = null;
            cbo_department.SelectedItem = null;
            Applicationnote.Text = null;

        }

        //提交
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbo_Supplies.SelectedValuePath.ToString() != string.Empty 
                    && Namegoods.SelectedValuePath.ToString() != string.Empty                    
                    && txt_applicant.Text.ToString() != string.Empty
                    && cbo_department.SelectedValuePath.ToString() != string.Empty
                    && Applicationnote.Text.ToString() != string.Empty)
                {
                    //获取数据用于保存
                    //申请号
                    string Applicatio = Application.Text.ToString();

                    //物资等级
                    int cboSupplies = Convert.ToInt32(cbo_Supplies.SelectedValue);

                    //物资名称
                    int Namegood = Convert.ToInt32(Namegoods.SelectedValue);

                    //当天申请时间
                    string txtApplicationdate = DateTime.Now.ToString("yyyy-MM-dd");
                    

                    //申请人
                    string txtapplicant = txt_applicant.Text.ToString();

                    //申请科室
                    int cbodepartment = Convert.ToInt32(cbo_department.SelectedValue);

                    //申请数量
                    string txtNumberapplications = txt_Numberapplications.Text.ToString();

                    //备注
                    string Applicationnot = Applicationnote.Text.ToString();

                    int Con = myThen.btnInformation(Applicatio, cboSupplies, Namegood, txtApplicationdate
                        , txtapplicant, cbodepartment, txtNumberapplications, Applicationnot);
                    if (Con >0)
                    {
                        MessageBox.Show("提交申请成功");
                        GetData();
                        cbo_Supplies.SelectedItem = null;Namegoods.SelectedItem = null;
                        txt_applicant.Text = null;
                        cbo_department.SelectedItem = null;Applicationnote.Text = null;
                        txt_Numberapplications.Text = null; Application.Text = null;
                        BanGon();
                    }
                    else
                    {
                        MessageBox.Show("提交申请失败！");
                    }
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

        //触发-选择类型后搜索对应的物资
        private void cbo_Post_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Supli = Convert.ToInt32(cbo_Supplies.SelectedValue);
            DataTable dt = myThen.btnSpecifyapplication(Supli).Tables[0];
            Namegoods.ItemsSource = dt.DefaultView;
            Namegoods.DisplayMemberPath = "Namequipment";
            Namegoods.SelectedValuePath ="ID";
        }

        //正则表达式
        private void txt_Numberapplications_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
    }
}
