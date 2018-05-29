using System;
using System.Collections.Generic;
using System.Text;
using Sombra.Infrastructure.DAL;

namespace Sombra.TimeService.DAL
{
    public class DesignTimeDbTimeContextFactory : DesignTimeDbSombraContextFactory<TimeContext>
    {
        protected override string ConnectionStringName { get; } = "TIME_DB_CONNECTIONSTRING";
    }
}