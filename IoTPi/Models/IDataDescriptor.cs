using System;
using System.Collections.Generic;
using System.Text;

namespace IoTPi.Models
{
    public interface IDataDescriptor
    {
        void SetData(string[] data, byte[] package);
        
        int Id { get; set; }
        string Measurement { get; set; }
        int AreaId { get; set; }
        string AreaName { get; set; }
        int SensorId { get; set; }
        string SensorName { get; set; }
        double Value { get; set; }
        string Units { get; set; }
        byte[] Package { get; set; }

        DateTime TimeStamp { get; set; }
    }
}
