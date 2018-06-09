using Sombra.Infrastructure.DAL;

namespace Sombra.StoryService.DAL
{
    public class DesignTimeDbStoryContextFactory : DesignTimeDbSombraContextFactory<StoryContext>
    {
        protected override string ConnectionStringName => "STORY_DB_CONNECTIONSTRING";
    }
}
