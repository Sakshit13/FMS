using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FMS._23.V1.Models
{
    public class PoPartsApprovalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string? PurchaseOrderCode { get; set; }

        [Required]
        [MaxLength(255)]
        public string? PartNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

        public int UserId { get; set; }

        public int DepotId { get; set; }
    }

}
