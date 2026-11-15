using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/v1/auth/login", (LoginRequest request, IConfiguration config) =>
{
    var errors = LoginValidation.Validate(request);
    if (errors.Count > 0)
    {
        return Results.BadRequest(new ErrorResponse(false,
            new ApiError("VALIDATION_ERROR", "Одно или несколько полей имеют недопустимое значение.", Guid.NewGuid().ToString("N"))));
    }

    // Учебный пример: ожидаемые значения берутся только из переменных окружения.
    // В реальной системе пароль проверяется через хранилище с солёным хешем,
    // а access token выпускается доверенным провайдером идентификации.
    var expectedUser = config["AUTH_DEMO_USERNAME"];
    var expectedPassword = config["AUTH_DEMO_PASSWORD"];
    if (string.IsNullOrEmpty(expectedUser) || string.IsNullOrEmpty(expectedPassword))
    {
        return Results.Problem("Демо-авторизация не настроена.", statusCode: 503);
    }

    var usernameMatches = FixedEquals(request.Username, expectedUser);
    var passwordMatches = FixedEquals(request.Password, expectedPassword);
    var clientMatches = FixedEquals(request.ClientId, "load-management-web");
    if (!usernameMatches || !passwordMatches || !clientMatches)
    {
        return Results.Json(new ErrorResponse(false,
            new ApiError("AUTH_INVALID_CREDENTIALS", "Не удалось выполнить вход с указанными данными.", Guid.NewGuid().ToString("N"))), statusCode: 401);
    }

    // Токен в этом примере случаен и не является JWT или полноценной сессией.
    var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    return Results.Ok(new LoginSuccess(true, token, "Bearer", 900,
        new AuthenticatedUser("usr-demo", "Демонстрационный пользователь", ["Methodist"])));
});

app.Run();

static bool FixedEquals(string supplied, string expected)
{
    var a = Encoding.UTF8.GetBytes(supplied);
    var b = Encoding.UTF8.GetBytes(expected);
    return a.Length == b.Length && CryptographicOperations.FixedTimeEquals(a, b);
}

public sealed record LoginRequest(string? Username, string? Password, string? ClientId);
public sealed record LoginSuccess(bool Success, string AccessToken, string TokenType, int ExpiresIn, AuthenticatedUser User);
public sealed record AuthenticatedUser(string Id, string DisplayName, string[] Roles);
public sealed record ErrorResponse(bool Success, ApiError Error);
public sealed record ApiError(string Code, string Message, string TraceId);

public static class LoginValidation
{
    private static readonly Regex UsernamePattern = new("^[A-Za-z0-9._@-]{3,64}$", RegexOptions.CultureInvariant);
    private static readonly Regex ClientPattern = new("^[A-Za-z0-9._-]{3,64}$", RegexOptions.CultureInvariant);

    public static List<string> Validate(LoginRequest request)
    {
        var errors = new List<string>();
        if (request.Username is null || !UsernamePattern.IsMatch(request.Username)) errors.Add("username");
        if (request.Password is null || request.Password.Length is < 8 or > 128 || string.IsNullOrWhiteSpace(request.Password)) errors.Add("password");
        if (request.ClientId is null || !ClientPattern.IsMatch(request.ClientId)) errors.Add("clientId");
        return errors;
    }
}
