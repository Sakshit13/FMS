
using FMS._23.V1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FMS._23.V1.ViewModels;

namespace FMS._23.V1.DAL
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<VendorDetails> VendorDetails { get; set; }
        public DbSet<JobCardDetail> JobCardDetail { get; set; }
        public DbSet<PoPartsApprovalDetail> PoPartsApprovalDetail { get; set; }
        public DbSet<GrntransactionDetail> GrntransactionDetail { get; set; }

        public DbSet<ItemCategories> ItemCategories { get; set; }

        public DbSet<UnitOfMeasurment> UnitOfMeasurment { get; set; }
        public DbSet<DesignationMaster> DesignationMaster { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<JobCardDetail> JobCardDetails { get; set; }
        public DbSet<POApprovalHead> POApprovalHead { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }

        public DbSet<StockTransferPartRequest> StockTransferPartRequest { get; set; }
        public DbSet<StockTransferTransactionDetails> StockTransferTransactionDetails { get; set; }

        public DbSet<ItemMaster> ItemMaster { get; set; }

        public DbSet<FMS._23.V1.ViewModels.STRequestModel>? STRequestModel { get; set; }

        public DbSet<FMS._23.V1.ViewModels.STRequestDetailsModel>? STRequestDetailsModel { get; set; }

    }
}
