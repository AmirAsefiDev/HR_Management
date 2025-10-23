using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Common;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using HR_Management.UnitTests.API.IntegrationTests.Common;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace HR_Management.UnitTests.API.IntegrationTests;

public class LeaveTypesControllerTests : IntegrationTestBase
{
    public LeaveTypesControllerTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Get_ShouldReturnOkAndLeaveTypes()
    {
        //Act
        var response = await _client.GetAsync("api/leave-types");
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetSelection_ShouldReturnOkAndLeaveTypes()
    {
        //Act
        var response = await _client.GetAsync("api/leave-types/selection");
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSelection_ShouldReturnNoContent_WhenEmpty()
    {
        //Arrange
        using var scope = new CustomWebApplicationFactory().Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeaveManagementDbContext>();
        var originalData = context.LeaveTypes.ToList();
        try
        {
            context.LeaveTypes.RemoveRange(context.LeaveTypes);
            await context.SaveChangesAsync();
            //Act
            var response = await _client.GetAsync("api/leave-types/selection");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        finally
        {
            context.ChangeTracker.Clear();
            await context.LeaveTypes.AddRangeAsync(originalData);
            await context.SaveChangesAsync();
        }
    }


    [Fact]
    public async Task Get_ShouldReturnOkAndLeaveType()
    {
        await Post_ShouldReturnCreated_WhenValid();
        var response = await _client.GetAsync("api/leave-types/4");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<ResultDto<LeaveTypeDto>>();
        Assert.IsType<LeaveTypeDto>(result.Data);
    }

    [Fact]
    public async Task Get_ShouldReturnNotFound_WhenInvalidId()
    {
        var response = await _client.GetAsync("api/leave-types/9999");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task Post_ShouldReturnCreated_WhenValid()
    {
        //Arrange
        var dto = new CreateLeaveTypeDto
        {
            Name = "Emergency",
            DefaultDay = 99
        };

        //Act
        var response = await _client.PostAsJsonAsync("api/leave-types", dto);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain("successfully");
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenNameIsEmpty()
    {
        //Arrange
        var dto = new CreateLeaveTypeDto
        {
            Name = "",
            DefaultDay = 10
        };

        //Act
        var response = await _client.PostAsJsonAsync("/api/leave-types", dto);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Put_ShouldReturnOk_WhenValid()
    {
        //Arrange
        var dto = new LeaveTypeDto
        {
            Name = "Updated Leave Type",
            DefaultDay = 99
        };
        //Act
        var response = await _client.PutAsJsonAsync("api/leave-types/4", dto);
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<ResultDto>();
        result.Message.Should().Contain("successfully");
    }

    [Fact]
    public async Task Put_ShouldReturnBadRequest_WhenDefaultDayIsInvalid()
    {
        //Arrange
        var dto = new LeaveTypeDto
        {
            Id = 1,
            Name = "Tested Leaved"
        };
        //Act
        var response = await _client.PutAsJsonAsync($"api/leave-types/{dto.Id}", dto);
        //Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Delete_ShouldReturnOk_WhenDeleted()
    {
        //Arrange
        //Creating new leave type handy
        using var scope = new CustomWebApplicationFactory().Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LeaveManagementDbContext>();
        var entity = new LeaveType
        {
            Name = "Test to delete",
            DefaultDay = 3
        };
        await context.LeaveTypes.AddAsync(entity);
        await context.SaveChangesAsync();
        //Act
        var response = await _client.DeleteAsync($"api/leave-types/{entity.Id}");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadAsStringAsync();
        result.Should().Contain("successfully");
    }

    [Fact]
    public async Task Delete_ShouldReturnConflict_WhenLeaveTypeUsed()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-types/1");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenIsNotExists()
    {
        //Act
        var response = await _client.DeleteAsync("api/leave-types/99");
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}