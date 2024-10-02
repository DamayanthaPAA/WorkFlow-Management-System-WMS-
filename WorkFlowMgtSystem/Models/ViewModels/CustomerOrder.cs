namespace WorkFlowMgtSystem.Models.ViewModels
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class CustomerOrder : DbContext
    {
        // Your context has been configured to use a 'CustomerOrder' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WorkFlowMgtSystem.Models.ViewModels.CustomerOrder' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CustomerOrder' 
        // connection string in the application configuration file.
        //public CustomerOrder()
        //    : base("name=CustomerOrder")
        //{
        //}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerOrder()
        {
            this.Orders = new HashSet<Order>();
        }

        public int CustomerID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "customer group required!")]
        public int CustomerGroupID { get; set; }

        [Required(ErrorMessage = "customer code required!")]
        public string CustomerCode { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "customer origine required!")]
        public int CustomerOrigin { get; set; }
        public string CustomerTitle { get; set; }

        [Required(ErrorMessage = "customer name required!")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "customer address required!")]
        public string CustomerAddress { get; set; }

        public string CustomerTelephone01 { get; set; }
        public string CustomerTelephone02 { get; set; }

        [Required(ErrorMessage = "customer mobile required!")]
        public string CustomerMobile { get; set; }

        [Required(ErrorMessage = "customer email required!")]
        public string CustomerEmail { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public int CreatedUserID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedUserID { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual CustomersGroup CustomersGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public int OrderID { get; set; }
        public string OrderCode { get; set; }
        public int LocationID { get; set; }
       
        public System.DateTime RegisteredDate { get; set; }
        public string OrderName { get; set; }
        public int ReferenceUserID { get; set; }
        public string OrderRemark { get; set; }
       

        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}