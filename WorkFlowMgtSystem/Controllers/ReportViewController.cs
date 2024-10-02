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
    public class ReportViewController : Controller
    {
        // GET: ReportView
        public ActionResult Index(int Id)
        {
            return View();
        }

        public ActionResult IndexMonthAnalizer(int Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndexMonthAnalizer(ReportView NewReportView)
        {
            SmartCRM db = new SmartCRM();



            try
            {
                List<Inquiry> newInquiryList = new List<Inquiry>();
                List<InquiryStatu> newInquiryStatu = new List<InquiryStatu>();

                if (NewReportView.InquiryId==0)
                {
                    newInquiryStatu = db.InquiryStatus.OrderBy(k => k.InquirySequence).ToList();
                }
                else
                {
                    newInquiryStatu = db.InquiryStatus.Where(i => i.InquiryStstusID== NewReportView.InquiryId).OrderBy(k => k.InquirySequence).ToList();
                }

                if (newInquiryStatu == null)
                {
                    return null;
                }
                List<RptSumeryMonth> RptSumeryMonthList = new List<RptSumeryMonth>();
                RptSumeryMonth _RptSumeryMonth = new RptSumeryMonth();

                _RptSumeryMonth.Id = 0;
                _RptSumeryMonth.Defietion = NewReportView.repYear.ToString();
                _RptSumeryMonth.JobType = "Year " + NewReportView.repYear.ToString();
                _RptSumeryMonth.JobJanCount = "Jan";
                _RptSumeryMonth.JobFebCount = "Feb";
                _RptSumeryMonth.JobMarCount = "Mar";
                _RptSumeryMonth.JobAprCount = "Apr";
                _RptSumeryMonth.JobMayCount = "May";
                _RptSumeryMonth.JobJuneCount = "Jun";
                _RptSumeryMonth.JobJuyCount = "Jul";
                _RptSumeryMonth.JobAugCount = "Aug";
                _RptSumeryMonth.JobSepCount = "Sep";
                _RptSumeryMonth.JobOctCount = "Oct";
                _RptSumeryMonth.JobNovCount = "Nov";
                _RptSumeryMonth.JobDecCount = "Dec";
                _RptSumeryMonth.JobSumCount = "Total";

                RptSumeryMonthList.Add(_RptSumeryMonth);

                string JobJanCountx = "0";
                string JobFebCountx = "0";
                string JobMarCountx = "0";
                string JobAprCountx = "0";
                string JobMayCountx = "0";
                string JobJuneCountx = "0";
                string JobJuyCountx = "0";
                string JobAugCountx = "0";
                string JobSepCountx = "0";
                string JobOctCountx = "0";
                string JobNovCountx = "0";
                string JobDecCountx = "0";
                string JobSumCountx = "0";

                foreach (InquiryStatu item in newInquiryStatu)
                {
                    RptSumeryMonth _RptSumeryMonthItem=new RptSumeryMonth();


                    string JobJanCount = "";
                    string JobFebCount = "";
                    string JobMarCount = "";
                    string JobAprCount = "";
                    string JobMayCount = "";
                    string JobJuneCount = "";
                    string JobJuyCount = "";
                    string JobAugCount = "";
                    string JobSepCount = "";
                    string JobOctCount = "";
                    string JobNovCount = "";
                    string JobDecCount = "";
                    string JobSumCount = "";


                    


                    for (int i = 1; i <= 12; i++)
                    {
                        _RptSumeryMonthItem = new RptSumeryMonth();

                        _RptSumeryMonthItem.Id = i;
                        _RptSumeryMonthItem.Defietion = item.InquiryName;
                        _RptSumeryMonthItem.JobType = item.InquiryName;
                        

                        ReportView xReportView = new ReportView();
                        xReportView.LocationId = NewReportView.LocationId;
                        xReportView.InquiryId = item.InquiryStstusID;
                        xReportView.AddUserId = NewReportView.UserId;
                        xReportView.HandalById = NewReportView.HandalById;
                        xReportView.UserId = NewReportView.UserId;
                        xReportView.repYear = NewReportView.repYear;
                        
                        //string refBegin = "";
                        //string refEnd = "";

                        var firstDayOfMonth = new DateTime(NewReportView.repYear, i, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                        //refBegin =String.Format("00", (firstDayOfMonth));
                        //refBegin = refBegin + "/" + String.Format("00", (i));
                        //refBegin = refBegin + "/" + String.Format("00", (NewReportView.repYear));


                        //refEnd = String.Format("00", (lastDayOfMonth));
                        //refEnd = refEnd + "/" + String.Format("00", (i));
                        //refEnd = refEnd + "/" + String.Format("00", (NewReportView.repYear));

                        xReportView.FromDate = firstDayOfMonth;// Convert.ToDateTime(String.Format("dd/MM/yyyy", firstDayOfMonth));
                        xReportView.ToDate = lastDayOfMonth;// Convert.ToDateTime(String.Format("dd/MM/yyyy", lastDayOfMonth));


                        newInquiryList = getReportOderList(xReportView);
                        if (string.IsNullOrWhiteSpace(JobSumCount))
                        {
                            JobSumCount = "0";
                        }
                        JobSumCount=(Convert.ToInt32(JobSumCount) + newInquiryList.Count ).ToString();
                        switch (i)
                        {
                            case 1:
                                JobJanCount = newInquiryList.Count.ToString();
                                break;
                            case 2:
                                JobFebCount = newInquiryList.Count.ToString();
                                break;
                            case 3:
                                JobMarCount = newInquiryList.Count.ToString();
                                break;
                            case 4:
                                JobAprCount = newInquiryList.Count.ToString();
                                break;
                            case 5:
                                JobMayCount = newInquiryList.Count.ToString();
                                break;
                            case 6:
                                JobJuneCount = newInquiryList.Count.ToString();
                                break;
                            case 7:
                                JobJuyCount = newInquiryList.Count.ToString();
                                break;
                            case 8:
                                JobAugCount = newInquiryList.Count.ToString();
                                break;
                            case 9:
                                JobSepCount = newInquiryList.Count.ToString();
                                break;
                            case 10:
                                JobOctCount = newInquiryList.Count.ToString();
                                break;
                            case 11:
                                JobNovCount = newInquiryList.Count.ToString();
                                break;
                            case 12:
                                JobDecCount = newInquiryList.Count.ToString();
                                break;
                            default:
                                Console.WriteLine("Default case");
                                break;
                        }

                    }



                    _RptSumeryMonthItem.JobJanCount = JobJanCount;
                    _RptSumeryMonthItem.JobFebCount = JobFebCount;
                    _RptSumeryMonthItem.JobMarCount = JobMarCount;
                    _RptSumeryMonthItem.JobAprCount = JobAprCount;
                    _RptSumeryMonthItem.JobMayCount = JobMayCount;
                    _RptSumeryMonthItem.JobJuneCount = JobJuneCount;
                    _RptSumeryMonthItem.JobJuyCount = JobJuyCount;
                    _RptSumeryMonthItem.JobAugCount = JobAugCount;
                    _RptSumeryMonthItem.JobSepCount = JobSepCount;
                    _RptSumeryMonthItem.JobOctCount = JobOctCount;
                    _RptSumeryMonthItem.JobNovCount = JobNovCount;
                    _RptSumeryMonthItem.JobDecCount = JobDecCount;
                    _RptSumeryMonthItem.JobSumCount = JobSumCount;


                    JobJanCountx =(Convert.ToInt32(JobJanCountx)+ Convert.ToInt32(JobJanCount)).ToString();
                    JobFebCountx = (Convert.ToInt32(JobFebCountx) + Convert.ToInt32(JobFebCount)).ToString();
                    JobMarCountx = (Convert.ToInt32(JobMarCountx) + Convert.ToInt32(JobMarCount)).ToString();
                    JobAprCountx = (Convert.ToInt32(JobAprCountx) + Convert.ToInt32(JobAprCount)).ToString();
                    JobMayCountx = (Convert.ToInt32(JobMayCountx) + Convert.ToInt32(JobMayCount)).ToString();
                    JobJuneCountx = (Convert.ToInt32(JobJuneCountx) + Convert.ToInt32(JobJuneCount)).ToString();
                    JobJuyCountx = (Convert.ToInt32(JobJuyCountx) + Convert.ToInt32(JobJuyCount)).ToString();
                    JobAugCountx = (Convert.ToInt32(JobAugCountx) + Convert.ToInt32(JobAugCount)).ToString();
                    JobSepCountx = (Convert.ToInt32(JobSepCountx) + Convert.ToInt32(JobSepCount)).ToString();
                    JobOctCountx = (Convert.ToInt32(JobOctCountx) + Convert.ToInt32(JobOctCount)).ToString();
                    JobNovCountx = (Convert.ToInt32(JobNovCountx) + Convert.ToInt32(JobNovCount)).ToString();
                    JobDecCountx = (Convert.ToInt32(JobDecCountx) + Convert.ToInt32(JobDecCount)).ToString();
                    JobSumCountx = (Convert.ToInt32(JobSumCountx) + Convert.ToInt32(JobSumCount)).ToString();



                    RptSumeryMonthList.Add(_RptSumeryMonthItem);
                }

                RptSumeryMonth _RptSumeryMonthSum = new RptSumeryMonth();

                _RptSumeryMonthSum.Id = 1000;// newInquiryStatu.Count+1;
                _RptSumeryMonthSum.Defietion = "Total";
                _RptSumeryMonthSum.JobType = "Total";
                _RptSumeryMonthSum.JobJanCount = JobJanCountx;
                _RptSumeryMonthSum.JobFebCount = JobFebCountx;
                _RptSumeryMonthSum.JobMarCount = JobMarCountx;
                _RptSumeryMonthSum.JobAprCount = JobAprCountx;
                _RptSumeryMonthSum.JobMayCount = JobMayCountx;
                _RptSumeryMonthSum.JobJuneCount = JobJuneCountx;
                _RptSumeryMonthSum.JobJuyCount = JobJuyCountx;
                _RptSumeryMonthSum.JobAugCount = JobAugCountx;
                _RptSumeryMonthSum.JobSepCount = JobSepCountx;
                _RptSumeryMonthSum.JobOctCount = JobOctCountx;
                _RptSumeryMonthSum.JobNovCount = JobNovCountx;
                _RptSumeryMonthSum.JobDecCount =  JobDecCountx;
                _RptSumeryMonthSum.JobSumCount =  JobSumCountx;


                 

                RptSumeryMonthList.Add(_RptSumeryMonthSum);





                string sqlString = "Inquiry Summery " + NewReportView.repYear.ToString() + "\n";


                if (NewReportView.InquiryId != 0)
                {
                    // sqlString= sqlString + " Inquiry Type " + db.InquiryStatus.Find(NewReportView.InquiryId).InquiryName.ToString();

                }

                if (NewReportView.HandalById != 0)
                {

                    sqlString = sqlString + " HandledBy " + db.Users.Find(NewReportView.HandalById).UserName.ToString();

                }

                if (NewReportView.LocationId != 0)
                {

                    sqlString = sqlString + " Location " + db.Locations.Find(NewReportView.LocationId).LocationName.ToString();
                }

                RptSumeryMonthList.First().Defietionx = sqlString;

                return View("IndexMonthAnalizerReport", RptSumeryMonthList);
            }
            catch (Exception)
            {

                return null;
            }
        }
        [HttpPost]
        public ActionResult Index(ReportView NewReportView)
        {
            SmartCRM db = new SmartCRM();



            try
            {
                List<Inquiry> newInquiryList = new List<Inquiry>();

                newInquiryList = getReportOderList(NewReportView);

                List<RptSumery1> newRptSumeryList1 = new List<RptSumery1>();



                var Query = from p in newInquiryList.GroupBy(p => p.InquiryName)
                            select new
                            {
                                count = p.Count(),
                                p.First().InquiryName,
                            };
                foreach (var item in Query)
                {
                    RptSumery1 newRptSumery1 = new RptSumery1();
                    newRptSumery1.Defietion = "";
                    newRptSumery1.JobCount = item.count;
                    newRptSumery1.JobType = item.InquiryName;
                    newRptSumeryList1.Add(newRptSumery1);

                }

                var QueryShowRoom = from p in newInquiryList.GroupBy(p => new { p.Order.LocationID, p.InquiryName })
                                    select new
                                    {
                                        count = p.Count(),
                                        p.First().InquiryName,
                                        p.First().Order.LocationID
                                    };

                List<RptSumery2> newRptSumeryList2 = new List<RptSumery2>();
                foreach (var item in QueryShowRoom)
                {
                    RptSumery2 newRptSumery2 = new RptSumery2();

                    newRptSumery2.JobCountShowRoom = item.count;
                    newRptSumery2.JobTypeShowRoom = item.InquiryName;
                    newRptSumery2.ShowRoom = db.Locations.Find(item.LocationID).LocationName;
                    newRptSumeryList2.Add(newRptSumery2);

                }

                newRptSumeryList1.First()._RptSumery2 = newRptSumeryList2;


                string sqlString = "Inquiry Summery " + NewReportView.FromDate.ToShortDateString() + " to " +
                    NewReportView.ToDate.ToShortDateString() + "\n";


                if (NewReportView.InquiryId != 0)
                {
                    // sqlString= sqlString + " Inquiry Type " + db.InquiryStatus.Find(NewReportView.InquiryId).InquiryName.ToString();

                }

                if (NewReportView.HandalById != 0)
                {

                    sqlString = sqlString + " HandledBy " + db.Users.Find(NewReportView.HandalById).UserName.ToString();

                }

                if (NewReportView.LocationId != 0)
                {

                    sqlString = sqlString + " Location " + db.Locations.Find(NewReportView.LocationId).LocationName.ToString();
                }

                newRptSumeryList1.First().Defietion = sqlString;

                return View("IndexReport", newRptSumeryList1);
            }
            catch (Exception)
            {

                return null;
            }
        }

        private List<Inquiry> getReportOderList(ReportView NewReportView)
        {

            List<Order> newOrderList = new List<Order>();
            List<Inquiry> newInquiryList = new List<Inquiry>();
            var db = new SmartCRM();
            var conn = db.Database.Connection;
            var ConnnectionState = conn.State;
            string sqlString = "";
            DataTable dt = new DataTable();



            User user = db.Users.Find(NewReportView.UserId);
            UserGroup userGroup = db.UserGroups.Find(user.UserGroupID);

            if (!userGroup.UserGroupName.Equals("Admin"))
            {
                NewReportView.HandalById = NewReportView.UserId;
            }
            else
            {
                if (NewReportView.HandalById !=0)
                {
                    NewReportView.HandalById = NewReportView.HandalById;
                }
                
            }
            try
            {
                sqlString = " SELECT Inquiry.InquiryID, Inquiry.OrderID, Inquiry.InquiryStatusID, Inquiry.HandledBy, Inquiry.AllocatedDate," +
                    "Inquiry.Telephone, Inquiry.Remark, Inquiry.NextInqDate, Inquiry.AddedDate, Inquiry.ModifiedUserID, " +
                    " Inquiry.ModifiedDate, Inquiry.CreatedDate, Inquiry.CreatedUserID, Inquiry.InquiryName, [Order].LocationID " +
                    " FROM            Inquiry INNER JOIN [Order] ON Inquiry.OrderID = [Order].OrderID " +
                    " WHERE(Inquiry.AllocatedDate BETWEEN @FromDate AND @ToDate) ";




                if (NewReportView.InquiryId != 0)
                {
                    sqlString = sqlString + " AND(Inquiry.InquiryStatusID = @InquiryStatusID)";

                }

                if (NewReportView.HandalById != 0)
                {
                    sqlString = sqlString + " AND(Inquiry.HandledBy = @HandledBy) ";

                }

                if (NewReportView.LocationId != 0)
                {
                    sqlString = sqlString + " AND  ([Order].LocationID = @LocationID) ";

                }

                sqlString = sqlString + " ORDER BY Inquiry.OrderID, Inquiry.InquiryID DESC ";



                using (db)
                {
                    if (ConnnectionState != ConnectionState.Open) { conn.Open(); }
                    ConnnectionState = conn.State;
                    using (System.Data.SqlClient.SqlCommand cmd = (System.Data.SqlClient.SqlCommand)conn.CreateCommand())
                    {

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlString;

                        cmd.Parameters.AddWithValue("FromDate", NewReportView.FromDate.ToShortDateString());
                        cmd.Parameters.AddWithValue("ToDate", NewReportView.ToDate.ToShortDateString());
                        if (NewReportView.InquiryId != 0) cmd.Parameters.AddWithValue("InquiryStatusID", NewReportView.InquiryId);
                        if (NewReportView.HandalById != 0) cmd.Parameters.AddWithValue("HandledBy", NewReportView.HandalById);
                        if (NewReportView.LocationId != 0) cmd.Parameters.AddWithValue("LocationID", NewReportView.LocationId);


                        using (var red = cmd.ExecuteReader())
                        {
                            dt.Load(red);
                        }
                        int NewOrderID = 0;
                        foreach (DataRow row in dt.Rows)
                        {



                            Inquiry xNew = new Inquiry();
                            xNew.InquiryID = Convert.ToInt32(row["InquiryID"]);
                            xNew.OrderID = Convert.ToInt32(row["OrderID"]);
                            xNew.InquiryStatusID = Convert.ToInt32(row["InquiryStatusID"]);
                            xNew.HandledBy = Convert.ToInt32(row["HandledBy"]);
                            xNew.AllocatedDate = Convert.ToDateTime(row["AllocatedDate"]);
                            xNew.Telephone = Convert.ToString(row["Telephone"]);
                            xNew.Remark = Convert.ToString(row["Remark"]);
                            xNew.AddedDate = Convert.ToDateTime(row["AddedDate"]);
                            try
                            {
                                xNew.NextInqDate = Convert.ToDateTime(row["NextInqDate"]);
                            }
                            catch (Exception)
                            {

                                xNew.NextInqDate = null;
                            }

                            try
                            {
                                xNew.ModifiedUserID = Convert.ToInt32(row["ModifiedUserID"]);
                            }
                            catch (Exception)
                            {

                                xNew.ModifiedUserID = null;
                            }
                            try
                            {
                                xNew.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                            }
                            catch (Exception)
                            {

                                xNew.ModifiedDate = null;
                            }
                            xNew.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                            xNew.CreatedUserID = Convert.ToInt32(row["CreatedUserID"]);
                            xNew.InquiryName = Convert.ToString(row["InquiryName"]);


                            if (NewOrderID != xNew.OrderID)
                                NewOrderID = 0;
                            Order xNewOrder = new Order();
                            if (NewOrderID == 0)
                            {


                                xNewOrder = db.Orders.Where(k => k.OrderID == xNew.OrderID).FirstOrDefault();
                                xNew.Order = xNewOrder;
                                newInquiryList.Add(xNew);
                            }
                            NewOrderID = xNew.OrderID;


                        }

                    }
                }
                return newInquiryList;

            }
            catch (Exception e)
            {

                return null;
            }
            finally
            {
                if (ConnnectionState == ConnectionState.Open) { conn.Close(); }
            }
        }



    }


}
