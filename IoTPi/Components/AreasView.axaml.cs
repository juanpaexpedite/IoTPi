using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using IoTPi.Models;
using System.Collections.Generic;

namespace IoTPi.Components
{
    public class AreasView : UserControl
    {
        public static AreasView Instance;

        public AreasView()
        {
            Instance = this;

            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            InitializeControls();
            InitializeChildren();
        }

        TabControl AreasSideBar;
        private void InitializeControls()
        {
            AreasSideBar = this.Find<TabControl>("AreasSideBar");
        }

        private void InitializeChildren()
        {
            
        }

        public void SetData(int areaid, string areaname)
        {
            AreaId = areaid;
            AreaName = areaname;
        }

        public int AreaId;


        public static readonly StyledProperty<string> AreaNameProperty =
        AvaloniaProperty.Register<AreasView, string>(nameof(AreaName));

        public string AreaName
        {
            get { return GetValue(AreaNameProperty); }
            set { SetValue(AreaNameProperty, value); }
        }

        private Dictionary<int, SensorModule> sensormodules = new Dictionary<int, SensorModule>();

        public void UpdateSensor(int sensorid, string sensorname, double value, string units)
        {
            if(!sensormodules.ContainsKey(sensorid))
            {
                AddSensor(sensorid, sensorname, value,units);
            }
            else
            {
                sensormodules[sensorid].Update(value, units);
            }
        }

        private List<SensorModule> modules = new List<SensorModule>();
        private void AddSensor(int sensorid, string sensorname, double value, string units)
        {
            SensorModule module = new SensorModule();
            module.AreaId = AreaId;
            module.AreaName = AreaName;
            module.SensorId = sensorid;
            module.SensorName = sensorname;
            module.Update(value, units);

            sensormodules.Add(sensorid, module);
        }
    }
}
