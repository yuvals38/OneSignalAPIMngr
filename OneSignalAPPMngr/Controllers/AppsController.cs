using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using OneSignalAPPMngr.Models;
using Newtonsoft.Json;
using System.Web.Configuration;

namespace OneSignalAPPMngr.Controllers
{
    public class AppsController : BaseController
    {
        //onesignal api uri stored in web.config
        string apiUri = WebConfigurationManager.AppSettings["OneSignalAPI"];
        // GET: Apps
        public ActionResult Index()
        {
            List<AppsViewModel> app = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUri);
   
                var responseTask = client.GetAsync("View_Apps");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
      
                    app = JsonConvert.DeserializeObject<List<AppsViewModel>>(readTask.Result);
    
                    readTask.Wait();

                }

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }

                return View("Index",app);
            }
        }
        public ActionResult CreateApp(string name)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUri);

                var responseTask = client.GetAsync("Add_App" + "?name=" + name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    readTask.Wait();

                }
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public ActionResult UpdateApp(AppsViewModel app)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUri);

                var responseTask = client.GetAsync("Update_App" + "?id=" + app.id + "&name=" + app.name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    readTask.Wait();

                }
                return RedirectToAction("Index");
  
            }
        }
    }
}