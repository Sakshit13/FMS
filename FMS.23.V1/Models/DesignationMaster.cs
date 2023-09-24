namespace FMS._23.V1.Models
{
    public class DesignationMaster
    {
        public int Id { get; set; }
        public string? DesignationName { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public int DepotId { get; set; }
    }

}
