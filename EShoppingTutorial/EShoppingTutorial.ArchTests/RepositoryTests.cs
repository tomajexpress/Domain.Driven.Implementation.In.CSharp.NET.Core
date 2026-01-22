namespace EShoppingTutorial.ArchTests;

public class RepositoryTests
{
    [Fact]
    public void RepositoryInterfaces_Should_Be_In_Domain_Repositories_Namespace()
    {
        var _domainAssembly = typeof(Core.Domain.IUnitOfWork).Assembly;

        // Rule: If it's an interface and ends in 'Repository', it MUST be in the correct namespace
        var result = Types.InAssembly(_domainAssembly)
                .That()
                .AreClasses()
                .And()
                .HaveNameEndingWith("Repository")
                .Should()
                // This is a "fail-fast" rule: 
                // We say they should be in a namespace that DOES NOT exist in this project
                .ResideInNamespace("EShoppingTutorial.Core.Persistence")
                .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(
            $"Architecture Violation: Domain should only contain interfaces. " +
            $"Implementations Repository Classes found: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Persistence_Should_Not_Contain_Any_Interfaces()
    {
        // Arrange
        var persistenceAssembly = typeof(Core.Persistence.UnitOfWork).Assembly;

        // Act: Scan for ANY interface in the Persistence project
        var result = Types.InAssembly(persistenceAssembly)
            .That()
            .AreInterfaces()
            .Should()
            // This is a "fail-fast" rule: 
            // We say they should be in a namespace that DOES NOT exist in this project
            .ResideInNamespace("EShoppingTutorial.Core.Domain")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue(
            $"Architecture Violation: Persistence should only contain implementations (Classes). " +
            $"Interfaces found: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }
}