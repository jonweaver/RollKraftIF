﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RollKraftIF.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<SqlParameter> ToSQLParameters(this object model) => model.GetType()
       .GetProperties()
       .Select(p => new SqlParameter(GetAttributeDisplayName(p) ?? p.Name, p.GetValue(model)));

        private static string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (atts.Length == 0)
                return null;
            return (atts[0] as DisplayNameAttribute).DisplayName;
        }
    }
}
