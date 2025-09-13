namespace HR_Management.Domain;

public class Role : BaseDomainEntity
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}