using System;
using System.Linq;

namespace RollKraftIF.Infrastructure.Data
{
    public interface IStoredProcedureRunner
    {
        T ExecuteStoredProcedure<T>(string procedureName, object model);
    }

}
