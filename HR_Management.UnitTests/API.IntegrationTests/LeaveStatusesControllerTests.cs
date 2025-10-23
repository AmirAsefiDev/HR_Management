using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Common;
using HR_Management.Persistence.Context;
using HR_Management.UnitTests.API.IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HR_Management.UnitTests.API.IntegrationTests;

public class LeaveStatusesControllerTests : IntegrationTestBase
{
    public LeaveStatusesControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ReturnOkAndLeaveStatuses()
    {
        //Act
        var response = await _client.GetAsync("api/leave-statuses");
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_ReturnNoContent_WhenEmpty()
    {
        //Arrange
        using var scope = new CustomWebApplicationFactory().Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeaveManagementDbContext>();
        var originalData = context.LeaveStatuses.ToList();
        try
        {
            context.LeaveStatuses.RemoveRange(context.LeaveStatuses);
            await context.SaveChangesAsync();
            //Act
            var response = await _client.GetAsync("api/leave-statuses");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        finally
        {
            context.ChangeTracker.Clear();
            await context.LeaveStatuses.AddRangeAsync(originalData);
            await context.SaveChangesAsync();
        }
    }


    [Fact]
    public async Task GetSelection_ReturnOkAndLeaveStatuses()
    {
        //Act
        var response = await _client.GetAsync("api/leave-statuses/selection");
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSelection_ReturnNoContent_WhenEmpty()
    {
        //Arrange
        using var scope = new CustomWebApplicationFactory().Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeaveManagementDbContext>();
        var originalData = context.LeaveStatuses.ToList();
        try
        {
            context.LeaveStatuses.RemoveRange(context.LeaveStatuses);
            await context.SaveChangesAsync();
            //Act
            var response = await _client.GetAsync("api/leave-statuses/selection");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        finally
        {
            context.ChangeTracker.Clear();
            await context.LeaveStatuses.AddRangeAsync(originalData);
            await context.SaveChangesAsync();
        }
    }


    [Fact]
    public async Task Get_ShouldReturnOkAndLeaveStatus()
    {
        //Act
        var response = await _client.GetAsync("api/leave-statuses/2");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<LeaveStatusDto>();
        Assert.IsType<LeaveStatusDto>(result);
    }

    [Fact]
    public async Task Get_ShouldReturnBadRequest_WhenInvalid()
    {
        //Act
        var response = await _client.GetAsync("api/leave-statuses/-1");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenIsNotExists()
    {
        //Act
        var response = await _client.GetAsync("api/leave-statuses/11");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_ShouldReturnCreated_WhenValid()
    {
        //Arrange
        var dto = new CreateLeaveStatusDto
        {
            Name = "Unique",
            Description = "Simple Des"
        };
        //Act
        var response = await _client.PostAsJsonAsync("api/leave-statuses", dto);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenInValid()
    {
        //Arrange
        var dto = new CreateLeaveStatusDto
        {
            Description = "Test"
        };
        //Act
        var response = await _client.PostAsJsonAsync("api/leave-statuses", dto);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_ShouldReturnOk_WhenValid()
    {
        //Arrange
        var dto = new UpdateLeaveStatusDto
        {
            Id = 2,
            Name = "Accepted",
            Description = "Accepted Des"
        };
        //Act
        var response = await _client.PutAsJsonAsync("api/leave-statuses", dto);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        result.Message.Should().Contain("successfully");
    }

    [Fact]
    public async Task Put_ShouldReturnBadRequest_WhenNameIsInValid()
    {
        //Arrange
        var dto = new UpdateLeaveStatusDto
        {
            Id = 2,
            Description = "Test"
        };
        //Act
        var response = await _client.PutAsJsonAsync("api/leave-statuses", dto);
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Delete_ShouldReturnOk_WhenValid()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-statuses/3");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        result.Message.Should().Contain("successfully");
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenIdIsInValid()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-statuses/-1");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_ShouldReturnConflict_WhenLeaveStatusUsed()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-statuses/1");
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
    }
}