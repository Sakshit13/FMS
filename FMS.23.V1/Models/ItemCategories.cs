using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.Models
{
    public partial class ItemCategories
    {
      
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public bool? Active { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
