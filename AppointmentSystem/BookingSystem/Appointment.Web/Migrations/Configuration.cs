namespace Appointment.Web.Migrations
{
    using Appointment.Web.Helper;
    using Appointment.Web.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Appointment.Web.Model.AppointmentDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
  
        }

        protected override void Seed(Appointment.Web.Model.AppointmentDb context)
        {
             List<Appointments> zones = new List<Appointments>();
            for (int i = 1; i < 14; i++)
            {
                DateTime date = DateUtil.GetBusinessDay(DateTime.Now.AddDays(i), 1);
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "10AM - 11AM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "10AM - 11AM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "10AM - 11AM", Date = date });

                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "11AM - 12PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "11AM - 12PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "11AM - 12PM", Date = date });

                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "12PM - 1PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "12PM - 1PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Export, Zone = "12PM - 1PM", Date = date });


                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "10AM - 11AM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "10AM - 11AM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "10AM - 11AM", Date = date });

                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "11AM - 12PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "11AM - 12PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "11AM - 12PM", Date = date });

                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "12PM - 1PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "12PM - 1PM", Date = date });
                zones.Add(new Appointments { Type = AppointmentTypes.Import, Zone = "12PM - 1PM", Date = date });
            }

            context.Appointments.AddRange(zones);


        }
        
    }
}
