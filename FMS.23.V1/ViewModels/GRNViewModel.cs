using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace FMS._23.V1.ViewModels
{
    public class GRNViewModel
    {
        [Key]

        public int Grntid { get; set; }
        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please select a supplier.")]
        public List<SupplierViewModel>? Supplier { get; set; }
        [Display(Name = "Purchase Order No")]
        public List<PurchaseOrderViewModel>? PurchaseOrderNo { get; set; }
        [Display(Name = "GRN NO")]
        public string? GRNNO { get; set; }
        [Display(Name = "Challan No")]
        public string? ChallanNo { get; set; }
        [Display(Name = "Bill NO")]
        public string? BillNO { get; set; } 
        [Display(Name = "Date Of Recipt")]
        public DateTime DateOfRecipt { get; set; }
        [Display(Name = "Bill Date/Challan Date")]
        public DateTime DateOfBill { get; set; } 
    }

    public class GRNFieldModel
    {
        public string? PartNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? POQuantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? RecievedQty { get; set; }
        public decimal? AcceptedQty { get; set; }
        public decimal? RejectedQty { get; set; }
        public decimal? BalanceQty { get; set; }
        public decimal? Cgst { get; set; }
        public decimal? Igst { get; set; }
        public decimal? Tcs { get; set; }
        public decimal? Sgst { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TaxAmount { get; set; }
        public string? PartName { get; set; }
    }

}
