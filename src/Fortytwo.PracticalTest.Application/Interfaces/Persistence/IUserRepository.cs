using Fortytwo.PracticalTest.Domain.Entities;

namespace Fortytwo.PracticalTest.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserIdByUserName(string? userName, CancellationToken cancellationToken);
}