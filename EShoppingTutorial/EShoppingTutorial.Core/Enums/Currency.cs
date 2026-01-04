namespace EShoppingTutorial.Core.Domain.Enums;

/// <summary>
/// Defines the currency units supported by the domain.
/// We explicitly set 'Unspecified = 0' to prevent default-value logic errors.
/// </summary>
public enum Currency
{
    Unspecified = 0,
    Rial = 1,
    USD = 2,
    EUR = 3,
}
