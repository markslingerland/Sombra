using UAParser;

namespace Sombra.Web.Infrastructure
{
    public static class UserAgentParser
    {
        public static UserAgent Extract(string userAgent)
        {
            var clientInfo = Parser.GetDefault().Parse(userAgent);
            var result =  new UserAgent(clientInfo.OS.Family, clientInfo.UserAgent.Family);

            return result;
        }
    }

    public class UserAgent
    {

        public UserAgent(string operatingSystem, string browserName)
        {
            OperatingSystem = operatingSystem;
            BrowserName = browserName;

        }
        public string OperatingSystem { get; set; }
        public string BrowserName { get; set; }

    }
}