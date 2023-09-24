using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.Models
{
    public partial class Grncontrol
    {
        [Key]
        public int Grnid { get; set; }
        public string? DepotCode { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string? Grnnumber { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? ChalanNumber { get; set; }
        public DateTime? ChalanDate { get; set; }
        public DateTime? DateOfReciept { get; set; }
        public string? VendorGstnumber { get; set; }
        public string? Remarks { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
