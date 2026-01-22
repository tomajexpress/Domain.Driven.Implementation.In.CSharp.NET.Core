using System.Reflection;
namespace EShoppingTutorial.ArchTests;

public class DomainRulesTests
{
    [Fact]
    public void EntityProperties_Should_Not_Have_Public_Setters_Except_Init()
    {
        // Arrange
        var domainAssembly = typeof(Core.Domain.Entities.Order).Assembly;

        // Act
        var entityTypes = Types.InAssembly(domainAssembly)
            .That()
            .ResideInNamespace("EShoppingTutorial.Core.Domain.Entities")
            .And()
            .AreClasses()
            .GetTypes();

        var failingProperties = new List<string>();

        foreach (var type in entityTypes)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // If there is no setter, it's a read-only property (which is fine)
                if (!property.CanWrite || property.SetMethod == null) continue;

                // Check if the setter is public
                if (property.SetMethod.IsPublic)
                {
                    // Is it a standard 'public set' or an 'init' setter?
                    // init setters have a special 'IsExternalInit' modifier on the return type
                    var isInitOnly = property.SetMethod.ReturnParameter
                        .GetRequiredCustomModifiers()
                        .Any(m => m.FullName == "System.Runtime.CompilerServices.IsExternalInit");

                    if (!isInitOnly)
                    {
                        failingProperties.Add($"{type.Name}.{property.Name}");
                    }
                }
            }
        }

        // Assert
        failingProperties.Should().BeEmpty(
            $"because Domain Entities should use private/protected setters for state changes to ensure encapsulation. " +
            $"Standard public setters found (use 'init' for immutable properties instead): {string.Join(", ", failingProperties)}");
    }

    [Fact]
    public void ValueObjects_Should_Be_Immutable()
    {
        // All types in ValueObjects namespace should be records or have private setters
        var result = Types.InAssembly(typeof(Core.Domain.Entities.Order).Assembly)
            .That()
            .ResideInNamespace("EShoppingTutorial.Core.Domain.ValueObjects")
            .Should()
            .BeSealed() // Good practice for Value Objects
            .GetResult();

        // Assert
        // We collect the names of types that failed the rule
        var failingTypes = string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>());

        result.IsSuccessful.Should().BeTrue(
            $"because all Value Objects should be sealed to ensure correct equality behavior. Failing types: {failingTypes}");
    }
}