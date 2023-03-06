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
    /// Refundmanagement.xaml 的交互逻辑
    /// </summary>
    public partial class Refundmanagement : Window
    {
        //声明全局变量
        DataTable dt;
        DataRowView DRV;
        public DataRowView DRC;

        public Refundmanagement(DataRowView dc)
        {
            InitializeComponent();
            DRV = dc;
        }

        //命名空间
        BLL.Doctortion.DoctortionClient myDoct = new BLL.Doctortion.DoctortionClient();

        BLL.Management.ManagementClient myMan = new BLL.Management.ManagementClient();

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

           
        }

        public void GetData()
        {
            int ID = Convert.ToInt32(DRV.Row["ID"]);
            dt = myDoct.btndruggo(ID).Tables[0];

            dgStation.ItemsSource = dt.DefaultView;

            //dgPager.ShowPages(dgvStaff, dt);
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
                
        //退回
        private void btn_Continue(object sender, RoutedEventArgs e)
        {
            try
            {
                int ZT = Convert.ToInt32(DRC.Row["Getmedicine"].ToString());
                if (ZT != 107)
                {
                    if (cbo_name.Text != "")
                    {
                        if (note.Text != "")
                        {
                            //获取退回备注
                            string Note = Convert.ToString(note.Text);

                            //获取当前药物ID（用于退药）
                            int UID = Convert.ToInt32(DRC.Row["ID"].ToString());

                            //执行保存
                            int con = myMan.btnprocessing(UID, Note);
                            if (con > 0)
                            {
                                MessageBox.Show("退回完成！");
                                GetData();
                            }
                            else
                            {
                                MessageBox.Show("退回失败！");
                            }

                        }
                        else
                        {
                            MessageBox.Show("请填写完整退药信息！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择需要退药的信息！");
                    }
                }
                else
                {
                    MessageBox.Show("该商品还未进行开药处理无法进行退药！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        //取消
        private void btn_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
