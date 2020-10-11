using ReactiveUI;
using System;

namespace IoTPi.Models
{
    public class SerialPortDescriptor : ReactiveObject
    {
        public SerialPortDescriptor() { }

        public SerialPortDescriptor((string name, string description) info)
        {
            this.Name = info.name;
            this.Description = info.description;
        }

        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }
    }
}
