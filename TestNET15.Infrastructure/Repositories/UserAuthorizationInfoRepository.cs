using Microsoft.EntityFrameworkCore;
using TestNET15.Domain.Entities;
using TestNET15.Domain.Ports;
using TestNET15.Infrastructure.Data;
using TestNET15.Infrastructure.Mappers;

namespace TestNET15.Infrastructure.Repositories;

public class UserAuthorizationInfoRepository : IUserAuthorizationInfoRepository
{
    private readonly AppDbContext _context;

    public UserAuthorizationInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserAuthorizationInfo?> FindByGoogleUserIdAsync(string googleUserId, CancellationToken cancellationToken = default)
    {
        var persisted = await _context.PersistedUserAuthorizationInfos
            .Include(user => user.Permissions)
            .FirstOrDefaultAsync(user => user.GoogleUserId == googleUserId, cancellationToken);

        return persisted?.ToDomainEntity();
    }
}