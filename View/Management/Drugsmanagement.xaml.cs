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
using WpfApp.View.Print;

namespace WpfApp.View.Management
{
    /// <summary>
    /// Drugsmanagement.xaml 的交互逻辑
    /// </summary>
    public partial class Drugsmanagement : Window
    {
        //声明全局变量
        DataTable dt;
        DataRowView DRV;
        public DataRowView DRC;
        

        //命名空间
        BLL.Doctortion.DoctortionClient myDoct = new BLL.Doctortion.DoctortionClient();

        BLL.Management.ManagementClient myMan = new BLL.Management.ManagementClient();

        public Drugsmanagement(DataRowView dr)
        {
            InitializeComponent();
            DRV = dr;
            
            
        }

        //页面加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
            //住院号
            txt_Number.Text = (DRV.Row["Hospital"].ToString());

            //姓名
            txt_Name.Text = (DRV.Row["Patientname"].ToString());

            //性别
            txtdoctor.Text = (DRV.Row["detailedAttributeName"].ToString());

            //年龄
            txt_age.Text = (DRV.Row["staffAge"].ToString());

            //科室
            txt_department.Text = (DRV.Row["detailedAttributeName"].ToString());

            //开药类型
            cbo_Type.Text = (DRV.Row["cycle"].ToString());

            //开嘱医生
            txt_epartment.Text = (DRV.Row["Thedoctor"].ToString());            

            //医嘱信息
            note.Text = (DRV.Row["D_Dote"].ToString());
        }

        public void GetData()
        {
            int ID = Convert.ToInt32(DRV.Row["ID"]);
            dt = myDoct.btndruggo(ID).Tables[0];

            dgStation.ItemsSource = dt.DefaultView;

            //dgPager.ShowPages(dgvStaff, dt);
        }
        //领取
        private void btn_Continue(object sender, RoutedEventArgs e)
        {
            
            try
            {
                
                if (DRC != null)
                {
                    int UID = Convert.ToInt32(DRC.Row["ID"].ToString());
                    if (cbo_name != null)
                    {
                        //开始领药流程
                        //根据ID方式
                        int TonZhn = Convert.ToInt32(DRC.Row["Getmedicine"].ToString());
                        if (TonZhn != 106)
                        {
                            //药物ID用于扣除数量
                            int ID = Convert.ToInt32(DRC.Row["Drugname"].ToString());
                            //获取指定药物的总数
                            int SL = Convert.ToInt32(DRC.Row["inventory"].ToString());

                            //将总数减需要领取的数量
                            int JG = SL - Convert.ToInt32(DRC.Row["WDNMD"].ToString());
                            //重新重新写入库存数量（领取后）
                            myMan.btncalculation(ID, JG);
                            //领药
                            int con = myMan.btnSinglereceive(UID);
                            if (con > 0)
                            {
                                MessageBox.Show("领取成功！");
                                GetData();
                            }
                            else
                            {
                                MessageBox.Show("领取失败！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("该药物已经领取无法再次领取！");
                        }                                                
                    }                    
                }
                else
                {
                    MessageBox.Show("请选择需要单独领药的信息！");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        //打印药袋
        private void btn_print(object sender, RoutedEventArgs e)
        {
            string YDX = Convert.ToString(note.Text);
            WD_Baginformation myBag = new WD_Baginformation(DRV,YDX);
            myBag.ShowDialog();
        }

        //取消
        private void btn_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //选择
        private void btn_choose(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dgStation.SelectedItem != null)
            {
                //获取当前行数据
                DataRowView dc = (DataRowView)dgStation.SelectedItem;                
                DRC = dc;
                //药物名称
                cbo_name.Text = (dc.Row["Medicines"].ToString());
            }
            else
            {
                MessageBox.Show("请选择需要单独领取的药物！");
            }
        }

        //领取全部
        //private void btn_receive(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult dr = MessageBox.Show("是否要领取全部药物？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
        //    if (dr == MessageBoxResult.OK)
        //    {
        //        try
        //        {
        //            int UID = Convert.ToInt32(DRV.Row["ID"].ToString());
        //            int CON = myMan.btnAllreceive(UID);
        //            if (CON > 0)
        //            {
        //                MessageBox.Show("领取成功！");
        //                GetData();
        //            }
        //            else
        //            {
        //                MessageBox.Show("领取失败!");
        //            }
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }

        //    }
        //}
    }
}
