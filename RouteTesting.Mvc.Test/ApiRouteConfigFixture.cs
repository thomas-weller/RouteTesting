using System.Web.Http;

namespace RouteTesting.Mvc.Test
{
    public class ApiRouteConfigFixture
    {
        public HttpConfiguration Config { get; private set; }

        public ApiRouteConfigFixture()
        {
            Config = new HttpConfiguration();
            WebApiConfig.Register(Config);
        }
    }
}