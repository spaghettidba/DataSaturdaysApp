using System;
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
            Uri uri;
            if(!Uri.TryCreate((string)value, UriKind.Absolute, out uri))
            {
                uri = new Uri((string)value, UriKind.Relative);
            }
            return uri;
        }
    }
}