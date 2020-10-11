using Avalonia.Collections;
using IoTPi.Managers;
using IoTPi.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoTPi.ViewModels
{
    public class SensorsViewModel : ViewModelBase
    {
        #region Visual 
        private string capturedatalabel;
        public string CaptureDataLabel
        {
            get => capturedatalabel;
            set => this.RaiseAndSetIfChanged(ref capturedatalabel, value);
        }
        #endregion


        public SensorsViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            cp2102 = new CP2102Manager();
            ExecuteRefreshPorts();
        }

        #region CP2102
        CP2102Manager cp2102 = null;

        private ReactiveCommand<Unit, Unit> startreading;
        public ReactiveCommand<Unit,Unit> StartReading => startreading ??= ReactiveCommand.Create(ExecuteStartReading);

        void ExecuteStartReading()
        {
            if (currentcomport != null)
            {
                //cp2102.Start(currentcomport.Name, ReadData);
                CaptureDataLabel = "READING...";
            }
            else
            {
                CaptureDataLabel = "CHOOSE COM";
            }
        }

        private ReactiveCommand<Unit, Unit> stopreading;
        public ReactiveCommand<Unit, Unit> StopReading => stopreading ??= ReactiveCommand.Create(ExecuteStopReading);

        void ExecuteStopReading()
        {
            cp2102.Stop();
           // CaptureDataLabel = "CAPTURE DATA";
        }
        #endregion

        #region Serial Ports
        private SerialPortDescriptor currentcomport;
        public SerialPortDescriptor CurrentComPort
        {
            get => currentcomport;
            set => this.RaiseAndSetIfChanged(ref currentcomport, value);
        }


        private bool autorearm;

        //A Serial Port Arduino Input Can Change from one port to another when restared so...we have to find it.
        public bool AutoRearm
        {
            get => autorearm;
            set => this.RaiseAndSetIfChanged(ref autorearm, value);
        }

        public AvaloniaList<SerialPortDescriptor> ComPorts { get; } = new AvaloniaList<SerialPortDescriptor>();

        
        private ReactiveCommand<Unit, Unit> refreshports;
        public ReactiveCommand<Unit, Unit> RefreshPorts => refreshports ??= ReactiveCommand.Create(ExecuteRefreshPorts);

        async void ExecuteRefreshPorts()
        {
            ComPorts.Clear();
            await Task.Delay(1000);

            foreach (var port in SerialPortManager.GetPortsInformation())
            {
                ComPorts.Add(new SerialPortDescriptor(port));
            }
        }
        #endregion

        #region SerialPort CallBack
        private string rawdata;
        public string RawData
        {
            get => rawdata;
            set => this.RaiseAndSetIfChanged(ref rawdata, value);
        }

        private void ReadData(bool sensoractive, string data)
        {
            if (sensoractive)
            {
                RawData = data;
                ProcessedDataViewModel.Instance.ReceiveData(data);
            }
            else
            {
                CaptureDataLabel = data;
            }
        }
        #endregion
    }
}
