namespace Liberty.Extensions.DependencyInjection.Tests.Services;

public sealed class C
{
    private readonly A _a;

    public C(A a)
    {
        _a = a;
    }

    public A GetA()
    {
        return _a;
    }
}