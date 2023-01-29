using Dapper;
using RollKraftIF.Infrastructure.Extensions;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace RollKraftIF.Infrastructure.Data
{
    public class DapperStoredProcedureRunner : IStoredProcedureRunner
    {
        //https://www.learndapper.com/stored-procedures

        private readonly string connectionString;

        public DapperStoredProcedureRunner(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string ExecuteStoredProcedure(string procedureName, object model)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var parameters = model.ToDynamicParameters();
                return connection.QuerySingleOrDefault<string>(
                    procedureName,
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
