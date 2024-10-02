using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;

namespace WorkFlowMgtSystem.Controllers
{
    public class InquiryController : Controller
    {
        // GET: Inquiry
        public ActionResult Index(int id)
        {
            //return View();
            SmartCRM db = new Models.SmartCRM();
            return View("ViewOrderInquiry", db.Inquiries.Where(i => i.OrderID == id).OrderByDescending(k => k.InquiryID));
        }

        public ActionResult ViewOrderInquiry(int id)
        {
            //return View();
            SmartCRM db = new Models.SmartCRM();


            Order order = new Order();
            order = db.Orders.Find(id);
            ViewBag.Order = order.OrderName;
            ViewBag.OrderID = order.OrderID;
            return View("ViewOrderInquiry", db.Inquiries.Where(i => i.OrderID == id).OrderByDescending(k => k.InquiryID));
        }

        public ActionResult CreateInquiry(int id)
        {
            SmartCRM db = new Models.SmartCRM();
            Inquiry inquiry = new Inquiry();
            Order order = new Order();
            order = db.Orders.Where(i => i.OrderID == id).First();
            inquiry.Order = order;
            inquiry.Order.Customer = order.Customer;

            ViewBag.CustomerGroupID = order.Customer.CustomerGroupID;
            ViewBag.CustomerOrigin = order.Customer.CustomerOrigin;

            ViewBag.LocationID = order.LocationID;
            ViewBag.ReferenceUserID = order.ReferenceUserID;
            ViewBag.RegisteredDate = order.RegisteredDate.ToShortDateString();

            Inquiry inquiryPre = new Inquiry();
            inquiryPre = db.Inquiries.Where(k => k.OrderID == id).OrderByDescending(i => i.InquiryID).First();
            if (inquiryPre == null)
            {
                ViewBag.HandledBy = order.ReferenceUserID;
                InquiryStatu  xInquiryName = new InquiryStatu();
                xInquiryName=db.InquiryStatus.Find(order.Inquiries.First().InquiryStatusID);

                ViewBag.PresentInquiryStatus = xInquiryName.InquiryName;
            }
            else
            {
                ViewBag.HandledBy = inquiryPre.HandledBy;
                InquiryStatu inquiryStatu = new InquiryStatu();
                inquiryStatu = db.InquiryStatus.Find(inquiryPre.InquiryStatusID);

                ViewBag.PresentInquiryStatus = inquiryStatu.InquiryName.ToString();

            }

            return View("CreateInquiry", inquiry);

            //return View("ViewOrderInquiry", db.Inquiries.Where(i => i.OrderID == id).OrderByDescending(k => k.InquiryID));
        }

