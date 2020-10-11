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
                StartDummyData();
            }
        }

        private void StartDummyData()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                var timer = new DispatcherTimer();
                timer.Tick += Timer_Tick;
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();
            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var id = rnd.Next(0, 6);
            Processed.ReceiveData($"{id} Temperature Area{id} {rnd.Next(25, 30)} C");
        }


        //Child Datacontext (I cannot make it work at the moment in other way)
        public ProcessedDataViewModel Processed { get; }
        public DatabaseViewModel Database { get; }
        public ConfigurationViewModel Configuration { get; }


    }
}
