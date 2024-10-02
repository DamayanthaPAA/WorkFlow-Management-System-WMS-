using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WorkFlowMgtSystem.Models.ViewModels
{
  public  class InquiryFileViewModel
    {
        public int InquiryFileID { get; set; }
        public int InquiryID { get; set; }
        [DisplayName("Upload File")]
        public string ImageName { get; set; }
        
        public string InquiryFileImage { get; set; }

        public HttpPostedFileBase imageFileName { get; set; }
        
        public int InquiryIdx { get; set; }
    }
}
