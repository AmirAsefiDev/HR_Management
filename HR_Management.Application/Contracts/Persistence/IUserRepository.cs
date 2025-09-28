using HR_Management.Domain;

namespace HR_Management.Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    IQueryable<User> GetUsersWithDetails();
    Task<User> GetUserWithDetails(int id);
}