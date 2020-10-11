using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace IoTPi.Components
{
    public class DataBox : UserControl
    {
        public DataBox()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly AvaloniaProperty<string> LabelProperty =
        AvaloniaProperty.Register<DataBox, string>(nameof(Label), inherits: true, defaultValue:"###");

        public string Label
        {
            get { return this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }

        public string Data
        {
            get { return this.GetValue(DataProperty); }
            set { this.SetValue(DataProperty, value); }
        }

        public static readonly AvaloniaProperty<string> DataProperty =
       AvaloniaProperty.Register<DataBox, string>("Data", inherits: true, defaultValue: "---");


   

    }
}
