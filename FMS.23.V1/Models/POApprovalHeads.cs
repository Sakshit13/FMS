using System;
using System.ComponentModel.DataAnnotations;

public class POApprovalHead
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Rank { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int DepotId { get; set; }
}
