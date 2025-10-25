using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Common;
using HR_Management.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveAllocations.Commands;

public class CreateLeaveAllocationCommandHandlerTests : TestBase
{
    private readonly Mock<ILeaveAllocationRepository> _mockAllocationRepo;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepo;

    public CreateLeaveAllocationCommandHandlerTests()
    {
        _mockAllocationRepo = MockLeaveAllocationRepository.AddMock();
        _mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeDetailsMock();
    }

    [Fact]
    public async Task Should_Create_LeaveAllocation()
    {
        //Arrange
        var dto = new CreateLeaveAllocationDto
        {
            LeaveTypeId = 1,
            UserId = 1
        };
        var handler =
            new CreateLeaveAllocationCommandHandler(_mockAllocationRepo.Object, _mapper, _mockLeaveTypeRepo.Object);
        //Act
        var result = await handler.Handle(new CreateLeaveAllocationCommand
        {
            CreateLeaveAllocationDto = dto
        }, CancellationToken.None);
        //Assert
        result.ShouldBeOfType<ResultDto<int>>();
        var leaveAllocations = await _mockAllocationRepo.Object.GetAllAsync();
        leaveAllocations.Count.ShouldBe(4);
    }
}