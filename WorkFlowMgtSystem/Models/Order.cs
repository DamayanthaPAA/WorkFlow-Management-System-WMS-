//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkFlowMgtSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Inquiries = new HashSet<Inquiry>();
        }
    
        public int OrderID { get; set; }
        public string OrderCode { get; set; }
        public int LocationID { get; set; }
        public int CustomerID { get; set; }
        public System.DateTime RegisteredDate { get; set; }
        public string OrderName { get; set; }
        public int ReferenceUserID { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUserID { get; set; }
        public Nullable<int> ModifiedUserID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int HandledByUserID { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public int InquiryIdx { get; set; }
    }
}
