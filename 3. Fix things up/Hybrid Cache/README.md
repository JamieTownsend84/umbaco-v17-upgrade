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


Official docs: https://docs.umbraco.com/umbraco-cms/reference/configuration/cache-settings \
Seeding: https://docs.umbraco.com/umbraco-cms/reference/cache/cache-seeding
