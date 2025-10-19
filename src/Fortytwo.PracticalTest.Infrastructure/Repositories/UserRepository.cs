using Fortytwo.PracticalTest.Application.Interfaces.Persistence;
using Fortytwo.PracticalTest.Domain.Entities;
using Fortytwo.PracticalTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fortytwo.PracticalTest.Infrastructure.Repositories;

public class UserRepository(PracticalTestDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetUserIdByUserName(string? userName, CancellationToken cancellationToken)
    {
        return await dbContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync(cancellationToken);
    }
}