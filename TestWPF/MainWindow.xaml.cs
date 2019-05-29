using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GISLib;

namespace TestWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GISLib.SearchClass.Instance.Init();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.ShowMap();
        }

        private void btTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = 0;
                while(i++<10)
                {
                    double x = Convert.ToDouble(tbX.Text.Trim());
                    double y = Convert.ToDouble(tbY.Text.Trim());
                    ThreadPool.QueueUserWorkItem((o) => {
                        DateTime time1 = DateTime.Now;
                        SearchClass instance = new SearchClass();
                        instance.InitMemMap();
                        //String Result = GISLib.SearchClass.Instance.SearchNearRoad(x, y);
                        SearchResult rs = instance.Search(x, y, true, true, true, double.MaxValue);
                        DateTime time2 = DateTime.Now;
                        MessageBox.Show(string.Format("Zone:{0} Road:{1} Point:{2} 耗时:{3}ms", rs.ZoneName, rs.RoadName, rs.PointName, (time2 - time1).TotalMilliseconds));
                    });
                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误;" + ex.Message);
            }
        }
    }
}
