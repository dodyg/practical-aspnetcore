# Custom binding - complete control

This shows how to take full control of the binding process. Make sure that the parameter does not use any of the binding attributes e.g. `[FromHeader]` or `[FromQuery]`.

Your type must implement this static method:
``` csharp
public static ValueTask<T?> BindAsync(HttpContext context, ParameterInfo parameter);
```