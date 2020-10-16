using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace IoTPi.Managers
{
    public class CP2102Manager
    {
        private SerialPort port;

        Action<bool, string> callbackstringaction;
        Action<byte[]> callbackbyteaction;
        public void Start(string comport, Action<bool, string> callbackstring, Action<byte[]> callbackbyte)
        {
            callbackstringaction = callbackstring;
            callbackbyteaction = callbackbyte;

            if (port != null && port.IsOpen)
            {
                try
                {
                    port.Close();
                }
                catch
                {

                }
            }

            port = new SerialPort();
            port.PortName = comport; //LIKE COM3
            port.Parity = Parity.None;
            port.Handshake = Handshake.None;
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.ReceivedBytesThreshold = 128;

            port.DataReceived += Port_DataReceived;
            port.ErrorReceived += Port_ErrorReceived;
            port.Disposed += Port_Disposed;
            port.PinChanged += Port_PinChanged;
            port.Open();
        }

        private void Port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            callbackstringaction?.Invoke(false, "pinchanged");
        }

        private void Port_Disposed(object sender, EventArgs e)
        {
            callbackstringaction?.Invoke(false, "disposed");
        }

        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            callbackstringaction?.Invoke(false, "error");
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int count = port.BytesToRead;
            byte[] data = new byte[count];
            port.Read(data, 0, data.Length);

            var stringdata = System.Text.Encoding.Default.GetString(data);

            callbackbyteaction?.Invoke(data);
            callbackstringaction?.Invoke(true, stringdata);
        }

        public void Stop()
        {
            port.Close();
        }
    }
}
