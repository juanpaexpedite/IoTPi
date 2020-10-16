using IoTPi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTPi.Managers
{
    public class ProcessedDataManager
    {
        //0,1,2,3,4,5,6
        //Data has 7 parts: Measure,AreaId,SensorId,AreaName,SensorName, Value,Units
        //Example:Temperature,0;4;Window 0;Left Sensor;20.0;C
        public static SensorDescriptor SetData(Dictionary<string, SensorDescriptor> collection, string[] values, byte[] package = null)
        {
            var entry = $"{values[0]}_{values[1]}_{values[2]}";

            if (!collection.ContainsKey(entry))
            {
                var type = Type.GetType($"IoTPi.Models.{values[0]}SensorDescriptor");
                var instance = Activator.CreateInstance(type, new object[] { values });

                if(instance is SensorDescriptor sd)
                {
                    sd.SetPackage(package);
                }

                collection.Add(entry, (SensorDescriptor)instance);

                return (SensorDescriptor)instance;

            }
            else
            {
                collection[entry].SetData(values);
                return collection[entry];
            }


        }
        
    }
}
