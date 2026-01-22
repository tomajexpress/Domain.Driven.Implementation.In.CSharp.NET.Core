namespace EShoppingTutorial.ArchTests;

public class LayerTests
{
    private const string ApplicationNamespace = "EShoppingTutorial.Core.Application";
    private const string InfrastructureNamespace = "EShoppingTutorial.Infrastructure";
    private const string PersistenceNamespace = "EShoppingTutorial.Core.Persistence";

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Other_Layers()
    {
        var assembly = typeof(Core.Domain.Entities.Order).Assembly;

        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(ApplicationNamespace, InfrastructureNamespace, PersistenceNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue($"Domain layer must be pure. Failures: {GetFailingTypes(result)}");
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Infrastructure_Or_Persistence()
    {
        var assembly = typeof(Core.Application.Orders.Commands.CreateOrder.CreateOrderCommand).Assembly;

        var result = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll(InfrastructureNamespace, PersistenceNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue($"Application layer should only depend on Domain. Failures: {GetFailingTypes(result)}");
    }

    private string GetFailingTypes(TestResult result) => 
        string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>());
}