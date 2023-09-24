using Microsoft.Data.SqlClient;

namespace FMS._23.V1.REPOSITORY
{
    public interface IRepository<T> where T : class
    {

        Task<List<T>> GetAllRow();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity, object key);
        bool IsExist(Func<T, bool> predicate);
        T GetDataSingle(Func<T, bool> predicate);
        Task<T> Delete(int id);
        IQueryable<T> GetAll();


		// Added by Shivanand 

		//List<T> ExecuteQuery(string sqlQuery, object[] usernameParam);
		//T ExecuteQuerySingle(string sqlQuery, object[] usernameParam);


		string ExecuteQueryDynamicSqlParameter(string spQuery, SqlParameter[] Param);
		string ExecuteQuerySingleDynamic(string sqlQuery, object[] usernameParam);
		string ExecuteQueryDynamicList(string sqlQuery, object[] usernameParam);
		string ExecuteQueryDynamicDataset(string sqlQuery, object[] usernameParam);
		string ExecuteQuerySingleDataTableDynamic(string sqlQuery, SqlParameter[] usernameParam);
		string ExecuteQuerySingleDataTableDynamicDataset(string sqlQuery, SqlParameter[] usernameParam);
		int ExecuteCommand(string spQuery, object[] parameters);
	}
}
