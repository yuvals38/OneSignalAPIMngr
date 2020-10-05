using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneSignalAPPMngr.Models
{
    public class AppsViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string players { get; set; }
        public string site_name { get; set; }


    }

    public class Root
    {
        public AppsViewModel AppsViewModel { get; set; }
       

    }
}