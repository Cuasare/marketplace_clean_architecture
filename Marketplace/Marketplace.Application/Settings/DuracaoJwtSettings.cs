namespace Marketplace.Infrastructure.Settings;

public class DuracaoJwtSettings
{
    public const string Secao = "Jwt";

    public int ExpiracaoEmMinutos { get; set; }
    public int ExpiracaoEmMinutosLembrarDeMim { get; set; }
}