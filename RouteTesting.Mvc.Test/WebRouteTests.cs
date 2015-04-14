using System.Collections;
using System.Web.Routing;
using MvcRouteTester;
using Xunit;

namespace RouteTesting.Mvc.Test
{
    public class WebRouteTests : IClassFixture<WebRouteConfigFixture>
    {
        #region Fields

        private readonly RouteCollection _routes;

        #endregion // Fields

        #region Construction

        public WebRouteTests(WebRouteConfigFixture fixture)
        {
            _routes = fixture.Routes;
        }

        #endregion // Construction

        #region Tests

        [Fact]
        public void EmptyRoute_ResolvesToHomeIndex()
        {
            RouteAssert.HasRoute(
                _routes,
                "/",
                new {Controller = "Home", Action = "Index"});
        }

        [Theory]
        [InlineData("/fred.axd")]
        [InlineData("/someroute.axd/something")]
        public void HasRoute_ButThisRouteIsIgnored(string url)
        {
            RouteAssert.HasRoute(_routes, url);
            RouteAssert.IsIgnoredRoute(_routes, url);
        }

        [Theory]
        [InlineData("/home/index")]
        [InlineData("/somecontroller/someaction/123")]
        [Trait("Note", "This test only verifies that the routing scheme generates a principal match. It does " + 
                       "NOT necessarily mean that the url subsequently can be resolved to a concrete " +
                       "controller or action method - i.e. the request will actually succeed.")]
        public void AcceptsRoutes(string route)
        {
            RouteAssert.HasRoute(_routes, route);
        }

        [Theory, MemberData("PhysicallyMatchingRoutes")]
        public void CanResolveRoutes(string url, object routeData)
        {
            RouteAssert.HasRoute(_routes, url, routeData);
        }

        #endregion // Tests

        #region Data Factories

        public static IEnumerable PhysicallyMatchingRoutes()
        {
            yield return new object[] {"/", new {Controller = "Home", Action = "Index"}};
            yield return new object[] {"/home", new {Controller = "Home", Action = "Index"}};
            yield return new object[] {"/home/index", new {Controller = "Home", Action = "Index"}};
            yield return new object[] {"/home/index/123", new {Controller = "Home", Action = "Index", id = 123}};
            yield return new object[] { "/foo", new { Controller = "RenamedFoo", Action = "Bar" } };
            yield return new object[] { "/foo/bar", new { Controller = "RenamedFoo", Action = "Bar" } };
        }

        #endregion // Data Factories

    }
}
