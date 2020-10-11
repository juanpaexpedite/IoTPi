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
        /// //Data in this case has  3 parts: Id, Measure, Area, Value, Units
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

        private int entryid;
        public int EntryId
        {
            get => entryid;
            set => this.RaiseAndSetIfChanged(ref entryid, value);
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

        private string area;
        public string Area
        {
            get => area;
            set => this.RaiseAndSetIfChanged(ref area, value);
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

        /// <summary>
        /// //Data in this case has  3 parts: Id, Measure, Area, Value, Units
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="rawpackage"></param>
        public void SetData(string[] raw, byte[] rawpackage = null)
        {
            //Data in this case has  3 parts: Id, Measure, Area, Value, Units
            if (raw.Length == 5)
            {
                if (int.TryParse(raw[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int idresult))
                {
                    EntryId = idresult;
                }
                Measurement = raw[1];
                Area = raw[2];
                if (double.TryParse(raw[3], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result))
                {
                    Value = result;
                }
                Units = raw[4];

                if(rawpackage!=null)
                {
                    Package = rawpackage;
                }

                TimeStamp = DateTime.Now;
            }
        }
    }
}
