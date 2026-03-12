# Macros

I've used the Umbraco Starter Kit and taken it from V13 to V17 and manually fixed the Macros with the steps advised, relatively pain-free.

## Template - Rendering

V13:

```csharp
await Umbraco.RenderMacroAsync("latestBlogposts",
new {
    numberOfPosts = Model.HowManyPostsShouldBeShown,
    startNodeId = Model.Id
})
```

V17:

```csharp
@{
    await Html.RenderPartialAsync("Partials/LatestBlogPosts", new LatestBlogPostsMacroViewModel()
    {                
        NumberOfPosts = Model.HowManyPostsShouldBeShown
        StartNodeId = Model.Id,
    });
}
```

## Macro Partial to View Partial

V13: [Source Code](./v13-LatestBlogposts.cshtml) \
V17: [Source Code](./v17-LatestBlogPosts.cshtml)

## Difference between RTE Macro and RTE Block

### Macro

```html
<?UMBRACO_MACRO macroAlias="latestBlogposts" numberOfPosts="3" startNodeId="umb://document/1d770f10d1ca4a269d68071e2c9f7ac1" />
```

### Block

```html
<umb-rte-block data-content-key="050cad41-b510-4c3f-bfb6-be71a348e330"></umb-rte-block>
```

You'll notice the block doesn't contain the `properties` that the Macro does, but this *GUID* effectively translate to this in the DB which contains the parameter detail.

```json
{
          "editorAlias": "Umbraco.RichText",
          "culture": null,
          "segment": null,
          "alias": "richText",
          "value": "{\"markup\":\"\\u003Cumb-rte-block data-content-key=\\u0022050cad41-b510-4c3f-bfb6-be71a348e330\\u0022\\u003E\\u003C/umb-rte-block\\u003E\\u003Cp\\u003E\\u003C/p\\u003E\",\"blocks\":{\"contentData\":[{\"contentTypeKey\":\"50cbb053-0204-4e9e-bb29-061631ea073e\",\"key\":\"050cad41-b510-4c3f-bfb6-be71a348e330\",\"values\":[{\"editorAlias\":\"Umbraco.ContentPicker\",\"culture\":null,\"segment\":null,\"alias\":\"startNodeId\",\"value\":\"umb://document/1d770f10d1ca4a269d68071e2c9f7ac1\"},{\"editorAlias\":\"Umbraco.Integer\",\"culture\":null,\"segment\":null,\"alias\":\"numberOfPosts\",\"value\":3}]}],\"settingsData\":[],\"expose\":[{\"contentKey\":\"050cad41-b510-4c3f-bfb6-be71a348e330\",\"culture\":null,\"segment\":null}],\"Layout\":{\"Umbraco.RichText\":[{\"contentUdi\":null,\"settingsUdi\":null,\"contentKey\":\"050cad41-b510-4c3f-bfb6-be71a348e330\",\"settingsKey\":null}]}}}"
        }
```

## Find all Macros

Credits to *Adam Sadler* - [Adam's GitHub](https://github.com/AaronSadlerUK) for sharing this SQL which might be useful in finding Macros so you can see at a glance the job ahead!

```sql
-- Find PUBLISHED content with macros in Umbraco 13
SELECT DISTINCT 
    n.text AS 'Content Name',
    n.id AS 'Node ID',
    pt.Alias AS 'Property Alias',
    SUBSTRING(pd.textValue, 1, 500) AS 'Preview'
FROM umbracoPropertyData pd
INNER JOIN umbracoContentVersion cv ON pd.versionId = cv.id
INNER JOIN umbracoDocumentVersion dv ON cv.id = dv.id AND dv.published = 1
INNER JOIN umbracoNode n ON cv.nodeId = n.id
INNER JOIN cmsPropertyType pt ON pd.propertyTypeId = pt.id
WHERE pd.textValue LIKE '%<?UMBRACO_MACRO%'
   OR pd.textValue LIKE '%umb://macro/%'
   OR pd.textValue LIKE '%"macroAlias"%'
ORDER BY n.text;
```