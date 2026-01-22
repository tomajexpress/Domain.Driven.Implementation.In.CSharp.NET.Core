namespace EShoppingTutorial.ArchTests;

public class ConventionTests
{
    private readonly System.Reflection.Assembly _applicationAssembly = typeof(Core.Application.ApplicationDependencyInjection).Assembly;

    [Fact]
    public void CommandHandlers_Should_Be_Internal()
    {
        var result = Types.InAssembly(_applicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .NotBePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue("because handlers contain the business execution logic and should not be exposed.");
    }

    [Fact]
    public void Handlers_Should_ResideIn_ApplicationNamespace_And_HaveCorrectName()
    {
        // Arrange & Act
        var result = Types.InAssembly(_applicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .ResideInNamespace("EShoppingTutorial.Core.Application")
            .GetResult();

        // Assert
        var failingTypes = string.Join(", ", result.FailingTypeNames ?? Enumerable.Empty<string>());
        result.IsSuccessful.Should().BeTrue(
            $"because all MediatR Handlers must be located in the Application layer and follow naming conventions. Failing types: {failingTypes}");
    }
}