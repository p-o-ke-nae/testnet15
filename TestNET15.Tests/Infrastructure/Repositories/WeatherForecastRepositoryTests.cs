using TestNET15.Infrastructure.Repositories;
using TestNET15.Tests.Infrastructure.TestSupport;
using Xunit;

namespace TestNET15.Tests.Infrastructure.Repositories;

public sealed class WeatherForecastRepositoryTests
{
    [Fact]
    public async Task SaveAsync_Persists_And_Reloads_Entity()
    {
        await using var database = await InfrastructureSqlServerTestDatabase.CreateAsync();
        await using var context = database.CreateDbContext();
        var repository = new WeatherForecastRepository(context);

        var created = TestNET15.Domain.Entities.WeatherForecast.Create(
            new DateOnly(2026, 3, 22),
            24,
            "SQL Server persistence",
            "owner-1",
            false);

        var saved = await repository.SaveAsync(created);
        var reloaded = await repository.FindByIdAsync(saved.Id);

        Assert.NotEqual(0, saved.Id);
        Assert.NotNull(reloaded);
        Assert.Equal(saved.Id, reloaded!.Id);
        Assert.Equal("SQL Server persistence", reloaded.Summary);
        Assert.Equal("owner-1", reloaded.OwnerGoogleUserId);
        Assert.False(reloaded.IsPublic);
    }
}