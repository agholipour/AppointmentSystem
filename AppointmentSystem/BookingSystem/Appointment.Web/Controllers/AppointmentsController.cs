using Appointment.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Appointment.Web.Controllers
{
 
    public class AppointmentsController : ApiController
    {
         private readonly IAppointmentRepository _appointymentRepository;

         public AppointmentsController()
        {
            _appointymentRepository =  new  AppointmentRepository();
        }

         [Route("api/appointments")]
         [System.Web.Http.HttpGet]
         public IHttpActionResult GetAppointments()
         {
             var availableZones = _appointymentRepository.GetAvailableAppointments();
             if (availableZones == null)
             {
                 return NotFound();
             }
             return Ok(availableZones);
         }
         [Route("api/appointments/List")]
         public IHttpActionResult GetBookedAppointments()
         {
             var appointments = _appointymentRepository.GetAppointments();
             if (appointments == null)
             {
                 return NotFound();
             }
             return Ok(appointments);
         }

         [Route("api/appointments/{year}/{month}/{day}")]
         public IHttpActionResult GetAppointments(int year, int month, int day)
         {
             DateTime dt = new DateTime(year, month, day);
             var appointments = _appointymentRepository.GetAppointmentByDate(dt);
             if (appointments == null)
             {
                 return NotFound();
             }
             return Ok(appointments);
         }
         [HttpPut]
         [Route("api/appointments/Add")]
         public IHttpActionResult AddAppointment(Appointments appointment)
         {
             if (_appointymentRepository.Save(appointment))
             {
                 return Ok();
             }
             return InternalServerError();
         }
    }
}
