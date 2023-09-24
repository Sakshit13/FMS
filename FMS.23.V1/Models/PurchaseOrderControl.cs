using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.Models
{
    public partial class PurchaseOrderControl
    { 
        [Key]
        public int Id { get; set; }
        public int DepotId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string? VendorGstnumber { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
