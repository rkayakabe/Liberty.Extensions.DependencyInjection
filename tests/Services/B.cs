namespace Liberty.Extensions.DependencyInjection.Tests.Services;

public sealed class B
{
    private readonly A _a;

    public B(A a)
    {
        _a = a;
    }

    public A GetA()
    {
        return _a;
    }
}