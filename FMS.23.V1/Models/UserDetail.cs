using System;
using System.Collections.Generic;

namespace FMS._23.V1.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? DepotId { get; set; }
        public bool? Active { get; set; }
        public bool? IsLocked { get; set; }

        public virtual DepotMaster? Depot { get; set; }
    }
}
