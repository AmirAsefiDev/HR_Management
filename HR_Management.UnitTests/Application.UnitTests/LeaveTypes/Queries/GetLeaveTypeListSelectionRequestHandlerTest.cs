using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeListSelectionRequestHandlerTest : TestBase
{
    private readonly IMock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypeListSelectionRequestHandlerTest()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeListSelectionMock();
    }

    [Fact]
    public async Task GetLeaveTypeListSelectionTest()
    {
        //Arrange
        var handler = new GetLeaveTypeListSelectionRequestHandler(_mockRepo.Object, _mapper);

        //Act
        var result = await handler.Handle(new GetLeaveTypeListSelectionRequest(), CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<List<LeaveTypeDto>>(result);
    }
}