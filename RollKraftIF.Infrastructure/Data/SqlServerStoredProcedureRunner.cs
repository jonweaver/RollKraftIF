using RollKraftIF.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace RollKraftIF.Infrastructure.Data
{
    public class SqlServerStoredProcedureRunner : IStoredProcedureRunner
    {
        private static readonly string ConnString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

   

        public string ExecuteStoredProcedure(string procedureName, object model)
        {
            if(string.IsNullOrEmpty(procedureName))
            {
                throw new ArgumentException($"{nameof(procedureName)} is null or empty.", nameof(procedureName));
            }

            if(model == null)
            {
                throw new ArgumentNullException(nameof(model), $"{nameof(model)} is null.");
            }

            var parameters = model.ToSQLParameters();

            var responseString = string.Empty;

            using(var sqlConnObj = new SqlConnection(ConnString))
            {
                try
                {
                    var sqlCmd = new SqlCommand(procedureName, sqlConnObj) { CommandType = CommandType.StoredProcedure };

                    foreach(var param in parameters)
                    {
                        sqlCmd.Parameters.Add(param);
                    }

                    var returnParameter = sqlCmd.Parameters.Add("@ReturnVal", SqlDbType.NChar);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    sqlCmd.ExecuteNonQuery();

                    responseString = returnParameter.Value.ToString();
                } catch(Exception ex)
                {
                    throw new ApplicationException(
                        $"Problem running stored procedure {procedureName}: {ex.Message}",
                        ex);
                } finally
                {
                    if(sqlConnObj != null)
                    {
                        sqlConnObj.Close();
                    }
                }

                return responseString;
            }
        }
    }
}
