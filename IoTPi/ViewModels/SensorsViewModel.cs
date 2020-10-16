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
        public static SensorsViewModel Instance;
        SerialPortManager serialportmanager;

        #region Visual 
        private string headerlabel;
        public string HeaderLabel
        {
            get => headerlabel;
            set => this.RaiseAndSetIfChanged(ref headerlabel, value);
        }
        #endregion


        public SensorsViewModel()
        {
            Instance = this;
            serialportmanager = new SerialPortManager();

            Initialize();
        }

        private void Initialize()
        {
            ExecuteRefreshPorts();
        }

        #region CP2102
        

        private ReactiveCommand<Unit, Unit> stopreading;
        public ReactiveCommand<Unit, Unit> StopReading => stopreading ??= ReactiveCommand.Create(ExecuteStopReading);

        void ExecuteStopReading()
        {
            serialportmanager.Stop();

            HeaderLabel = "STOPPED";
        }
        #endregion

        #region Serial Ports
        private SerialPortDescriptor currentcomport;
        public SerialPortDescriptor CurrentComPort
        {
            get => currentcomport;
            set => this.RaiseAndSetIfChanged(ref currentcomport, value);
        }

        public bool CanRead()
        {
            return CurrentComPort != null;
        }

        public void Read()
        {
            serialportmanager.Read(CurrentComPort.Name, TransferData);
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

            foreach (var port in serialportmanager.GetPortsInformation())
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
      

        private void TransferData(string header, byte[] package)
        {
            ProcessedDataViewModel.Instance.ReceiveData(header, package);
            HeaderLabel = header;
        }
        #endregion
    }
}
