using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KalingaHub.DataAccess
{
    public class BaseRepository
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["KalingaHubDB"].ConnectionString;

        IDbConnection databaseConnection => new SqlConnection(connectionString);

        /// <summary>
        /// Executes a query that returns a list of items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query, dynamic parameters = null)
        {
            IEnumerable<T> result = null;
            using (var connection = databaseConnection)
            {
                connection.Open();
                result = await connection.QueryAsync<T>(query, (object)parameters);
            }
            return result;
        }

        /// <summary>
        /// Executes a query for INSERT, UPDATE or DELETE statements.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<int> ExecuteQueryAsync(string query, dynamic parameters = null)
        {
            int result;
            using (var connection = databaseConnection)
            {
                connection.Open();
                result = await connection.ExecuteAsync(query, (object)parameters);
            }
            return result;
        }

        /// <summary>
        /// Executes a query for INSERT, UPDATE or DELETE statements.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<object> ExecuteScalarAsync(string query, dynamic parameters = null)
        {
            object result;
            using (var connection = databaseConnection)
            {
                connection.Open();
                result = await connection.ExecuteScalarAsync(query, (object)parameters);
            }
            return result;
        }

        /// <summary>
        /// Executes a query that returns multiple result sets.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>> QueryMultipleAsync<TFirst, TSecond>(string query, dynamic parameters = null)
        {
            using (var connection = databaseConnection)
            {
                connection.Open();
                using (var reader = await connection.QueryMultipleAsync(query, (object)parameters))
                {
                    var tFirst = reader.Read<TFirst>();
                    var tSecond = reader.Read<TSecond>();
                    return new Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>>(tFirst, tSecond);
                }
            }
        }
    }
}
