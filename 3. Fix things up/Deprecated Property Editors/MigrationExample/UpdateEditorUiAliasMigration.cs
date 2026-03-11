using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Packaging;

public class UpdateEditorUiAliasMigration(
        IPackagingService packagingService,
        IMediaService mediaService,
        MediaFileManager mediaFileManager,
        MediaUrlGeneratorCollection mediaUrlGenerators,
        IShortStringHelper shortStringHelper,
        IContentTypeBaseServiceProvider contentTypeBaseServiceProvider,
        IMigrationContext context,
        IOptions<PackageMigrationSettings> packageMigrationsSettings,
        BaseMigrationHelper baseMigrationHelper) : AsyncPackageMigrationBase(packagingService, mediaService, mediaFileManager, mediaUrlGenerators, shortStringHelper, contentTypeBaseServiceProvider, context, packageMigrationsSettings)
{
    protected override async Task MigrateAsync()
    {
        var dataTypeKey = Guid.Parse("00000000-0000-0000-0000-000000000000");
        var editorAlias = "Umbraco.Plain.String";
        var editorUiAlias = "Shout.ColourPicker";

        var dataType = await dataTypeService.GetAsync(dataTypeKey);
        if (dataType == null)
            throw new Exception("Data Type not found");

        dataType.EditorUiAlias = editorUiAlias;
        dataType.Editor = new DataEditor(dataValueEditorFactory) { Alias = editorAlias };
        await dataTypeService.UpdateAsync(dataType, Umbraco.Cms.Core.Constants.Security.SuperUserKey);
    }
}