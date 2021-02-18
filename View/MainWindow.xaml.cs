using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using Charts.Data;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;

namespace Charts
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window

    {
      

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // timer.Interval = TimeSpan.FromMilliseconds(10);
            // timer.Tick += Timer_Tick;
            // timer.Start();
            // //要求数据源必须是可观察的ObservableCollection 继承自INotifyCollectionChanged
            // //否则无法EnumerableDataSourceBase中的事件自动更新数据
            // siSource1 = new EnumerableDataSource<SignalItem>(signalsCollection.Signals);
            //
            // siSource1.SetXMapping(x => x.Current);
            // siSource1.SetYMapping(y => y.Position);
            // // siSource1.SetXMapping(x =>dateAxis.ConvertToDouble(x.TimeStamp) );
            // // siSource1.SetYMapping(y => y.Position);
            // // // ((NumericTicksProvider)((NumericAxis)plotter.HorizontalAxis).TicksProvider).MinStep = 0.5;
            // // Plotter.AddLineGraph(siSource1, new Pen(Brushes.Blue, 1),
            // //     new CirclePointMarker {Size = 3, Fill = Brushes.Red}, new PenDescription("Current"));
            // // siSource2 = new EnumerableDataSource<SignalItem>(signalsCollection.Signals);
            // // siSource2.SetXMapping(x => dateAxis.ConvertToDouble(x.TimeStamp));
            // // siSource2.SetYMapping(y => y.Current);
            // Plotter.AddLineGraph(siSource1, new Pen(Brushes.DarkGreen, 1),
            //     new CirclePointMarker {Size = 3, Fill = Brushes.DarkOrchid}, new PenDescription("Position"));
        }

        private void Plotter_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            //获得鼠标的位置
            Point mousePosition = e.GetPosition(Plotter);
            //获得曲线的转换关系
            var transform = Plotter.Viewport.Transform;
            //将鼠标的位置转换成鼠标的点在图表中水平和垂直轴的值
            Point mousePosInData = mousePosition.ScreenToData(transform);
            double height = Plotter.ActualHeight;
            mousePosInData.X = mousePosInData.X - (height / 2000);
        }




        // private void CoordinateGraph_OnGotMouseCapture(object sender, MouseEventArgs e)
        // {
        //     // //
        //     // Point mousePosition = e.GetPosition(Plotter);
        //     // var transform = Plotter.Viewport.Transform;
        //     // Point mousePosInData = mousePosition.ScreenToData(transform);
        //     // double height = Plotter.ActualHeight;
        //     // CoordinateGraph.XTextMapping = delegate { return dateAxis.ConvertFromDouble(mousePosInData.X).ToString(); };
        // }
    }
}