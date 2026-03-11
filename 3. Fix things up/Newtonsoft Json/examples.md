 ## Property Value Converters

 ### Addition of EditorUiAlias

 Previously:

 ```csharp
IsConverter(IPublishedPropertyType propertyType)
    =>  propertyType.EditorAlias.Equals("Shout.Modifiers");
 ```

 Depending on how the type is set up, you may need to check against `EditorUiAlias` instead.

  ```csharp
IsConverter(IPublishedPropertyType propertyType) 
    =>  propertyType.EditorUiAlias.Equals("Shout.Modifiers");
 ```

### Newtonsoft.Json

An example of a `Property Value Converter` migrated to `System.Text.Json`.

Previous: https://github.com/umbraco/Umbraco-CMS/blob/v13/main/src/Umbraco.Infrastructure/PropertyEditors/ValueConverters/ImageCropperValueConverter.cs

```csharp
using Newtonsoft.Json;

private static readonly JsonSerializerSettings _imageCropperValueJsonSerializerSettings = new()
{
    Culture = CultureInfo.InvariantCulture,
    FloatParseHandling = FloatParseHandling.Decimal,
};

public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
{
    ....

    value = JsonConvert.DeserializeObject<ImageCropperValue>(
        sourceString,
        _imageCropperValueJsonSerializerSettings);

    ----
}
```

New: https://github.com/umbraco/Umbraco-CMS/blob/main/src/Umbraco.Infrastructure/PropertyEditors/ValueConverters/ImageCropperValueConverter.cs

```csharp
using System.Text.Json;

public ImageCropperValueConverter(IJsonSerializer jsonSerializer)
{
    _jsonSerializer = jsonSerializer;
}

public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
{
    ....

    value = _jsonSerializer.Deserialize<ImageCropperValue>(sourceString);

    ....
}


```