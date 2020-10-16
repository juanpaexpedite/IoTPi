using IoTPi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IoTPi.Managers
{
    public class DataService
    {
        public static void InsertData(object sensor)
        {
            using (var db = new IoTDbContext())
            {
                try
                {
                    db.Add(sensor);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error in DataService.InsertData {ex.Message}");
                }
            }
        }

        public static DbSet<SensorDescriptor> GetTable(object instance, string tablename)
        {
            var info = instance.GetType().GetProperty(tablename);
            var table = info.GetValue(instance);

            return (DbSet<SensorDescriptor>)table;
        }
    }
}
