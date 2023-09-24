using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.ViewModels
{
    public class STRequestDetailsModel
    {
        [Key]
        public int Id { get; set; } 
        public string RequisitionCode { get; set; }
        public string RequestedDate { get; set; }
        public string RequestedByDepot { get; set; }
        public string PartNumber { get; set; }
        public int RequestQty { get; set; }
        public int StockQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Cgst { get; set; }
        public decimal Igst { get; set; }
        public decimal Discount { get; set; }
    }
}
