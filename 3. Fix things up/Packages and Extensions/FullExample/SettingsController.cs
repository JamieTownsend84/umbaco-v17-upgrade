using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Umbraco.Cms.Api.Management.Controllers;
using Umbraco.Cms.Api.Management.Routing;

[VersionedApiBackOfficeRoute("shout/base/settings")]
[ApiExplorerSettings(GroupName = "Shout Base Settings")]
public class SettingsController(IConfiguration configuration) : ManagementApiControllerBase
{
    [HttpGet("getShoutBaseSettings")]
    public IActionResult GetSettings()
    {
        // {
        //     "Components": {
        //         "Base": {
        //             "HideUmbracoContentSection": true,
        //             "HideTranslationSection": true,
        //             "HideMemberSection": true,
        //             "HidePackageSection": false,
        //             "HideFormsSection": true,
        //             "HideRedirectDashboard": true,
        //             "HideTheDashboardSection": false,
        //             "HideSettingsWelcomeDashboard": true
        //         }
        //     }
        // }

        var settings = new AppSettings();
        configuration.Bind("Components:Base", settings);

        return Ok(settings);
    }
}