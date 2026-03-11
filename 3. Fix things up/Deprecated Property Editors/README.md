## Legacy / Deprecated Property Editors - [Examples](./examples.md)

Whilst legacy property editors have long been marked as deprecated and the advice has been to migrate away from them - with the new back-office in V14 they are officially removed. 

Official documentation: https://docs.umbraco.com/umbraco-cms/fundamentals/setup/upgrading/version-specific#umbraco-14

> Nested Content -> Block List or Block Grid \
> Grid -> Block Grid \
> MediaPicker -> MediaPicker3

Depending on how many legacy property editors you have, you may want to migrate them away in V13 before doing the V17 upgrade. 

### uSync Migrations

In my experience the easiest way to migrate legacy property editors is by using `uSync.Migrations` https://github.com/Jumoo/uSyncMigrations - most legacy property editors are covered already, if you are using any that are not handled OOTB with `uSync.Migrations` it's pretty easy to roll your own `Migrator` to handle it.


### Custom Package Migrations

If you want a little more control, another option is to create a custom package and inside that create `Migrations` which you can then use to modify the data types directly via `IDataTypeService` - the good thing about Custom Package Migrations is that you can create as many of them as you want, when you run the project only the `Migrations` which have not been run previously are processed. 

I've included some code [examples here](./examples.md) on how to define a Package, Migration Plan and a Migration which will update a `DataType` directly.

See official documentation here: https://docs.umbraco.com/umbraco-cms/extending/packages/creating-a-package#custom-package-migration