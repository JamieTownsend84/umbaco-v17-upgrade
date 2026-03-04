# Let's fix things up

## Hybrid Cache

Introduced originally in Umbraco 15, depending on how much content your project has and depending on how you are querying this content you may notice a few broken methods. 

Previously on-boot everything was stored in memory which for content heavy websites this meant long startup times. Whereas with Hybrid Cache - Umbraco uses a lazy-load cache on an as-needed basis. Not everything is added to memory allowing for faster boot up times. This does however then mean if you require something that is not in memory then this will need to be loaded into the cache. It also means depending on any code you may have such as .Descendants() .Children() you may find this code is broken, which we have some examples on how to resolve.

What is cached and how much is configurable though, you can add specific document types to an appsetting which will ensure ALL content of that type will be cached. 

This is decided based on an `ISeedKeyProvider` whose role it is to specify what keys need to be seeded.
During startup all of the `ISeedKeyProvider`'s are run and the keys are then seeded into their respective caches `IPublishedContentCache` and `IPublishedMediaCache`

Umbraco runs with two seed key providers out of the box
- **ContentTypeSeedKeyProvider**
  - Which reads the appsetting `Umbraco:CMS:Cache:ContentTypeKeys` for any content types you want to cache
- **BreadthFirstKeyProvider**
  - Which does a breadth-first traversal of the content and media, seeding N number of content as specified in the appsettings `Umbraco:CMS:Cache:DocumentBreadthFirstSeedCount` and `Umbraco:CMS:Cache:MediaBreadthFirstSeedCount`

However you are of course able to run your own `ISeedKeyProvider` if you need more control.

---


Official docs: https://docs.umbraco.com/umbraco-cms/reference/configuration/cache-settings

Seeding: https://docs.umbraco.com/umbraco-cms/reference/cache/cache-seeding

## XPATH

We've known about the removal of `XPATH` for a while now, so hopefully this isn't used too widely in your projects, as this can be quite a painful one.

Firstly, as part of the `Build Solution` step, you may have commented out any code which was showing that the previous methods used to get content via `XPATH` are no longer available.

The second issue you may not notice at first, which is the `Multinode Tree Picker` - if you have any data types using this along with `XPATH`, it will no longer work and in fact the upgrade path would have removed the `XPATH` query data entirely. For this, you'll need to query the V13 database to find any `Multinode Tree Pickers` along with their configuration which you can then use to migrate the data type to use `Dynamic Root` instead.  Depending on how many you have, this could be quite time-consuming, you can do what I prefer, which is to compare the V13 and the V17 systems and adjust the `Multinode Tree Picker` to use the `Dynamic Root` then use uSync to check-in the changes into your repo, so when you deploy, the changes are persisted. Alternatively you can create custom migration scripts, to fix the issue automatically but this requires the previous configuration being available - so this may be easier to do BEFORE doing the V17 upgrade.

## Macros

## Newtonsoft.Json

## Custom Umbraco Extensions

## Block Labels