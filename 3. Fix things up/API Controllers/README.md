## Umbraco - API Controllers - [Examples](./examples.md)

Umbraco 14 has obsoleted or removed base classes that were widely adopted for building APIs in previous versions of Umbraco.

### UmbracoApiController & PluginController

`UmbracoApiController` has been removed and the recommended approach is to base APIs on the ASP.NET Core `Controller` class instead.

`UmbracoApiController` would automatically route the API actions to `/umbraco/api/[ControllerName]/[ControllerAction]`. Moving forward, you control your API routes with the `[Route]` annotation.

If you have used `PluginController` you will also need to do the above as this is based on `UmbracoApiController`

Official documentation on porting is here: \
https://docs.umbraco.com/umbraco-cms/reference/routing/umbraco-api-controllers/porting-old-umbraco-apis

---

### UmbracoAuthorizedApiController & UmbracoAuthorizedJsonController

These have also been removed and the recommendation is to update to use the `Management API` by basing your APIs on `ManagementApiControllerBase` - this is, however, slightly more involved - I've included an example on how to implement and then call an API based on the `Management API`.

There is a full walk-through on creating a backoffice API in the official documentation here: \
https://docs.umbraco.com/umbraco-cms/tutorials/creating-a-backoffice-api
