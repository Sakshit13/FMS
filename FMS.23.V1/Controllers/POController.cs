using FMS._23.V1.Models;
using FMS._23.V1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using FMS._23.V1.BAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using FMS._23.V1.DAL;
using FMS._23.V1.REPOSITORY;

namespace FMS._23.V1.Controllers
{
    public class POController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public POController(IConfiguration configuration, ApplicationDbContext context)
        {
            this._configuration = configuration;
			_context = context;
		}

        public IActionResult Index()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(StaticConfig.GetConnectionString()))
                {
                    connection.Open();

                    // Execute the stored procedure
                    using (SqlCommand command = new SqlCommand("sp_GetPurchaseOrdersDetails", connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var model = new List<PurchaseOrderDetailsResponse>();

                            while (reader.Read())
                            {
                                var row = new PurchaseOrderDetailsResponse
                                {
                                    RowNumber = Convert.ToInt32(reader["RowNumber"]),
                                    Id = Convert.ToInt32(reader["Id"]),
                                    PurchaseOrderNumber = reader["PurchaseOrderNumber"].ToString(),
                                    GrnNumber = reader["Grnnumber"].ToString(),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    VendorName = reader["VendorName"].ToString(),
                                    // Add properties for other columns in the result set
                                };

                                model.Add(row);
                            }

                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string purchaseOrderNo, string vendorName, string fromDate, string toDate)
        {
            // Parse fromDate and toDate strings into DateTime objects
            DateTime? fromDateParsed = string.IsNullOrEmpty(fromDate) ? (DateTime?)null : DateTime.Parse(fromDate);
            DateTime? toDateParsed = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.Parse(toDate);
            try
            {
                using (SqlConnection connection = new SqlConnection(StaticConfig.GetConnectionString()))
                {
                    connection.Open();

                    // Execute the stored procedure
                    using (SqlCommand command = new SqlCommand("sp_GetPurchaseOrdersDetails", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@purchaseOrderNo", purchaseOrderNo));
                        command.Parameters.Add(new SqlParameter("@vendorName", vendorName));
                        command.Parameters.Add(new SqlParameter("@fromDate", fromDateParsed));
                        command.Parameters.Add(new SqlParameter("@toDate", toDateParsed));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var model = new List<PurchaseOrderDetailsResponse>();

                            while (reader.Read())
                            {
                                var row = new PurchaseOrderDetailsResponse
                                {
                                    RowNumber = Convert.ToInt32(reader["RowNumber"]),
                                    Id = Convert.ToInt32(reader["Id"]),
                                    PurchaseOrderNumber = reader["PurchaseOrderNumber"].ToString(),
                                    GrnNumber = reader["Grnnumber"].ToString(),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    VendorName = reader["VendorName"].ToString(),
                                    // Add properties for other columns in the result set
                                };

                                model.Add(row);
                            }

                            return PartialView("_SearchResultsPartial", model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during database access
                // You can log the exception or return an error view
                return View("Error");
            }
        }

        public IActionResult Create()
        {
			PoBAL poBAL = new PoBAL();

			ViewBag.PurchaseOrderNumbers = poBAL.GetPONO();
           
			ViewBag.RequestedToDepot = new SelectList(_context.Set<DepotMaster>(), "Id", "DepotName");
			ViewBag.PartNumber = new SelectList(_context.ItemMaster, "ItemNumber", "ItemNumber");
			//var data = GetCurrentStock(ViewBag.PartNumber,1);
            //ViewBag.Igst = data.Igst;

			return View();
        }

        [HttpPost]
        public IActionResult Create(string podata,string PurchaseOrderNumber,DateTime OrderDate, string VendorGstnumber)
        {
            try
            {
                PoBAL poBAL = new PoBAL();
                string PurchaseOrderNumbers =  poBAL.GetPONO();
                var potransactionDetails = JsonConvert.DeserializeObject<List<PotransactionDetail>>(podata);

                SqlConnection con = new SqlConnection(StaticConfig.GetConnectionString());
                SqlCommand cmd = new SqlCommand("sp_InsertPotransactionDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PurchaseOrderNumber", SqlDbType.NVarChar, 100).Value = PurchaseOrderNumbers;
                cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = OrderDate;
                cmd.Parameters.Add("@VendorGstnumber", SqlDbType.NVarChar, 100).Value = VendorGstnumber;

                DataTable potransactionDetailsTable = new DataTable();
                //potransactionDetailsTable.Columns.Add("Id", typeof(int));
                potransactionDetailsTable.Columns.Add("PurchaseOrderNumber", typeof(string));
                potransactionDetailsTable.Columns.Add("PartNumber", typeof(string));
                potransactionDetailsTable.Columns.Add("OrderQty", typeof(decimal));
                potransactionDetailsTable.Columns.Add("UnitPrice", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Tax", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Discount", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Cgst", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Sgst", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Igst", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Tcs", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Cartage", typeof(decimal));
                potransactionDetailsTable.Columns.Add("FreightAmount", typeof(decimal));
                potransactionDetailsTable.Columns.Add("CurrentStock", typeof(decimal));
                potransactionDetailsTable.Columns.Add("Active", typeof(bool));
                potransactionDetailsTable.Columns.Add("IsDeleted", typeof(bool));
                potransactionDetailsTable.Columns.Add("Remarks", typeof(string));
                potransactionDetailsTable.Columns.Add("CreatedOn", typeof(DateTime));
                potransactionDetailsTable.Columns.Add("CreatedBy", typeof(string));
                potransactionDetailsTable.Columns.Add("ModifiedOn", typeof(DateTime));
                potransactionDetailsTable.Columns.Add("ModifiedBy", typeof(string));

                foreach (var item in potransactionDetails)
                {
                    DataRow row = potransactionDetailsTable.NewRow();
					//row["Id"] = item.Id;
					row["PartNumber"] = string.IsNullOrEmpty(item.PartNumber) ? DBNull.Value : (object)item.PartNumber;
					row["OrderQty"] = item.OrderQty.HasValue ? (object)item.OrderQty.Value : DBNull.Value;
					row["UnitPrice"] = item.UnitPrice.HasValue ? (object)item.UnitPrice.Value : DBNull.Value;
					row["Tax"] = item.Tax.HasValue ? (object)item.Tax.Value : DBNull.Value;
					row["Discount"] = item.Discount.HasValue ? (object)item.Discount.Value : DBNull.Value;
					row["Cgst"] = item.Cgst.HasValue ? (object)item.Cgst.Value : DBNull.Value;
					row["Sgst"] = item.Sgst.HasValue ? (object)item.Sgst.Value : DBNull.Value;
					row["Igst"] = item.Igst.HasValue ? (object)item.Igst.Value : DBNull.Value;
					row["Tcs"] = item.Tcs.HasValue ? (object)item.Tcs.Value : DBNull.Value;
					row["Cartage"] = item.Cartage.HasValue ? (object)item.Cartage.Value : DBNull.Value;
					row["FreightAmount"] = item.FreightAmount.HasValue ? (object)item.FreightAmount.Value : DBNull.Value;
					row["CurrentStock"] = item.CurrentStock.HasValue ? (object)item.CurrentStock.Value : DBNull.Value;
					row["Remarks"] = string.IsNullOrEmpty(item.Remarks) ? DBNull.Value : (object)item.Remarks;
					row["Active"] = 0;
					row["IsDeleted"] = 0;
					row["CreatedOn"] = DateTime.Now;
                    row["CreatedBy"] = "";
                    row["ModifiedOn"] = DateTime.Now;
                    row["ModifiedBy"] = "";

                    potransactionDetailsTable.Rows.Add(row);
                }
                SqlParameter tvpParameter = cmd.Parameters.AddWithValue("@PotransactionDetailsTable", potransactionDetailsTable);
                tvpParameter.SqlDbType = SqlDbType.Structured;
                con.Open();
                int result = (int)cmd.ExecuteScalar();
                con.Close();
                if (result == 1)
                {
                    Console.WriteLine("Data inserted successfully.");
                }
                else
                {
                    Console.WriteLine("Data insertion failed.");
                }
            }
            catch (Exception ex)
            {


            }

           
            return View();
        }

		[HttpPost]
		public ActionResult<StockInfo> GetCurrentStock(string partNumber, string depotId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(StaticConfig.GetConnectionString()))
				{
					connection.Open();

					// Create a command to execute the stored procedure
					using (SqlCommand command = new SqlCommand("sp_GetCurrentStockByPartNumAndDepot", connection))
					{
						command.CommandType = CommandType.StoredProcedure;

						// Add parameters
						command.Parameters.Add(new SqlParameter("@PartNumber", partNumber));
						command.Parameters.Add(new SqlParameter("@DepotId", depotId));

						// Execute the command
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								// Retrieve the values and populate the StockInfo instance
								StockInfo stockInfo = new StockInfo
								{
									StockQuantity = reader.GetDecimal(reader.GetOrdinal("StockQuantity")),
									Rate = reader.GetDecimal(reader.GetOrdinal("Rate")),
									Cgst = reader.GetDecimal(reader.GetOrdinal("Cgst")),
									Sgst = reader.GetDecimal(reader.GetOrdinal("Sgst")),
									Igst = reader.GetDecimal(reader.GetOrdinal("Igst"))
								};

								return Ok(stockInfo);
							}
						}
					}
				}

				// If no data was found or an error occurred, handle it appropriately
				return NotFound("No data found.");
			}
			catch (Exception ex)
			{
				// Handle exceptions or log errors as needed
				return StatusCode(500, "Internal Server Error");
			}
		}


	}

	public class StockInfo
	{
		public decimal StockQuantity { get; set; }
		public decimal Rate { get; set; }
		public decimal Cgst { get; set; }
		public decimal Sgst { get; set; }
		public decimal Igst { get; set; }
	}
}
