using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using OneSignalAPPMngr.Models;
using Newtonsoft.Json;

namespace OneSignalAPPMngr.Controllers
{
    public class AppsController : BaseController
    {
        // GET: Apps
        public ActionResult Index()
        {
            List<AppsViewModel> app = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50631/api/apps/View_Apps");
                //HTTP GET
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
                client.BaseAddress = new Uri("http://localhost:50631/api/apps/Add_App" + "?name=" + name );
                //HTTP GET
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
                client.BaseAddress = new Uri("http://localhost:50631/api/apps/Update_App" + "?id=" + app.id + "&name=" + app.name);// + "&name=" + name);
                //HTTP GET
                var responseTask = client.GetAsync("Update_App" + "?id=" + app.id + "&name=" + app.name);// + "&name=" + name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();

                    // app = JsonConvert.DeserializeObject<List<AppsViewModel>>(readTask.Result);

                    readTask.Wait();

                }
                return RedirectToAction("Index");
                //Index();
                // return View("Index", app);
            }
        }
    }
}