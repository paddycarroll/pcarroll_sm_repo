// dmcDbEnvironment Version 1.0
// Paddy Carroll
// Exposes platform environment variables for SQL server
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString getDomainName()
    {
        return System.Environment.GetEnvironmentVariable("USERDOMAIN");
    }
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString getPlatformName()
    {
        return System.Environment.GetEnvironmentVariable("COMPUTERNAME");
    }
};

