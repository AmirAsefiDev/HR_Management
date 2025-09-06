using HR_Management.Application.DTOs.Authentication.Signup;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-request")]
[ApiController]
[Authorize]
public class LeaveRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/leave-request
    [HttpGet]
    [ProducesResponseType(typeof(ResultDto<SignupDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<SignupDto>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(List<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestsListRequest());
        return Ok(leaveRequests);
    }

    // GET api/leave-request/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/leave-request
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/leave-request/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/leave-request/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}