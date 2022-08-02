namespace Liberty.Extensions.DependencyInjection.Tests.Services;

public sealed class D
{
    private readonly C _c;

    public D(C c)
    {
        _c = c;
    }

    public A GetA()
    {
        return _c.GetA();
    }
}