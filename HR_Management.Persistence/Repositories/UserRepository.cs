using HR_Management.Application.Contracts.Persistence;
using HR_Management.Domain;
using HR_Management.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly LeaveManagementDbContext _context;

    public UserRepository(LeaveManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<User> GetUsersWithDetails()
    {
        var users = _context.Users
            .Include(u => u.Role);
        return users;
    }

    public async Task<User> GetUserWithDetails(int id)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
}