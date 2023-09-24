using COMMON;
using FMS._23.V1.Helpers;
using FMS._23.V1.REPOSITORY;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FMS._23.V1.BAL
{
	public class PoBAL
	{
		private readonly IConfiguration _configuration;

		public PoBAL(IConfiguration configuration)
		{
			_configuration = configuration;
		}
        public PoBAL()
        {
            
        }

        public string GetPONO()
		{
			var PONumber = "";
			using (SqlConnection connection = new SqlConnection(StaticConfig.GetConnectionString()))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(StoredProcedureHelper.sp_GetLastPONumber, connection))
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
							int POMAXID = (Convert.ToInt32(reader["LASTPONumber"]) + 1);
							string YEAR = DateTime.Now.ToString("yy");
							string YEAR1 = DateTime.Now.AddYears(1).ToString("yy");
							string PONO = "PO/" + POMAXID.ToString("0000") + "/" + FinYear;
							PONumber = PONO;
						}
					}
				}
				return PONumber;
			}
		}

	}
}
