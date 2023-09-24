using FMS._23.V1.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS._23.V1.REPOSITORY
{
	public class GenericRepository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext db_Context;
		private readonly IConfiguration _Configuration;
		private string connectionString;
		public GenericRepository(ApplicationDbContext Context,IConfiguration configuration)
		{
			this.db_Context = Context;
			this._Configuration= configuration;

			connectionString = _Configuration.GetConnectionString("Conn");
		}

		#region EF Core Methods
		public async Task<T> Add(T entity)
		{
			try
			{
				var result = await db_Context.AddAsync(entity);
				await db_Context.SaveChangesAsync();
				return result.Entity;
			}
			catch (Exception ex)
			{
				// Handle or log the exception
				throw;
			}
		}

		public async Task<T> Delete(int id)
		{
			var entity = await db_Context.Set<T>().FindAsync(id);
			if (entity == null)
			{
				return null;
			}

			db_Context.Set<T>().Remove(entity);
			await db_Context.SaveChangesAsync();

			return entity;
		}

		public async Task<T> GetById(int id)
		{
			return await db_Context.Set<T>().FindAsync(id);
		}

		public async Task<List<T>> GetAllRow()
		{
			try
			{

				return await db_Context.Set<T>().ToListAsync();
			}
			catch (Exception ex)
			{
				
				return new List<T>();
			}
		}


		public IQueryable<T> GetAll()
		{
			return db_Context.Set<T>().AsNoTracking();
		}

		public bool IsExist(Func<T, bool> predicate)
		{
			return db_Context.Set<T>().Any(predicate);
		}

		public T GetDataSingle(Func<T, bool> predicate)
		{
			return db_Context.Set<T>().Where(predicate).FirstOrDefault();
		}

		public async Task<T> Update(T t, object key)
		{
			if (t == null)
				return null;

			T exist = await db_Context.Set<T>().FindAsync(key);
			if (exist != null)
			{
				db_Context.Entry(exist).CurrentValues.SetValues(t);
				await db_Context.SaveChangesAsync();
			}

			return exist;
		}
		#endregion

		#region Ado

		[Obsolete]
		public string ExecuteQuerySingleDynamic(string spQuery, object[] parameters)
		{
			string output = string.Empty;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(string.Format(spQuery, parameters)))
				{
					cmd.Connection = con;
					cmd.CommandTimeout = Int32.MaxValue;
					cmd.CommandType = CommandType.Text;

					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						sda.Fill(dt);

						output = DataTableToJsonSingleObj(dt);
						// output = DataTableToJSONWithJSONNet(dt);
					}

				}
			}

			return output;
		}
		[Obsolete]
		public string ExecuteQueryDynamicSqlParameter(string spQuery, SqlParameter[] parameters)
		{
			string output = string.Empty;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(spQuery, con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					foreach (var item in parameters)
					{
						cmd.Parameters.Add(item);
					}
					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						sda.Fill(dt);

						output = DataTableToJSONWithJSONNet(dt);
					}

				}
			}

			return output;
		}
		[Obsolete]
		public string ExecuteQueryDynamicList(string spQuery, object[] parameters)
		{
			string output = string.Empty;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(string.Format(spQuery, parameters)))
				{
					cmd.Connection = con;
					cmd.CommandTimeout = Int32.MaxValue;
					cmd.CommandType = CommandType.Text;

					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						sda.Fill(dt);
						output = DataTableToJSONWithJSONNet(dt);
					}

				}
			}

			return output;

		}
		[Obsolete]
		public string ExecuteQuerySingleDataTableDynamic(string spQuery, SqlParameter[] parameters)
		{
			string output = string.Empty;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(spQuery, con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					foreach (var item in parameters)
					{
						cmd.Parameters.Add(item);
					}
					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						sda.Fill(dt);
						output = DataTableToJsonSingleObj(dt);
					}
				}
			}
			return output;
		}
		[Obsolete]
		public string ExecuteQuerySingleDataTableDynamicDataset(string spQuery, SqlParameter[] parameters)
		{
			string output = string.Empty;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(spQuery, con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					foreach (var item in parameters)
					{
						cmd.Parameters.Add(item);
					}
					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
					{
						DataSet dt = new DataSet();
						sda.Fill(dt);
						output = DataSetToJSONWithJSONNet(dt);
					}
				}
			}
			return output;
		}
		[Obsolete]
		public string ExecuteQueryDynamicDataset(string spQuery, object[] parameters)
		{
			string output = string.Empty;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(string.Format(spQuery, parameters)))
				{
					cmd.Connection = con;
					cmd.CommandTimeout = Int32.MaxValue;
					cmd.CommandType = CommandType.Text;

					using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
					{
						DataSet dt = new DataSet();
						sda.Fill(dt);

						output = DataSetToJSONWithJSONNet(dt);
					}

				}
			}

			return output;

		}

		/// <summary>
		/// Insert/Update/Delete Data To Database
		/// <para>Use it when to Insert/Update/Delete data through a stored procedure</para>
		/// </summary>
		public int ExecuteCommand(string spQuery, object[] parameters)
		{

			var conn = db_Context.Database.GetDbConnection();
			conn.Open();

			var command = conn.CreateCommand();
			command.CommandText = spQuery;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = 9000;
			command.Parameters.AddRange(parameters.ToArray());
			var result = command.ExecuteNonQuery();
			command.Dispose();
			conn.Close();
			return result;
		}
		#endregion


		public string DataTableToJsonObj(DataTable dt)
		{
			DataSet ds = new DataSet();
			ds.Merge(dt);
			StringBuilder JsonString = new StringBuilder();
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				JsonString.Append("[");
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					JsonString.Append("{");
					for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
					{
						if (j < ds.Tables[0].Columns.Count - 1)
						{
							JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
						}
						else if (j == ds.Tables[0].Columns.Count - 1)
						{
							JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
						}
					}
					if (i == ds.Tables[0].Rows.Count - 1)
					{
						JsonString.Append("}");
					}
					else
					{
						JsonString.Append("},");
					}
				}
				JsonString.Append("]");
				return JsonString.ToString();
			}
			else
			{
				return null;
			}
		}
		public string DataTableToJsonSingleObj(DataTable dt)
		{
			DataSet ds = new DataSet();
			ds.Merge(dt);
			StringBuilder JsonString = new StringBuilder();
			if (ds != null && ds.Tables[0].Rows.Count > 0)
			{
				// JsonString.Append("[");
				for (int i = 0; i < 1; i++)
				{
					JsonString.Append("{");
					for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
					{
						if (j < ds.Tables[0].Columns.Count - 1)
						{
							JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
						}
						else if (j == ds.Tables[0].Columns.Count - 1)
						{
							JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
						}
					}
					if (i == ds.Tables[0].Rows.Count - 1)
					{
						JsonString.Append("}");
					}
					else
					{
						JsonString.Append("}");
					}
				}
				//JsonString.Append("]");
				return JsonString.ToString();
			}
			else
			{
				return null;
			}
		}
		public string DataTableToJSONWithJSONNet(DataTable table)
		{
			string JSONString = string.Empty;
			JSONString = JsonConvert.SerializeObject(table);
			return JSONString;
		}
		public string DataSetToJSONWithJSONNet(DataSet table)
		{
			string JSONString = string.Empty;
			JSONString = JsonConvert.SerializeObject(table);
			return JSONString;
		}

	}
}
