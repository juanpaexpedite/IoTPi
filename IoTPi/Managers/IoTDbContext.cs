using IoTPi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IoTPi.Managers
{
    public class IoTDbContext : DbContext
    {
        //Might be not required
        //public DbSet<TemperatureSensorDescriptor> Temperature { get; set; }
        //public DbSet<PressureSensorDescriptor> Pressure { get; set; }
        //public DbSet<LightnessSensorDescriptor> Lightness { get; set; }
        //public DbSet<HumiditySensorDescriptor> Humidity { get; set; }
        //public DbSet<BatterySensorDescriptor> Battery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=iot.db");

       
    }
}
