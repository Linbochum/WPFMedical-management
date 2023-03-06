using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.View.Thenurse
{
    /// <summary>
    /// Distrbution.xaml 的交互逻辑
    /// </summary>
    public partial class Distrbution : UserControl
    {
        public Distrbution()
        {
            InitializeComponent();
        }



        public int intLineID = 0;//公共线路ID

        BLL.Thenurse.ThenurseClient myThen = new BLL.Thenurse.ThenurseClient();

        public void SelectLine()
        {
            try
            {
                //1.移除控件
                if (WPLines != null)
                {
                    WPLines.Children.Clear();
                }

                //2.获取数据(主键ID、线路姓名、总站数、停用否)
                DataTable dtLine = myThen.btnQuerydata().Tables[0];
                if (dtLine.Rows.Count > 0)
                {

                    //3、循环自定义控件布局
                    for (int i = 0; i < dtLine.Rows.Count; i++)
                    {
                        //(主键ID、线路姓名、总站数、停用否)
                        int intID = Convert.ToInt32(dtLine.Rows[i]["detailedAttributeID"]);
                        string strLineName = dtLine.Rows[i]["detailedAttributeName"].ToString().Trim();
                        //string strStopNo = dtLine.Rows[i]["stopNo"].ToString().Trim();
                        //总站数
                        DataTable dtDetailLine = myThen.SelectDetailLine().Tables[0];
                        //string strCount = ":" + (dtDetailLine.Rows.Count + 1);
                        #region 自定义控件布局
                        //(1)边框
                        //Width="125" Height="105" Background="White" BorderBrush="SkyBlue" BorderThickness="1,1,1,1" CornerRadius="5" Margin="5"
                        Border border = new Border();
                        border.Width = 138;
                        border.Height = 120;
                        border.Background = Brushes.DodgerBlue;//背景色
                        border.BorderBrush = Brushes.SkyBlue;//边框颜色
                       
                        border.BorderThickness = new Thickness(1);//边框粗度
                        border.CornerRadius = new CornerRadius(5);//圆角
                        border.Margin = new Thickness(5);

                        //(2)网格布局
                        //Width="120" Height="100" MouseRightButtonDown="grd_MouseRightButtonDown" MouseLeftButtonDown="grd_MouseLeftButtonDown" MouseEnter="grd_MouseEnter" MouseLeave="grd_MouseLeave"
                        //ToolTip="" Tag=""
                        Grid grid = new Grid();
                        grid.Width = 138;
                        grid.Height = 120;
                        //事件：按下鼠标右键
                        grid.MouseRightButtonDown += new MouseButtonEventHandler(grd_MouseRightButtonDown);
                        //事件：按下鼠标左键
                        grid.MouseLeftButtonDown += new MouseButtonEventHandler(grd_MouseLeftButtonDown);
                        //事件：鼠标移入
                        grid.MouseEnter += new MouseEventHandler(grd_MouseEnter);
                        //事件：鼠标移开
                        grid.MouseLeave += new MouseEventHandler(grd_MouseLeave);
                        //grid.ToolTip = strStopNo;//停用否
                        grid.Tag = intID;//主键ID
                       


                        


                        
                        TextBlock txt_DetailCount = new TextBlock();
                       
                        txt_DetailCount.Text = strLineName;
                        //txt_DetailCount.Foreground = strStopNo == "停用" ? Brushes.Red : Brushes.SkyBlue;
                        txt_DetailCount.FontSize = 18;
                        txt_DetailCount.HorizontalAlignment = HorizontalAlignment.Center;
                        txt_DetailCount.VerticalAlignment = VerticalAlignment.Center;

                        grid.Children.Add(txt_DetailCount);
                        border.Child = grid;
                        WPLines.Children.Add(border);
                        #endregion

                    }
                }




            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "系统提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //页面加载事件
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SelectLine();
        }

        //新增方法
        private void mi_Insert_Click(object sender, RoutedEventArgs e)
        {

        }

        //修改方法
        private void mi_Stop_Click(object sender, RoutedEventArgs e)
        {

        }
        
        //删除方法
        private void mi_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        //鼠标右键
        public void grd_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            //目标
            cm.PlacementTarget = sender as Button;
            //位置
            cm.Placement = PlacementMode.MousePoint;
            //显示菜单
            cm.IsOpen = true;

            //获取网格信息
            Grid dg = sender as Grid;
            //记录点击的科室ID（修改，删除使用到主键ID）
            intLineID = Convert.ToInt32(dg.Tag);
        }
        
        //鼠标左键
        public void grd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            //获取ID
            int as_departmentK = Convert.ToInt32(grid.Tag);
            //查询明细
            DataTable dtDetailLine = myThen.btnrdata(as_departmentK).Tables[0];
            //绑定数据
            dgStation.ItemsSource = dtDetailLine.DefaultView;
        }

        //鼠标移入
        public void grd_MouseEnter(object sender, MouseEventArgs e) 
        {
            Grid grid = sender as Grid;
            grid.Background = Brushes.SkyBlue;
            //批量获取子元素
            List<TextBlock> list = Tools.Utils.GetChilObjects.GetChildObject<TextBlock>(grid);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Foreground = Brushes.White;//字体颜色白色
            }
        }
       
        //鼠标移出
        public void grd_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid; 
            grid.Background = Brushes.DodgerBlue;
            //批量获取子元素
            List<TextBlock> list = Tools.Utils.GetChilObjects.GetChildObject<TextBlock>(grid);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Foreground = Brushes.White;//字体颜色白色
            }
        }

        private void dgStation_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }

        //打开分配床位
        private void Points_Click(object sender, RoutedEventArgs e)
        {
            UC_Distributionbed MYdIS = new UC_Distributionbed();
            MYdIS.ShowDialog();
        }

        //查看所有病人信息
        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            UC_ModifyDistributionbed MYody = new UC_ModifyDistributionbed();
             MYody.ShowDialog();
        }
    }
}
