// uncomment below to try out Crane ORM https://github.com/gtaylor44/Crane
//#define USE_CRANE

using RollKraftIF.Infrastructure.Data;
using RollKraftIF.Infrastructure.Models;
using System;
using System.Configuration;
using System.Linq;

namespace RollKraftIF.Infrastructure
{
    public interface IProgramInfoRepository
    {
        ProgramDetails GetProgramDetails(JobInfo jobInfo);

        string GetProgramName(JobInfo jobInfo);

        string GetRollBore(JobInfo jobInfo);

        string GetRollMaterial(JobInfo jobInfo);
    }

    public class RKIProdProgramInfoRepository : IProgramInfoRepository
    {
        private static readonly string GetProgramProcedureName = ConfigurationManager.AppSettings
            .Get("GetProgramProcedureName") ?? "spGetTandP_Program";
        private static readonly string GetRollBarProcedureName = ConfigurationManager.AppSettings
            .Get("GetRollBarProcedureName") ?? "spGetRollBore";
        private static readonly string GetRollMaterialProcedureName = ConfigurationManager.AppSettings
            .Get("GetRollMaterialProcedureName") ?? "spGetRollMaterial"; 
        private static readonly string GetProgramDetailsProcedureName = ConfigurationManager.AppSettings
            .Get("GetProgramDetailsProcedureName") ?? "spGetProgramDetails";

        private readonly IStoredProcedureRunner storedProcedureRunner;

        public RKIProdProgramInfoRepository(string connectionString = "") 
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    connectionString = ConfigurationManager.ConnectionStrings[DbConstants.ConnectionStringName].ConnectionString;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Problem reading SqlConnection connection string", ex);
                }
            }

#if USE_CRANE

            storedProcedureRunner = new CraneStoredProcedureRunner(connectionString);
#else 
            storedProcedureRunner = new DapperStoredProcedureRunner(connectionString);
#endif

            if (storedProcedureRunner == null)
            {
                throw new InvalidOperationException("no StoredProcedureRunnerDefined");
            }
        }

        private void ValidateInput(JobInfo jobInfo)
        {
            if(jobInfo == null)
            {
                throw new ArgumentNullException(nameof(jobInfo), $"{nameof(jobInfo)} is null.");
            }

            if(string.IsNullOrWhiteSpace(jobInfo.JobNumber))
            {
                throw new ArgumentException($"{nameof(JobInfo.JobNumber)} is required");
            }

            if(string.IsNullOrWhiteSpace(jobInfo.Location))
            {
                throw new ArgumentException($"{nameof(JobInfo.Location)} is required");
            }
        }

        public ProgramDetails GetProgramDetails(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure<ProgramDetails>(GetProgramDetailsProcedureName, jobInfo);
        }

        public string GetProgramName(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure<string>(GetProgramProcedureName, jobInfo);
        }

        public string GetRollBore(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure<string>(GetRollBarProcedureName, jobInfo);
        }

        public string GetRollMaterial(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure<string>(GetRollMaterialProcedureName, jobInfo);
        }
    }
}
