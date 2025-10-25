using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Handlers.Queries;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveAllocations.Queries;

public class GetLeaveAllocationDetailRequestHandlerTest : TestBase
{
    private readonly Mock<ILeaveAllocationRepository> _mockRepo;

    public GetLeaveAllocationDetailRequestHandlerTest()
    {
        _mockRepo = MockLeaveAllocationRepository.GetLeaveAllocationDetailsMock();
    }

    [Theory]
    [InlineData(2, -1)]
    public async Task GetLeaveAllocationDetailsTest(int validId, int inValidId)
    {
        //Arrange
        var handler = new GetLeaveAllocationDetailRequestHandler(_mockRepo.Object, _mapper);

        //ValidMode
        //Act
        var validResult = await handler.Handle(new GetLeaveAllocationDetailRequest
        {
            Id = validId
        }, CancellationToken.None);
        //Assert
        Assert.NotNull(validResult.Data);
        Assert.InRange(validResult.Data.Id, 1, int.MaxValue);
        Assert.IsType<LeaveAllocationDto>(validResult.Data);
        //InvalidMode
        //Act
        var inValidResult = await handler.Handle(new GetLeaveAllocationDetailRequest
        {
            Id = inValidId
        }, CancellationToken.None);
        //Assert
        Assert.Null(inValidResult.Data);
        Assert.IsNotType<LeaveAllocationDto>(inValidResult.Data);
    }
}