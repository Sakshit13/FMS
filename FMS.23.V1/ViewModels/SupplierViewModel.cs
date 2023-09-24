using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public int SupplierId { get; set; } 
        public int SupplierName { get; set; } 
    }
}
