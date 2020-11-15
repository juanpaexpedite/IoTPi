using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static IoTPi.Managers.BytesManager;

namespace IoTPi.Models
{
    [Table("Battery")]
    public class BatterySensorDescriptor : SensorDescriptor, ICompoundSensor
    {
        public BatterySensorDescriptor() { }

        public BatterySensorDescriptor(string[] data) : base(data) { CreateChildrenSensors(); }


        //TESTING Nesting sensors

        #region Compound Sensor

        [NotMapped]
        public List<SensorDescriptor> ChildrenSensors { get; } = new List<SensorDescriptor>(); 

        private void CreateChildrenSensors()
        {
            var TotalVoltageSensor = new SensorDescriptor()
            {
                Measurement = "Voltage",
                SensorName = "Total Voltage",
                SensorId = 0,
                Units = "V"
            };

            ChildrenSensors.Add(TotalVoltageSensor);

            var CellVoltageMinSensor = new SensorDescriptor()
            {
                Measurement = "Voltage",
                SensorName = "Cell Min Voltage",
                SensorId = 1,
                Units = "V"
            };

            ChildrenSensors.Add(CellVoltageMinSensor);

            var CellVoltageMaxSensor = new SensorDescriptor()
            {
                Measurement = "Voltage",
                SensorName = "Cell Max Voltage",
                SensorId = 2,
                Units = "V"
            };

            ChildrenSensors.Add(CellVoltageMaxSensor);

            var TemperatureMinSensor = new SensorDescriptor()
            {
                Measurement = "Temperature",
                SensorName = "Temp. Min",
                SensorId = 3,
                Units = "C"
            };

            ChildrenSensors.Add(TemperatureMinSensor);

            var TemperatureMaxSensor = new SensorDescriptor()
            {
                Measurement = "Temperature",
                SensorName = "Temp. Max",
                SensorId = 4,
                Units = "C"
            };

            ChildrenSensors.Add(TemperatureMaxSensor);


            var EnergyConsumedSensor = new SensorDescriptor()
            {
                Measurement = "Energy",
                SensorName = "Energy Consumed",
                SensorId = 5,
                Units = "kWh"
            };

            ChildrenSensors.Add(EnergyConsumedSensor);



        }
        #endregion



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

            ProcessEnergyConsumed();
        }

        #region Total Voltage //0,1,2
        public double TotalVoltage { get; set; }
        public string TotalVoltageLabel { get => $"{TotalVoltage} V"; }

        private void ProcessTotalVoltage()
        {
            var bytes = ParseThreeBytes(data[0], data[1], data[2]);
            TotalVoltage = Math.Round(bytes * 0.005, 3);

            ChildrenSensors[0].Value = TotalVoltage;
            ChildrenSensors[0].ValueLabel = $"{TotalVoltage} V";
            ChildrenSensors[0].TimeStamp = DateTime.Now;
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

            ChildrenSensors[1].Value = VoltageMin;
            ChildrenSensors[1].ValueLabel = $"{VoltageMin} V";
            ChildrenSensors[1].TimeStamp = DateTime.Now;
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

            ChildrenSensors[2].Value = VoltageMax;
            ChildrenSensors[2].ValueLabel = $"{VoltageMax} V";
            ChildrenSensors[2].TimeStamp = DateTime.Now;
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

            ChildrenSensors[3].Value = TMin;
            ChildrenSensors[3].ValueLabel = $"{TMin} C";
            ChildrenSensors[3].TimeStamp = DateTime.Now;
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

            ChildrenSensors[4].Value = TMax;
            ChildrenSensors[4].ValueLabel = $"{TMax} C";
            ChildrenSensors[4].TimeStamp = DateTime.Now;
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

        #region ProcessEneryConsumed //37,38,39
        public double EnergyConsumed { get; set; }

        public string EnergyConsumedLabel { get => $"Energy Consumed {EnergyConsumed} kWh"; }

        private void ProcessEnergyConsumed()
        {
            var bytes = ParseThreeBytes(data[37], data[38], data[39]);
            EnergyConsumed = Math.Round(bytes*0.005, 3);

            ChildrenSensors[5].Value = EnergyConsumed;
            ChildrenSensors[5].ValueLabel = $"{EnergyConsumed} kWh";
            ChildrenSensors[5].TimeStamp = DateTime.Now;
        }
        #endregion

    }
}
