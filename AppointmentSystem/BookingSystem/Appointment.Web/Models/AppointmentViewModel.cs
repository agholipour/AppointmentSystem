using Appointment.Web.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Web.Models
{
    public class AppointmentViewModel
    {
        [Display(Name = "Zone")]
        public string SelectedItem { get; set; }
        public IEnumerable<SelectListItem> SelectedZone { get; set; }

        public String Description { get; set; }
    }
}