using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowMgtSystem.Models;
using WorkFlowMgtSystem.Models.ViewModels;

namespace WorkFlowMgtSystem.Controllers
{
    public class OrderSummeryController : Controller
    {
        // GET: OrderSummery
        public ActionResult Index()
        {
            //return View();
            SmartCRM db = new Models.SmartCRM();
            return View("OrderSummery", db.OrderSummeries.OrderByDescending(k => k.OrderID));
        }

        private DataTable GetSummerybyInquiryName()
        {
            DataTable dt = new DataTable();
            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;

            try
            {
                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT     ROW_NUMBER() OVER (ORDER BY OrderSummery.InquiryName DESC) AS Row, " +
                            " OrderSummery.InquiryName, COUNT(OrderSummery.InquiryName) AS NoOfJob, InquiryStatusID " +
                            " FROM OrderSummery GROUP BY  InquiryStatusID,InquiryName ORDER BY InquiryStatusID";

                        //if (!String.IsNullOrWhiteSpace(CODE))
                        //    cmd.CommandText = cmd.CommandText + " WHERE ([CODE] ='" + CODE.ToString().Trim() + "')";
                        cmd.CommandType = CommandType.Text;

                        using (var red = cmd.ExecuteReader())
                        {
                            dt.Load(red);
                        }

                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
        }


        private DataTable GetSummerybyInquiryName(int id)
        {
            DataTable dt = new DataTable();
            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;

            try
            {
                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT     ROW_NUMBER() OVER (ORDER BY OrderSummery.InquiryName DESC) AS Row, " +
                            " OrderSummery.InquiryName, COUNT(OrderSummery.InquiryName) AS NoOfJob, InquiryStatusID " +
                            " FROM OrderSummery WHERE HandledBy=" + id + "   GROUP BY  InquiryStatusID,InquiryName ORDER BY InquiryStatusID";

                        //if (!String.IsNullOrWhiteSpace(CODE))
                        //    cmd.CommandText = cmd.CommandText + " WHERE ([CODE] ='" + CODE.ToString().Trim() + "')";
                        cmd.CommandType = CommandType.Text;

                        using (var red = cmd.ExecuteReader())
                        {
                            dt.Load(red);
                        }

                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
        }

        private DataTable GetSummerybyInquiryNameCreateUser(int id)
        {
            DataTable dt = new DataTable();
            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;

            try
            {
                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT     ROW_NUMBER() OVER (ORDER BY OrderSummery.InquiryName DESC) AS Row, " +
                            " OrderSummery.InquiryName, COUNT(OrderSummery.InquiryName) AS NoOfJob, InquiryStatusID " +
                            " FROM OrderSummery WHERE CreatedUserID=" + id + "   GROUP BY  InquiryStatusID,InquiryName ORDER BY InquiryStatusID";

                        //if (!String.IsNullOrWhiteSpace(CODE))
                        //    cmd.CommandText = cmd.CommandText + " WHERE ([CODE] ='" + CODE.ToString().Trim() + "')";
                        cmd.CommandType = CommandType.Text;

                        using (var red = cmd.ExecuteReader())
                        {
                            dt.Load(red);
                        }

                    }
                }

                return dt;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
        }
        public ActionResult IndexUser(int id)
        {
            ViewBag.Title = "Order Summery";

            ViewBag.NextDay = "";
            try
            {
                List<OrderSummerySume> orderSummerySumeList = new List<OrderSummerySume>();
                DataTable dt = null;// GetSummerybyInquiryName();
                                    //return View();
                SmartCRM db = new Models.SmartCRM();
                User user = db.Users.Find(id);
                UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);

                if (userGroup.UserGroupName.Equals("Admin"))
                {
                    //HobbyHomeService().FetchLearner();
                    //ICollection<Person> personlist = new HobbyHomeService().FetchPerson(list);
                    //IEnumerable<OrderSummerySume> orderSummeryX = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    //var versions =db.OrderSummeries.Select(v => new { v.InquiryName ,v.InquiryName.Count() });
                    //ViewBag.data = orderSummeryX;

                    //List<OrderSummerySume> list = new List<OrderSummerySume>();
                    //list= db.OrderSummerySume.ToList();
                    //var OrderSummeriesobj = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).ToList();

                    //var dd = (from obj in OrderSummeriesobj
                    //          select new
                    //          {
                    //              InquiryName = obj.InquiryName,
                    //              InquiryNameCount = obj.InquiryName.Count()
                    //          }).ToList();

                    dt = GetSummerybyInquiryName();
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    ViewBag.data = orderSummerySumeList;

                    //IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);

                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);

                    //return View("OrderSummery", db.OrderSummeries.Where(i => i.InquiryStatus == false).OrderByDescending(k => k.OrderID));
                }
                else
                {
                    dt = GetSummerybyInquiryName(id);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);
                }



            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult IndexCreateUser(int id)
        {
            ViewBag.Title = "Order Summery";

            ViewBag.NextDay = "";
            try
            {
                List<OrderSummerySume> orderSummerySumeList = new List<OrderSummerySume>();
                DataTable dt = null;// GetSummerybyInquiryName();
                                    //return View();
                SmartCRM db = new Models.SmartCRM();
                User user = db.Users.Find(id);
                UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);

                //if (userGroup.UserGroupName.Equals("Admin"))
                //{
                //    //HobbyHomeService().FetchLearner();
                //    //ICollection<Person> personlist = new HobbyHomeService().FetchPerson(list);
                //    //IEnumerable<OrderSummerySume> orderSummeryX = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                //    //var versions =db.OrderSummeries.Select(v => new { v.InquiryName ,v.InquiryName.Count() });
                //    //ViewBag.data = orderSummeryX;

                //    //List<OrderSummerySume> list = new List<OrderSummerySume>();
                //    //list= db.OrderSummerySume.ToList();
                //    //var OrderSummeriesobj = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).ToList();

                //    //var dd = (from obj in OrderSummeriesobj
                //    //          select new
                //    //          {
                //    //              InquiryName = obj.InquiryName,
                //    //              InquiryNameCount = obj.InquiryName.Count()
                //    //          }).ToList();

                //    dt = GetSummerybyInquiryName();
                //    if (dt != null)
                //    {
                //        foreach (DataRow row in dt.Rows)
                //        {
                //            OrderSummerySume orderSummerySume = new OrderSummerySume();
                //            //string name = row["name"].ToString();
                //            //string description = row["description"].ToString();
                //            //string icoFileName = row["iconFile"].ToString();
                //            //string installScript = row["installScript"].ToString();

                //            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                //            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                //            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                //            orderSummerySumeList.Add(orderSummerySume);


                //        }

                //    }
                //    ViewBag.data = orderSummerySumeList;

                //    //IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);

                //    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                //    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                //    return View("OrderSummery", orderSummery);

                //    //return View("OrderSummery", db.OrderSummeries.Where(i => i.InquiryStatus == false).OrderByDescending(k => k.OrderID));
                //}
                //else
                {
                    dt = GetSummerybyInquiryNameCreateUser(id);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.CreatedUserID == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);
                }



            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult IndexUserNextDay(int id)
        {
            ViewBag.Title = "Order Summery Next Day";

            try
            {
                List<OrderSummerySume> orderSummerySumeList = new List<OrderSummerySume>();
                DataTable dt = null;// GetSummerybyInquiryName();
                                    //return View();
                SmartCRM db = new Models.SmartCRM();
                User user = db.Users.Find(id);
                UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);

                if (userGroup.UserGroupName.Equals("Admin"))
                {
                    //HobbyHomeService().FetchLearner();
                    //ICollection<Person> personlist = new HobbyHomeService().FetchPerson(list);
                    //IEnumerable<OrderSummerySume> orderSummeryX = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    //var versions =db.OrderSummeries.Select(v => new { v.InquiryName ,v.InquiryName.Count() });
                    //ViewBag.data = orderSummeryX;

                    //List<OrderSummerySume> list = new List<OrderSummerySume>();
                    //list= db.OrderSummerySume.ToList();
                    //var OrderSummeriesobj = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).ToList();

                    //var dd = (from obj in OrderSummeriesobj
                    //          select new
                    //          {
                    //              InquiryName = obj.InquiryName,
                    //              InquiryNameCount = obj.InquiryName.Count()
                    //          }).ToList();

                    dt = GetSummerybyInquiryName();
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    ViewBag.data = orderSummerySumeList;

                    //IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    DateTime nextday =Convert.ToDateTime( System.DateTime.Now.AddDays(1).ToShortDateString());
                    ViewBag.NextDay = nextday.ToString("dd/MM/yyyy");
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.InquiryStatus == false && i.AllocatedDate.Value.Year == nextday.Year && i.AllocatedDate.Value.Month == nextday.Month && i.AllocatedDate.Value.Day == nextday.Day).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);

