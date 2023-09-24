using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.Models
{
    public partial class PotransactionDetail
    {
        [Key]
        public int Id { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string? PartNumber { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Tcs { get; set; }
        public decimal? Cartage { get; set; }
        public decimal? FreightAmount { get; set; }
        public decimal? CurrentStock { get; set; }
        public bool? Active { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }

	public class PurchaseOrderDetailsResponse
	{
        [Key]
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int TotalCount { get; set; }
        public string PurchaseOrderNumber { get; set; }
		public string GrnNumber { get; set; }
		public DateTime OrderDate { get; set; }
		public string VendorName { get; set; }
	}
}
