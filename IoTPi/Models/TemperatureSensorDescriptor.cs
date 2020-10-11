using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoTPi.Models
{

    //In order to create one per type you need this

    [Table("Temperature")]
    public class TemperatureSensorDescriptor : SensorDescriptor
    {
        public TemperatureSensorDescriptor() {  }

        public TemperatureSensorDescriptor(string[] data) : base(data) {  }
    }

    [Table("Pressure")]
    public class PressureSensorDescriptor : SensorDescriptor
    {
        public PressureSensorDescriptor() { }

        public PressureSensorDescriptor(string[] data) : base(data) { }
    }

    [Table("Lightness")]
    public class LightnessSensorDescriptor : SensorDescriptor
    {
        public LightnessSensorDescriptor() { }

        public LightnessSensorDescriptor(string[] data) : base(data) { }
    }

    [Table("Humidity")]
    public class HumiditySensorDescriptor : SensorDescriptor
    {
        public HumiditySensorDescriptor() { }

        public HumiditySensorDescriptor(string[] data) : base(data) { }
    }

    [Table("Battery")]
    public class BatterySensorDescriptor : SensorDescriptor
    {
        public BatterySensorDescriptor() { }

        public BatterySensorDescriptor(string[] data) : base(data) { }
    }
}
