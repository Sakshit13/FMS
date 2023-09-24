using System;
using System.Collections.Generic;

namespace FMS._23.V1.Models
{
    public partial class UserRegistration
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? DepotId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? RoleId { get; set; }
        public int? DesiId { get; set; }
        public bool? Approver { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public int? LoginId { get; set; }

        public virtual DepotMaster? Depot { get; set; }
    }
}
