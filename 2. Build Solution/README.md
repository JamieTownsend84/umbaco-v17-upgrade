# Build Solution

Now we have a plan, I recommend next getting the project building. There will most likely be lots of build errors. I personally go through and fix any easy ones and any others I comment out with TODO: to come back to. This essentially allows you to work on specific pieces of code, side by side with the V13 branch and ensure any new code you introduce works as expected.

## Update packages

As part of the getting started, update the packages to versions compatible with Umbraco 17 and .NET 10. Any gaps, I recommend getting the project building first and working through each in isolation.

### Razor runtime compilation

One noted potential breaking change is the removing of `Razor runtime compilation` and the Models Builder mode `InMemoryAuto` moved to its own NuGet package. If you use or want to use either of these features you need to install this package `Umbraco.Cms.DevelopmentMode.Backoffice`
https://www.nuget.org/packages/Umbraco.Cms.DevelopmentMode.Backoffice/

However, it is recommended not to use Razor runtime compilation as Microsoft have marked this as **Obsolete**.  This was also holding Umbraco back implementing `hot-reload` which is now included OTB.

### Smidge

Smidge is now *NOT* included in the core. If you want to continue the use of this, you'll need to install this package and configure.
https://marketplace.umbraco.com/package/umbraco.community.smidge

### TinyMCE

As you may or may not know, Umbraco have moved away from TinyMCE in favour of TipTap RTE. The reason for this is that TinyMCE changed their license from `MIT` to `GPLv2` - this matters as `MIT` allowed for commercial / closed-source project use.  However `GPLv2` is more restrictive in that any code we as developers write needs to also be open-sourced in order to use it without a licence. You are able to purchase a licence for the continued use of TinyMCE and this may be an option initially depending on your needs. But it's worth noting not only is there a fee, there is also a limit in how many times the editor can `load` which risks the costs escalating. 

Thankfully, if you do not do much custom with TinyMCE this migration to TipTap will be pretty much automatic as soon as you upgrade to v17. The biggest issue we have had with TipTap is that it does not seem OTB it supports the targeting styles dropdown that we had in TinyMCE, such as when you select a `table` for example you could show specific styles available to just that content. We are yet to figure this out and may need some custom TipTap extensions, so take this into consideration before making the decision.

If you want to continue to use TinyMCE, the good people at `ProWorks` have released a package which needs to be installed before you do the upgrade as it will prevent the migration to TipTap and will add all dependencies needed for TinyMCE - and everything else should just work as before, so this may be an option originally with the view to move away to TipTap when comfortable. 
https://github.com/ProWorksCorporation/TinyMCE-Umbraco

Those interested in more information on the licence change with TinyMCE, you can check this feed out
https://github.com/tinymce/tinymce/issues/9453

