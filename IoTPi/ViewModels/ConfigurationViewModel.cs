using Avalonia.Threading;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace IoTPi.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        public static ConfigurationViewModel Instance;

        public ConfigurationViewModel()
        {
            Instance = this;
            ExecuteApplyInterval();
        }

        #region Configuration Visibility
        private bool isvisible = false;
        public bool IsVisible
        {
            get => isvisible;
            set => this.RaiseAndSetIfChanged(ref isvisible, value);
        }

        private ReactiveCommand<Unit,Unit> showconfig;
        public ReactiveCommand<Unit, Unit> ShowConfig => showconfig ??= ReactiveCommand.Create(ExecuteShowConfig);

        void ExecuteShowConfig()
        {
            IsVisible = true;
        }

        private ReactiveCommand<Unit, Unit> closeconfig;
        public ReactiveCommand<Unit, Unit> CloseConfig => closeconfig ??= ReactiveCommand.Create(ExecuteCloseConfig);

        void ExecuteCloseConfig()
        {
            IsVisible = false;
        }
        #endregion


        #region Save Interval Minutes
        private int saveinterval = 1;
        public int SaveInterval
        {
            get => saveinterval;
            set => this.RaiseAndSetIfChanged(ref saveinterval, value);
        }

        private ReactiveCommand<Unit, Unit> applyinterval;
        public ReactiveCommand<Unit, Unit> ApplyInterval => applyinterval ??= ReactiveCommand.Create(ExecuteApplyInterval);

        void ExecuteApplyInterval()
        {
            DatabaseViewModel.Instance.SetInsertInterval(saveinterval);
        }


        #endregion


        
    }
}
