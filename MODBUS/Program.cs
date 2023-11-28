using System.IO.Ports;
using System.Text;

using ShowWeight;

internal class Program
{

    public static async Task Main(string comName = "COM6")
    {
        var port = new MySerialPort(comName);
        var offset = 0;
        var command = new byte[] { 0x01, 0x03, 0x00, 0x01, 0x00, 0x02, 0x95, 0xCB };

        port.Open();
        Thread.Sleep(500);
        port.Write(command, offset, command.Length);
        Thread.Sleep(500);
        port.ShowWeight();
        port.Close();
    }
}
