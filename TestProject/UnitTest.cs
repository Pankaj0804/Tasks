using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TestProject.tests
{
    public class UnitTest
    
    {
        public class MyApiControllerTests : IClassFixture<WebApplicationFactory<WebApplication5Factory.Startup>>
        {
            private readonly WebApplicationFactory<YourWebApiProject.Startup> _factory;

            public MyApiControllerTests(WebApplicationFactory<YourWebApiProject.Startup> factory)
            {
                _factory = factory;
            }

            [Fact]
            public async Task Get_MyData_ReturnsData()
            {
                // Arrange
                var client = _factory.CreateClient();

                // Act
                var response = await client.GetAsync("/api/mydata");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.Equal("Expected data", responseString);
            }
        }
    }

}
        }
    }

}
    }
}