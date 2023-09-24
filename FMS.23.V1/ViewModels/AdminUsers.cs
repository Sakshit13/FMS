using System;

namespace FMS._23.V1.ViewModels
{
    public class AdminUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DOB { get; set; }
        public bool Active { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Modifieddate { get; set; }
        public string ModifiedBy { get; set; }

        // Add Extra 
        public int Result { get; set; }
    }
}
