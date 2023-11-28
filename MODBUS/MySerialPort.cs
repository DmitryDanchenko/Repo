using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowWeight
{
    public class MySerialPort : SerialPort
    {
        private const int KoeffFoConvertToKg = 10;
        private const int DataSize = 9;
        private const byte FirstByte = 01;
        private readonly byte[] _bufer = new byte[DataSize];
        private int _stepIndex = 0;
        private double _weight = 0;

        public MySerialPort(string comName)
            : base()
        {
            base.PortName = comName;
            base.BaudRate = 9600;
            base.DataBits = 8;
            base.StopBits = StopBits.One;
            base.Parity = Parity.None;
            base.ReadTimeout = 0;
            base.DataReceived += GettingData;
        }

        public void ShowWeight()
        {
            PrintBufer();
            CalculateWeigt();
            Console.WriteLine();
            Console.WriteLine($"Weight: {_weight * KoeffFoConvertToKg} Kg");
        }

        public new void Open()
        {
            if (base.IsOpen)
            {
                base.Close();
            }
            base.Open();
        }

        private void GettingData(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            try
            {
                int buferSize = port.BytesToRead;
                for (var i = 0; i < buferSize; ++i)
                {
                    byte bt = (byte)port.ReadByte();

                    if (FirstByte == bt)
                    {
                        _stepIndex = 0;
                    }
                        _bufer[_stepIndex] = bt;
                        ++_stepIndex;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PrintBufer()
        {
            Console.Write("Answer sensor: ");
            for (var i = 0; i < _bufer.Count(); i++)
            {
                Console.Write(_bufer[i] + " ");
            }
            Console.WriteLine();
        }

        private void CalculateWeigt()
        {
            _weight = _bufer[3] * Math.Pow(256, 3) + _bufer[4] * Math.Pow(256, 2) + _bufer[5] * 256 + _bufer[6];
        }
    }
}
