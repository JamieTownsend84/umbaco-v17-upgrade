# Getting Started

We recommend doing a full backup of media and databases, then restoring them locally as the upgrade can take in our experience around 30/40 minutes for an average-sized website.

## Review installed Umbraco Packages

Do a review of all installed Umbraco packages, and check if they have a .NET 10 / Umbraco 17 targeted version.
In our experience most popular packages do.


## Review other NuGet packages

Do a full review of any NuGet dependencies and ensure they are or have versions which are compatible with .NET 10

## Any custom backoffice extensions 

Does the project have any backoffice extensions, such as AngularJS.
Any custom logic will no longer work and will need to be re-worked, depending on the extension this can be quite a difficult task, especially if there is a skill gap with Lit / web components.

## Make a plan

With the bigger picture now known, make a plan for any gaps such as any Umbraco Packages which do not have a .NET 10 / Umbraco 17 targeted version. Any NuGet packages which are either not compatible with .NET 10 or need updating, you'll need to plan for any breaking changes this may cause.