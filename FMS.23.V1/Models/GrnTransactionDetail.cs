using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FMS._23.V1.Models
{
    public partial class GrntransactionDetail
    {
		[Key]
		public int Grntid { get; set; }

		[Display(Name = "GRN Number")]
		public string? Grnnumber { get; set; }

		[Display(Name = "Part Number")]
		public string? PartNumber { get; set; }

		[Display(Name = "Received Quantity")]
		public decimal? RecievedQty { get; set; }

		[Display(Name = "Accepted Quantity")]
		public decimal? AcceptedQty { get; set; }

		[Display(Name = "Rejected Quantity")]
		public decimal? RejectedQty { get; set; }

		[Display(Name = "Unit Price")]
		public decimal? UnitPrice { get; set; }

		[Display(Name = "Tax")]
		public decimal? Tax { get; set; }

		[Display(Name = "Discount")]
		public decimal? Discount { get; set; }

		[Display(Name = "CGST")]
		public decimal? Cgst { get; set; }

		[Display(Name = "SGST")]
		public decimal? Sgst { get; set; }

		[Display(Name = "IGST")]
		public decimal? Igst { get; set; }

		[Display(Name = "TCS")]
		public decimal? Tcs { get; set; }

		[Display(Name = "Cartage")]
		public decimal? Cartage { get; set; }

		[Display(Name = "Freight Amount")]
		public decimal? FreightAmount { get; set; }

		[Display(Name = "Opening Stock")]
		public decimal? OpeningStock { get; set; }

		[Display(Name = "Closing Stock")]
		public decimal? ClosingStock { get; set; }

		[Display(Name = "Active")]
		public bool Active { get; set; }

		[Display(Name = "Is Deleted")]
		public bool IsDeleted { get; set; }

		[Display(Name = "Remarks")]
		public decimal? Remarks { get; set; }

		[Display(Name = "Created On")]
		public DateTime? CreatedOn { get; set; }

		[Display(Name = "Created By")]
		public string? CreatedBy { get; set; }

		[Display(Name = "Modified On")]
		public DateTime? ModifiedOn { get; set; }

		[Display(Name = "Modified By")]
		public string? ModifiedBy { get; set; }

		[Display(Name = "Upload Invoice")]
		public byte[]? UploadInvoice { get; set; }
	}
}
