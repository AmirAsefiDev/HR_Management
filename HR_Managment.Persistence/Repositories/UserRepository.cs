using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;

namespace HR_Management.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly LeaveManagementDbContext _context;

    public UserRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }
}