        public ActionResult EditInquiryx(int id,int InqId)
        {
            SmartCRM db = new Models.SmartCRM();
            Inquiry inquiry = new Inquiry();
            Order order = new Order();
            order = db.Orders.Where(i => i.OrderID == id).First();
            inquiry.Order = order;
            inquiry.Order.Customer = order.Customer;

            ViewBag.CustomerGroupID = order.Customer.CustomerGroupID;
            ViewBag.CustomerOrigin = order.Customer.CustomerOrigin;

            ViewBag.LocationID = order.LocationID;
            ViewBag.ReferenceUserID = order.ReferenceUserID;
            ViewBag.RegisteredDate = order.RegisteredDate.ToShortDateString();

            Inquiry inquiryPre = new Inquiry();
            inquiryPre = db.Inquiries.Where(k => k.OrderID == id).OrderByDescending(i => i.InquiryID).First();
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

        [HttpPost]
        public ActionResult CreateInquiry(Inquiry inquiry)
        {

            SmartCRM dbconrext = new SmartCRM();
            try
            {
                if (dbconrext.Orders.Any(c => c.OrderID == inquiry.Order.OrderID))
                {
                    //customer.CreatedDate = DateTime.Now;
                    //customer.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());
                    //dbconrext.Customers.Add(customer);
                    @ViewBag.Customer = inquiry.Order.OrderCode;
                    //if (dbconrext.SaveChanges() == 1)
                    //{
                    if (PlaceInquiry(inquiry) == 1)
                    {
                        ViewBag.Status = "1";
                        ModelState.Clear();
                        //return View(new Customer());
                        return View("ViewOrderInquiry", dbconrext.Inquiries.Where(i => i.OrderID == inquiry.Order.OrderID).OrderByDescending(k => k.InquiryID));
                    }
                    else
                    {
                        ViewBag.Status = "3";
                        return View(inquiry);
                    }
                    //}
                    //else
                    //{
                    //    ViewBag.Status = "3";
                    //    return View(inquiry);
                    //}
                }
                else
                {
                    ViewBag.Status = "2";
                    return View(inquiry);
                }

            }
            catch (Exception)
            {
                ViewBag.Status = "3";
                return View(inquiry);
            }

        }
        [HttpPost]
        public ActionResult EditInquiry(Inquiry inquiry)
        {

            SmartCRM dbconrext = new SmartCRM();
            try
            {
                if (dbconrext.Orders.Any(c => c.OrderID == inquiry.Order.OrderID))
                {
                    //customer.CreatedDate = DateTime.Now;
                    //customer.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());
                    //dbconrext.Customers.Add(customer);
                    @ViewBag.Customer = inquiry.Order.OrderCode;
                    //if (dbconrext.SaveChanges() == 1)
                    //{
                    if (EditSaveinquiry(inquiry))
                    {
                        OrderController orC = new OrderController();
                        orC.UpdateHadalBy(inquiry.Order.OrderID, inquiry.HandledBy);
                        ViewBag.Status = "1";
                        ModelState.Clear();
                        //return View(new Customer());
                        return View("ViewOrderInquiry", dbconrext.Inquiries.Where(i => i.OrderID == inquiry.Order.OrderID).OrderByDescending(k => k.InquiryID));
                    }
                    else
                    {
                        ViewBag.Status = "3";
                        return View(inquiry);
                    }
                    //}
                    //else
                    //{
                    //    ViewBag.Status = "3";
                    //    return View(inquiry);
                    //}
                }
                else
                {
                    ViewBag.Status = "2";
                    return View(inquiry);
                }

            }
            catch (Exception)
            {
                ViewBag.Status = "3";
                return View(inquiry);
            }

        }

        private int PlaceInquiry(Inquiry inquiry)
        {
            SmartCRM db = new SmartCRM();
            // Inquiry inquiry = new Inquiry();

            try
            {
                //InquiryID
                inquiry.OrderID = inquiry.OrderID;
                //inquiry.InquiryStatusID = 0;
                inquiry.HandledBy = inquiry.HandledBy;
                inquiry.AllocatedDate = inquiry.AllocatedDate;
                if (inquiry.AllocatedDate.Date.Year==0001)
                {
                    //inquiry.AllocatedDate = inquiry.AllocatedDate;
                    inquiry.AllocatedDate = System.DateTime.Now.Date;
                }
                
                inquiry.Telephone = ValidateString(inquiry.Telephone);
                inquiry.Remark = ValidateString(inquiry.Remark);
                //inquiry.NextInqDate = null;
                inquiry.AddedDate = DateTime.Now;
                inquiry.ModifiedUserID = null;
                inquiry.ModifiedDate = null;
                inquiry.CreatedDate = DateTime.Now;
                inquiry.CreatedUserID = Convert.ToInt32(Session["loggeduserid"].ToString());


                if (Saveinquiry(inquiry))
                {
                    OrderController orC = new OrderController();
                    orC.UpdateHadalBy(inquiry.Order.OrderID, inquiry.HandledBy);
                    return 1;
                }
                else
                {
                    return 0;
                }



            }
            catch (Exception e)
            {

                throw;
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
        public bool Saveinquiry(Inquiry inquiry)
        {


            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;

            try
            {
                String SqlString = "";
                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (System.Data.SqlClient.SqlCommand cmd = (System.Data.SqlClient.SqlCommand)conn.CreateCommand())
                    {

                        if (inquiry.NextInqDate == null)
                        {
                            SqlString = @"INSERT INTO Inquiry " +
" (OrderID, InquiryStatusID, HandledBy, AllocatedDate, Telephone, Remark, AddedDate,  CreatedDate, CreatedUserID,InquiryName) " +
"VALUES     (@OrderID, @InquiryStatusID, @HandledBy, @AllocatedDate, @Telephone, @Remark, @AddedDate,  @CreatedDate, @CreatedUserID,@InquiryName)";


                        }
                        else
                        {


                            SqlString = @"INSERT INTO Inquiry " +
                   " (OrderID, InquiryStatusID, HandledBy, AllocatedDate, Telephone, Remark, NextInqDate, AddedDate,  CreatedDate, CreatedUserID,InquiryName) " +
                  "VALUES     (@OrderID, @InquiryStatusID, @HandledBy, @AllocatedDate, @Telephone, @Remark, @NextInqDate, @AddedDate,  @CreatedDate, @CreatedUserID,@InquiryName)";

                        }

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlString;


                        cmd.Parameters.AddWithValue("@OrderID", inquiry.Order.OrderID);
                        cmd.Parameters.AddWithValue("@InquiryStatusID", inquiry.InquiryStatusID);
                        cmd.Parameters.AddWithValue("@HandledBy", inquiry.HandledBy);
                        cmd.Parameters.AddWithValue("@AllocatedDate", inquiry.AllocatedDate);
                        cmd.Parameters.AddWithValue("@Telephone", inquiry.Telephone);
                        cmd.Parameters.AddWithValue("@Remark", inquiry.Remark);
                        if (inquiry.NextInqDate != null)
                            cmd.Parameters.AddWithValue("@NextInqDate", inquiry.NextInqDate);
                        cmd.Parameters.AddWithValue("@AddedDate", inquiry.AddedDate);
                        cmd.Parameters.AddWithValue("@CreatedDate", inquiry.CreatedDate);
                        cmd.Parameters.AddWithValue("@CreatedUserID", inquiry.CreatedUserID);

                        cmd.Parameters.AddWithValue("@InquiryName",db.InquiryStatus.Find( inquiry.InquiryStatusID).InquiryName.ToString());



                        int saved = 0;
                        saved = cmd.ExecuteNonQuery();

                        if (saved != 0) return true; else return false;
                    }
                }


            }
            catch (Exception e)
            {
                return false;
                throw;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
        }


        public bool EditSaveinquiry(Inquiry inquiry)
        {


            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;

            try
            {
                String SqlString = "";
                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (System.Data.SqlClient.SqlCommand cmd = (System.Data.SqlClient.SqlCommand)conn.CreateCommand())
                    {

                        if (inquiry.NextInqDate == null)
                        {
                            SqlString = @"UPDATE    Inquiry " +
                                        "SET InquiryStatusID =@InquiryStatusID, HandledBy =@HandledBy, Telephone =@Telephone, Remark =@Remark ," +
                                        " ModifiedUserID =@ModifiedUserID, ModifiedDate =@ModifiedDate, InquiryName =@InquiryName " +
                                            "WHERE     (InquiryID = @InquiryID)";


 


                        }
                        else
                        {


                            SqlString = @"UPDATE    Inquiry " +
                                        "SET InquiryStatusID =@InquiryStatusID, HandledBy =@HandledBy, Telephone =@Telephone, Remark =@Remark, NextInqDate =@NextInqDate," +
                                        " ModifiedUserID =@ModifiedUserID, ModifiedDate =@ModifiedDate, InquiryName =@InquiryName " +
                                            "WHERE     (InquiryID = @InquiryID)";


                  //          INSERT INTO Inquiry " +
                  // " (OrderID, InquiryStatusID, HandledBy, AllocatedDate, Telephone, Remark, NextInqDate, AddedDate,  CreatedDate, CreatedUserID,InquiryName) " +
                  //"VALUES     (@OrderID, @InquiryStatusID, @HandledBy, @AllocatedDate, @Telephone, @Remark, @NextInqDate, @AddedDate,  @CreatedDate, @CreatedUserID,@InquiryName)";

                        }

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = SqlString;

                        cmd.Parameters.AddWithValue("@InquiryID", inquiry.InquiryID);
                        cmd.Parameters.AddWithValue("@OrderID", inquiry.Order.OrderID);
                        cmd.Parameters.AddWithValue("@InquiryStatusID", inquiry.InquiryStatusID);
                        cmd.Parameters.AddWithValue("@HandledBy", inquiry.HandledBy);
                        //cmd.Parameters.AddWithValue("@AllocatedDate", inquiry.AllocatedDate);
                        cmd.Parameters.AddWithValue("@Telephone", inquiry.Telephone);
                        cmd.Parameters.AddWithValue("@Remark", inquiry.Remark);
                        if (inquiry.NextInqDate != null)
                            cmd.Parameters.AddWithValue("@NextInqDate", inquiry.NextInqDate);
                        //cmd.Parameters.AddWithValue("@AddedDate", inquiry.AddedDate);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedUserID", Convert.ToInt32(Session["loggeduserid"].ToString()));

                        cmd.Parameters.AddWithValue("@InquiryName", db.InquiryStatus.Find(inquiry.InquiryStatusID).InquiryName.ToString());



                        int saved = 0;
                        saved = cmd.ExecuteNonQuery();

                        if (saved != 0) return true; else return false;
                    }
                }


            }
            catch (Exception e)
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