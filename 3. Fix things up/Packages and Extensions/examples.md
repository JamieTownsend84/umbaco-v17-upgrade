## Umbraco Packages & Extensions

### Load a custom JS file in the backoffice

Sometimes you need custom JS to load in the backoffice - we specifically needed to do this for `BlockPreview`.

```json
{
  "$schema": "https://json.schemastore.org/umbraco-package.json",
  "name": "Component.Name",
  "id": "Component.Name",
  "version": "1.0.0",
  "allowTelemetry": true,
  "extensions": [
    {
      "type": "bundle",
      "alias": "alias",
      "name": "Name",
      "js": "/App_Plugins/Component.Name/index.min.js"
    }
  ]
}
```

### Load a custom CSS file in the backoffice

Sometimes you need to load custom CSS into the backoffice, see below on how to do this - I found this to be intermittent at best but at the time of writing was the recommended way of doing it

**umbraco-package.json**

```json
{
    "id": "Component.Name",
    "name": "Component.Name",
    "version": "1.0.0",
    "extensions": [
        {
            "name": "Component - Custom CSS",
            "alias": "component.customcss",
            "type": "appEntryPoint",
            "js": "/App_Plugins/Component.Name/customcss.js"
        }
    ]
}

```

**customcss.js**

```javascript
const myCss = document.createElement('link')
myCss.rel = 'stylesheet'
myCss.href = '/App_Plugins/Component.Name/styles/stylesheet.css'

document.head.appendChild(myCss)
```