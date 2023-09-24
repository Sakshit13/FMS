using System;
using System.ComponentModel.DataAnnotations;
namespace FMS._23.V1.Models
{
    public class VehicleDetails
    {
        public int Id { get; set; }

      
        [StringLength(255)]
        public string VehicleCode { get; set; }

        [StringLength(255)]
        public string? RegistrationNumber { get; set; }

     
        public string ChasisNumber { get; set; }

        public string EngineNumber { get; set; }

       
        public DateTime RegistrationDate { get; set; }

        [StringLength(50)]
        public string Make { get; set; }

        public int ModelID { get; set; }


        public DateTime CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        
        public DateTime ModifiedDate { get; set; }

     
        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public int FreeServiceCount { get; set; }
        public int SeatCapacity { get; set; }
        public int DockingCount { get; set; }

       
        public bool IsDeleted { get; set; }

     
        public int DepotId { get; set; }
    }

}
