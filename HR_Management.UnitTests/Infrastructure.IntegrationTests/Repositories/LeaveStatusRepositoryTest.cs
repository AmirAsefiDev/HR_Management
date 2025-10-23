using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using HR_Management.Persistence.Repositories;
using HR_Management.UnitTests.Infrastructure.IntegrationTests.Common;

namespace HR_Management.UnitTests.Infrastructure.IntegrationTests.Repositories;

public class LeaveStatusRepositoryTest
{
    private readonly LeaveManagementDbContext _context;
    private readonly ILeaveStatusRepository _repo;

    public LeaveStatusRepositoryTest()
    {
        _context = TestDbContextFactory.Create();
        _repo = new LeaveStatusRepository(_context);
    }

    [Fact]
    public void GetLeaveStatusesDetails_ShouldReturn_IQueryable()
    {
        //Act
        var result = _repo.GetLeaveStatusesWithDetails();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<LeaveStatus>>(result);
        Assert.Equal(4, result.Count());
    }

    [Fact]
    public async Task GetLeaveTypeAsync_ShouldReturn_LeaveStatus()
    {
        //Act
        var result = await _repo.GetAsync(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<LeaveStatus>(result);
        Assert.Equal("Pending", result.Name);
    }

    [Fact]
    public async Task AddAsync_ShouldAdd_NewLeaveStatus()
    {
        //Arrange
        var leaveStatus = new LeaveStatus
        {
            Name = "Unknown",
            Description = "No body knows."
        };
        //Act
        await _repo.AddAsync(leaveStatus);
        //Assert
        var leaveStatuses = await _repo.GetAllAsync();
        Assert.Equal(5, leaveStatuses.Count);
        Assert.Contains(leaveStatuses, ls => ls.Name == "Unknown");
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_LeaveStatus()
    {
        //Arrange
        var leaveStatus = await _repo.GetAsync(4);
        leaveStatus.Name = "Unknown for cancelled";
        leaveStatus.Description = "No body knows who cancelled the leave.";

        //Act
        await _repo.UpdateAsync(leaveStatus);

        //Assert
        var updatedLS = await _repo.GetAsync(4);
        Assert.Equal("Unknown for cancelled", updatedLS.Name);
        Assert.Equal("No body knows who cancelled the leave.", updatedLS.Description);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDelete_LeaveStatus()
    {
        //Arrange
        var leaveStatus = await _repo.GetAsync(4);
        //Act
        await _repo.DeleteAsync(leaveStatus);
        //Assert
        var all = await _repo.GetAllAsync();
        Assert.Equal(3, all.Count);
        Assert.DoesNotContain(all, ls => ls.Id == 4);
    }
}