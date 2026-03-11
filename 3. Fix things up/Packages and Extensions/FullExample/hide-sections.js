export const onInit = async (host) => {
    try {

        const authContext = await host.getContext('UmbAuthContext');
        const token = await authContext.getLatestToken();

        const response = await fetch('/umbraco/management/api/v1/shout/base/settings/getShoutBaseSettings', {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        const settings = await response.json();

        if (settings.hideTranslationSection) {
            host.extensionRegistry.exclude('Umb.Section.Translation');
        }

        if (settings.hidePackageSection) {
            host.extensionRegistry.exclude('Umb.Section.Packages');
        }

        if (settings.hideMemberSection) {
            host.extensionRegistry.exclude('Umb.Section.Members');
        }

        if (settings.hideFormsSection) {
            host.extensionRegistry.exclude('Umb.Section.Forms');
        }

        if (settings.hideUmbracoContentSection) {
            host.extensionRegistry.exclude('Umb.Dashboard.UmbracoNews');
        }

        if (settings.hideRedirectDashboard) {
            host.extensionRegistry.exclude('Umb.Dashboard.RedirectManagement');
        }

        if (settings.hideSettingsWelcomeDashboard) {
            host.extensionRegistry.exclude('Umb.Dashboard.SettingsWelcome');
        }

        if (settings.hideTheDashboardSection) {
            host.extensionRegistry.exclude('TheDashboard.Dashboard');
        }


    } catch (error) {
        console.error('Failed to load section settings', error);
    }
};