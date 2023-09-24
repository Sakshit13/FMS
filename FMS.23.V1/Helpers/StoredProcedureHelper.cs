namespace FMS._23.V1.Helpers
{
    public class StoredProcedureHelper
    {
        #region Admin Users All Sp
        //public const string sp_GetItemMasterDetails = "[dbo].[sp_GetItemMasterDetails] '{0}','{1}','{2}','{3}','{4}'";
        public const string sp_GetItemMasterDetails = "[dbo].[sp_GetItemMasterDetails]";
        public const string sp_GetPartDetails = "sp_GetPartDetails";
        public const string sp_GetLastPONumber = "sp_GetLastPONumber";
        public const string sp_GetPartDetailsByPartNumAndDepot = "sp_GetPartDetailsByPartNumAndDepot";
        public const string sp_GetLastGRNNumber = "[dbo].[sp_GetLastGRNNumber]";
        public const string sp_VendorNameDetails = "dbo.sp_VendorNameDetails";
        public const string sp_GetPurchaseOrdersByVendorGST = "dbo.sp_GetPurchaseOrdersByVendorGST";
        public const string sp_GetPurchaseOrdersDetails = "dbo.sp_GetAllGRNFields";
        public const string sp_InsertGRNData = "dbo.sp_InsertGRNData";
        #endregion
    }
}
