# EnvironmentView

This sample demonstrates the `EnvironmentView` component for environment-based conditional rendering, similar to the MVC environment tag helper.

```html
<EnvironmentView Include="Development">
    <div>Debug mode enabled</div>
</EnvironmentView>

<EnvironmentView Exclude="Production">
    <p>@DateTime.Now</p>
</EnvironmentView>
```

`Include`/`Exclude` accept comma-separated environment names and match case-insensitively. 

How to test:

`
dotnet run -e ASPNETCORE_ENVIRONMENT=Development
`

`dotnet run -e ASPNETCORE_ENVIRONMENT=Staging
`

`
dotnet run -e ASPNETCORE_ENVIRONMENT=Production
`