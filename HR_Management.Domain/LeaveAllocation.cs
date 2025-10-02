namespace HR_Management.Domain;

public class LeaveAllocation : BaseDomainEntity
{
    public LeaveType LeaveType { get; set; }
    public int LeaveTypeId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int TotalDays { get; set; }
    public int UsedDays { get; set; } = 0;

    public int RemainingDays { get; set; }

    public int Period { get; set; }
}