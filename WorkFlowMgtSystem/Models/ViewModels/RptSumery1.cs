using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowMgtSystem.Models.ViewModels
{
    public partial class RptSumery1 
    {
        public string Defietion { get; set; }
        public string JobType { get; set; }
        public int JobCount { get; set; }
        public List<RptSumery2> _RptSumery2 = new List<RptSumery2>();
    }

    public partial class RptSumery2
    {
        public string ShowRoom { get; set; }
        public string JobTypeShowRoom { get; set; }
        public int JobCountShowRoom { get; set; }
    }
    public partial class RptSumeryMonth
    {
        public int Id { get; set; }
        public string Defietionx { get; set; }
        public string Defietion { get; set; }
        public string JobType { get; set; }
        public string JobJanCount { get; set; }
        public string JobFebCount { get; set; }
        public string JobMarCount { get; set; }
        public string JobAprCount { get; set; }
        public string JobMayCount { get; set; }
        public string JobJuneCount { get; set; }
        public string JobJuyCount { get; set; }
        public string JobAugCount { get; set; }
        public string JobSepCount { get; set; }
        public string JobOctCount { get; set; }
        public string JobNovCount { get; set; }
        public string JobDecCount { get; set; }
        public string JobSumCount { get; set; }
         
    }

}
