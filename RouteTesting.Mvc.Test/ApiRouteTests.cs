using System.Collections;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using MvcRouteTester;
using Xunit;

namespace RouteTesting.Mvc.Test
{
    public class ApiRouteTests : IClassFixture<ApiRouteConfigFixture>
    {
        #region Fields

        private readonly HttpConfiguration _config;

        #endregion // Fields

        #region Construction

        public ApiRouteTests(ApiRouteConfigFixture fixture)
        {
            _config = fixture.Config;
        }

        #endregion // Construction

        #region Tests

        [Theory]
        [InlineData("/api/values", "GET")]
        [InlineData("/api/values/5", "PUT")]
        [Trait("Note", "This test only verifies that the routing scheme generates a principal match. It does " +
                       "NOT necessarily mean that the url subsequently can be resolved to a concrete " +
                       "controller or action method - i.e. the request will actually succeed.")]
        public void AcceptsRoutes(string url, string httpMethod)
        {
            RouteAssert.HasApiRoute(_config, url, new HttpMethod(httpMethod));
        }

        [Theory, MemberData("PhysicallyMatchingApiRoutes")]
        public void CanResolveRoutes(string url, HttpMethod httpMethod, object routeData)
        {
            RouteAssert.HasApiRoute(_config, url, httpMethod, routeData);
        }

        #endregion // Tests

        #region Data Factories

        public static IEnumerable PhysicallyMatchingApiRoutes()
        {
            yield return new object[]
            {
                "/api/values/1", 
                HttpMethod.Get, 
                new { Controller = "Values", Action = "get", id = "1" }
            };
            yield return new object[]
            {
                "/api/values/234", 
                HttpMethod.Delete, 
                new { Controller = "Values", Action = "delete", id = "234" }
            };
        }

        #endregion // Data Factories

    }
}
