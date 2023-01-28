using AutoFixture.Idioms;
using AutoFixture.Xunit2;
using Moq;
using RollKraftIF.Infrastructure.Data;
using Xunit;
using Xunit.Sdk;
using Crane.Interface;
using Crane.SqlServer;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RollKraftIF.Infrastructure.UnitTests
{
    public class CraneStoredProcedureRunnerTests
    {
        [Theory, AutoMoqData]
        public void ExecuteStoredProcedure_ExecutesReader([Frozen]Mock<ICraneAccess>crane, [Greedy]CraneStoredProcedureRunner runner, JobInfo jobInfo, string procedureName)
        {
            runner.ExecuteStoredProcedure(procedureName, jobInfo);

            crane.Verify(x => x.Query().AddSqlParameterCollection(It.IsAny<IEnumerable<SqlParameter>>()));


        }
    }
}