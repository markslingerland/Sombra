using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sombra.Infrastructure.DAL;

namespace Sombra.SearchService.DAL
{
    public class DesignTimeDbSearchContextFactory : DesignTimeDbSombraContextFactory<SearchContext>
    {
        protected override string ConnectionStringName => "SEARCH_DB_CONNECTIONSTRING";
    }
    
}