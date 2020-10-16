using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static IoTPi.Managers.BytesManager;

namespace IoTPi.Models
{
    [Table("Battery")]
    public class BatterySensorDescriptor : SensorDescriptor
    {
        public BatterySensorDescriptor() { }

        public BatterySensorDescriptor(string[] data) : base(data) { }

        byte[] data = null;
        public override void SetPackage(byte[] package)
        {
            data = package;

            ProcessTotalVoltage();
            ProcessCurrentInSign();
            ProcessCurrentIn();
            ProcessCurrent2InSign();
            ProcessCurrent2In();

            ProcessVoltageMin();
            ProcessCellVMin();
            ProcessVoltageMax();
            ProcessCellVMax();
            ProcessTMin();
            ProcessCellTMin();
            ProcessTMax();
            ProcessCellTMax();
        }

        #region Total Voltage //0,1,2
        public double TotalVoltage { get; set; }
        public string TotalVoltageLabel { get => $"{TotalVoltage} V"; }

        private void ProcessTotalVoltage()
        {
            var bytes = ParseThreeBytes(data[0], data[1], data[2]);
            TotalVoltage = Math.Round(bytes * 0.005, 3);
        }
        #endregion

        #region Current In Sign //3
        public char CurrentInSign { get; set; }

        private void ProcessCurrentInSign()
        {
            CurrentInSign = ParseSign(data[3]);
        }
        #endregion

        #region Current In //4,5
        public double CurrentIn { get; set; }
        public string CurrentInLabel { get => $"{CurrentInSign} {CurrentIn} A"; }
        private void ProcessCurrentIn()
        {
            var bytes = ParseTwoBytes(data[4], data[5]);
            CurrentIn = Math.Round(bytes * 0.125, 3);
        }
        #endregion

        #region Current2 In Sign //6
        public char Current2InSign { get; set; }

        private void ProcessCurrent2InSign()
        {
            Current2InSign = ParseSign(data[6]);
        }
        #endregion

        #region Current2 In //7,8
        public double Current2In { get; set; }
        public string Current2InLabel { get => $"{Current2InSign} {Current2In} A"; }
        private void ProcessCurrent2In()
        {
            var bytes = ParseTwoBytes(data[7], data[8]);
            Current2In = Math.Round(bytes * 0.125, 3);
        }
        #endregion

        #region Voltage Min //12,13
        public double VoltageMin { get; set; }
        public string VoltageMinLabel { get => $"Cell Voltage Min {VoltageMin} V"; }

        private void ProcessVoltageMin()
        {
            var bytes = ParseTwoBytes(data[12], data[13]);
            VoltageMin = Math.Round(bytes * 0.005, 3);
        }
        #endregion

        #region CellVMin //14
        public byte CellVMin { get; set; }

        public string CellVMinLabel { get => $"#Cell Voltage Min: {CellVMin}" ;}

        private void ProcessCellVMin()
        {
            CellVMin = ParseOneByte(data[14]);
        }
        #endregion

        #region Voltage Max //15,16
        public double VoltageMax { get; set; }
        public string VoltageMaxLabel { get => $"Cell Voltage Max {VoltageMax} V"; }

        private void ProcessVoltageMax()
        {
            var bytes = ParseTwoBytes(data[15], data[16]);
            VoltageMax = Math.Round(bytes * 0.005, 3);
        }
        #endregion

        #region CellVMax //17
        public byte CellVMax { get; set; }

        public string CellVMaxLabel { get => $"#Cell Voltage Max: {CellVMax}"; }

        private void ProcessCellVMax()
        {
            CellVMax = ParseOneByte(data[17]);
        }
        #endregion

        #region TMin //18,19
        public double TMin { get; set; }
        public string TMinLabel { get => $"Cell Temp. Min {TMin} ºC"; }

        private void ProcessTMin()
        {
            var bytes = ParseTwoBytes(data[18], data[19]);
            TMin = Math.Round(bytes - 276.0, 3);
        }
        #endregion

        #region CellTMin //20
        public byte CellTMin { get; set; }

        public string CellTMinLabel { get => $"#Cell Temp. Min: {CellTMin}"; }

        private void ProcessCellTMin()
        {
            CellTMin = ParseOneByte(data[20]);
        }
        #endregion

        #region TMax //21,22
        public double TMax { get; set; }
        public string TMaxLabel { get => $"Cell Temp. Max {TMax} ºC"; }

        private void ProcessTMax()
        {
            var bytes = ParseTwoBytes(data[21], data[22]);
            TMax = Math.Round(bytes - 276.0, 3);
        }
        #endregion

        #region CellTMax //23
        public byte CellTMax { get; set; }

        public string CellTMaxLabel { get => $"#Cell Temp. Max: {CellTMax}"; }

        private void ProcessCellTMax()
        {
            CellTMax = ParseOneByte(data[23]);
        }
        #endregion

    }
}
