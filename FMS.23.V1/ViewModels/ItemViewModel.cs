using FMS._23.V1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS._23.V1.ViewModels
{
    public class ItemViewModel
    {
        [Key]
        public int Id { get; set; }
        public string PartNumber { get; set; } = null!;
        public string Description { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? Active { get; set; }
        public string AltPartNum { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("ItemCategories")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        [ForeignKey("UnitOfMeasurment")]
        public int UnitOfMeasurmentId { get; set; }
        public virtual ItemCategories ItemCategories { get; set; }
        public virtual UnitOfMeasurment UnitOfMeasurment { get; set; }

       
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public decimal Igst { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public string  Location { get; set; }
        public string MinimumStock { get; set; }
        public decimal Mrp { get; set; }
        public int DepotId { get; set; }

        public virtual DepotMaster Depot { get; set; }
    }
}
