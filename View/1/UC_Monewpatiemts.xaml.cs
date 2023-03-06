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
using WpfApp.Tools.Utils;

namespace WpfApp.View._1
{
    /// <summary>
    /// UC_Monewpatiemts.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Monewpatiemts : Window
    {
        //声明公共变量
        public DataRowView DRV;
        public UC_Monewpatiemts( DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
        }

        BLL.ChuRuYuan.ChuRuYuanClient myChu = new BLL.ChuRuYuan.ChuRuYuanClient();
        private void InsertStaffInformation_Loaded(object sender, RoutedEventArgs e)
        {
            //查询性别
            DataTable dt = myChu.SelectGender().Tables[0];
            cbo_gender.ItemsSource = dt.DefaultView;
            cbo_gender.DisplayMemberPath = "Gender_name";
            cbo_gender.SelectedValuePath = "Gender_id";

            //查询科室
            DataTable dtPost = myChu.department().Tables[0];
            cbo_Post.ItemsSource = dtPost.DefaultView;
            cbo_Post.DisplayMemberPath = "detailedAttributeName";
            cbo_Post.SelectedValuePath = "attributeGatherID";

            //查询病人血型
            DataTable dos = myChu.btnBlood().Tables[0];
            Blood_type.ItemsSource = dos.DefaultView;
            Blood_type.DisplayMemberPath = "detailedAttributeName";
            Blood_type.SelectedValuePath = "detailedAttributeID";

            //查询过敏状态
            DataTable dk = myChu.btnAllergic().Tables[0];
            Drug_allergy.ItemsSource = dk.DefaultView;
            Drug_allergy.DisplayMemberPath = "detailedAttributeName";
            Drug_allergy.SelectedValuePath = "detailedAttributeID";

            //住院编号
            txt_Number.Text = (DRV.Row["Hospital"].ToString());

            //姓名
            txt_Name.Text = (DRV.Row["Patientname"].ToString());

            //科室
            cbo_Post.Text = (DRV.Row["detailedAttributeName"].ToString());

            //诊断医生
            cbo_StaffType.Text = (DRV.Row["Attending"].ToString());

            //联系电话
            txt_PhoneNumber.Text = (DRV.Row["phoneNumber"].ToString());

            //民族
            cbo_Work.Text = (DRV.Row["Nationa"].ToString());

            //身份证号
            txt_idCar.Text = (DRV.Row["idCard"].ToString());

            //出生日期
            dtp_Birthday.Text = (DRV.Row["dateOfBirth"].ToString());

            //年龄
            txt_Age.Text = (DRV.Row["staffAge"].ToString());

            //入院日期
            dtp_EnterDate.Text = (DRV.Row["Admissiontime"].ToString());

            //性别
            cbo_gender.Text = (DRV.Row["gender"].ToString());

            //户籍地址
            txt_Address.Text = (DRV.Row["staffAddress"].ToString());

            //备注
            txt_Note.Text = (DRV.Row["Nane"].ToString());

            //血型
            Blood_type.Text = (DRV.Row["Blood"].ToString());

            //过敏状态
            Drug_allergy.Text = (DRV.Row["Allergicon"].ToString());

            //紧急联系人
            Emergency_contact.Text = (DRV.Row["Emergency_contact"].ToString());

            //紧急联系人电话
            Emergency_number.Text = (DRV.Row["Emergency_number"].ToString());

            //家庭地址
            Home_address.Text = (DRV.Row["Current_address"].ToString());

        }
        //保存
        private void btnSave_Click(object sender,RoutedEventArgs e) 
        {
            try
            {
                if (txt_Number.Text.ToString() != string.Empty && txt_Name.Text.ToString() != string.Empty
                && cbo_Post.SelectedValue.ToString() != string.Empty && cbo_StaffType.Text.ToString() != string.Empty
                && txt_PhoneNumber.Text.ToString() != string.Empty && cbo_Work.Text.ToString() != string.Empty
                && txt_idCar.Text.ToString() != string.Empty && dtp_Birthday.Text.ToString() != string.Empty
                && txt_Age.Text.ToString() != string.Empty && dtp_EnterDate.Text.ToString() != string.Empty
                && cbo_gender.SelectedValue.ToString() != string.Empty)
                {
                    //获取页面上的数据
                    //下拉框数据
                    int ID = Convert.ToInt32(DRV.Row["patientID"]);
                    int cbo_PostSelectionChanged = Convert.ToInt32(cbo_Post.SelectedValue);
                    int cbogender = Convert.ToInt32(cbo_gender.SelectedValue);
                    string txtNumber = txt_Number.Text.ToString();
                    string txtName = txt_Name.Text.ToString();
                    string cboStaffType = cbo_StaffType.Text.ToString();
                    string txtPhoneNumber = txt_PhoneNumber.Text.ToString();
                    string cboWork = cbo_Work.Text.ToString();
                    string txtidCar = txt_idCar.Text.ToString();
                    DateTime dtmbirth = Convert.ToDateTime(dtp_Birthday.Text.ToString());
                    string txtAge = txt_Age.Text.ToString();
                    DateTime dtmentry_date = Convert.ToDateTime(dtp_EnterDate.Text.ToString());
                    string txtAddress = txt_Address.Text.ToString();
                    string txtNote = txt_Note.Text.ToString();

                    int count = myChu.Modifythedata(ID, cbo_PostSelectionChanged, cbogender, txtNumber, txtName, cboStaffType, txtPhoneNumber, cboWork, txtidCar,
             dtmbirth, txtAge,
             dtmentry_date, txtAddress, txtNote);
                    if (count > 0)
                    {
                        MessageBox.Show("数据修改成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("数据修改失败！");
                    }
                }
                else
                {
                    MessageBox.Show("请检查数据是否完整！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据修改失败，请联系管理员！");
                throw;
            }
        }

        //取消
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //下拉框
        private void cbo_Post_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //验证手机号
        private void txt_PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
                  国内手机号码的规则:前3位为网络识别号；第4-7位为地区编码；第8-11位为用户号码。
                   现有手机号段:

                   移动：134 135 136 137 138 139 147 148 150 151 152 157 158 159 172 178 182 183 184 187 188 198
                   联通：130 131 132 145 146 155 156 166 171 175 176 185 186  
                   电信：133 149 153 173 174 177 180 181 189 199 
                   虚拟运营商：170

                   整理后：130~139    14[5-9]    15[012356789]    166   17[0-8]    18[0-9]    19[8-9]
               */

            /*
            
            */
            string strPhoneNumber = txt_PhoneNumber.Text.Trim();
            if (strPhoneNumber.Length == 11)
            {
                ////使用正则表达式判断是否匹配               
                if (!Regex.IsMatch(strPhoneNumber, @"^0?(13[0-9]|14[5-9]|15[012356789]|166|17[0-8]|18[0-9]|19[89])[0-9]{8}$"))
                {
                    MessageBox.Show("手机号格式不对，请重新输入！", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    txt_PhoneNumber.Text = "";
                }
            }
        }

        //验证身份证
        private void txt_idCar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strIdCard = txt_idCar.Text.Trim();
            try
            {
                if (strIdCard.Length == 18)
                {

                    //闰年出生日期的合法性正则表达式 || 平年出生日期的合法性正则表达式 
                    if (!Regex.IsMatch(strIdCard, @"(^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$)") || !Regex.IsMatch(strIdCard, @"(^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$)"))
                    {
                        MessageBox.Show("身份证不合法！");
                        txt_idCar.Text = "";
                    }
                    else
                    {
                        string keys = strIdCard;
                        ////获取地址码
                        //string dmzm = keys.Substring(0, 6);
                        //性别
                        int sex = int.Parse(keys.Substring(16, 1));
                        //年
                        string birth_y = keys.Substring(6, 4);
                        //月
                        string birth_m = keys.Substring(10, 2);
                        //日
                        string birth_d = keys.Substring(12, 2);
                        ListViewItem l = new ListViewItem();
                        //绑定出生日期
                        dtp_Birthday.Text = birth_y + "年" + birth_m + "月" + birth_d + "日";
                        //l.Content = i.dmmc + "公安局";
                        //l.SubItems.Add(i.dmmcl);
                        //获取今年年份
                        string strNow = DateTime.Now.Year.ToString();
                        //把今年转化成数字
                        decimal decNow = Convert.ToDecimal(strNow);
                        //获取（截取身份证）出生年份
                        decimal decbirth_y = Convert.ToDecimal(birth_y);
                        //获取虚岁
                        decimal decAge = Convert.ToDecimal(decNow - decbirth_y) + 1;
                        //绑定年龄
                        txt_Age.Text = decAge.ToString().Trim();
                        //取余
                        if (sex % 2 == 0)
                        {
                            //l.SubItems.Add("女");
                            cbo_gender.SelectedValue = 28;//79跟下拉框ID值对应
                        } //if
                        else
                        {
                            //l.SubItems.Add("男");
                            cbo_gender.SelectedValue = 27;//78跟下拉框ID值对应
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("输入的身份证不正确。");
                txt_idCar.Text = "";
            }
            if (txt_idCar.Text.ToString().Length == 6)
            {

                string strAddress = CheckIDCardGetDiQu.LoadAddress(txt_idCar.Text.ToString());
                if (strAddress == "")
                {
                    MessageBox.Show("身份证不合法！");
                    txt_idCar.Text = "";
                }
                else
                {
                    txt_Address.Text = strAddress;
                }
            }
            if (txt_idCar.Text.ToString().Length < 6)
            {
                txt_Address.Text = "";
            }
        }
    }
}
