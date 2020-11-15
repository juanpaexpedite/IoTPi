using DynamicData;
using IoTPi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IoTPi.Managers
{
    public class ProcessedDataManager
    {
        //0,1,2,3,4,5,6
        //Data has 7 parts: Measure,AreaId,SensorId,AreaName,SensorName, Value,Units
        //Example:Temperature,0;4;Window 0;Left Sensor;20.0;C

        private static Dictionary<string, SensorDescriptor> compoundsensorscollection = new Dictionary<string, SensorDescriptor>();

        //TESTING Nesting sensors
        public static SensorDescriptor[] SetData(Dictionary<string, SensorDescriptor> collection, string[] values, byte[] package = null)
        {
            //0.- Check if it is a simple sensor or a compound sensor:
            var type = Type.GetType($"IoTPi.Models.{values[0]}SensorDescriptor");

            //TODO CACHE THE TYPES to do not be checking this all the time
            var compound = type.GetInterface("ICompoundSensor");


            //We are going to add its children not the main sensor
            if (compound != null)
            {
                var entry = $"{values[0]}_{values[1]}_{values[2]}";
                if (!compoundsensorscollection.ContainsKey(entry))
                {
                    var instance = Activator.CreateInstance(type, new object[] { values });

                    SensorDescriptor sd = (SensorDescriptor)instance;

                    sd.SetPackage(package);

                    compoundsensorscollection.Add(entry, (SensorDescriptor)instance);

                    //Now I have to implement all the children
                    var compoundSensor = (ICompoundSensor)instance;
                    foreach (var sensor in compoundSensor.ChildrenSensors)
                    {
                        //To ADD in Set Data, we have to split the method
                        sensor.AreaId = sd.AreaId;
                        sensor.AreaName = sd.AreaName;
                        sensor.TimeStamp = DateTime.Now;
                        sensor.ValueLabel = $"{sensor.Value.ToString("00.00", CultureInfo.InvariantCulture)} {sensor.Units}";

                        var sensorentry = $"{sensor.Measurement}_{sensor.AreaId}_{sensor.SensorId}";
                        collection.Add(sensorentry, sensor);
                    }

                    return compoundSensor.ChildrenSensors.ToArray();
                }
                else
                {
                    var instance = (SensorDescriptor)compoundsensorscollection[entry];
                    SensorDescriptor sd = (SensorDescriptor)instance;
                    sd.SetPackage(package);

                    var compoundSensor = (ICompoundSensor)compoundsensorscollection[entry];

                    //foreach (var sensor in compoundSensor.ChildrenSensors)
                    //{
                    //    var sensorentry = $"{sensor.Measurement}_{sensor.AreaId}_{sensor.SensorId}";
                    //    collection.Add(sensorentry, sensor);
                    //}

                    return compoundSensor.ChildrenSensors.ToArray();
                }
            }
            else
            {
                var entry = $"{values[0]}_{values[1]}_{values[2]}";
                if (!collection.ContainsKey(entry))
                {
                    var instance = Activator.CreateInstance(type, new object[] { values });
                    SensorDescriptor sd = (SensorDescriptor)instance;

                    sd.SetPackage(package);

                    collection.Add(entry, sd);

                    return new SensorDescriptor[] { sd };

                }
                else
                {
                    collection[entry].SetData(values, package);
                    return new SensorDescriptor[] { collection[entry] };
                }
            }

        }

        public static SensorDescriptor ORIGINALSetData(Dictionary<string, SensorDescriptor> collection, string[] values, byte[] package = null)
        {
            var entry = $"{values[0]}_{values[1]}_{values[2]}";

            if (!collection.ContainsKey(entry))
            {
                var type = Type.GetType($"IoTPi.Models.{values[0]}SensorDescriptor");
                var instance = Activator.CreateInstance(type, new object[] { values });

                if (instance is SensorDescriptor sd)
                {
                    sd.SetPackage(package);
                }

                collection.Add(entry, (SensorDescriptor)instance);

                return (SensorDescriptor)instance;

            }
            else
            {
                collection[entry].SetData(values, package);
                return collection[entry];
            }
        }

    }
}
