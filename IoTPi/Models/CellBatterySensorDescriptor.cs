using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static IoTPi.Managers.BytesManager;

namespace IoTPi.Models
{
    public class CellBatterySensorDescriptor : SensorDescriptor
    {
        public CellBatterySensorDescriptor() {  }

        public CellBatterySensorDescriptor(string[] data) : base(data) { }

        byte[] data = null;
        public override void SetPackage(byte[] package)
        {
            data = package;

            ProcessVoltage();
        }

        #region Voltage //26,27
        public double Voltage { get; set; }
        public string VoltageLabel { get => $"Cell Voltage {Voltage} V"; }

        private void ProcessVoltage()
        {
            var bytes = ParseTwoBytes(data[26], data[27]);
            Voltage = Math.Round(bytes * 0.005, 3);
        }
        #endregion
    }
}
