using System;
using System.Collections.Generic;

namespace FMS._23.V1.Models
{
    public partial class DepotMaster
    {
        public DepotMaster()
        {
            ItemDeatils = new HashSet<ItemDetails>();
            UserDetails = new HashSet<UserDetail>();
            UserRegistrations = new HashSet<UserRegistration>();
        }

        public int Id { get; set; }
        public string? DepotName { get; set; }
        public string? DepotCode { get; set; }
        public string? DbName { get; set; }
        public string? Location { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<ItemDetails> ItemDeatils { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserRegistration> UserRegistrations { get; set; }
    }
}
