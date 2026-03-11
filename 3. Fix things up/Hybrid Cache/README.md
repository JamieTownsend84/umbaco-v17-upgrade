## Hybrid Cache - [Examples](./examples.md)

Originally introduced in Umbraco 15, depending on how much content your project has and how you are querying it, you may notice that some methods are no longer available.

Previously, everything was loaded into memory on boot. For content-heavy websites, this resulted in long startup times. With Hybrid Cache, Umbraco uses a lazy-loading cache on an as-needed basis. Not everything is added to memory, allowing for faster boot-up times. This does, however, mean that if you require something that is not in memory, it will need to be loaded into the cache. It also means that, depending on code you may have — such as `.Descendants()` or `.Children()` — you may find this no longer works, and we have some examples below on how to resolve this.

What is cached and how much is configurable, though. You can add specific document types to an `appsettings` key, which will ensure all content of that type is cached.

This is decided based on an `ISeedKeyProvider` whose role it is to specify what keys need to be seeded.
During startup, all of the `ISeedKeyProvider`s are run and the keys are then seeded into their respective caches `IPublishedContentCache` and `IPublishedMediaCache`

Umbraco runs with two seed key providers out of the box
- **ContentTypeSeedKeyProvider**
  - Which reads the appsetting `Umbraco:CMS:Cache:ContentTypeKeys` for any content types you want to cache
- **BreadthFirstKeyProvider**
  - Which does a breadth-first traversal of the content and media, seeding N number of content as specified in the appsettings `Umbraco:CMS:Cache:DocumentBreadthFirstSeedCount` and `Umbraco:CMS:Cache:MediaBreadthFirstSeedCount`

However, you are of course able to run your own `ISeedKeyProvider` if you need more control.

---


Official docs: https://docs.umbraco.com/umbraco-cms/reference/configuration/cache-settings \
Seeding: https://docs.umbraco.com/umbraco-cms/reference/cache/cache-seeding
