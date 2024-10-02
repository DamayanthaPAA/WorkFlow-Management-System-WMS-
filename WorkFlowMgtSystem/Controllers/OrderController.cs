using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;

namespace WorkFlowMgtSystem.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {


            SmartCRM db = new Models.SmartCRM();
            return View("ViewAllOrder", db.Orders.OrderBy(k => k.OrderID));
            //return View(db.Customers.OrderBy(c => c.CustomerName));

        }

        public ActionResult IndexByUser(int Id)
        {
            


               SmartCRM db = new Models.SmartCRM();
            return View("ViewAllOrderHandledByMe", db.Orders.Where(i => i.HandledByUserID==Id).OrderBy(k => k.OrderID));
            //return View(db.Customers.OrderBy(c => c.CustomerName));

        }

        public ActionResult IndexByAdminUser(int Id)
        {

            try
            {

            
            
            SmartCRM db = new Models.SmartCRM();

            User user = db.Users.Find(Id);
            UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);
            if (userGroup.UserGroupName.Equals("Admin"))
            {
                ViewBag.Title = "Order Admin Control";
                return View("ViewAllOrder", db.Orders.OrderBy(k => k.OrderID));
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
            }
            //return View(db.Customers.OrderBy(c => c.CustomerName));
            catch (Exception EX)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult IndexByCreateUser(int Id)
        {


            SmartCRM db = new Models.SmartCRM();
            return View("ViewAllOrder", db.Orders.Where(i => i.CreatedUserID == Id).OrderBy(k => k.OrderID));
            //return View(db.Customers.OrderBy(c => c.CustomerName));

        }
        public ActionResult EditOrder(int id)
        {
            SmartCRM db = new Models.SmartCRM();
            var customerOrder = db.Orders.Where(c => c.OrderID == id).First();
            customerOrder.OrderID = id;
            ViewBag.CustomerGroupID = customerOrder.Customer.CustomerGroupID;
            ViewBag.CustomerOrigin = customerOrder.Customer.CustomerOrigin;

            ViewBag.LocationID = customerOrder.LocationID;
            ViewBag.ReferenceUserID = customerOrder.ReferenceUserID;
            ViewBag.RegisteredDate = customerOrder.RegisteredDate.ToShortDateString();

            if (customerOrder.Customer.CustomerTitle == "Mr")
            {
                ViewBag.CusTitle = "1";
            }
            else if (customerOrder.Customer.CustomerTitle == "Mrs")
            {
                ViewBag.CusTitle = "2";
            }

            else if (customerOrder.Customer.CustomerTitle == "Miss")
            {
                ViewBag.CusTitle = "3";
            }
            else if (customerOrder.Customer.CustomerTitle == "Dr")
            {
                ViewBag.CusTitle = "4";
            }
            else if (customerOrder.Customer.CustomerTitle == "Prof")
            {
                ViewBag.CusTitle = "5";
            }
            else if (customerOrder.Customer.CustomerTitle == "Rev")
            {
                ViewBag.CusTitle = "6";
            }


            return View(customerOrder);

        }
        [HttpPost]
        public ActionResult EditOrder(Order Order)
        {
            try
            {
                SmartCRM db = new SmartCRM();
                var dbcustomerOrder = db.Orders.Where(c => c.OrderID == Order.OrderID).First();

                dbcustomerOrder.OrderID = Order.OrderID;
                dbcustomerOrder.OrderCode = Order.OrderCode;
                dbcustomerOrder.LocationID = Order.LocationID;
                //dbcustomerOrder.CustomerID = dbcustomerOrder.CustomerID;
                dbcustomerOrder.RegisteredDate = Order.RegisteredDate;
                dbcustomerOrder.OrderName = ValidateString(Order.OrderName);
                dbcustomerOrder.ReferenceUserID = Order.ReferenceUserID;
                dbcustomerOrder.Remark = ValidateString(Order.Remark);
                //dbcustomerOrder.CreatedDate = Order.CreatedDate;
                //dbcustomerOrder.CreatedUserID = Order.CreatedUserID;
                dbcustomerOrder.ModifiedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());
                dbcustomerOrder.ModifiedDate = DateTime.Now;




                ViewBag.Customer = Order.OrderCode;

                if (db.SaveChanges() == 1)
                {
                    ViewBag.Status = "1";
                    ModelState.Clear();
                    //return View(new Order());
                    return View("ViewAllOrder", db.Orders.OrderBy(k => k.OrderID));
                }
                else
                {
                    ViewBag.Status = "3";
                    return View(Order);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Status = "3";
                return View(Order);
            }

        }
        //public ActionResult ViewAllCustomers()
        //{
        //    SmartCRM db = new Models.SmartCRM();
        //    return View(db.Customers.OrderBy(c => c.CustomerName));
        //}
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
        public ActionResult ViewOrderInquiry(int id)
        {
            //return View();
            SmartCRM db = new Models.SmartCRM();

            return View("~/Views/Inquiry/ViewOrderInquiry.cshtml", db.Inquiries.Where(i => i.OrderID == id).OrderByDescending(k => k.InquiryID));


        }


        public ActionResult EditInquiry(int id)
        {
            SmartCRM db = new Models.SmartCRM();
            Inquiry inquiry = new Inquiry();
            Order order = new Order();
            inquiry = db.Inquiries.Where(i => i.InquiryID == id).First();
            order = db.Orders.Where(i => i.OrderID == inquiry.OrderID).First();
            inquiry.Order = order;
            inquiry.Order.Customer = order.Customer;
            inquiry.InquiryID = id;
            ViewBag.InquiryID = id;
            ViewBag.CustomerGroupID = order.Customer.CustomerGroupID;
            ViewBag.CustomerOrigin = order.Customer.CustomerOrigin;

            ViewBag.LocationID = order.LocationID;
            ViewBag.ReferenceUserID = order.ReferenceUserID;
            ViewBag.RegisteredDate = order.RegisteredDate.ToShortDateString();

            Inquiry inquiryPre = new Inquiry();
            inquiryPre = db.Inquiries.Where(k => k.OrderID == inquiry.OrderID).OrderByDescending(i => i.InquiryID).First();
            if (inquiryPre == null)
            {
                ViewBag.HandledBy = order.ReferenceUserID;
                InquiryStatu xInquiryName = new InquiryStatu();
                xInquiryName = db.InquiryStatus.Find(order.Inquiries.First().InquiryStatusID);

                ViewBag.PresentInquiryStatus = xInquiryName.InquiryName;
            }
            else
            {
                ViewBag.HandledBy = inquiryPre.HandledBy;
                InquiryStatu inquiryStatu = new InquiryStatu();
                inquiryStatu = db.InquiryStatus.Find(inquiryPre.InquiryStatusID);

                ViewBag.PresentInquiryStatus = inquiryStatu.InquiryName.ToString();

            }

            return View("EditInquiry", inquiry);

            //return View("ViewOrderInquiry", db.Inquiries.Where(i => i.OrderID == id).OrderByDescending(k => k.InquiryID));
        }

        public bool UpdateHadalBy(int OrderID, int HandledByUserID)
        {

            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;

            try
            {
                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (System.Data.SqlClient.SqlCommand cmd = (System.Data.SqlClient.SqlCommand)conn.CreateCommand())
                    {
                        String SqlString = @"UPDATE  [Order] SET    HandledByUserID = @HandledByUserID  WHERE        (OrderID = @OrderID)";


                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlString;


                        cmd.Parameters.AddWithValue("@OrderID", OrderID);
                        cmd.Parameters.AddWithValue("@HandledByUserID", HandledByUserID);
                       


                        int saved = 0;
                        saved = cmd.ExecuteNonQuery();

                        if (saved != 0) return true; else return false;
                    }
                }


            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
        }
    }
}