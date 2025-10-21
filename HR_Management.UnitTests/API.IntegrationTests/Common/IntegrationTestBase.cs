using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HR_Management.UnitTests.API.IntegrationTests.Common;

public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient _client;
    protected readonly CustomWebApplicationFactory _factory;

    public IntegrationTestBase(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Test");
    }
}