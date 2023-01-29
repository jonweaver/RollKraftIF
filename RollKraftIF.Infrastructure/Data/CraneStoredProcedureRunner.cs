using Crane.Interface;
using Crane.SqlServer;
using RollKraftIF.Infrastructure.Extensions;
using System;
using System.Configuration;
using System.Linq;

namespace RollKraftIF.Infrastructure.Data
{
    public class CraneStoredProcedureRunner : IStoredProcedureRunner
    {
        private readonly string ConnString;
        private readonly ICraneAccess sqlAccess;

        public CraneStoredProcedureRunner(string connectionString)
        {
            ConnString = connectionString;
            sqlAccess = new SqlServerAccess(ConnString);
        }
  
        public T ExecuteStoredProcedure<T>(string procedureName, object model)
        {
            try
            {
                var parameters = model.ToSQLParameters();

                return sqlAccess.Query()
                    .AddSqlParameterCollection(parameters)
                    .ExecuteReader<T>(procedureName)
                    .FirstOrDefault();

            } catch(Exception ex)
            {
                throw new ApplicationException($"Problem running stored procedure {procedureName}: {ex.Message}", ex);
            }
        }
    }
}
