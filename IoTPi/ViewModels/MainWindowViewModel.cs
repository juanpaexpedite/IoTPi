using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTPi.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            Processed = new ProcessedDataViewModel();
            Database = new DatabaseViewModel();
            Configuration = new ConfigurationViewModel();
            Sensors = new SensorsViewModel();
            Areas = new AreasViewModel();

            Initialize();
        }

        private void Initialize()
        {
            Task.Run(() => CheckDatabaseAsync());
        }

        Random rnd = new Random();
        private async Task CheckDatabaseAsync()
        {
            await Task.Delay(1000);

            if(!Database.DatabaseExists())
            {
                Configuration.IsVisible = true;
            }
            else
            {
                StartReceivingData();
            }
        }

        private void StartReceivingData()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                var timer = new DispatcherTimer();
                timer.Tick += Timer_Tick;
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Start();
            });
        }

        
        private void Timer_Tick(object sender, EventArgs e)
        {
            //Data has 7 parts: Measure;AreaId;Id;AreaName;Name;Value;Units
            //var id = rnd.Next(0, 6);
            //Processed.ReceiveData($"Temperature;0;{id};Window 0;Left;{rnd.Next(25, 30)};C");

            //Compound data example
            //Real Package
            Processed.ReceiveData($"Battery;0;0;Main;Battery;{rnd.Next(25, 30)};C",
                new byte[] {
                0, 10, 64, 88, 0, 0, 88, 8, 0, 88, 0, 0, 2, 143, 1, 2,
                146, 4, 1, 41, 4, 1, 44, 3, 3, 4, 2, 143, 1, 44, 3, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                19,8,253,175,2,28,2,228,2,168,31});

            Processed.ReceiveDataSerialPort();

        }


        //Child Datacontexts (I cannot make it work at the moment in other way)
        public ProcessedDataViewModel Processed { get; }
        public DatabaseViewModel Database { get; }
        public ConfigurationViewModel Configuration { get; }

        public SensorsViewModel Sensors { get; }

        public AreasViewModel Areas { get; }

    }
}
