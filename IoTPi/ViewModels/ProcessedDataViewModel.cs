using IoTPi.Managers;
using IoTPi.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace IoTPi.ViewModels
{
    public class ProcessedDataViewModel : ViewModelBase
    {
        internal static ProcessedDataViewModel Instance;

        public Dictionary<string, SensorDescriptor> SensorsCollection = new Dictionary<string, SensorDescriptor>();

        public ProcessedDataViewModel()
        {
            Instance = this;
        }

        /// <summary>
        /// Id, Sensor, Area, Value, Units
        /// Example 4, Temperature, Area0, 20.0, C
        /// </summary>
        /// <param name="rawdata"></param>

        public void ReceiveData(string rawdata)
        {
            if(DatabaseViewModel.Instance.SavingData)
            {
                return;
            }

            var data = rawdata.Split(' ');

            if (data.Length == 5)
            {
                ProcessedDataManager.SetData(SensorsCollection, data);
            }
        }
    }
}
