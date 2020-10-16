using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using IoTPi.Models;

namespace IoTPi.Components
{
    public class SensorsView : UserControl
    {
        public SensorsView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

           
        }

        protected override void OnDataContextChanged(System.EventArgs e)
        {
            

            base.OnDataContextChanged(e);
        }
    }
}
