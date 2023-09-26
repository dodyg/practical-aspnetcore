# Implement multiple IExceptionHandler to handle unhandled exceptions

Implement multiple `IExceptionHandler` for handling exceptions.

```csharp
builder.Services.AddExceptionHandler<TimeOutHandler>();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();
```

In this case **the order** of the handles are **important**. `TimeOutHandler` is called first then `DefaultExceptionHandler`.  