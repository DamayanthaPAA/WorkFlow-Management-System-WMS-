using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowMgtSystem.Models.ViewModels
{
  public partial  class ReportView
    {
        public int LocationId { get; set; }
        public int InquiryId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int AddUserId { get; set; }
        public int HandalById { get; set; }
        public int UserId { get; set; }

        public int repYear { get; set; }

    }
}
