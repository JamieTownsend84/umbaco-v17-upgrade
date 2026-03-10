# Umbraco - API Controllers

## UmbracoApiController

V13:

```csharp
public class ProductsController : UmbracoApiController
{
    public IActionResult GetAll() 
    => Ok(new[] { "Table", "Chair", "Desk", "Computer" });
}
```

V17:

```csharp
[ApiController]
[Route("/umbraco/api/products")]
public class ProductsController : Controller
{
    [HttpGet("getall")]
    public IActionResult GetAll() 
    => Ok(new[] { "Table", "Chair", "Desk", "Computer" });
}
```

> In both cases, the API endpoint will be available at **/umbraco/api/products/getall**.

## UmbracoAuthorizedApiController 

V13:

```csharp
public class AzureSearchBackofficeController : UmbracoAuthorizedApiController
{
    [HttpGet]
    public async Task<IActionResult> RefreshIndex()
    {
        return Ok();
    }
}
```

V17:

```csharp
[VersionedApiBackOfficeRoute("shout/azure/search")]
[ApiExplorerSettings(GroupName = "Shout Azure Search")]
public class AzureSearchBackofficeController : ManagementApiControllerBase
{
    [HttpGet("refreshIndex")]
    public async Task<IActionResult> RefreshIndex()
    {
        return Ok();
    }
}
```

> The new URL would be **/umbraco/management/api/v1/shout/azure/search/refreshIndex**

In order to call this API you'll now need to be authenticated, here is an example of calling the API, along with gathering the Bearer token to send.

```javascript
export const onInit = async (host) => {
    const authContext = await host.getContext('UmbAuthContext');
        const token = await authContext.getLatestToken();

        const response = await fetch('/umbraco/management/api/v1/shout/azure/search/refreshIndex', {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        ....
};
```