using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace IoTPi.Managers
{
    public class CP2102Manager
    {
        private SerialPort port;

        Action<bool, string> callbackaction;
        public void Start(string comport, Action<bool, string> callback)
        {
            callbackaction = callback;

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
            callbackaction?.Invoke(false, "pinchanged");
        }

        private void Port_Disposed(object sender, EventArgs e)
        {
            callbackaction?.Invoke(false, "disposed");
        }

        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            callbackaction?.Invoke(false, "error");
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            callbackaction?.Invoke(true, port.ReadLine());
        }

        public void Stop()
        {
            port.Close();
        }
    }
}
