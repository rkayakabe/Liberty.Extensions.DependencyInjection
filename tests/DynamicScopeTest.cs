using Liberty.Extensions.DependencyInjection.Extensions;
using Liberty.Extensions.DependencyInjection.Tests.Services;
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

    [Fact]
    public void ReadMeTest()
    {
        using var provider = new ServiceCollection()
            .AddDynamicScoped<string>()
            .AddDynamicScoped<A>()
            .AddScoped<B>()
            .AddScoped<C>()
            .AddScoped<D>()
            .AddScoped<E>()
            .BuildServiceProvider();
        using var scope = provider.CreateDynamicScope(new A());
        var a = scope.ServiceProvider.GetRequiredService<A>();
        var b = scope.ServiceProvider.GetRequiredService<B>();
        var c = scope.ServiceProvider.GetRequiredService<C>();
        var d = scope.ServiceProvider.GetRequiredService<D>();
        using var scope2 = scope.ServiceProvider.CreateDynamicScope("");
        var e = scope2.ServiceProvider.GetRequiredService<E>();
        Assert.Same(a.GetA(), b.GetA());
        Assert.Same(b.GetA(), c.GetA());
        Assert.Same(c.GetA(), d.GetA());
        Assert.Same(d.GetA(), e.GetA());
    }

    private static ServiceProvider BuildDynamicStringProvider()
    {
        return new ServiceCollection()
            .AddDynamicScoped<string>()
            .BuildServiceProvider();
    }
}