namespace TestNET15.Authentication;

public interface IGoogleAccessTokenValidationService
{
    Task<GoogleAccessTokenValidationResult> ValidateAsync(string accessToken, CancellationToken cancellationToken = default);
}