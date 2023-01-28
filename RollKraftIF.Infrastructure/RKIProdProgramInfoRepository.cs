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
            .Get("GetProgramProcedureName");
        private static readonly string GetRollBarProcedureName = ConfigurationManager.AppSettings
            .Get("GetRollBarProcedureName");
        private static readonly string GetRollMaterialProcedureName = ConfigurationManager.AppSettings
            .Get("GetRollMaterialProcedureName");

        private readonly IStoredProcedureRunner storedProcedureRunner;

        public RKIProdProgramInfoRepository() 
        { 
            storedProcedureRunner = new SqlServerStoredProcedureRunner(); 
        }

        public RKIProdProgramInfoRepository(IStoredProcedureRunner storedProcedureRunner)
        {
            this.storedProcedureRunner = storedProcedureRunner;
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
            return new ProgramDetails
            {
                Program = GetProgramName(jobInfo),
                RollBore = GetRollBore(jobInfo),
                RollMaterial = GetRollMaterial(jobInfo)
            };
        }

        public string GetProgramName(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure(GetProgramProcedureName, jobInfo);
        }

        public string GetRollBore(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure(GetRollBarProcedureName, jobInfo);
        }

        public string GetRollMaterial(JobInfo jobInfo)
        {
            ValidateInput(jobInfo);
            return storedProcedureRunner.ExecuteStoredProcedure(GetRollMaterialProcedureName, jobInfo);
        }
    }
}
