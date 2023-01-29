using System;
using System.Linq;

namespace RollKraftIF.Infrastructure.Data
{
    public interface IStoredProcedureRunner
    {
        string ExecuteStoredProcedure(string procedureName, object model);
    }

}
