        [Fact]
        public async Task StatusLinkTest()
        {
            using (var container = MockHelper.GetMockingContainer<ValuesApiController>())
            {
                // Arrange                
                var httpRequest =  new HttpRequestMessage(HttpMethod.Get, string.Empty);               
                httpRequest.RequestUri = new Uri("https://localhost/webapi/values");

                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute("RouteName.OperationsV1Controller.Get", "operations/{operationId}");

                var requestContext = new HttpRequestContext()
                {
                    Configuration = config,
                    Url = new UrlHelper(httpRequest),
                    VirtualPathRoot = "/webapi"
                };

                container.Instance.Request = httpRequest;
                container.Instance.Request.Properties[HttpPropertyKeys.RequestContextKey] = requestContext;

                var operationId =Guid.NewGuid();
                
                var expectedStatusUrl = "https://localhost/webapi/operations/" + operationId;                

                // Act
                var result = await container.Instance.Get(operationId);

                // Assert
                container.AssertAll();
                Assert.NotNull(result);
                
                Assert.Equal(expectedStatusUrl, result);
            }
        }
        
        public class ValuesApiController: ApiController
        {
        public string Get(Guid operationGuid){
        var urlHelper = new UrlHelper(request);
            var routeValues = new { operationId = operationGuid };
            var uriString = urlHelper.Link(RouteName.OperationsV1Controller.Get, routeValues);
            return uriString;
        }
        }
