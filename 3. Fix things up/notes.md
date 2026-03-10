# Let's fix things up

## Hybrid Cache

Introduced originally in Umbraco 15, depending on how much content your project has and depending on how you are querying this content, you may notice a few broken methods. 

Previously on-boot everything was stored in memory which for content-heavy websites, this meant long startup times. Whereas with Hybrid Cache - Umbraco uses a lazy-load cache on an as-needed basis. Not everything is added to memory allowing for faster boot up times. This does however then mean if you require something that is not in memory then this will need to be loaded into the cache. It also means depending on any code you may have such as .Descendants() .Children() you may find this code is broken, which we have some examples on how to resolve.

What is cached and how much is configurable though, you can add specific document types to an appsetting which will ensure ALL content of that type will be cached. 

This is decided based on an `ISeedKeyProvider` whose role it is to specify what keys need to be seeded.
During startup all of the `ISeedKeyProvider`'s are run and the keys are then seeded into their respective caches `IPublishedContentCache` and `IPublishedMediaCache`

Umbraco runs with two seed key providers out of the box
- **ContentTypeSeedKeyProvider**
  - Which reads the appsetting `Umbraco:CMS:Cache:ContentTypeKeys` for any content types you want to cache
- **BreadthFirstKeyProvider**
  - Which does a breadth-first traversal of the content and media, seeding N number of content as specified in the appsettings `Umbraco:CMS:Cache:DocumentBreadthFirstSeedCount` and `Umbraco:CMS:Cache:MediaBreadthFirstSeedCount`

However, you are of course able to run your own `ISeedKeyProvider` if you need more control.

---


Official docs: https://docs.umbraco.com/umbraco-cms/reference/configuration/cache-settings

Seeding: https://docs.umbraco.com/umbraco-cms/reference/cache/cache-seeding

## XPATH

We've known about the removal of `XPATH` for a while now, so hopefully this isn't used too widely in your projects, as this can be quite a painful one.

Firstly, as part of the `Build Solution` step, you may have commented out any code which was showing that the previous methods used to get content via `XPATH` are no longer available.

The second issue you may not notice at first, which is the `Multinode Tree Picker` - if you have any data types using this along with `XPATH`, it will no longer work and in fact the upgrade path would have removed the `XPATH` query data entirely. For this, you'll need to query the V13 database to find any `Multinode Tree Pickers` along with their configuration which you can then use to migrate the data type to use `Dynamic Root` instead.  Depending on how many you have, this could be quite time-consuming, you can do what I prefer, which is to compare the V13 and the V17 systems and adjust the `Multinode Tree Picker` to use the `Dynamic Root` then use uSync to check-in the changes into your repo, so when you deploy, the changes are persisted. Alternatively you can create custom migration scripts, to fix the issue automatically but this requires the previous configuration being available - so this may be easier to do BEFORE doing the V17 upgrade.

## Newtonsoft.Json

It has been migrated to `System.Text.Json` and is one of the changes I incorrectly assumed wasn't too much of a big deal. To quote the documentation:

> Although this sounds like it's not a big change, it’s one of the most breaking changes on the backend. Whereas Newtonsoft.Json was flexible and error-tolerant by default, System.Text.Json is strict but more secure by default. You can therefore run into things that will not be serialized.

Which is exactly what we've found, be it custom data types, property editors, API controllers or even Property Value Converters you will want to either migrate these to `System.Text.Json` or at a minimum - do full regression testing to find any potential issues. 

I've included a couple of examples for `Property Value Converters`.

## Umbraco Flavored Markdown

With the removal of AngularJS, any advanced labels you have configured; such as those in Blocks and Collection Views will now be broken - the fix is to update the syntax of the labels to UFM. I've included some examples and links to the documentation and to a blog post by Joe Glombek which is really useful.

We had lot's of instances of advanced labels, so I've also included a SQL Script you can run on your database to aid you in finding them all. 

https://docs.umbraco.com/umbraco-cms/reference/umbraco-flavored-markdown
https://24days.in/umbraco-cms/2025/template-for-success/

## Macros
## Custom Umbraco Extensions