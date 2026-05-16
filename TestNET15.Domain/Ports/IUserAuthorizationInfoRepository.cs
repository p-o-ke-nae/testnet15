using TestNET15.Domain.Entities;

namespace TestNET15.Domain.Ports;

public interface IUserAuthorizationInfoRepository
{
    Task<UserAuthorizationInfo?> FindByGoogleUserIdAsync(string googleUserId, CancellationToken cancellationToken = default);
}