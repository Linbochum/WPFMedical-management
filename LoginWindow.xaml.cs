using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp
{

    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DataRowView DRV;
        public LoginWindow()
        {
            InitializeComponent();

        }
        //实例化服务空间
        BLL.WDLogin.WDLoginClient myClient = new BLL.WDLogin.WDLoginClient();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //记住账号密码
            if (GetSettingString("isRemember") == "true")
            {
                loginCheckBoxUne.IsChecked = true;
                txtAccount.Text = GetSettingString("UserName");
                pwdPassword.Password = GetSettingString("UserPassword");
            }
            else
            {
                loginCheckBoxUne.IsChecked = false;
            }
        }

        //登录
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //获取页面数据
                string strAccount = txtAccount.Text.Trim();
                string strPassword = pwdPassword.Password.Trim();
                if (strAccount == string.Empty || strPassword == string.Empty)
                {
                    MessageBox.Show("请检查账号密码是否为空", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Error);
                }
                else
                {
                    DataTable dt = myClient.btnLogin_Click_CheckLogin(strAccount, strPassword).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["effective"]) == true)
                        {
                            //获取表格值
                            //当前登录用户名
                            string strStaffName = dt.Rows[0]["staffName"].ToString().Trim();
                            //当前账号
                            string Accounts = dt.Rows[0]["operatorAccounts"].ToString().Trim();
                            //管理区域（身份）
                            string deprtm = dt.Rows[0]["department"].ToString().Trim();

                            

                            
                            if (dt.Rows.Count > 0)
                            {
                                string txt_Account = txtAccount.Text.ToString();
                                string permissionGroupName = dt.Rows[0]["permissionGroupName"].ToString().Trim();
                                DateTime Time = DateTime.Now;

                                //保存登录记录
                                int count = myClient.Login_record(strStaffName, txt_Account, permissionGroupName, Time);
                                //打开窗口
                                MainWindow myMain = new MainWindow(strStaffName,Accounts,deprtm);
                                myMain.Show();
                                //关闭当前窗口
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("登录异常！");
                            }

                        }
                        else if (Convert.ToBoolean(dt.Rows[0]["effective"]) == false)
                        {
                            MessageBox.Show("该账号已被停用！", "提示！", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                    }
                    else
                    {
                        MessageBox.Show("你输入的账号密码有误", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            //记住用户名和密码
            if (Convert.ToBoolean(loginCheckBoxUne.IsChecked))
            {
                UpdateSettingString("UserName", txtAccount.Text);
                UpdateSettingString("UserPassword", pwdPassword.Password);
                UpdateSettingString("isRemember", "true");
                UpdateSettingString("isLogin", "");


            }
            else
            {
                UpdateSettingString("UserName", "");
                UpdateSettingString("UserPassword", "");
                UpdateSettingString("isRemember", "");
                UpdateSettingString("isLogin", "");
            }

        }

        //退出
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否要关闭系统？", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //关闭应用程序
                Application.Current.Shutdown();
            }
        }
        public static string GetSettingString(string settingName)
        {
            try
            {
                string settingString = ConfigurationManager.AppSettings[settingName].ToString();
                return settingString;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="valueName"></param>
        public static void UpdateSettingString(string settingName, string valueName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (ConfigurationManager.AppSettings[settingName] != null)
            {
                config.AppSettings.Settings.Remove(settingName);
            }
            config.AppSettings.Settings.Add(settingName, valueName);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
