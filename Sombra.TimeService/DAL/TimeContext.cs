using Microsoft.EntityFrameworkCore;
using Sombra.Infrastructure.DAL;

namespace Sombra.TimeService.DAL
{
    public class TimeContext : SombraContext<TimeContext>
    {
        public TimeContext() { }

        public TimeContext(DbContextOptions<TimeContext> options, SombraContextOptions sombraContextOptions) : base(options, sombraContextOptions) { }
        public DbSet<TimeEvent> TimeEvents { get; set; }
    }
}