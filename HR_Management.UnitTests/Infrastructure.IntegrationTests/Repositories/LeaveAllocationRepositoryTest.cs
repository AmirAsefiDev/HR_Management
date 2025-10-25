using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using HR_Management.Persistence.Repositories;
using HR_Management.UnitTests.Infrastructure.IntegrationTests.Common;

namespace HR_Management.UnitTests.Infrastructure.IntegrationTests.Repositories;

public class LeaveAllocationRepositoryTest
{
    private readonly LeaveManagementDbContext _context;
    private readonly ILeaveAllocationRepository _repo;

    public LeaveAllocationRepositoryTest()
    {
        _context = TestDbContextFactory.Create();
        _repo = new LeaveAllocationRepository(_context);
    }

    [Fact]
    public void GetLeaveAllocationsWithDetails_ShouldReturnIQueryable()
    {
        //Act
        var result = _repo.GetLeaveAllocationsWithDetails();
        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<LeaveAllocation>>(result);
        Assert.Equal(6, result.Count());
    }

    [Fact]
    public async Task GetLeaveAllocationWithDetailsAsync_ShouldReturnLeaveAllocation()
    {
        //Act
        var result = await _repo.GetLeaveAllocationWithDetailsAsync(1);
        //Assert
        Assert.NotNull(result);
        Assert.IsType<LeaveAllocation>(result);
    }

    [Theory]
    [InlineData(0, 27)]
    public async Task HasSufficientAllocationAsync_ShouldReturnTrue(int validAmount, int invalidAmount)
    {
        //ValidMode
        //Act
        var validResult = await _repo.HasSufficientAllocationAsync(1, 1, validAmount);
        //Assert
        Assert.True(validResult);

        //InvalidMode
        //Act
        var inValidResult = await _repo.HasSufficientAllocationAsync(1, 1, invalidAmount);
        //Assert
        Assert.False(inValidResult);
    }

    [Fact]
    public async Task GetUserAllocationAsync_ShouldReturnLeaveAllocation()
    {
        //Act
        var result = await _repo.GetUserAllocationAsync(1, 1);
        //Assert
        Assert.NotNull(result);
        Assert.IsType<LeaveAllocation>(result);
    }

    [Fact]
    public async Task DeleteAllAsync_ShouldDeleteAllocations()
    {
        //Act
        var all = await _repo.GetAllAsync();
        await _repo.DeleteAllAsync();
        //Assert
        var result = await _repo.GetAllAsync();
        Assert.Equal(0, result.Count);
        await _repo.AddRangeAsync(all);
    }

    [Fact]
    public async Task AddRangeAsync_ShouldAddLeaveAllocationsList()
    {
        //Act
        var all = await _repo.GetAllAsync();
        await _repo.DeleteAllAsync();
        await _repo.AddRangeAsync(all);
        //Assert
        var result = await _repo.GetAllAsync();
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewLeaveAllocation()
    {
        //Arrange
        var newLeaveAllocation = new LeaveAllocation
        {
            LeaveTypeId = 1,
            Period = DateTime.UtcNow.Year,
            TotalDays = 112,
            UserId = 1
        };
        //Act
        await _repo.AddAsync(newLeaveAllocation);
        //Assert
        var result = await _repo.GetAllAsync();
        Assert.Equal(7, result.Count);
        Assert.Equal(112, result.Last().TotalDays);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteLeaveAllocation()
    {
        //Act
        var leaveAllocation = await _repo.GetAsync(6);
        await _repo.DeleteAsync(leaveAllocation);
        //Assert
        var result = await _repo.GetAllAsync();
        Assert.Equal(5, result.Count);
        Assert.Equal(90, result.Last().TotalDays);
    }
}