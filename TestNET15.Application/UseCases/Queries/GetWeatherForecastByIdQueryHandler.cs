using MediatR;
using TestNET15.Application.Authorization;
using TestNET15.Application.DTOs;
using TestNET15.Application.Mappers;
using TestNET15.Domain.Exceptions;
using TestNET15.Domain.Ports;

namespace TestNET15.Application.UseCases.Queries;

public class GetWeatherForecastByIdQueryHandler : IRequestHandler<GetWeatherForecastByIdQuery, GetWeatherForecastByIdResponse>
{
    private readonly IWeatherForecastAccessEvaluator _accessEvaluator;
    private readonly IWeatherForecastRepository _repository;
    private readonly IUserAuthorizationInfoRepository _userAuthorizationInfoRepository;

    public GetWeatherForecastByIdQueryHandler(
        IWeatherForecastRepository repository,
        IUserAuthorizationInfoRepository userAuthorizationInfoRepository,
        IWeatherForecastAccessEvaluator accessEvaluator)
    {
        _repository = repository;
        _userAuthorizationInfoRepository = userAuthorizationInfoRepository;
        _accessEvaluator = accessEvaluator;
    }

    public async Task<GetWeatherForecastByIdResponse> Handle(GetWeatherForecastByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.FindByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new WeatherForecastNotFoundException(request.Id);
            }

            var userAuthorizationInfo = await loadUserAuthorizationInfoAsync(request.RequestingGoogleUserId, cancellationToken);
            if (!_accessEvaluator.CanRead(entity, request.RequestingGoogleUserId, userAuthorizationInfo))
            {
                throw new WeatherForecastNotFoundException(request.Id);
            }

            var dto = entity.ToWeatherForecastResponseDto();

            return new GetWeatherForecastByIdResponse
            {
                Success = true,
                Message = "Weather forecast retrieved successfully.",
                Data = dto
            };
        }
        catch (DomainException ex)
        {
            return new GetWeatherForecastByIdResponse
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
        }
        catch (Exception ex)
        {
            return new GetWeatherForecastByIdResponse
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };
        }
    }

    private async Task<TestNET15.Domain.Entities.UserAuthorizationInfo?> loadUserAuthorizationInfoAsync(string? googleUserId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(googleUserId))
        {
            return null;
        }

        return await _userAuthorizationInfoRepository.FindByGoogleUserIdAsync(googleUserId, cancellationToken);
    }
}
