namespace HR_Management.Domain;

public class LeaveType : BaseDomainEntity
{
    public string Name { get; set; }
    public int DefaultDay { get; set; }

    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    public ICollection<LeaveAllocation> LeaveAllocations { get; set; } = new List<LeaveAllocation>();
}