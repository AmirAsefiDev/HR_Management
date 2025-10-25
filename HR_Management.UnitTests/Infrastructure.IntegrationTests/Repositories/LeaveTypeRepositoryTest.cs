using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using HR_Management.Persistence.Repositories;
using HR_Management.UnitTests.Infrastructure.IntegrationTests.Common;

namespace HR_Management.UnitTests.Infrastructure.IntegrationTests.Repositories;

public class LeaveTypeRepositoryTest
{
    private readonly LeaveManagementDbContext _context;
    private readonly ILeaveTypeRepository _repo;

    public LeaveTypeRepositoryTest()
    {
        _context = TestDbContextFactory.Create();
        _repo = new LeaveTypeRepository(_context);
    }

    [Fact]
    public void GetLeaveTypesDetails_Should_Return_IQueryable()
    {
        // Act
        var result = _repo.GetLeaveTypesWithDetails();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IQueryable<LeaveType>>(result);
        Assert.Equal(6, result.Count());
    }

    [Fact]
    public async Task GetLeaveTypeDetail_Should_Return_LeaveType()
    {
        //Act
        var leaveType = await _repo.GetAsync(1);

        //Assert
        Assert.NotNull(leaveType);
        Assert.IsType<LeaveType>(leaveType);
        Assert.Equal("Annual Leave", leaveType.Name);
    }

    [Fact]
    public async Task ExistAsync_ShouldReturnTrue()
    {
        //Act
        var result = await _repo.ExistAsync(1);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddAsync_Should_Add_New_LeaveType()
    {
        // Arrange
        var newLeaveType = new LeaveType { Name = "Emergency", DefaultDay = 10 };

        // Act
        await _repo.AddAsync(newLeaveType);
        var result = await _repo.GetAllAsync();

        // Assert
        Assert.Equal(7, result.Count);
        Assert.Contains(result, it => it.Name == "Emergency");
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Existing_LeaveType()
    {
        // Arrange
        var leaveType = await _repo.GetAsync(1);
        leaveType.DefaultDay = 20;


        // Act
        await _repo.UpdateAsync(leaveType);
        var updated = await _repo.GetAsync(1);

        // Assert
        Assert.Equal(20, updated.DefaultDay);
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_LeaveType()
    {
        // Arrange
        var leaveType = await _repo.GetAsync(2);

        // Act
        await _repo.DeleteAsync(leaveType);
        var all = await _repo.GetAllAsync();

        // Assert
        Assert.DoesNotContain(all, it => it.Id == 2);
    }
}