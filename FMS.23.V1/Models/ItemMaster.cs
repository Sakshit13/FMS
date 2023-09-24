using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS._23.V1.Models
{
    public partial class ItemMaster
    {
        [Key]
        public int Id { get; set; }
        public string PartNumber { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? Active { get; set; }
        public string? AltPartNum { get; set; }
        public string? Remarks { get; set; }
        [ForeignKey("ItemCategories")]
        public int? CategoryId { get; set; }
        [ForeignKey("UnitOfMeasurment")]
        public int? UnitOfMeasurmentId { get; set; }
        public virtual ItemCategories? ItemCategories { get; set; }
        public virtual UnitOfMeasurment? UnitOfMeasurment { get; set; }
    }
}
