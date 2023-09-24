using System;
using System.ComponentModel.DataAnnotations;

public class EmployeeDetails
{
    [Key]
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeCode { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int RoleId { get; set; }
    public int DepotId { get; set; }


}
