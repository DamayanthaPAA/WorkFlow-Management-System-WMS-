using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;
using WorkFlowMgtSystem.Models.ViewModels;
using System.IO;
namespace WorkFlowMgtSystem.Controllers
{
    public class InquiryFileController : Controller
    {
        // GET: InquiryFile
        public ActionResult Index(int id, int OrderID)
        {
            ViewBag.id = id;
            ViewBag.OrderID = OrderID;

            return View();


        }
        [HttpPost]
        public ActionResult Index(InquiryFileViewModel inquiryFile)
        {
            InquiryFile objInquiryFile = new InquiryFile();
            string fileName = Path.GetFileNameWithoutExtension(inquiryFile.imageFileName.FileName);
            string extension = Path.GetExtension(inquiryFile.imageFileName.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + inquiryFile.InquiryID.ToString() + extension;

            objInquiryFile.InquiryID = inquiryFile.InquiryID;
            objInquiryFile.ImageName = inquiryFile.ImageName;
            objInquiryFile.InquiryFileImage = "~/InquiryFile/"+ fileName;
            fileName = Path.Combine(Server.MapPath("~/InquiryFile"), fileName);
            inquiryFile.imageFileName.SaveAs(fileName);

            using (SmartCRM db=new SmartCRM())
            {
                db.InquiryFiles.Add(objInquiryFile);
                db.SaveChanges();
                ModelState.Clear();
            }

            return View();
        }

        public ActionResult ViewImage(int id)
        {
            ViewBag.id = id;

            SmartCRM db = new SmartCRM();
            return View("ViewImage", db.InquiryFiles.Where(i => i.InquiryID== id).OrderByDescending(k =>k.InquiryFileID));


        }
    }
}