using Liberty.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Liberty.Extensions.DependencyInjection.Tests;

public class DynamicScopeTest
{
    [Fact]
    public void ThrowOutOfScopeExceptionTest()
    {
        using var provider = BuildDynamicStringProvider();
        using var scope = provider.CreateScope();
        Assert.Throws<InvalidOperationException>(() => scope.ServiceProvider.GetRequiredService<string>());
    }

    [Fact]
    public void ThrowNotFoundServiceExceptionTest()
    {
        using var provider = BuildDynamicStringProvider();
        using var scope = provider.CreateDynamicScope();
        Assert.Throws<InvalidOperationException>(() => scope.ServiceProvider.GetRequiredService<string>());
    }

    [Fact]
    public void GetDynamicServiceTest()
    {
        using var provider = BuildDynamicStringProvider();
        using var scope = provider.CreateDynamicScope("test");
        Assert.Equal("test", scope.ServiceProvider.GetRequiredService<string>());
    }

    [Fact]
    public void GetDynamicServiceOverScopeTest()
    {
        using var provider = BuildDynamicStringProvider();
        using var scope = provider.CreateDynamicScope("test");
        using var scope2 = scope.ServiceProvider.CreateDynamicScope();
        Assert.Equal("test", scope2.ServiceProvider.GetRequiredService<string>());
    }

    [Fact]
    public void ThrowDuplicateDynamicServiceException()
    {
        using var provider = BuildDynamicStringProvider();
        using var scope = provider.CreateDynamicScope("test");
        Assert.Throws<ArgumentException>(() =>
        {
            using var _ = scope.ServiceProvider.CreateDynamicScope("test2");
        });
    }

    private static ServiceProvider BuildDynamicStringProvider()
    {
        return new ServiceCollection()
            .AddDynamicScoped<string>()
            .BuildServiceProvider();
    }
}