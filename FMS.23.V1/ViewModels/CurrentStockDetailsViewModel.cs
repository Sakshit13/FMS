namespace FMS._23.V1.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CurrentStockDetails
    {
        [Key]
        public int Id { get; set; }

        public int? DepotId { get; set; }

        [MaxLength(30)]
        public string PartNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? StockQuantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:N3}")]
        public decimal? Rate { get; set; }

        public DateTime? CreatedOn { get; set; }

        [MaxLength(20)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [MaxLength(20)]
        public string ModifiedBy { get; set; }

        [MaxLength(80)]
        public string Remark { get; set; }
    }


}
