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

        public CraneStoredProcedureRunner()
        {
            try
            {
                ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Problem reading SqlConnection connection string", ex);
            }

            sqlAccess = new SqlServerAccess(ConnString);
        }

        public CraneStoredProcedureRunner(ICraneAccess sqlAccess) 
        { 
            this.sqlAccess = sqlAccess; 
        }

        public string ExecuteStoredProcedure(string procedureName, object model)
        {
            string result;

            try
            {
                var parameters = model.ToSQLParameters();

                result = sqlAccess.Query()
                    .AddSqlParameterCollection(parameters)
                    .ExecuteReader<string>(procedureName)
                    .FirstOrDefault();

            } catch(Exception ex)
            {
                throw new ApplicationException($"Problem running stored procedure {procedureName}: {ex.Message}", ex);
            }

            return result;
        }
    }
}
