## Umbraco Licence Changes

```json
{
  "Umbraco": {
    "Licenses": {
      "Products": {
        "Umbraco.Commerce": "YOUR_LICENSE_KEY",
        "Umbraco.Deploy.OnPrem": "YOUR_LICENSE_KEY",
        "Umbraco.Engage": "YOUR_LICENSE_KEY",
        "Umbraco.Forms": "YOUR_LICENSE_KEY",
        "Umbraco.UIBuilder": "YOUR_LICENSE_KEY",
        "Umbraco.Workflow": "YOUR_LICENSE_KEY"
      }
    }
  }
}

```

### Umbraco Cloud

If you're using Umbraco Cloud then some licences are included, but in my experience you still needed to configure them with a special keyword `UMBRACO-CLOUD` - see below:

```json
{
  "Umbraco": {
    "Licenses": {
      "Products": {
        "Umbraco.Deploy": "UMBRACO-CLOUD",
        "Umbraco.Forms": "UMBRACO-CLOUD"
      }
    }
  }
}
```
