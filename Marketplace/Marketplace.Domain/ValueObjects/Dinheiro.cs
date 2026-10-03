using Marketplace.Domain.Exceptions;

namespace Marketplace.Domain.ValueObjects;

public class Dinheiro
{
    public decimal Valor { get; }

    public Dinheiro(decimal valor)
    {
        if (valor < 0)
        {
            throw new DomainException("Valor monetário não pode ser negativo");
        }

        Valor = decimal.Round(valor, 2, MidpointRounding.AwayFromZero);
    }

    public static Dinheiro Zero => new(0m);
    
    public static Dinheiro operator +(Dinheiro a, Dinheiro b) => new(a.Valor + b.Valor);
    public static Dinheiro operator -(Dinheiro a, Dinheiro b) => new(a.Valor - b.Valor);
    public static Dinheiro operator *(Dinheiro a, int quantidade) => new(a.Valor* quantidade);

    public static bool operator >(Dinheiro a, Dinheiro b) => a.Valor > b.Valor;
    public static bool operator <(Dinheiro a, Dinheiro b) => a.Valor < b.Valor;
    public static bool operator >=(Dinheiro a, Dinheiro b) => a.Valor >= b.Valor;
    public static bool operator <=(Dinheiro a, Dinheiro b) => a.Valor <= b.Valor;

}