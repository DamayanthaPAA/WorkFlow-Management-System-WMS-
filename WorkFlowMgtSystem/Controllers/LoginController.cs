using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;




namespace WorkFlowMgtSystem.Controllers
{
    public class LoginController : Controller
    {
      
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Models.ViewModels.LoginViewModel loginviewmodel)
        {
            using (SmartCRM dc = new SmartCRM())
            {
                if (String.IsNullOrEmpty(loginviewmodel.Username)||String.IsNullOrEmpty(loginviewmodel.Password))
                {
                    return View("Login");        
                }
                else
                {
                    
                    var v = dc.Users.Where(x => x.UserName == loginviewmodel.Username && x.UserPassword == loginviewmodel.Password).FirstOrDefault();//Checking user name and password
                    if (v!=null)
                    {
                        Session["loggeduserid"] = v.UserID;
                        Session["UserGroupName"] = "";
                        var g = dc.UserGroups.Where(i => i.UserGroupID == v.UserGroupID).FirstOrDefault();
                        if (g==null)
                        {

                            return View("Login");
                        }
                        Session["UserGroupName"] =g.UserGroupName.ToString().Trim();
                        // return RedirectToAction("Index","Dashboard");
                        //return RedirectToAction("Index", "OrderSummery");
                        return RedirectToAction("IndexUser", "OrderSummery" ,new {id = Convert.ToInt32(Session["loggeduserid"].ToString())});
                        
                    }
                    else
                    {
                        return View("Login");
                   
                    }
                }
            }
            //if login failed
          //  return View("Login");

            // if success    return View("_Layout");
        }
	}
}