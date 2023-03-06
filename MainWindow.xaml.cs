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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.View._1;
using WpfApp.View.Assist;
using WpfApp.View.Maintenance;
using WpfApp.View.Management;
using WpfApp.View.Thenurse;
using WpfApp.View.workstation;
using WpfApplication1.MainWindow2.Resources;

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //声明全局变量
        //DataTable dt;
        public static TabControl Tc;
        public static bool blAdd = false;//不允许添加
        public DataRowView DRV;

        string Xm;
        string zh;
        string Sf;

        BLL.ChuRuYuan.ChuRuYuanClient myChu = new BLL.ChuRuYuan.ChuRuYuanClient();

        public MainWindow(string strStaffName, string Accounts,string deprtm)
        {
            DataTable dt = myChu.logintime().Tables[0];
            InitializeComponent();
            string Time = dt.Rows[0]["Login_time"].ToString().Trim();
            string ID = dt.Rows[0]["ID"].ToString().Trim();

            string Tima = dt.Rows[0]["Login_time"].ToString().Trim();
            string Tiama = dt.Rows[0]["Login_time"].ToString().Trim();

            //姓名
            Xm = strStaffName;

            //账号
            zh = Accounts;

            //身份
            Sf = deprtm;

        }
        
            private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Tc = taowa;
        }

        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {

        }
        public static void AddItems(object sender, string trname, UserControl uc)//添加选项卡 
        {
            //第一步。判断选项卡是否有内容
            if (Tc.Items.Count > 0)
            {
                //第二步；判断点击重复
                for (int i = 0; i < Tc.Items.Count; i++)
                {

                    if (((UCTabItemWithClose)Tc.Items[i]).Name == trname)//判断是否存在
                    {
                        //判断到当前已有的选项卡
                        Tc.SelectedItem = (UCTabItemWithClose)Tc.Items[i];//选中子选项
                        blAdd = true;
                        break;//当有相同的选项卡的时候跳出for循环
                    }
                }
                if (blAdd == false)
                {
                    UCTabItemWithClose item = new UCTabItemWithClose();//创建子选项卡
                    item.Name = trname;//名称
                    item.Header = string.Format(trname);//标头名称
                    item.Content = uc;//子选择里面的内容
                    item.Margin = new Thickness(0, 0, 1, 0);
                    item.Height = 28;
                    Tc.Items.Add(item);//添加子选项
                    Tc.SelectedItem = item;//选中子选项
                }
            }
            else
            {
                UCTabItemWithClose item = new UCTabItemWithClose();//创建子选项卡
                item.Name = trname;//名称
                item.Header = string.Format(trname);//标头名称
                item.Content = uc;//子选择里面的内容
                item.Margin = new Thickness(0, 0, 1, 0);
                item.Height = 28;
                Tc.Items.Add(item);//添加子选项
                Tc.SelectedItem = item;//选中子选项
                blAdd = false;
            }
        }

        //弹出新增页面
        private void The_new(object sender, MouseButtonEventArgs e)
        {
            
            cs mycs = new cs();
            AddItems(null, ls.Text, mycs);
        }

        private void taowa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //出院管理
        private void The_hospital(object sender, MouseButtonEventArgs e)
        {

        }

        //召回病人
        private void Recall_the(object sender, MouseButtonEventArgs e)
        {

        }

        //费用结算
        private void Cost_settlement(object sender, MouseButtonEventArgs e)
        {

        }

        //出院病人查询
        private void Discharge_Enquiries(object sender, MouseButtonEventArgs e)
        {

        }

        //转科病人查询
        private void Referral_enquiries(object sender, MouseButtonEventArgs e)
        {

        }

        //住院明细查询
        private void Hospital_subsidiary(object sender, MouseButtonEventArgs e)
        {
            Hospital myHo = new Hospital();
            AddItems(null, subs.Text, myHo);
        }

        //床位一览
        private void Distribution_bed(object sender, MouseButtonEventArgs e)
        {
            Distrbution mycs = new Distrbution();
            AddItems(null, bed.Text, mycs);
        }

        //办理出院
        private void Go_through(object sender, MouseButtonEventArgs e)
        {

        }

        //查对医嘱
        private void Check_the(object sender, MouseButtonEventArgs e)
        {

        }

        //领药查询
        private void Recipients_query(object sender, MouseButtonEventArgs e)
        {

        }

        //查对中药方
        private void Check_prescription(object sender, MouseButtonEventArgs e)
        {

        }

        //记账管理
        private void charge_account(object sender, MouseButtonEventArgs e)
        {

        }

        //医嘱管理
        private void The_management(object sender, MouseButtonEventArgs e)
        {
            Workstation myor = new Workstation();
            AddItems(null, manag.Text, myor);
        }

        //住院中药处方
        private void Chinese_hospital(object sender, MouseButtonEventArgs e)
        {

        }
        
        //病历浏览
        private void Medical_browse(object sender, MouseButtonEventArgs e)
        {

        }

        //住院医嘱查询
        private void Hospital_advice(object sender, MouseButtonEventArgs e)
        {

        }
        
        //功能科室病人查询
        private void Functional_inquiry(object sender, MouseButtonEventArgs e)
        {

        }

        //手术申请与报告
        private void Surgical_report(object sender, MouseButtonEventArgs e)
        {
            Querycation myQu = new Querycation();
            AddItems(null, report.Text, myQu);
        }

        //检查申请与报告
        private void Review_reports(object sender, MouseButtonEventArgs e)
        {

        }

        //辅助科室记账
        private void Assisted_bookkeeping(object sender, MouseButtonEventArgs e)
        {

        }

        //住院费用明细查询
        private void hospital_expenses(object sender, MouseButtonEventArgs e)
        {

        }

        //物资库领用申请
        private void Warehouse_application(object sender, MouseButtonEventArgs e)
        {
            Ofapplication myof = new Ofapplication();
            AddItems(null, appli.Text,myof);
        }

        //住院发药
        private void The_pills(object sender, MouseButtonEventArgs e)
        {
            IMedicineback myIm = new IMedicineback();
            AddItems(null, pilis.Text, myIm);
        }

        //退药
        private void Return_medicine(object sender, MouseButtonEventArgs e)
        {

        }

        //住院中药发放
        private void Distribution_hospital(object sender, MouseButtonEventArgs e)
        {

        }

        //药房药品设置
        private void Drug_set(object sender, MouseButtonEventArgs e)
        {
            Management myMan = new Management();
            AddItems(null, sete.Text, myMan);
        }
        //科室修改
        private void Modify_the(object sender, MouseButtonEventArgs e)
        {

        }

        //查询登录信息
        private void Log_query(object sender, MouseButtonEventArgs e)
        {
            Loginrecord myLogin = new Loginrecord();
            AddItems(null, lOG.Text, myLogin);
        }

        //账号管理
        private void Account_management(object sender, MouseButtonEventArgs e)
        {
            Accountmangent myAcc = new Accountmangent(Xm,zh,Sf);
            AddItems(null, manage.Text,myAcc);
        }

        //手术开始
        private void Surgical_start(object sender, MouseButtonEventArgs e)
        {
            Suostart mySou = new Suostart();
            AddItems(null, start.Text, mySou);
        }

        //物资申请
        private void charge_Equipment(object sender, MouseButtonEventArgs e)
        {
            Toapplyfor myToa = new Toapplyfor();
            AddItems(null, Equipment.Text,myToa);
        }
    }
}
