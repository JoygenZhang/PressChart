using Sharp7;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace Charts.Data
{
    public class SignalItem
    {
        public SignalItem(DateTime timeStamp, bool up, bool down, double current, double position)
        {
            TimeStamp = timeStamp;
            Up = up;
            Down = down;
            Current = current;
            this.Position = position;
        }

        public DateTime TimeStamp { get; private set; }

        public bool Up { get; private set; }
        public bool Down { get; private set; }

        public double Current { get; private set; }
        public double Position { get; private set; }
    }

    public class SignalsCollection
    {
        S7Client plcClient = new S7Client();
        private byte[] buffer1 = new byte[32];
        private byte[] buffer2 = new byte[32];

        public ObservableCollection<SignalItem> Signals = new ObservableCollection<SignalItem>();
 

        public SignalsCollection()
        {
            //TODO:修改压机PLC地址
            //860371 	Press 8	10.130.114.16	CPU317-2 PN/DP
            // plcClient.ConnectTo("10.130.114.16", 0, 2);

            //860377 	Press 9	10.130.114.18	CPU317-2 PN/DP	0	2
            // plcClient.ConnectTo("10.130.114.18", 0, 2);

            // 860425  Press 10    10.130.114.45   CPU317 - 2 PN / DP  0   2
            // var connectTo = plcClient.ConnectTo("10.130.114.45", 0, 2);
            // 860500  Press 11    10.130.114.47   CPU317 - 2 PN / DP  0   2
            // plcClient.ConnectTo("10.130.114.47", 0, 2);
            // 860626  Press 12    10.130.114.25   CPU 317 - 2PN / DP  0   2
            plcClient.ConnectTo("10.130.114.25", 0, 2);
        }

        double ConverCurrent(int current)
        {
        
            return current / 1073741824f * 7.6;//电流的参考值是P2002=7.6
        }
        double ConverSpeed(int speed)
        {
            return speed / 1073741824f * 3000;//转速的参考值是P2000=3000

        }

        public void AddSignal()
        {
            bool up = true, down = true;


            buffer1.Initialize();
            if (plcClient.Connected)
            {
                S7Client.S7DataItem[] s7Data = new[] {new S7Client.S7DataItem(), new S7Client.S7DataItem(),};

                S7MultiVar s7Multi=new S7MultiVar(plcClient);
                s7Multi.Add((int) S7Area.DB, (int) S7WordLength.Byte, 34, 34, 4, ref buffer1);
                s7Multi.Add((int) S7Area.DB, (int) S7WordLength.Byte, 2614, 700, 32, ref buffer2);
                int i = s7Multi.Read();


                // plcClient.DBRead(2614, 700, 32, buffer1);
               
             
                int position = S7.GetDIntAt(buffer1, 0);
              double  position1 = position / 1000d;
                int current = S7.GetDIntAt(buffer2, 22);

                double converCurrent = ConverCurrent(current);
                SignalItem signalItem = new SignalItem(DateTime.Now, up, down, converCurrent,position1);
                Signals.Add(signalItem);
            }
            else
            {
                plcClient.Connect();
            }
        }


    }
}