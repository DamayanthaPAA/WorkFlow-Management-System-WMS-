using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;
using WorkFlowMgtSystem.Models.ViewModels;
using WorkFlowMgtSystem.Service;

namespace WorkFlowMgtSystem.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CreateCustomer()
        {
            ViewBag.InquiryStatusID = 1;
            return View();
        }


        public ActionResult ViewAllCustomers()
        {
            SmartCRM db = new Models.SmartCRM();
            return View(db.Customers.OrderBy(c => c.CustomerName));
        }

        public ActionResult EditCustomer(int id)
        {
            SmartCRM db = new Models.SmartCRM();
            var customer = db.Customers.Where(c => c.CustomerID == id).First();
            customer.CustomerID = id;
            ViewBag.CustomerGroupID = customer.CustomerGroupID;
            ViewBag.CustomerOrigin = customer.CustomerOrigin;

            if (customer.CustomerTitle == "Mr")
            {
                ViewBag.CusTitle = "1";
            }
            else if (customer.CustomerTitle == "Mrs")
            {
                ViewBag.CusTitle = "2";
            }

            else if (customer.CustomerTitle == "Miss")
            {
                ViewBag.CusTitle = "3";
            }
            else if (customer.CustomerTitle == "Dr")
            {
                ViewBag.CusTitle = "4";
            }
            else if (customer.CustomerTitle == "Prof")
            {
                ViewBag.CusTitle = "5";
            }
            else if (customer.CustomerTitle == "Rev")
            {
                ViewBag.CusTitle = "6";
            }


            return View(customer);

        }

        public ActionResult DeleteCustomer(int id)
        {
            SmartCRM db = new Models.SmartCRM();
            var customer = db.Customers.Where(c => c.CustomerID == id).First();
            customer.CustomerID = id;
            ViewBag.CustomerGroupID = customer.CustomerGroupID;
            ViewBag.CustomerOrigin = customer.CustomerOrigin;

            if (customer!=null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            return View("ViewAllCustomers",db.Customers.OrderBy(c => c.CustomerName));

        }
        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            try
            {


                customer.CustomerTelephone01 = ValidateString(customer.CustomerTelephone01);
                customer.CustomerTelephone02 = ValidateString(customer.CustomerTelephone02);
                customer.CustomerMobile = ValidateString(customer.CustomerMobile);
                customer.CustomerEmail= ValidateString(customer.CustomerEmail);
                customer.Remark = ValidateString(customer.Remark);

                SmartCRM db = new SmartCRM();
                var dbcustomer = db.Customers.Where(c => c.CustomerID == customer.CustomerID).First();
                dbcustomer.CustomerName = customer.CustomerName;
                dbcustomer.CustomerAddress = customer.CustomerAddress;
                dbcustomer.CustomerMobile = customer.CustomerMobile;
                dbcustomer.CustomerTelephone01 = customer.CustomerTelephone01;
                dbcustomer.CustomerTelephone02 = customer.CustomerTelephone02;
                dbcustomer.CustomerEmail = customer.CustomerEmail;
                dbcustomer.CustomerOrigin = customer.CustomerOrigin;
                dbcustomer.CustomerGroupID = customer.CustomerGroupID;
                dbcustomer.Remark = customer.Remark;
                dbcustomer.IsActive = customer.IsActive;
                dbcustomer.ModifiedDate = DateTime.Now;
                dbcustomer.ModifiedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());
                ViewBag.Customer = customer.CustomerCode;

                if (db.SaveChanges() == 1)
                {
                    ViewBag.Status = "1";
                    ModelState.Clear();
                    return View(new Customer());
                }
                else
                {
                    ViewBag.Status = "3";
                    return View(customer);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Status = "3";
                return View(customer);
            }

        }
        [HttpPost]
        public ActionResult CreateCustomer(CustomerEx customer)
        {
            Customer newCus = new Customer();

            customer.CustomerTelephone01 = ValidateString(customer.CustomerTelephone01);
            customer.CustomerTelephone02 = ValidateString(customer.CustomerTelephone02);
            customer.CustomerMobile = ValidateString(customer.CustomerMobile);
            customer.CustomerEmail = ValidateString(customer.CustomerEmail);
            customer.Remark = ValidateString(customer.Remark);

            newCus.CustomerID = customer.CustomerID;
            newCus.CustomerGroupID = customer.CustomerGroupID;
            newCus.CustomerCode = customer.CustomerCode;
            newCus.CustomerOrigin = customer.CustomerOrigin;
            newCus.CustomerTitle = customer.CustomerTitle;
            newCus.CustomerName = customer.CustomerName;
            newCus.CustomerAddress = ValidateString(customer.CustomerAddress);
            newCus.CustomerTelephone01 = ValidateString(customer.CustomerTelephone01);
            newCus.CustomerTelephone02 = ValidateString(customer.CustomerTelephone02);
            newCus.CustomerMobile = ValidateString(customer.CustomerMobile);
            newCus.CustomerEmail = ValidateString(customer.CustomerEmail);
            newCus.Remark = ValidateString(customer.Remark);
            newCus.IsActive = customer.IsActive;
            newCus.CreatedUserID = customer.CreatedUserID;
            newCus.CreatedDate = customer.CreatedDate;
            newCus.ModifiedUserID = customer.ModifiedUserID;
            newCus.ModifiedDate = customer.ModifiedDate;

            SmartCRM dbconrext = new SmartCRM();
            try
            {
                if (!dbconrext.Customers.Any(c => c.CustomerCode == customer.CustomerCode))
                {
                    if (String.IsNullOrWhiteSpace(customer.CustomerCode))
                    {
                        ServiceAutoNumber AutoNo = new ServiceAutoNumber();
                        customer.CustomerCode = AutoNo.getAutoNumber("CustomerCode");
                        newCus.CustomerCode = customer.CustomerCode;
                    }
                    customer.CreatedDate = DateTime.Now;
                    customer.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());

                    newCus.CreatedDate = DateTime.Now;
                    newCus.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());

                    dbconrext.Customers.Add(newCus);
                    @ViewBag.Customer = customer.CustomerCode;
                    if (dbconrext.SaveChanges() == 1)
                    {
                        customer.CustomerID = newCus.CustomerID;
                        if (PlaceOrder(customer) == 1)
                        {
                            ViewBag.Status = "1";
                            ModelState.Clear();
                            ViewBag.InquiryStatusID = 1;
                            return View(new CustomerEx());
                        }
                        else
                        {
                            ViewBag.Status = "3";
                            ViewBag.InquiryStatusID = customer.InquiryStatusID;
                            return View(customer);
                        }
                    }
                    else
                    {
                        ViewBag.Status = "3";
                        ViewBag.InquiryStatusID = customer.InquiryStatusID;
                        return View(customer);
                    }
                }
                else
                {
                    ViewBag.Status = "2";
                    ViewBag.InquiryStatusID = customer.InquiryStatusID;
                    return View(customer);
                }

            }
            catch (Exception e)
            {
                ViewBag.Status = "3";
                ViewBag.InquiryStatusID = customer.InquiryStatusID;
                return View(customer);
            }

        }

        private int PlaceOrder(CustomerEx cust)
        {
            try
            {



                SmartCRM db = new SmartCRM();
                Order order = new Order();
                order.OrderCode = cust.Order.OrderCode;// "DEF/" + cust.CustomerID + "/" + cust.CustomerOrigin;

                if (String.IsNullOrWhiteSpace(order.OrderCode))
                {
                    ServiceAutoNumber AutoNo = new ServiceAutoNumber();
                    order.OrderCode = AutoNo.getAutoNumber("OrderCode");
                }
                order.LocationID = cust.CustomerOrigin;
                order.CustomerID = cust.CustomerID;
                order.RegisteredDate = cust.Order.RegisteredDate;// DateTime.Now;
                order.OrderName = ValidateString(cust.Order.OrderName);// "Default";
                order.ReferenceUserID = cust.ReferenceUserID;// ;// cust.Order.ReferenceUserID;// Convert.ToInt32(Session["loggeduserid"].ToString());
                order.CreatedDate = DateTime.Now;
                order.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());
                order.Remark = ValidateString(cust.Order.Remark);// "Auto Generated order";
                order.User = db.Users.Find(cust.ReferenceUserID);
                order.User1 = db.Users.Find(cust.ReferenceUserID);
                order.HandledByUserID = cust.ReferenceUserID;
                db.Orders.Add(order);
                if (db.SaveChanges() == 1)
                {
                    PlaceInquiry(order, cust.CustomerMobile, cust.InquiryStatusID);

                    //OrderController orC = new OrderController();
                    //orC.UpdateHadalBy(order.OrderID,order.ReferenceUserID);


                    return 1;
                }
                else
                {
                    return 0;
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
                throw;
            }

        }


        private int PlaceInquiry(Order order, string Telephone, int InquiryStatusID)
        {
            SmartCRM db = new SmartCRM();
            Inquiry inquiry = new Inquiry();

            try
            {
                //InquiryID
                inquiry.OrderID = order.OrderID;
                inquiry.InquiryStatusID = 0;
                inquiry.InquiryStatusID = InquiryStatusID;

                inquiry.HandledBy = order.ReferenceUserID;
                inquiry.AllocatedDate = order.RegisteredDate;
                inquiry.Telephone = Telephone;
                inquiry.Remark = order.Remark;
                inquiry.NextInqDate = null;
                inquiry.AddedDate = DateTime.Now;
                inquiry.ModifiedUserID = null;
                inquiry.ModifiedDate = null;
                inquiry.CreatedDate = DateTime.Now;
                inquiry.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());
                inquiry.InquiryName = db.InquiryStatus.Find(inquiry.InquiryStatusID).InquiryName.ToString();


                if (Saveinquiry(inquiry))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

                //db.Inquiries.Add(inquiry);
                //if (db.SaveChanges() == 1)
                //{
                //    return 1;
                //}
                //else
                //{
                //return 0;
                //}

            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpGet]
        public JsonResult GetCustomerGroups()
        {
            SmartCRM db = new SmartCRM();
            var customergroups = db.CustomersGroups.OrderBy(k => k.CustomerGroupName);
            List<UserGroupViewModel> vvm = new List<UserGroupViewModel>();
            foreach (var g in customergroups)
            {
                UserGroupViewModel vm = new UserGroupViewModel();
                vm.UserGroupId = g.CustomerGroupID;
                vm.UserGroupName = g.CustomerGroupName;
                vvm.Add(vm);
            }
            return Json(JsonConvert.SerializeObject(vvm, Formatting.None, new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLocations()
        {
            SmartCRM db = new SmartCRM();
            var locations = db.Locations.OrderBy(k => k.LocationName);
            List<UserGroupViewModel> vvm = new List<UserGroupViewModel>();
            foreach (var g in locations)
            {
                UserGroupViewModel vm = new UserGroupViewModel();
                vm.UserGroupId = g.LocationID;
                vm.UserGroupName = g.LocationName;
                vvm.Add(vm);
            }
            return Json(JsonConvert.SerializeObject(vvm, Formatting.None, new JsonSerializerSettings
            { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), JsonRequestBehavior.AllowGet);
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
        public bool Saveinquiry(Inquiry inquiry)
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
                        String SqlString = @"INSERT INTO Inquiry " +
                     " (OrderID, InquiryStatusID, HandledBy, AllocatedDate, Telephone, Remark, AddedDate, CreatedDate, CreatedUserID,InquiryName) " +
                    "VALUES     (@OrderID, @InquiryStatusID, @HandledBy, @AllocatedDate, @Telephone, @Remark, @AddedDate, @CreatedDate, @CreatedUserID,@InquiryName)";



                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlString;


                        cmd.Parameters.AddWithValue("@OrderID", inquiry.OrderID);
                        cmd.Parameters.AddWithValue("@InquiryStatusID", inquiry.InquiryStatusID);
                        cmd.Parameters.AddWithValue("@HandledBy", inquiry.HandledBy);
                        cmd.Parameters.AddWithValue("@AllocatedDate", inquiry.AllocatedDate);
                        cmd.Parameters.AddWithValue("@Telephone", inquiry.Telephone);
                        cmd.Parameters.AddWithValue("@Remark", inquiry.Remark);
                        cmd.Parameters.AddWithValue("@AddedDate", inquiry.AddedDate);
                        cmd.Parameters.AddWithValue("@CreatedDate", inquiry.CreatedDate);
                        cmd.Parameters.AddWithValue("@CreatedUserID", inquiry.CreatedUserID);
                        InquiryStatu inquiryStatu = new InquiryStatu();
                        inquiryStatu = db.InquiryStatus.Find(inquiry.InquiryStatusID);

                        if (inquiryStatu == null)
                        {
                            cmd.Parameters.AddWithValue("@InquiryName", "");
                        }
                        else
                            cmd.Parameters.AddWithValue("@InquiryName", inquiryStatu.InquiryName.ToString());


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