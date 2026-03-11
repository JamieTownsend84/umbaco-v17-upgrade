using System.Text.Json.Nodes;
using Umbraco.Cms.Core.Manifest;
using Umbraco.Cms.Infrastructure.Manifest;

internal sealed class ManifestReader : IPackageManifestReader
{
    public Task<IEnumerable<PackageManifest>> ReadPackageManifestsAsync()
    {
        var assembly = typeof(ManifestReader).Assembly;
        var name = "Shout.Migration.Example";
        var version = GetPackageVersion();

        List<PackageManifest> manifest = [new PackageManifest
        {
            Id = name,
            Name = name,
            AllowTelemetry = true,
            Version = GetPackageVersion(),
            Extensions = [
                new JsonObject {
                    ["name"] = $"{name.Replace("."," ")} - Hide Sections",
                    ["alias"] = $"{name.ToLower()}.hide.sections",
                    ["type"] = "backofficeEntryPoint",
                    ["js"] = $"/App_Plugins/{name}/scripts/hide-sections.js?v={version}"
                }]
        }];

        return Task.FromResult(manifest.AsEnumerable());
    }

    private string GetPackageVersion()
    {
        var assembly = typeof(ManifestReader).Assembly;
        return assembly.GetName()?.Version?.ToString(3) ?? "17.0.0";
    }
}