                    //return View("OrderSummery", db.OrderSummeries.Where(i => i.InquiryStatus == false).OrderByDescending(k => k.OrderID));
                }
                else
                {
                    dt = GetSummerybyInquiryName(id);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    DateTime nextday = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToShortDateString());
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false && i.AllocatedDate.Value.Year == nextday.Year && i.AllocatedDate.Value.Month == nextday.Month && i.AllocatedDate.Value.Day == nextday.Day).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);
                }



            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }

        public ActionResult IndexUserNextDayCreateUser(int id)
        {
            ViewBag.Title = "Order Summery My Next Day";

            try
            {
                List<OrderSummerySume> orderSummerySumeList = new List<OrderSummerySume>();
                DataTable dt = null;// GetSummerybyInquiryName();
                                    //return View();
                SmartCRM db = new Models.SmartCRM();
                User user = db.Users.Find(id);
                UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);


                {
                    dt = GetSummerybyInquiryNameCreateUser(id);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    DateTime nextday = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToShortDateString());
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.CreatedUserID == id && i.InquiryStatus == false && i.AllocatedDate.Value.Year == nextday.Year && i.AllocatedDate.Value.Month == nextday.Month && i.AllocatedDate.Value.Day == nextday.Day  ).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);
                }



            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }
        
        public ActionResult IndexUserNextDayApointmnet(int id)
        {
            ViewBag.Title = "Order Summery Next Day Appointment";

            try
            {
                List<OrderSummerySume> orderSummerySumeList = new List<OrderSummerySume>();
                DataTable dt = null;// GetSummerybyInquiryName();
                                    //return View();
                SmartCRM db = new Models.SmartCRM();
                User user = db.Users.Find(id);
                UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);

                if (userGroup.UserGroupName.Equals("Admin"))
                {
                    //HobbyHomeService().FetchLearner();
                    //ICollection<Person> personlist = new HobbyHomeService().FetchPerson(list);
                    //IEnumerable<OrderSummerySume> orderSummeryX = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    //var versions =db.OrderSummeries.Select(v => new { v.InquiryName ,v.InquiryName.Count() });
                    //ViewBag.data = orderSummeryX;

                    //List<OrderSummerySume> list = new List<OrderSummerySume>();
                    //list= db.OrderSummerySume.ToList();
                    //var OrderSummeriesobj = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).ToList();

                    //var dd = (from obj in OrderSummeriesobj
                    //          select new
                    //          {
                    //              InquiryName = obj.InquiryName,
                    //              InquiryNameCount = obj.InquiryName.Count()
                    //          }).ToList();

                    dt = GetSummerybyInquiryName();
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    ViewBag.data = orderSummerySumeList;

                    //IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false).OrderByDescending(k => k.OrderID);
                    DateTime nextday = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToShortDateString());
                    ViewBag.NextDay = nextday.ToString("dd/MM/yyyy");
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.InquiryStatus == false && i.NextInqDate.Value.Year == nextday.Year && i.NextInqDate.Value.Month == nextday.Month && i.NextInqDate.Value.Day == nextday.Day).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);

                    //return View("OrderSummery", db.OrderSummeries.Where(i => i.InquiryStatus == false).OrderByDescending(k => k.OrderID));
                }
                else
                {
                    dt = GetSummerybyInquiryName(id);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    DateTime nextday = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToShortDateString());
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.HandledBy == id && i.InquiryStatus == false &&   i.NextInqDate.Value.Year == nextday.Year && i.NextInqDate.Value.Month == nextday.Month && i.NextInqDate.Value.Day == nextday.Day).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);
                }



            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }


        public ActionResult IndexUserNextDayApointmnetCreateUser(int id)
        {
            ViewBag.Title = "Order Summery My Next Day Appointment";

            try
            {
                List<OrderSummerySume> orderSummerySumeList = new List<OrderSummerySume>();
                DataTable dt = null;// GetSummerybyInquiryName();
                                    //return View();
                SmartCRM db = new Models.SmartCRM();
                User user = db.Users.Find(id);
                UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);

 
                {
                    dt = GetSummerybyInquiryNameCreateUser(id);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            OrderSummerySume orderSummerySume = new OrderSummerySume();
                            //string name = row["name"].ToString();
                            //string description = row["description"].ToString();
                            //string icoFileName = row["iconFile"].ToString();
                            //string installScript = row["installScript"].ToString();

                            orderSummerySume.Row = Convert.ToInt16(row["Row"].ToString());
                            orderSummerySume.InquiryName = row["InquiryName"].ToString();
                            orderSummerySume.NoOfJob = row["NoOfJob"].ToString();

                            orderSummerySumeList.Add(orderSummerySume);


                        }

                    }
                    DateTime nextday = Convert.ToDateTime(System.DateTime.Now.AddDays(1).ToShortDateString());
                    IEnumerable<OrderSummery> orderSummery = db.OrderSummeries.Where(i => i.CreatedUserID == id && i.InquiryStatus == false && i.NextInqDate.Value.Year == nextday.Year && i.NextInqDate.Value.Month==nextday.Month && i.NextInqDate.Value.Day==nextday.Day).OrderByDescending(k => k.OrderID);
                    orderSummery.FirstOrDefault().orderSummerySume = orderSummerySumeList;
                    return View("OrderSummery", orderSummery);
                }



            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Dashboard");
            }
        }
    }
}