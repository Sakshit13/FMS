using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.Models
{
    public partial class UnitOfMeasurment
    {
        [Key]       
        public int Id { get; set; }
        public string? UnitName { get; set; }
        public string? UnitShortName { get; set; }
        public string? UnitOf { get; set; }
        public bool? Active { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ItemMaster> ItemMasters { get; set; }
    }
}
