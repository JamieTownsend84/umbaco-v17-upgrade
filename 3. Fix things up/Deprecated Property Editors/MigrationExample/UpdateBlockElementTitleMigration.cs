using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Packaging;

public class UpdateBlockElementTitleMigration(
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
        var blockGridName = "Component - Site Settings - Navigation Block List";
        var elementAlias = "megaNavItem";
        var newLabel = "Mega Nav - ${propertyAlias}";

        var dataType = await dataTypeService.GetAsync(blockGridName) as DataType;
        if (dataType == null)
        {
            return;
        }

        var element = contentTypeService.Get(elementAlias);
        if (element == null) { return; }

        var configDict = dataType.ConfigurationData;

        JsonArray blocksArray;
        if (configDict.TryGetValue("blocks", out var blocksNode) && blocksNode is JsonArray existingArray)
        {
            blocksArray = existingArray;
        }
        else
        {
            blocksArray = new JsonArray();
            configDict["blocks"] = blocksArray;
        }

        JsonObject? blockObj = null;
        foreach (var node in blocksArray)
        {
            if (node is not JsonObject obj)
                continue;

            var keyString = obj["contentElementTypeKey"]?.GetValue<string>();
            if (Guid.TryParse(keyString, out var contentKey) && contentKey == element.Key)
            {
                blockObj = obj;
                break;
            }
        }

        if (blockObj != null)
        {
            blockObj["label"] = newLabel;
        }

        await dataTypeService.UpdateAsync(dataType, Umbraco.Cms.Core.Constants.Security.SuperUserKey);
    }
}