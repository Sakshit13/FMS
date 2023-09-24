using System;
using System.ComponentModel.DataAnnotations;

public class JobCardDetail
{
    public int Id { get; set; }

    [Required(ErrorMessage = "JobCardCode is required.")]
    [StringLength(255)]
    public string JobCardCode { get; set; }

    [Required(ErrorMessage = "VehicleCode is required.")]
    [StringLength(255)]
    public string VehicleCode { get; set; }

    public int MeterReading { get; set; }

    [Required(ErrorMessage = "RequestDate is required.")]
    public DateTime RequestDate { get; set; }

    [Required(ErrorMessage = "ServiceEngineerCode is required.")]
    [StringLength(255)]
    public string ServiceEngineerCode { get; set; }

    public int ReasonId { get; set; }

    [StringLength(500)]
    public string Complaint { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string ModifiedBy { get; set; }

    [StringLength(500)]
    public string Remarks { get; set; }

    public int DockingKm { get; set; }

    [StringLength(255)]
    public string TechnicianCode { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int DepotId { get; set; }
}
