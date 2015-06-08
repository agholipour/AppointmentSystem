using Appointment.Web.Model;
using Appointment.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Appointment.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Appointments> appointments = GetBookedList();
            return View(appointments);
        }

        public ActionResult Create()
        {
            var AvailableAppointment = GetNextAvailableBusinessDay();
            Dictionary<int, string> zones = new Dictionary<int,string>();
           
            AppointmentViewModel model = new AppointmentViewModel()
            {
                Description ="",
                SelectedZone = GetSelectListItems(AvailableAppointment)
            };
            return View(model);
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Appointments> AvailableAppointment)
        {
            var selectList = new List<SelectListItem>();
            foreach (var item in AvailableAppointment)
            {
                selectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Type.ToString() + " - " +
                    item.Date.Day + "/" + item.Date.Month + "/" + item.Date.Year +
                    " - (" + item.Zone + " )"
                });
            }

            return selectList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentViewModel appointment)
        {
            if (ModelState.IsValid)
            {
                SaveAppointment(appointment);
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        private void SaveAppointment(AppointmentViewModel appointment)
        {

            string api = string.Format("{0}/api/appointments/Add", ConfigurationManager.AppSettings["apiBaseAddress"]);
             var requestBody = new Appointments
                {
                    Id = int.Parse(appointment.SelectedItem),
                    Description = appointment.Description
                };
             var result = SendHttpRequest(api, HttpMethod.Put,
                   new ObjectContent(typeof (Appointments), requestBody, new JsonMediaTypeFormatter()));

             Task<string> resultTask = result.Content.ReadAsStringAsync();
             resultTask.Wait();
        }

        private List<Appointments> GetBookedList()
        {
            string api = string.Format("{0}/api/appointments/List",
                ConfigurationManager.AppSettings["apiBaseAddress"]);

            List<Appointments> appointments = getAppoinmentList(api);
            return appointments;
        }

        private List<Appointments> getAppoinmentList(string api)
        {
            List<Appointments> appointments = new List<Appointments>();
            HttpContent response = null;
            var result = SendHttpRequest(api, HttpMethod.Get,
                 new StringContent(string.Empty, Encoding.Default, "text/plain"));

            if (result.StatusCode == HttpStatusCode.OK)
            {
                response = result.Content;
                Task<string> resultTask = response.ReadAsStringAsync();
                resultTask.Wait();
                appointments = JsonConvert.DeserializeObject<List<Appointments>>(resultTask.Result,
           new JsonSerializerSettings());
            }
            return appointments;
        }

        private List<Appointments> GetNextAvailableBusinessDay()
        {
            string api = string.Format("{0}/api/appointments",
                ConfigurationManager.AppSettings["apiBaseAddress"]);
            List<Appointments> appointments = getAppoinmentList(api);
            return appointments;

        }

        private HttpResponseMessage SendHttpRequest<T>(string uri, HttpMethod verb, T requestBody,
           string acceptContentType = "application/json") where T : HttpContent
        {
            using (var client = new HttpClient { Timeout = new TimeSpan(1, 0, 0) })
            {
                var request = new HttpRequestMessage
                {
                    
                    Content  = verb != HttpMethod.Get ? requestBody : null,
                    RequestUri = new Uri(uri),
                    Method = verb
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentType));

                Task<HttpResponseMessage> apiTask = client.SendAsync(request);
                apiTask.Wait();

                return apiTask.Result;


            }


        }
    }
}
