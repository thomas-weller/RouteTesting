using System.Web.Routing;

namespace RouteTesting.Mvc.Test
{
    public class WebRouteConfigFixture
    {
        public RouteCollection Routes { get; private set; }

        public WebRouteConfigFixture()
        {
            Routes = new RouteCollection();
            RouteConfig.RegisterRoutes(Routes);
        }
    }
}