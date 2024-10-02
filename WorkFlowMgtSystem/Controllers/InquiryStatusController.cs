using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;

namespace WorkFlowMgtSystem.Controllers
{
    public class InquiryStatusController : Controller
    {
        // GET: InquiryStatus
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetInquiryStatus()
        {
            SmartCRM db = new SmartCRM();
            var InquiryStatus = db.InquiryStatus.OrderBy(k => k.InquiryStatus ==true);
            List<InquiryStatu> vvm = new List<InquiryStatu>();
            foreach (var g in InquiryStatus)
            {
                InquiryStatu vm = new InquiryStatu();
                vm.InquiryStstusID = g.InquiryStstusID;
                vm.InquiryName = g.InquiryName;
                vvm.Add(vm);
            }
            return Json(JsonConvert.SerializeObject(vvm, Formatting.None, new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        }
    }
}