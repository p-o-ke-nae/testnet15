using MediatR;
using TestNET15.Application.DTOs;

namespace TestNET15.Application.UseCases.Queries;

public class GetAllWeatherForecastsQuery : IRequest<GetAllWeatherForecastsResponse>
{
    public string? RequestingGoogleUserId { get; set; }
}

public class GetAllWeatherForecastsResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IReadOnlyList<WeatherForecastResponseDto>? Data { get; set; }
}
