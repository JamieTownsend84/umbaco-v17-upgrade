public class Composer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddSingleton<IPackageManifestReader, ManifestReader>();
    }
}