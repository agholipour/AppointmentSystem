using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.Web.Helper;
namespace Appointment.Web.Model
{
   public class AppointmentRepository:AppointmentDb,IAppointmentRepository
    {

        public List<Appointments> GetAppointments()
        {
            var result = new List<Appointments>();
            using (var context = new AppointmentDb())
            {
                var appointments = context.Appointments.Where(x=>x.Taken==true)
                        .OrderBy(c => c.Date).ToList();
                return appointments;
            }
           
        }

        public List<Appointments> GetAppointmentByDate(DateTime date)
        {
            var result = new List<Appointments>();
            using (var context = new AppointmentDb())
            {
                var appointments = context.Appointments
                        .Where(x =>( x.Date.Year == date.Year && 
                             x.Date.Month == date.Month && 
                              x.Date.Day == date.Day 
                            )).ToList();

                return appointments;    
            }
            
        }

        public Boolean Save(Appointments appointment)
        {
            using (var context = new AppointmentDb())
            {
                Boolean result = false;
                var appointmentItem = context.Appointments.Where(x => x.Id == appointment.Id).FirstOrDefault();
                if (appointmentItem!= null)
                {
                    appointmentItem.Taken = true;
                    appointmentItem.Description = appointment.Description;
                    context.Entry(appointmentItem).State = EntityState.Modified;
                   result =context.SaveChanges()>0;
                }

                return result;
            }
        }


        public List<Appointments> GetAvailableAppointments()
        {
          
            using (var context = new AppointmentDb())
            {

                DateTime date=DateUtil.NextBusinessDay(DateTime.Now.AddDays(1));
                
                var appointments = context.Appointments
                    .Where(z => (z.Date.Year == date.Year) && (z.Date.Month == date.Month) && (z.Date.Day == date.Day) && (z.Taken == false))
                    .ToList();
                return appointments;    
            }
            
        }

      


    }
}
