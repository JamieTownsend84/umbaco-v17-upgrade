# XPATH

### Get Content by XPATH

Any code which previously gathered content via `XPATH` will need to be rewritten, an example is below.

Previous:

```csharp
var xpath = string.Format("id({0})//{1}", 123, "siteSettings"); // id(123)//siteSettings
var siteSettings = _publishedContentCache.GetSingleByXPath(false, xpath);
```

New:

```csharp
var rootPage = _publishedContentCache.GetById(123);
var siteSettings = rootPage.DescendantOfType(
    _documentNavigationQueryService,
    _publishedStatusFilteringService,
    "siteSettings");
```

----

### Find all instances of the Multinode Tree Picker

Run this script on the V13 database to find all instances of the data-type `Multinode Tree Picker` - you can check the `config` field to see if it contains a `query` value - as this will be the `XPATH` value we then need to update to use `Dynamic Root` in V17. The script includes the **GUID** and the **NAME** to help you find the data-type in Umbraco.

```sql
select lower(un.uniqueId), un.text, udt.*
from umbracoDataType udt
inner join umbracoNode un on un.id = udt.nodeId
where udt.propertyEditorAlias = 'Umbraco.MultiNodeTreePicker'
```

#### Example

Previous:

```json
{
  "startNode": {
    "type": "content",
    "query": "//companyProfilesFolder" //5b486f72-c784-4515-a8e4-6f63fd9f4a37
  },
  "filter": "companyProfile", //dcbd7b84-387a-44d0-aabe-e5e7506160bd
  "minNumber": 0,
  "maxNumber": 0,
  "showOpenButton": false,
  "ignoreUserStartNodes": false
}
```

New:

```json
{
  "startNode": {
    "type": "content",
    "id": null,
    "dynamicRoot": {
      "originAlias": "ContentRoot",
      "querySteps": [
        {
          "unique": "9de2b271-95d6-4c4e-8c12-fa4d11ff0f8a",
          "alias": "NearestDescendantOrSelf",
          "anyOfDocTypeKeys": [
            "5b486f72-c784-4515-a8e4-6f63fd9f4a37" // companyProfilesFolder
          ]
        }
      ]
    }
  },
  "filter": "dcbd7b84-387a-44d0-aabe-e5e7506160bd", //companyProfile
  "minNumber": 0,
  "maxNumber": 0,
  "showOpenButton": false,
  "ignoreUserStartNodes": false,
}
```