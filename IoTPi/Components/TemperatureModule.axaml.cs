using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ReactiveUI;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IoTPi.Components
{
    public class TemperatureModule : UserControl
    {
        public TemperatureModule()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            UpdateData();
            
        }

        AreaSeries series1;
        OxyPlot.Avalonia.PlotView plotview;
        TextBlock CurrentValueLabel;
        private  void UpdateData()
        {
            var tmp = new PlotModel();

            tmp.PlotAreaBorderThickness = new OxyThickness(0);

            
            // Create two line series (markers are hidden by default)
            series1 = new AreaSeries { MarkerType = MarkerType.None};
            series1.StrokeThickness = 6.0;
            series1.LineStyle = LineStyle.Solid;
            series1.Color = OxyColors.DodgerBlue;
            series1.InterpolationAlgorithm = InterpolationAlgorithms.CanonicalSpline;
            series1.Fill = OxyColor.FromArgb(255,240,245,255);

            series1.Points.Add(new DataPoint(8, 16.5));
            series1.Points.Add(new DataPoint(9, 18));
            series1.Points.Add(new DataPoint(10, 20));
            series1.Points.Add(new DataPoint(11, 22));
            series1.Points.Add(new DataPoint(12, 23));
            series1.Points.Add(new DataPoint(13, 24));
            series1.Points.Add(new DataPoint(14, 26));
            series1.Points.Add(new DataPoint(15, 28));
            series1.Points.Add(new DataPoint(16, 26));
            series1.Points.Add(new DataPoint(17, 23));
            series1.Points.Add(new DataPoint(18, 22));
            series1.Points.Add(new DataPoint(19, 19));
            series1.Points.Add(new DataPoint(20, 16));
            //series1.XAxis.TickStyle = OxyPlot.Axes.TickStyle.None;

            // Add the series to the plot model
            tmp.Series.Add(series1);

            // Axes are created automatically if they are not defined


            plotview = this.Find<OxyPlot.Avalonia.PlotView>("Plot");

            CurrentValueLabel = this.Find<TextBlock>("CurrentValueLabel");

            plotview.Model = tmp;



            plotview.Model.InvalidatePlot(true);

            foreach(var axis in plotview.Model.Axes)
            {
                axis.AxislineStyle = LineStyle.None;
                axis.TickStyle = TickStyle.None;
                axis.MajorStep = 1.0;
                axis.Font = "Calibri";
                axis.TextColor = OxyColors.DarkGray;
            }



            plotview.Model.Axes[1].MajorStep = 5;
            plotview.Model.Axes[1].Minimum = 10;
            plotview.Model.Axes[1].Maximum = 30;

            InitializeTimer();
        }

        private void InitializeTimer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        int time = 20;
        Random rnd = new Random();
        private void Timer_Tick(object sender, EventArgs e)
        {
            series1.Points.RemoveAt(0);
            var nextvalue = rnd.Next(12, 26);
            series1.Points.Add(new DataPoint(time,nextvalue ));
            time++;

            CurrentValueLabel.Text = nextvalue.ToString("00.00", CultureInfo.InvariantCulture);

            plotview.Model.InvalidatePlot(true);
        }
    }
}
