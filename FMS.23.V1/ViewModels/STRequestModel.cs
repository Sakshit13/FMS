using System.ComponentModel.DataAnnotations;

namespace FMS._23.V1.ViewModels
{
    public class STRequestModel
    {
        [Key]
        public int Id { get; set; }
        public string RequestedByDepot { get; set; }
        public string RequestedToDepot { get; set; }
        public string RequisitionCode { get; set; }
        public string Status { get; set; }

    }

}
