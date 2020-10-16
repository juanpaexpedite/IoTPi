using Avalonia.Collections;
using Microsoft.EntityFrameworkCore.Internal;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Avalonia.Threading;

namespace IoTPi.Models
{
    public class AreaDescriptor : ReactiveObject
    {
        public AvaloniaList<SensorDescriptor> Sensors { get; } = new AvaloniaList<SensorDescriptor>();

        private int id;
        public int Id
        {
            get => id;
            set => this.RaiseAndSetIfChanged(ref id, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        internal void CheckSensor(SensorDescriptor sensor)
        {
            if (Sensors.Any(s => s.Measurement == sensor.Measurement && s.AreaId == sensor.AreaId && s.Id == sensor.Id))
            {
                return;
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Sensors.Add(sensor);
                });
            }
        }
    }
}
