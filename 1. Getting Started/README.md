# Getting Started

I am not going to get into 'Why upgrade' - I want this talk to focus on the developer. You've been tasked with upgrading and this talk will give you the information and guidance to do so. But v17 and .NET 10 are the next/current LTS versions respectively, so you'll most likely be doing this upgrade.

## Upgrade to the latest v13

In my experience, this is not strictly necessary and I have not had any issues when skipping this step, but the official recommendation is to upgrade to the latest v13 version before doing the upgrade to v17, so let's do this first.

## Backup

We recommend doing a full backup of media and database, then restoring them locally as the upgrade can take in our experience around 30–40 minutes for an average-sized website.

## Review installed Umbraco Packages

Do a review of all installed Umbraco packages, and check if they have a .NET 10 / Umbraco 17 targeted version.
In our experience, most popular packages do. If not, maybe reach out or do a PR.


## Review other NuGet packages

Do a full review of any NuGet dependencies and ensure they are or have versions which are compatible with .NET 10

## Any custom backoffice extensions

Does the project have any backoffice extensions such as using AngularJS?
Any custom logic will no longer work and will need to be re-worked. Depending on the extension this can be quite a difficult task, especially if there is a skill gap with TypeScript, Lit, or Web Components.

## Deprecated Property Editors & Macros

We'll talk about these in more detail later, but I've included it here as a consideration. If you have a website with heavy usage of `Deprecated Property Editors` or `Macros` it may be better to do the migration for these *BEFORE* upgrading to V17.

## Make a plan

With the bigger picture now known, make a plan for any gaps such as any Umbraco Packages which do not have a .NET 10 / Umbraco 17 targeted version. For any NuGet packages which are either not compatible with .NET 10 or need updating, you'll need to plan for any breaking changes this may cause.


