namespace Marketplace.Infrastructure.Settings;

public class JwtSettings
{
    public const string Secao = "Jwt";

    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Secret { get; set; }
    public int ExpiracaoEmMinutos { get; set; }
}