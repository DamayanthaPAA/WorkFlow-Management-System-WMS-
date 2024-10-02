using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;
using WorkFlowMgtSystem.Models.ViewModels;

namespace WorkFlowMgtSystem.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult ViewAllUsers()
        {
            SmartCRM sc = new SmartCRM();          
            return View(sc.Users.OrderBy(u => u.UserCode));
            
    
        }

        public ActionResult EditUser(int id)
        {
            SmartCRM sc = new SmartCRM();
            var user = sc.Users.Where(u => u.UserID == id).First();
            ViewBag.UserGroupID = user.UserGroupID;
            return View(user);


        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            int id = Convert.ToInt32(RouteData.Values["id"] + Request.Url.Query);
            SmartCRM dbcontext = new SmartCRM();
            
                using (var dbtransaction = dbcontext.Database.BeginTransaction())
                {
                    try
                    {
                    if (user != null)
                    {
                        user.UserEmail = ValidateString(user.UserEmail);
                        user.UserPhone01 = ValidateString(user.UserPhone01);
                        user.UserPhone02 = ValidateString(user.UserPhone02);
                    }
                    var dbuser = dbcontext.Users.Where(u => u.UserID == id).First();

                            @ViewBag.UserCode = dbuser.UserCode;
                            dbuser.UserCode= dbuser.UserCode;
                            dbuser.UserFullName = user.UserFullName;
                            dbuser.UserName = user.UserName;
                            dbuser.UserGroupID = user.UserGroupID;
                            dbuser.UserPassword = user.UserPassword;
                            dbuser.ConfirmPassword = user.ConfirmPassword;
                            dbuser.UserPhone01 = user.UserPhone01;
                            dbuser.UserPhone02 = user.UserPhone02;
                            dbuser.UserEmail = user.UserEmail;
                            dbuser.UserStatus = user.UserStatus;
                            dbuser.Inquiries = null;
                            dbuser.Inquiries1 = null;
                            dbuser.Orders = null;
                            dbuser.UserGroups = null;
                   
                            if (dbcontext.SaveChanges() == 1)
                            {
                                dbtransaction.Commit();
                                ViewBag.Status = "1";
                                ModelState.Clear();
                                return View(new User());
                            }
                            else
                            {
                                dbtransaction.Rollback();
                                ViewBag.Status = "3";
                                return View(user);
                            }
                }
                catch (Exception ex)
                {
                    dbtransaction.Rollback();
                    ViewBag.Status = "3";
                    return View(user);
                }
            }


        }
        private String ValidateString(String Values)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(Values))
                {
                    return Values.Trim();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {

                return "";
            }
        }
        [HttpPost]
        public ActionResult CreateUser([Bind(Exclude = "UserID")] User sysuser)
        {
           
            try
            {

                if (sysuser!=null)
                {
                    sysuser.UserEmail = ValidateString(sysuser.UserEmail);
                    sysuser.UserPhone01 = ValidateString(sysuser.UserPhone01);
                    sysuser.UserPhone02 = ValidateString(sysuser.UserPhone02);
                }
                using (SmartCRM SC = new SmartCRM())
                {
                    @ViewBag.UserCode = sysuser.UserCode;
                    if (SC.Users.Any(x => x.UserCode == sysuser.UserCode))
                    {
                        ViewBag.Status = "2";
                        return View(sysuser);
                    }
                    else
                    {

                        sysuser.CreatedDate = DateTime.Now;
                        SC.Users.Add(sysuser);
                        if (SC.SaveChanges() == 1)
                        {

                            ViewBag.Status = "1";
                            return View(new User());
                        }
                        else
                        {
                            ViewBag.Status = "3";
                            return View(sysuser);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return View(sysuser);
            }
            
            
        }

        [HttpGet]
        public JsonResult GetUserGroupListJsonResult()
        {
            SmartCRM db =new SmartCRM();
            var usergroups = db.UserGroups.OrderBy(k => k.UserGroupName);
            List<UserGroupViewModel> vvm = new List<UserGroupViewModel>();
            foreach (var g in usergroups)
            {
                Models.ViewModels.UserGroupViewModel vm = new Models.ViewModels.UserGroupViewModel();
                vm.UserGroupId = g.UserGroupID;
                vm.UserGroupName = g.UserGroupName;
                vvm.Add(vm);
            }
            return Json(JsonConvert.SerializeObject(vvm, Formatting.None, new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers()
        {
            SmartCRM db = new SmartCRM();
            var Users = db.Users.Where(i => i.UserStatus ==true).OrderBy(k => k.UserCode);
            List<UserGroupViewModel> vvm = new List<UserGroupViewModel>();
            foreach (var g in Users)
            {
                UserGroupViewModel vm = new UserGroupViewModel();
                vm.UserGroupId = g.UserID;
                vm.UserGroupName = g.UserFullName;
                vvm.Add(vm);
            }
            return Json(JsonConvert.SerializeObject(vvm, Formatting.None, new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        }
    }
}