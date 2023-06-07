﻿using System;
using System.Data;
using Dapper;

namespace DataSaturdays.Core.Data
{
    public class DapperUriTypeHandler : SqlMapper.TypeHandler<Uri>
    {
        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            parameter.Value = value.ToString();
        }

        public override Uri Parse(object value)
        {
            return new Uri((string)value);
        }
    }
}