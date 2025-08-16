namespace HR_Management.Domain;

public class LeaveStatus : BaseDomainEntity
{
    /// <summary>
    ///     /"Pending", "Approved", "Rejected", "Cancelled"
    /// </summary>
    public string Name { get; set; }

    public string Description { get; set; }
}