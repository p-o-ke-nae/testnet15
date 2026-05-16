using MediatR;
using TestNET15.Application.DTOs;

namespace TestNET15.Application.UseCases.Queries;

public class GetWeatherForecastByIdQuery : IRequest<GetWeatherForecastByIdResponse>
{
    public int Id { get; set; }
    public string? RequestingGoogleUserId { get; set; }
}

public class GetWeatherForecastByIdResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public WeatherForecastResponseDto? Data { get; set; }
}
