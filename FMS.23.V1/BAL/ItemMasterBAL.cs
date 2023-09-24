using FMS._23.V1.Helpers;
using FMS._23.V1.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FMS._23.V1.BAL
{
    public class ItemMasterBAL
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString;
        public ItemMasterBAL(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn");
        }

        public List<ItemViewModel> GetItemList()
        {
            List<ItemViewModel> itemList = new List<ItemViewModel>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Create a SqlCommand to execute the stored procedure
                using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_GetPartDetails, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Execute the stored procedure and retrieve the result
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ItemViewModel itemViewModel = new ItemViewModel();
                        while (reader.Read())
                        {
                            
                            itemViewModel.PartNumber = Convert.ToString(reader["PartNumber"]);
                            itemViewModel.Description = Convert.ToString(reader["Description"]);
                            itemViewModel.Rate = Convert.ToDecimal(reader["Rate"]);
                            itemViewModel.UnitName = Convert.ToString(reader["UnitName"]);
                            itemViewModel.CategoryName = Convert.ToString(reader["CategoryName"]);
                            itemViewModel.Remarks = reader["Remarks"].ToString();
                            itemViewModel.Mrp = Convert.ToDecimal(reader["MRP"]);
                            itemViewModel.Discount = Convert.ToDecimal(reader["Discount"]);
                            itemViewModel.Igst = Convert.ToDecimal(reader["Igst"]);
                            itemViewModel.Cgst = Convert.ToDecimal(reader["Cgst"]);
                            itemViewModel.Sgst = Convert.ToDecimal(reader["Sgst"]);
                            itemViewModel.AltPartNum = Convert.ToString(reader["AltPartNum"]);
                            itemViewModel.Location = Convert.ToString(reader["Location"]);
                            itemViewModel.MinimumStock = Convert.ToString(reader["MinimumStock"]);

                            itemList.Add(itemViewModel);
                        }
                    }
                }
            }

            return itemList;
        }


        public ItemViewModel GetItemList(string PartNumber)
        {
            ItemViewModel itemViewModel = new ItemViewModel();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Create a SqlCommand to execute the stored procedure
                using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_GetPartDetailsByPartNumAndDepot, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter parameter = new SqlParameter("@PartNumber", SqlDbType.NVarChar);
                    parameter.Value = PartNumber; // Set the parameter value
                    command.Parameters.Add(parameter);

                    // Execute the stored procedure and retrieve the result
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           
                            itemViewModel.PartNumber = Convert.ToString(reader["Partnumber"]);
                            itemViewModel.Description = Convert.ToString(reader["Description"]);
                            itemViewModel.Rate = Convert.ToDecimal(reader["Rate"]);
                            itemViewModel.UnitName = Convert.ToString(reader["UnitName"]);
                            itemViewModel.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                            itemViewModel.Remarks = reader["Remarks"].ToString();
                            itemViewModel.Mrp = Convert.ToDecimal(reader["Mrp"]);
                            itemViewModel.Discount = Convert.ToDecimal(reader["Discount"]);
                            itemViewModel.Igst = Convert.ToDecimal(reader["Igst"]);
                            itemViewModel.Cgst = Convert.ToDecimal(reader["Cgst"]);
                            itemViewModel.Sgst = Convert.ToDecimal(reader["Sgst"]);
                            itemViewModel.AltPartNum = Convert.ToString(reader["AltPartNum"]);
                            itemViewModel.Location = Convert.ToString(reader["Location"]);
                            itemViewModel.MinimumStock = Convert.ToString(reader["MinimumStock"]);

                          
                        }
                    }
                }
            }

            return itemViewModel;
        }

    }
}
