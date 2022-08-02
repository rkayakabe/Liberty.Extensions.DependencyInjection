# Liberty.Extensions.DependencyInjection

This is an extension to Microsoft.Extensions.DependencyInjection.
Provides the ability to dynamically inject services.

## Examples

<details>
<summary>class A</summary>

```c#
public sealed class A
{
    public A GetA()
    {
        return this;
    }
}
```

</details>

<details>
<summary>class B</summary>

```c#
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
```

</details>

<details>
<summary>class C</summary>

```c#
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
```

</details>

<details>
<summary>class D</summary>

```c#
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
```

</details>

<details>
<summary>class E</summary>

```c#
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
```

</details>

```c#
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
```

## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are warmly welcome.

## Licensing

The code in this project is licensed under MIT license.
