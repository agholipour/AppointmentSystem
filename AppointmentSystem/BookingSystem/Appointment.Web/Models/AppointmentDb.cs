using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Web.Model
{
   public class AppointmentDb :DbContext
    {
       public AppointmentDb()
           : base("name=DefaultConnection")
       {

       }
        public DbSet<Appointments> Appointments { get; set; }

    }
}
