# Key Changes/Information
This file will be used to include any example code,any additional information and troubleshooting.

## Update projects to use .NET 10

Update all `csproj` files to target .NET 10

```cs
 <TargetFramework>net10.0</TargetFramework>
```

## Program.cs

Remove this line, it is no longer required.

```cs
u.UseInstallerEndpoints();
```

### Smidge
If you're using Smidge.


```cs
builder.CreateUmbracoBuilder()
    ....
    .AddRuntimeMinifier()
```

```cs
app.UseSmidge();
await app.RunAsync();
```

## Generated Models

If you have generated models and have build issues you can fix this by doing the following, which will allow you to build the solution. Once the upgrade is finished you can then regenerate which will fix any other gaps, but this should be enough to get the solution building.

Replace `IPublishedSnapshotAccessor` with `IPublishedContentTypeCache`

Replace `publishedSnapshotAccessor` with `contentTypeCache`

## Umbraco.Cms.Web.BackOffice

This has been removed, any NuGet package references to it will need to be removed.

## Umbraco.GetDictionaryValue

This has been removed, update your code to use

```cs
Umbraco.GetDictionaryValueOrDefault("REF", "Fallback");
```