using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;


namespace IoTPi.Models
{
   
    public class SensorDescriptor : ReactiveObject, IDataDescriptor
    {
        /// <summary>
        /// This is only for sqlite
        /// </summary>
        public SensorDescriptor()
        {

        }

        /// <summary>
        /// //Data in this case has  6 parts: Id, Measure,Name, Area, Value, Units
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public SensorDescriptor(string[] data)
        {
            SetData(data);
        }

        private int id;

        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        private int sensorid;
        public int SensorId
        {
            get => sensorid;
            set => this.RaiseAndSetIfChanged(ref sensorid, value);
        }

      

        private string sensorname;
        [NotMapped]
        public string SensorName
        {
            get => sensorname;
            set => this.RaiseAndSetIfChanged(ref sensorname, value);
        }

        private string measurement;
        [NotMapped]
        public string Measurement
        {
            get => measurement;
            set => this.RaiseAndSetIfChanged(ref measurement, value);
        }

        private double dvalue;
        public double Value
        {
            get => dvalue;
            set => this.RaiseAndSetIfChanged(ref dvalue, value);
        }

        private int areaid;
        public int AreaId
        {
            get => areaid;
            set => this.RaiseAndSetIfChanged(ref areaid, value);
        }

        private string areaname;
        [NotMapped]
        public string AreaName
        {
            get => areaname;
            set => this.RaiseAndSetIfChanged(ref areaname, value);
        }

        private string units;
        public string Units
        {
            get => units;
            set => this.RaiseAndSetIfChanged(ref units, value);
        }

        private byte[] package;
        public byte[] Package
        {
            get => package;
            set => this.RaiseAndSetIfChanged(ref package, value);
        }

        private DateTime timestamp;
        public DateTime TimeStamp
        {
            get => timestamp;
            set => this.RaiseAndSetIfChanged(ref timestamp, value);
        }

        private string valuelabel = "#";

        [NotMapped]
        public string ValueLabel
        {
            get => valuelabel;
            set => this.RaiseAndSetIfChanged(ref valuelabel, value);
        }

        /// <summary>
        /// //Data has 7 parts: Measure,AreaId,SensorId,AreaName,SensorName,Value,Units
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="rawpackage"></param>
        public void SetData(string[] raw, byte[] rawpackage = null)
        {
            
            if (raw.Length == 7)
            {
                Measurement = raw[0];

                if (int.TryParse(raw[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int areaidresult))
                {
                    AreaId = areaidresult;
                }

                if (int.TryParse(raw[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int idresult))
                {
                    SensorId = idresult;
                }

                AreaName = raw[3];
                SensorName = raw[4];

                if (double.TryParse(raw[5], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result))
                {
                    Value = result;
                }
                Units = raw[6];

                if(rawpackage!=null)
                {
                    Package = rawpackage;
                }

                TimeStamp = DateTime.Now;

                ValueLabel = $"{dvalue.ToString("00.00", CultureInfo.InvariantCulture)} {units}";
            }
        }

        /// <summary>
        /// This method is overrided when data comes from a third party source
        /// </summary>
        /// <param name="package"></param>
        public virtual void SetPackage(byte[] package)
        {

        }
    }
}
