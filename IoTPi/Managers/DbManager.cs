using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoTPi.Managers
{
    public class DbManager
    {
        public static bool ExistsDatabase()
        {
            try
            {
                using (var db = new IoTDbContext())
                {
                    return db.Database.CanConnect();
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool CreateDatabase(bool addsampledata = false)
        {
            try
            {
                using (var db = new IoTDbContext())
                {
                    db.Database.EnsureDeleted();
                    var result = db.Database.EnsureCreated();


                    if (addsampledata)
                    {

                        db.Add(new Models.TemperatureSensorDescriptor(new string[] { "0","Temperature","Area0","20.0","C" }));
                        db.Add(new Models.PressureSensorDescriptor(new string[] { "0", "Pressure", "Area0", "1200.0", "Mb" }));
                        db.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
