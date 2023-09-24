using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS._23.V1.Models
{
    public class ItemDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string PartNumber { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Rate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Discount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Igst { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Cgst { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Sgst { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal MinimumStock { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal MRP { get; set; }

        [Required]
        public int DepotId { get; set; }

        // Navigation property to the related DepotMaster (assuming you have such a class)

        public DepotMaster Depot { get; set; }
    }

}
