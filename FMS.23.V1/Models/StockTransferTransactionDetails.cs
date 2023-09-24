using System;
using System.ComponentModel.DataAnnotations;


namespace FMS._23.V1.Models
{
  
    public class StockTransferTransactionDetails
    {
        public int Id { get; set; }

        [Display(Name = "Stock Transfer By Depot")]
        public int? StockTransferByDepot { get; set; }

        [Display(Name = "Stock Received By Depot")]
        public int? StockReceivedByDepot { get; set; }

        [Display(Name = "Requisition Code")]
        [StringLength(40)]
        public string RequisitionCode { get; set; }

        [Display(Name = "Stock Transfer No")]
        [StringLength(40)]
        public string StockTransferNo { get; set; }

        [Display(Name = "Stock Issue Date")]
        public DateTime? StockIssueDate { get; set; }

        [Display(Name = "Part Number")]
        [StringLength(30)]
        public string PartNumber { get; set; }

        [Display(Name = "Rate")]
        public decimal? Rate { get; set; }

        [Display(Name = "Requested Quantity")]
        public decimal? RequestedQty { get; set; }

        [Display(Name = "Issue Quantity")]
        public decimal? IssueQty { get; set; }

        [Display(Name = "Balance Quantity")]
        public decimal? BalanceQty { get; set; }

        [Display(Name = "Current Stock")]
        public decimal? CurrentStock { get; set; }

        [Display(Name = "CGST")]
        public decimal? cGst { get; set; }

        [Display(Name = "SGST")]
        public decimal? sGst { get; set; }

        [Display(Name = "IGST")]
        public decimal? iGst { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Issued By")]
        [StringLength(20)]
        public string IssuedBy { get; set; }

        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }
    }

}
