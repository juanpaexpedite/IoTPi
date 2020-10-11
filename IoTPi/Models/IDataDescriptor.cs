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

        double Value { get; set; }
        string Area { get; set; }
        string Units { get; set; }

        byte[] Package { get; set; }

        DateTime TimeStamp { get; set; }
    }
}
