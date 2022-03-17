using System;
using System.IO.Ports;

namespace Serial_Port_reader
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort mySerialPort = new SerialPort();
            string[] ports;

            while (!mySerialPort.IsOpen)
            {
                ports = SerialPort.GetPortNames();
                Console.WriteLine("The following serial ports were found:\n");

                foreach (string port in ports)
                {
                    Console.WriteLine(port);
                }

                Console.Write("\nChose a serial port: ");
                try 
                {
                    mySerialPort = new SerialPort(Console.ReadLine(), 9600, Parity.None, 8, StopBits.One);
                    Console.Clear();
                    mySerialPort.Open();
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("\nChose another serial port\n\n");
                }
            }

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Console.ReadLine();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received: " + indata);
        }
    }
}
