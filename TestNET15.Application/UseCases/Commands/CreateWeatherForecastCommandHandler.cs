using MediatR;
using TestNET15.Application.DTOs;
using TestNET15.Application.Mappers;
using TestNET15.Domain.Ports;

namespace TestNET15.Application.UseCases.Commands;

public class CreateWeatherForecastCommandHandler : IRequestHandler<CreateWeatherForecastCommand, CreateWeatherForecastResponse>
{
    private readonly IWeatherForecastRepository _repository;

    public CreateWeatherForecastCommandHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateWeatherForecastResponse> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = Domain.Entities.WeatherForecast.Create(
                request.Date,
                request.TemperatureC,
                request.Summary,
                request.OwnerGoogleUserId,
                request.IsPublic
            );

            var saved = await _repository.SaveAsync(entity, cancellationToken);
            var responseDto = saved.ToWeatherForecastResponseDto();

            return new CreateWeatherForecastResponse
            {
                Success = true,
                Message = "Weather forecast created successfully.",
                Data = responseDto
            };
        }
        catch (Exception ex)
        {
            return new CreateWeatherForecastResponse
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
        }
    }
}
