using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.ViewModels
{
    public class PurchaseOrderViewModel
    {
        [Key]
        public int Id { get; set; }

        public int DepotId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string? VendorGstnumber { get; set; }


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
        public decimal? Active { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? Remarks { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
