using System.Net;
using System.Net.Http.Json;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Common.Pagination;
using HR_Management.UnitTests.API.IntegrationTests.Common;

namespace HR_Management.UnitTests.API.IntegrationTests;

public class LeaveRequestStatusHistoriesControllerTests : IntegrationTestBase
{
    public LeaveRequestStatusHistoriesControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ReturnOKAndLeaveRequestStatusHistoriesList()
    {
        //Act
        var response = await _client.GetAsync("api/leave-request-status-histories");
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<PagedResultDto<LeaveRequestStatusHistoryDto>>();
        Assert.Equal(1, result.Filter.First().Count);
    }
}