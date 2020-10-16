﻿using Avalonia;
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
    public class SensorModule : UserControl
    {
        public SensorModule()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            FindControls();
            InitializePlot();
        }

        
        OxyPlot.Avalonia.PlotView plotview;
        TextBlock CurrentValueLabel;
        private void FindControls()
        {
            plotview = this.Find<OxyPlot.Avalonia.PlotView>("Plot");
            CurrentValueLabel = this.Find<TextBlock>("CurrentValueLabel");
        }

        PlotModel plotmodel;
        AreaSeries series;
        private void InitializePlot()
        {
            plotmodel = new PlotModel();

            plotmodel.PlotAreaBorderThickness = new OxyThickness(0);

            series = new AreaSeries { MarkerType = MarkerType.None };
            series.StrokeThickness = 6.0;
            series.LineStyle = LineStyle.Solid;
            series.Color = OxyColors.DodgerBlue;
            series.InterpolationAlgorithm = InterpolationAlgorithms.CanonicalSpline;
            series.Fill = OxyColor.FromArgb(255, 240, 245, 255);

            plotmodel.Series.Add(series);
            plotview.Model = plotmodel;

            plotview.Model.InvalidatePlot(true);

            foreach (var axis in plotview.Model.Axes)
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

        }

        public void SetSensor(int areaid, string areaname, int sensorid, string sensorname)
        {
            AreaId = areaid;
            AreaName = areaname;
            SensorId = sensorid;
            SensorName = sensorname;
        }

        #region Properties
        public int AreaId;
        

        public static readonly StyledProperty<string> AreaNameProperty =
        AvaloniaProperty.Register<SensorModule, string>(nameof(AreaName));

        public string AreaName
        {
            get { return GetValue(AreaNameProperty); }
            set { SetValue(AreaNameProperty, value); }
        }

        public int SensorId;
        public static readonly StyledProperty<string> SensorNameProperty =
        AvaloniaProperty.Register<SensorModule, string>(nameof(SensorName));
        
        public string SensorName
        {
            get { return GetValue(SensorNameProperty); }
            set { SetValue(SensorNameProperty, value); }
        }


        #endregion

        int hour = 0;
        public void Update(double value, string unit)
        {
            if (series.Points.Count > 10)
            {

                series.Points.RemoveAt(0);
            }
            series.Points.Add(new DataPoint(hour, value));

            CurrentValueLabel.Text = $"{value.ToString("00.00", CultureInfo.InvariantCulture)} {unit}";

            plotview.Model.InvalidatePlot(true);

            hour++;
            if (hour > 24)
            {
                hour = 0;
            }
        }
    }
}