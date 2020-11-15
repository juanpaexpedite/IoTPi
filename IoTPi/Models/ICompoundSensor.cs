using System;
using System.Collections.Generic;
using System.Text;

namespace IoTPi.Models
{
    interface ICompoundSensor
    {
        List<SensorDescriptor> ChildrenSensors { get; }
    }
}
