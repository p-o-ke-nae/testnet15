using TestNET15.Domain.Entities;
using TestNET15.Infrastructure.Data.Models;

namespace TestNET15.Infrastructure.Mappers;

public static class PersistedUserAuthorizationInfoMapper
{
    public static UserAuthorizationInfo ToDomainEntity(this PersistedUserAuthorizationInfo persisted)
    {
        return UserAuthorizationInfo.Restore(
            persisted.GoogleUserId,
            persisted.Role,
            persisted.Permissions.Select(permission => permission.Permission));
    }
}