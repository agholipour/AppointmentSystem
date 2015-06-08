using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Web.Model
{

    public enum AppointmentTypes
    {
        Import = 1,
        Export = 2
    }
   public class Appointments
    {
      
       [Key]
       public int Id { get; set; }

       [StringLength(50, ErrorMessage = "Description Max Length is 50")]
       public String Zone{ get; set; }

       [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
       [DataType(DataType.Date)]
       public DateTime Date  { get; set; }
       [Display(Name = "Appointment Type")]
       public  AppointmentTypes Type { get; set; }
       public Boolean Taken { get; set; }
       [StringLength(500, ErrorMessage = "Description Max Length is 500")]
       public string Description { get; set; }
    }
}
