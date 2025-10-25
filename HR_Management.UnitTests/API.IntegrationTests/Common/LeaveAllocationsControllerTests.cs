using System.Net;
using System.Net.Http.Json;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Common;
using HR_Management.Common.Pagination;
using HR_Management.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HR_Management.UnitTests.API.IntegrationTests.Common;

public class LeaveAllocationsControllerTests : IntegrationTestBase
{
    public LeaveAllocationsControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ShouldReturnOkAndLeaveAllocations()
    {
        //Act
        var response = await _client.GetAsync("api/leave-allocations");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<PagedResultDto<LeaveAllocationDto>>();
        content.Items.Count.ShouldBe(7);
    }

    [Fact]
    public async Task Get_ShouldReturnNoContent_WhenEmpty()
    {
        //Arrange
        using var scope = new CustomWebApplicationFactory().Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeaveManagementDbContext>();
        var originalData = context.LeaveAllocations.ToList();
        try
        {
            context.LeaveAllocations.RemoveRange(context.LeaveAllocations);
            await context.SaveChangesAsync();

            //Act
            var response = await _client.GetAsync("api/leave-allocations");
            //Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
        finally
        {
            context.ChangeTracker.Clear();
            await context.LeaveAllocations.AddRangeAsync(originalData);
            await context.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task Get_ShouldReturnOkAndLeaveAllocation()
    {
        //Act
        var response = await _client.GetAsync("api/leave-allocations/1");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<LeaveAllocationDto>();
        Assert.IsType<LeaveAllocationDto>(content);
        content.TotalDays.ShouldBe(26);
    }

    [Fact]
    public async Task Get_ShouldReturnBadRequest_WhenIdInvalid()
    {
        //Act
        var response = await _client.GetAsync("api/leave-allocations/-1");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound()
    {
        //Act
        var response = await _client.GetAsync("api/leave-allocations/8");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldReturnCreatedAddNewLeaveAllocation()
    {
        //Arrange
        var dto = new CreateLeaveAllocationDto
        {
            LeaveTypeId = 6,
            UserId = 1
        };
        //Act
        var response = await _client.PostAsJsonAsync("api/leave-allocations", dto);
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        var content = await response.Content.ReadFromJsonAsync<ResultDto<int>>();
        Assert.True(content.IsSuccess);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenInvalid()
    {
        //Arrange
        var dto = new CreateLeaveAllocationDto
        {
            UserId = 1
        };
        //Act
        var response = await _client.PostAsJsonAsync("api/leave-allocations", dto);
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<ResultDto<int>>();
        Assert.False(content.IsSuccess);
    }

    [Fact]
    public async Task Delete_ShouldReturnOKAndDeleteLeaveAllocation()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-allocations/5");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.True(content.IsSuccess);
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenInvalid()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-allocations/-1");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.IsType<ResultDto>(content);
        Assert.False(content.IsSuccess);
    }

    [Fact]
    public async Task Post_ShouldReturnOkAndResetAllocations()
    {
        //Act
        var response = await _client.PostAsync("api/leave-allocations/reset", null);
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadFromJsonAsync<ResultDto>();
        Assert.IsType<ResultDto>(content);
        Assert.True(content.IsSuccess);
    }
}