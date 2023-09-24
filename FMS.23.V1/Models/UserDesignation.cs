using System;
using System.Collections.Generic;

namespace FMS._23.V1.Models
{
    public partial class UserDesignation
    {
        public int Id { get; set; }
        public string? Designation { get; set; }
        public bool? Active { get; set; }
    }
}
