using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS._23.V1.Models
{
    public class VendorDetails
    {
        [Key]
        public int VendorId { get; set; }
        public string? VendorName { get; set; }
        public string? VendorPhone { get; set; }
        public string? VendorEmail { get; set; }
        public string? VendorWebsite { get; set; }
        public string? VendorAddress { get; set; }
        public string? Gstnumber { get; set; }
        public bool? Active { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
