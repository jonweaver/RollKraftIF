using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace RollKraftIF.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        private static string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if(atts.Length == 0)
                return null;
            return (atts[0] as DisplayNameAttribute).DisplayName;
        }

        public static DynamicParameters ToDynamicParameters(this object model)
        {
            var dynamicParameters = new DynamicParameters();
            model.GetType()
                .GetProperties()
                .ToList()
                .ForEach(p => dynamicParameters.Add(GetAttributeDisplayName(p) ?? p.Name, p.GetValue(model)));

            return dynamicParameters;
        }

        public static IEnumerable<SqlParameter> ToSQLParameters(this object model) => model.GetType()
            .GetProperties()
            .Select(p => new SqlParameter(GetAttributeDisplayName(p) ?? p.Name, p.GetValue(model)));
    }
}
