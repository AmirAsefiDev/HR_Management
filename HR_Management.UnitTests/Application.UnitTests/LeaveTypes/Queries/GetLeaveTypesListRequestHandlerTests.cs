using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Handlers.Queries;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Common.Pagination;
using HR_Management.UnitTests.Mocks;
using Moq;

namespace HR_Management.UnitTests.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypesListRequestHandlerTests : TestBase
{
    private readonly Mock<ILeaveTypeRepository> _mockRepository;

    public GetLeaveTypesListRequestHandlerTests()
    {
        _mockRepository = MockLeaveTypeRepository.GetLeaveTypesListMock();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        //Arrange
        var handler = new GetLeaveTypeListRequestHandler(_mockRepository.Object, _mapper);
        //Act
        var result = await handler.Handle(new GetLeaveTypeListRequest { Pagination = new PaginationDto() },
            CancellationToken.None);
        //Assert
        Assert.IsType<PagedResultDto<LeaveTypeDto>>(result);
    }
}