using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Web.Model
{
    public interface IAppointmentRepository
    {
        List<Appointments> GetAppointments();
        List<Appointments> GetAvailableAppointments();
        List<Appointments> GetAppointmentByDate(DateTime date);
        Boolean Save(Appointments appointments);
       
    }
}
