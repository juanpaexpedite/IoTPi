using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Linq;

namespace IoTPi.Managers
{
    public class SerialPortManager
    {
        public static List<(string name, string description)> GetPortsInformation()
        {
            string[] names = SerialPort.GetPortNames();

            return (from n in names select (n, "not available")).ToList();
        }
    }
}
