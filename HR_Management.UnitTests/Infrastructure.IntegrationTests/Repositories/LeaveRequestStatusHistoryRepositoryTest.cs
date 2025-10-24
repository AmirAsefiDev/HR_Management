using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using HR_Management.Persistence.Repositories;
using HR_Management.UnitTests.Infrastructure.IntegrationTests.Common;

namespace HR_Management.UnitTests.Infrastructure.IntegrationTests.Repositories;

public class LeaveRequestStatusHistoryRepositoryTest
{
    private readonly LeaveManagementDbContext _context;
    private readonly ILeaveRequestStatusHistoryRepository _repo;

    public LeaveRequestStatusHistoryRepositoryTest()
    {
        _context = TestDbContextFactory.Create();
        _repo = new LeaveRequestStatusHistoryRepository(_context);
    }

    [Fact]
    public void GetLeaveRequestStatusHistories_Should_Return_IQueryable()
    {
        //Act
        var result = _repo.GetLeaveRequestStatusHistoriesWithDetails();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<LeaveRequestStatusHistory>>(result);
        Assert.Equal(1, result.Count());
    }

    [Fact]
    public void GetLeaveRequestStatusHistoriesByRequestId_ShouldReturn_IQueryable()
    {
        //Act
        var result = _repo.GetLeaveRequestStatusHistoriesByLeaveRequestId(1);

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<LeaveRequestStatusHistory>>(result);
        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async Task HasAnyLeaveHistoryWithStatusIdAsync_ShouldReturn_True()
    {
        //Act
        var result = await _repo.HasAnyLeaveHistoryWithStatusIdAsync(1);
        //Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public async Task HasAnyLeaveHistoryWithRequestIdAsync_ShouldReturn_True()
    {
        //Act
        var result = await _repo.HasAnyLeaveHistoryWithStatusIdAsync(1);
        //Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public async Task AddAsync_Should_Add_NewLeaveRequestStatusHistory()
    {
        //Arrange
        var entity = new LeaveRequestStatusHistory
        {
            LeaveStatusId = 2,
            LeaveRequestId = 1,
            Comment = "Test Description",
            DateCreated = DateTime.UtcNow
        };
        //Act
        await _repo.AddAsync(entity);
        var result = await _repo.GetAllAsync();
        //Assert
        Assert.Equal(2, result.Count);
    }
}