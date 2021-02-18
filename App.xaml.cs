using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Charts
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //TODO: 如果是给多个压机使用，需要修改
        private static readonly Mutex mutex = new Mutex(true, "压机12电流监控 By Joygen");
        public App()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                this.InitializeComponent();
            }
            else
            {
                MessageBox.Show("已经打开了一个应用程序！", "提醒", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
            }

        }
    }
}
