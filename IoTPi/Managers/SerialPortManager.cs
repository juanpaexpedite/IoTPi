using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Linq;

namespace IoTPi.Managers
{
    public class SerialPortManager
    {
        public  List<(string name, string description)> GetPortsInformation()
        {
            string[] names = SerialPort.GetPortNames();

            return (from n in names select (n, "not available")).ToList();
        }

        private SerialPort port;

        private void CheckCurrentPort(string comport)
        {
            if (port == null || port.PortName != comport)
            {
                InitializePort(comport);
            }
        }

        
        private void InitializePort(string comport)
        {
            port = new SerialPort();
            port.PortName = comport; //LIKE COM3
         //   port.Parity = Parity.None;
         //   port.Handshake = Handshake.None;
            port.BaudRate = 9600;
            //  port.DataBits = 8;
            //  port.StopBits = StopBits.One;
            //port.ReceivedBytesThreshold = 128;

            port.ReadTimeout = 500;
            port.WriteTimeout = 500;

            port.DataReceived += Port_DataReceived;
            port.ErrorReceived += Port_ErrorReceived;
            port.Disposed += Port_Disposed;
            port.PinChanged += Port_PinChanged;
            
            port.Open();
        }

        Action<string, byte[]> callbackmessageaction;
        string call = String.Empty;
        string messageheader = String.Empty;
        byte[] messagepackage = null;

        internal void Read(string comport, Action<string, byte[]> callback)
        {
            callbackmessageaction = callback;

            CheckCurrentPort(comport);

            call = "header";
            port.WriteLine(call);
            
        }

        private void Port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
          //  callbackstringaction?.Invoke(false, "pinchanged");
        }

        private void Port_Disposed(object sender, EventArgs e)
        {
          //  callbackstringaction?.Invoke(false, "disposed");
        }

        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
           // callbackstringaction?.Invoke(false, "error");
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           

            if (call == "header")
            {
                //messageheader = System.Text.Encoding.Default.GetString(data);
                messageheader = port.ReadLine();
                call = "data";
                port.WriteLine(call);
            }
            else if(call == "data")
            {
                int count = port.BytesToRead;
                byte[] data = new byte[count];
                port.Read(data, 0, data.Length);
                messagepackage = data;
                callbackmessageaction?.Invoke(messageheader, messagepackage);

            }

           
        }

        public void Stop()
        {
            port.Close();
        }
    }
}
