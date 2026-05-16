using TestNET15.Domain.Entities;

namespace TestNET15.Application.Authorization;

public interface IWeatherForecastAccessEvaluator
{
    bool CanRead(WeatherForecast weatherForecast, string? googleUserId, UserAuthorizationInfo? userAuthorizationInfo);

    bool CanManage(WeatherForecast weatherForecast, string? googleUserId, UserAuthorizationInfo? userAuthorizationInfo);
}