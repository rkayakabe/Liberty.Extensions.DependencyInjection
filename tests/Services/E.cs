namespace Liberty.Extensions.DependencyInjection.Tests.Services;

public sealed class E
{
    private readonly A _a;
    private readonly string _str;

    public E(A a, string str)
    {
        _a = a;
        _str = str;
    }

    public A GetA()
    {
        return _a;
    }

    public string GetString()
    {
        return _str;
    }
}