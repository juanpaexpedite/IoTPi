using IoTPi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoTPi.Managers
{
    public class ProcessedDataManager
    {
        public static void SetData(Dictionary<string, SensorDescriptor> collection, string[] values)
        {
            var entry = $"{values[1]}{values[0]}";

            if (!collection.ContainsKey(entry))
            {
                var type = Type.GetType($"IoTPi.Models.{values[1]}SensorDescriptor");
                var instance = Activator.CreateInstance(type, new object[] { values });
                collection.Add(entry, (SensorDescriptor)instance);

            }
            else
            {
                collection[entry].SetData(values);
            }
        }
        
    }
}
