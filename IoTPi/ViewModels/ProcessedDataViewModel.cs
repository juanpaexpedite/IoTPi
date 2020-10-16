﻿using Avalonia.Threading;
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
        /// Data has 7 parts: Measure,AreaId,Id,AreaName,SensorName,Value,Units
        /// </summary>
        /// <param name="rawdata"></param>

        public void ReceiveData(string rawdata, byte[] package = null)
        {
            if (DatabaseViewModel.Instance.SavingData)
            {
                return;
            }

            var data = rawdata.Split(';');

            var sensor = ProcessedDataManager.SetData(SensorsCollection, data, package);

            var area = AreasViewModel.Instance.CheckArea(sensor.AreaId, sensor.AreaName);

            area.CheckSensor(sensor);

        }

        internal void ReceiveDataSerialPort()
        {
            if(SensorsViewModel.Instance.CanRead())
            {
                SensorsViewModel.Instance.Read();
            }
        }
    }
}
