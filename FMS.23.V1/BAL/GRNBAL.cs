
using FMS._23.V1.Helpers;
using FMS._23.V1.Models;
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace FMS._23.V1.BAL
{
    public class GRNBAL
    {
        private readonly IConfiguration _configuration;

        private string ConnectionStrng;
        public GRNBAL(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionStrng = _configuration.GetConnectionString("Conn");
        }

        public string GetGRNNO()
        {
            var GrnNumber = "";
            using (SqlConnection connection = new SqlConnection(ConnectionStrng))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_GetLastGRNNumber, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Execute the stored procedure and retrieve the result
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int CurrentYear = Convert.ToInt32(DateTime.Now.ToString("yy"));
                            int PreviousYear = Convert.ToInt32(DateTime.Now.ToString("yy")) - 1;
                            int NextYear = Convert.ToInt32(DateTime.Now.ToString("yy")) + 1;
                            string PreYear = PreviousYear.ToString();
                            string NexYear = NextYear.ToString();
                            string CurYear = CurrentYear.ToString();
                            string FinYear = null;
                            if (DateTime.Today.Month > 3)
                                FinYear = CurYear + "-" + NexYear;
                            else
                                FinYear = PreYear + "-" + CurYear;
                            int POMAXID = (Convert.ToInt32(reader["LASTGrnNumber"]) + 1);
                            string YEAR = DateTime.Now.ToString("yy");
                            string YEAR1 = DateTime.Now.AddYears(1).ToString("yy");
                            string GrnNO = "GRN/" + POMAXID.ToString("0000") + "/" + FinYear;
                            GrnNumber = GrnNO;
                        }
                    }
                }
                return GrnNumber;
            }
        }

        public List<SelectListItem> GetVendorList()
        {
            List<SelectListItem> vendorList = new List<SelectListItem>();

            using (SqlConnection connection = new SqlConnection(ConnectionStrng))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_VendorNameDetails, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Execute the stored procedure and retrieve the result
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assuming your data is in the format "VendorName Gstnumber"
                            string vendorName = Convert.ToString(reader["VendorName"]);
                            string gstNumber = Convert.ToString(reader["Gstnumber"]);
                            string formattedItem = $"{vendorName}, {gstNumber}";

                            vendorList.Add(new SelectListItem { Value = gstNumber, Text = formattedItem });
                        }
                    }
                }
            }

            return vendorList;
        }


        public List<SelectListItem> GetAllPendingPOList(string VendorCode)
        {
            List<SelectListItem> PONumber = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(VendorCode))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrng))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_GetPurchaseOrdersByVendorGST, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter parameter = new SqlParameter("@VendorGstnumber", SqlDbType.VarChar);
                        parameter.Value = VendorCode;
                        command.Parameters.Add(parameter);

                        // Execute the stored procedure and retrieve the result
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming your data is in the format "VendorName Gstnumber"
                                string PurchaseOrderNumber = Convert.ToString(reader["PurchaseOrderCode"]);

                                PONumber.Add(new SelectListItem { Value = PurchaseOrderNumber, Text = PurchaseOrderNumber });
                            }
                        }
                    }
                }
            }


            return PONumber;
        }

        public List<GRNFieldModel> GetPurchaseOrdersDetails(string purchaseOrderNo)
        {
            List<GRNFieldModel> potransactionDetails = new List<GRNFieldModel>();
            if (!string.IsNullOrEmpty(purchaseOrderNo))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStrng))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_GetPurchaseOrdersDetails, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter parameter = new SqlParameter("@purchaseOrderNo", SqlDbType.VarChar);
                        parameter.Value = purchaseOrderNo;
                        command.Parameters.Add(parameter);

                        // Execute the stored procedure and retrieve the result
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create a new PotransactionDetail object and populate it with data from the SqlDataReader
                                GRNFieldModel detail = new GRNFieldModel
                                {
                                    PartNumber = Convert.ToString(reader["PartNumber"]),
                                    PartName = Convert.ToString(reader["PartName"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    POQuantity = Convert.ToDecimal(reader["POQuantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    RecievedQty = Convert.ToDecimal(reader["RecievedQty"]),
                                    AcceptedQty = Convert.ToDecimal(reader["AcceptedQty"]),
                                    RejectedQty = Convert.ToDecimal(reader["RejectedQty"]),
                                    BalanceQty = Convert.ToDecimal(reader["BalanceQty"]),
                                    Cgst = Convert.ToDecimal(reader["Cgst"]),
                                    Igst = Convert.ToDecimal(reader["Igst"]),
                                    Tcs = Convert.ToDecimal(reader["Tcs"]),
                                    Sgst = Convert.ToDecimal(reader["Sgst"]),
                                    TotalPrice = Convert.ToDecimal(reader["TotalPrice"]),
                                    TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                                };

                                potransactionDetails.Add(detail);

                            }
                        }
                    }
                }
            }


            return potransactionDetails;
        }
        public string SaveData(string GRNData, string GRNNO, string ChallanNo, string BillNO, DateTime DateOfRecipt, DateTime DateOfBill, string Supplier, string PurchaseOrderNo)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConnectionStrng);
                con.Open(); 

                SqlCommand cmd = new SqlCommand(StoredProcedureHelper.sp_InsertGRNData, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command
                cmd.Parameters.Add("@jsonData", SqlDbType.NVarChar, -1).Value = GRNData; // Use -1 for MAX length

                cmd.Parameters.Add("@GRNNO", SqlDbType.NVarChar, 40).Value = GRNNO;
                cmd.Parameters.Add("@ChallanNo", SqlDbType.NVarChar, 40).Value = ChallanNo;
                cmd.Parameters.Add("@BillNO", SqlDbType.NVarChar, 40).Value = BillNO;
                cmd.Parameters.Add("@DateOfRecipt", SqlDbType.DateTime).Value = DateOfRecipt;
                cmd.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = DateOfBill;
                cmd.Parameters.Add("@Supplier", SqlDbType.NVarChar, 40).Value = Supplier;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 40).Value = "Rohit";
                cmd.Parameters.Add("@PurchaseOrderNo", SqlDbType.NVarChar, 40).Value = PurchaseOrderNo;
                cmd.Parameters.Add("@DepotId", SqlDbType.Int).Value = Convert.ToInt32(0);
                cmd.ExecuteNonQuery();
                con.Close();
                return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
