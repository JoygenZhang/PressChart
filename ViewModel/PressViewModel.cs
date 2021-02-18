using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Charts.Data;
using GalaSoft.MvvmLight;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;


namespace Charts.ViewModel
{
    public class PressViewModel : ViewModelBase

    {
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged("Status");
            }
        }


        public string Cycletime
        {
            get => _cycletime;
            set
            {
                _cycletime = value;
                RaisePropertyChanged("Cycletime");
            }
        }

        public string ServoCurrent
        {
            get => _servoCurrent;
            set
            {
                _servoCurrent = value;
                RaisePropertyChanged("ServoCurrent");
            }
        }

        public string ServoPosition
        {
            get => _servoPosition;
            set
            {
                _servoPosition = value;
                RaisePropertyChanged("ServoPosition");
            }
        }

        private ICommand ClearDataCommamd;


        SignalsCollection signalsCollection = new SignalsCollection();
        readonly DispatcherTimer timer = new DispatcherTimer();
        private string _servoCurrent;
        private string _servoPosition;
        private string _cycletime;
        private string _status;
        public EnumerableDataSource<SignalItem> SiSource { get; set; }

        public PressViewModel()
        {
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
            //要求数据源必须是可观察的ObservableCollection 继承自INotifyCollectionChanged
            //否则无法EnumerableDataSourceBase中的事件自动更新数据
            SiSource = new EnumerableDataSource<SignalItem>(signalsCollection.Signals);

            SiSource.SetXMapping(x => x.Position);
            SiSource.SetYMapping(y => y.Current);

           
        }


        /// <summary>
        /// 定时50ms抓取一次数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            signalsCollection.AddSignal();
            var signals = signalsCollection.Signals;
            var count = signals.Count;
            if (count >= 2)
            {
                var time1 = signals[count - 1].TimeStamp;
                var time2 = signals[count - 2].TimeStamp;
                TimeSpan timeSpan = time1 - time2;
                double timeSpanTotalMilliseconds = timeSpan.TotalMilliseconds;
                Cycletime = timeSpanTotalMilliseconds.ToString("N2")+" ms";
                ServoCurrent = signals[count - 1].Current.ToString("N2")+" A";
                ServoPosition = signals[count - 1].Position.ToString("N2")+" mm";
            }
        }
    }
}