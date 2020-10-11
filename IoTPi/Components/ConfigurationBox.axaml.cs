using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace IoTPi.Components
{
    public class ConfigurationBox : UserControl
    {
        public ConfigurationBox()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
