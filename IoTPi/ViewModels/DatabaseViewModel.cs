using Avalonia.Threading;
using IoTPi.Managers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace IoTPi.ViewModels
{
    public class DatabaseViewModel : ViewModelBase
    {
        internal static DatabaseViewModel Instance;

        public DatabaseViewModel()
        {
            Instance = this;
        }

        #region Database Creation
        private ReactiveCommand<Unit, Unit> createdatabase;
        public ReactiveCommand<Unit, Unit> CreateDatabase => createdatabase ??= ReactiveCommand.Create(ExecuteCreateDatabase);

        void ExecuteCreateDatabase()
        {
            DbManager.CreateDatabase(true);

        }
        #endregion

        #region Check Database
        public bool DatabaseExists()
        {
            return DbManager.ExistsDatabase();
        }
        #endregion

        #region Database Interval Insert
        private DispatcherTimer savetimer;
        public void SetInsertInterval(int minutes)
        {
            if (minutes < 1)
                return;

            if (savetimer != null)
            {
                savetimer.Stop();
            }
            else
            {
                savetimer = new DispatcherTimer();
                savetimer.Tick += Savetimer_Tick;
            }

            savetimer.Interval = TimeSpan.FromSeconds(minutes * 60);
            savetimer.Start();
        }


        public bool SavingData = false;
        private void Savetimer_Tick(object sender, EventArgs e)
        {
            var processeddata = ProcessedDataViewModel.Instance;

            SavingData = true;

            foreach (var data in processeddata.SensorsCollection.Values)
            {
                DataService.InsertData(data);
            }

            SavingData = false;
        }
        #endregion
    }
}
