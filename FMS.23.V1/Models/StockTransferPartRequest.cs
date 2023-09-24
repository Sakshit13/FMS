using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS._23.V1.Models
{
    public class StockTransferPartRequest
    {
        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        public string RequisitionCode { get; set; }

        [StringLength(30)]
        public string PartNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal RequestQty { get; set; }

        [StringLength(80)]
        public string? Remarks { get; set; }

     
        public int? RequestedByDepot { get; set; }
        [ForeignKey("DepotMaster")]

        public int? RequestedToDepot { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RequestedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [StringLength(20)]
        public string? CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [StringLength(20)]
        public string? ModifiedBy { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual DepotMaster DepotMaster { get; set; }
    }

}
