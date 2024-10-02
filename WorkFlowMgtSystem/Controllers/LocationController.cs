using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;

namespace WorkFlowMgtSystem.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLocation()
        {
            SmartCRM db = new SmartCRM();
            var InquiryStatus = db.Locations.OrderBy(k => k.LocationID);
            List<Location> vvm = new List<Location>();
            foreach (var g in InquiryStatus)
            {
                Location vm = new Location();
                vm.LocationID = g.LocationID;
                vm.LocationName = g.LocationName;
                vvm.Add(vm);
            }
            return Json(JsonConvert.SerializeObject(vvm, Formatting.None, new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        }
    }
}