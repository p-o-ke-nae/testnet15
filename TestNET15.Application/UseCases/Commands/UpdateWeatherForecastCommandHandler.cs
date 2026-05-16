using MediatR;
using TestNET15.Application.Mappers;
using TestNET15.Domain.Ports;

namespace TestNET15.Application.UseCases.Commands;

public class UpdateWeatherForecastCommandHandler : IRequestHandler<UpdateWeatherForecastCommand, UpdateWeatherForecastResponse>
{
    private readonly IWeatherForecastRepository _repository;

    public UpdateWeatherForecastCommandHandler(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateWeatherForecastResponse> Handle(UpdateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.FindByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return new UpdateWeatherForecastResponse
                {
                    Success = false,
                    Message = "Weather forecast not found.",
                    Data = null
                };
            }

            entity.Update(request.Date, request.TemperatureC, request.Summary, request.IsPublic);
            var saved = await _repository.SaveAsync(entity, cancellationToken);

            return new UpdateWeatherForecastResponse
            {
                Success = true,
                Message = "Weather forecast updated successfully.",
                Data = saved.ToWeatherForecastResponseDto()
            };
        }
        catch (Exception ex)
        {
            return new UpdateWeatherForecastResponse
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
        }
    }
}