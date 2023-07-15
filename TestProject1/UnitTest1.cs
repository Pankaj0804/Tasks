using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http.Json;
using WebApplication5;
using WebApplication5.Controllers;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.DependencyInjection;
using WebApplication5.Data;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class UnitTest1
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UnitTest1()
        {
            _server = new TestServer(new WebHostBuilder().ConfigureServices(services =>
            {
                // add services
                services.AddHttpClient();
            })
            .Configure(app =>
            {
                // add middlewares
            }));

            //var application = new WebApplicationFactory<Program>()
            //.WithWebHostBuilder(builder =>
            //{
            //    builder.ConfigureServices(services =>
            //    {
            //        services.AddHttpClient();
            //        services.AddDbContext<EmployeeDbContext>(builder =>
            //        {
            //            builder.UseNpgsql("Server=localhost;Database=myWebApi;Port=8240;User id=postgres;Password=Puran@76");
            //        });
            //    });
            //});

            _client = this._server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost:8240/");
            var expectedStatusCode = HttpStatusCode.OK;
            //var expectedContent = "Hello, World!"; // Replace with your expected content);
        }

        [Fact]
        public async void GetAllEmployees_ReturnsOkResult()
        {
            //Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Employee");

            //Act
            var response = await _client.SendAsync(request).ConfigureAwait(true);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                          response.Content.Headers.ContentType.ToString());
        }
        [Fact]

        public async Task AddEmployee_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var employee = new Employee { EmployeeName = "John" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/employee", employee);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

    }
}




