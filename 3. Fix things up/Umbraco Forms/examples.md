# Umbraco Forms

## Field - Settings View

Previous:

```csharp
....

[Setting(
    ...
    View = "Dropdownlist"
    ...
)]
public string? ActionList { get; set; }

[Setting(
...
    View = "TextWithFieldPicker"
...
)] 
public string? ConsentField { get; set; }

....
```

New:

```csharp
....

[Setting(
    ...
    View = "Umb.PropertyEditorUi.Dropdown"
    ...
)]
public string? ActionList { get; set; }

[Setting(
...
    View = "Forms.PropertyEditorUi.TextWithFieldPicker"
...
)] 
public string? ConsentField { get; set; }

....
```

## Field Type - Full Example
Here is a full example of the difference between V13 and V17 for a built in Field Type `HiddenField` - of course this is a real simple field type but as I mentioned before - depending on how much you care about the preview / editor dictates how much you'll need to do if OTB UI's aren't enough.

V13:

```csharp
[Serializable]
public class HiddenField : FieldType
{
    [Setting("Default Value", Description = "Enter a default value.", SupportsPlaceholders = true, DisplayOrder = 10)]
    public virtual string DefaultValue { get; set; } = string.Empty;
    public override bool HideLabel => true;

    public HiddenField()
    {
        base.Id = new Guid("DA206CAE-1C52-434E-B21A-4A7C198AF877");
        base.Name = "Hidden";
        base.Alias = "hidden";
        base.Description = "Renders a HTML hidden field";
        Icon = "icon-checkbox-dotted";
        DataType = FieldDataType.String;
        Category = "Simple";
        SortOrder = 120;
        FieldTypeViewName = "FieldType.HiddenField.cshtml";
        EditView = "textfield";
        RenderInputType = RenderInputType.Custom;
    }
}
```

V17:

```csharp
[Serializable]
public class HiddenField : FieldType
{
    [Setting("Default Value", Description = "Enter a default value.", SupportsPlaceholders = true, DisplayOrder = 10)]
    public virtual string DefaultValue { get; set; } = string.Empty;
    public override bool HideLabel => true;

    public HiddenField()
    {
        base.Id = new Guid("DA206CAE-1C52-434E-B21A-4A7C198AF877");
        base.Name = "Hidden";
        base.Alias = "hidden";
        base.Description = "Renders a HTML hidden field";
        Icon = "icon-checkbox-dotted";
        DataType = FieldDataType.String;
        Category = "Simple";
        SortOrder = 120;
        FieldTypeViewName = "FieldType.HiddenField.cshtml";
        EditView = "textfield";
        PreviewView = "Forms.FieldPreview.HiddenField";
        RenderInputType = RenderInputType.Custom;
    }
}
```

### Slightly more complicated Field Type

If you compare the below you'll notice actually not much has changed.

V13:

```csharp
[Serializable]
public class Textfield : FieldType
{
    [Setting("Default Value", Description = "Enter a default value.", SupportsPlaceholders = true, DisplayOrder = 10)]
    public virtual string DefaultValue { get; set; } = string.Empty;

    [Setting("Placeholder", Description = "Enter a HTML5 placeholder value.", View = "TextField", SupportsPlaceholders = true, DisplayOrder = 20)]
    public virtual string Placeholder { get; set; } = string.Empty;

    [Setting("Show Label", Description = "Indicate whether the the field's label should be shown when rendering the form.", View = "checkbox", PreValues = "true", DisplayOrder = 30)]
    public virtual string ShowLabel { get; set; }

    [Setting("Maximum Length", Description = "Enter the maximum number of characters accepted.", View = "NumericField", PreValues = "1,255", DisplayOrder = 40)]
    public virtual string MaximumLength { get; set; } = string.Empty;

    [Setting("Field Type", Description = "Select the type of information expected.", View = "dropdownlist", PreValues = "date,datetime-local,email,tel,text,number,time,url,week", DisplayOrder = 50)]
    public virtual string FieldType { get; set; } = string.Empty;

    public override bool HideLabel => ShowLabel == "False";

    [Setting("Autocomplete attribute", Description = "Optionally enter a value for the autocomplete attribute.", View = "TextField", DisplayOrder = 60)]
    public virtual string AutocompleteAttribute { get; set; } = string.Empty;

    public override bool SupportsRegex => true;

    public Textfield()
    {
        base.Id = new Guid("3F92E01B-29E2-4A30-BF33-9DF5580ED52C");
        base.Name = "Short answer";
        base.Alias = "shortAnswer";
        base.Description = "Renders an text input field, for short answers";
        Icon = "icon-autofill";
        DataType = FieldDataType.String;
        Category = "Simple";
        SortOrder = 10;
        ShowLabel = "True";
        FieldTypeViewName = "FieldType.Textfield.cshtml";
        EditView = "textfield";
    }

    public override List<Exception> ValidateSettings()
    {
        List<Exception> list = new List<Exception>();
        if (int.TryParse(MaximumLength, out var result) && result > 255)
        {
            list.Add(new Exception("The maxmimum length setting for the 'short answer' field cannot be more than 255 characters."));
        }

        return list;
    }

    public override IEnumerable<string> ValidateField(Form form, Field field, IEnumerable<object> postedValues, HttpContext context, IPlaceholderParsingService placeholderParsingService, IFieldTypeStorage fieldTypeStorage)
    {
        List<string> list = base.ValidateField(form, field, postedValues, context, placeholderParsingService, fieldTypeStorage).ToList();
        if (!TryGetMaximumLength(field, out var maximumLength))
        {
            maximumLength = 255;
        }

        string text = postedValues.FirstOrDefault()?.ToString();
        if (!string.IsNullOrEmpty(text) && text.Length > maximumLength)
        {
            list.Add($"The value provided exceeds {maximumLength} characters.");
        }

        return list;
    }

    private bool TryGetMaximumLength(Field field, out int maximumLength)
    {
        if (!field.Settings.TryGetValue("MaximumLength", out string value))
        {
            maximumLength = -1;
            return false;
        }

        return int.TryParse(value, out maximumLength);
    }
}
```

V17:

```csharp
[Serializable]
public class Textfield : FieldType
{
    [Setting("Default Value", Description = "Enter a default value.", SupportsPlaceholders = true, DisplayOrder = 10)]
    public virtual string DefaultValue { get; set; } = string.Empty;

    [Setting("Placeholder", Description = "Enter a HTML5 placeholder value.", SupportsPlaceholders = true, DisplayOrder = 20)]
    public virtual string Placeholder { get; set; } = string.Empty;

    [Setting("Show Label", Description = "Indicate whether the field's label should be shown when rendering the form.", View = "Umb.PropertyEditorUi.Toggle", PreValues = "true", DisplayOrder = 30)]
    public virtual string ShowLabel { get; set; }

    [Setting("Maximum Length", Description = "Enter the maximum number of characters accepted.", View = "Umb.PropertyEditorUi.Integer", PreValues = "1,255", DisplayOrder = 40)]
    public virtual string MaximumLength { get; set; } = string.Empty;

    [Setting("Field Type", Description = "Select the type of information expected.", View = "Umb.PropertyEditorUi.Dropdown", PreValues = "date,datetime-local,email,tel,text,number,time,url,week", DisplayOrder = 50)]
    public virtual string FieldType { get; set; } = string.Empty;

    public override bool HideLabel => ShowLabel == "False";

    [Setting("Autocomplete attribute", Description = "Optionally enter a value for the autocomplete attribute.", DisplayOrder = 60)]
    public virtual string AutocompleteAttribute { get; set; } = string.Empty;

    public override bool SupportsRegex => true;

    public Textfield()
    {
        base.Id = new Guid("3F92E01B-29E2-4A30-BF33-9DF5580ED52C");
        base.Name = "Short answer";
        base.Alias = "shortAnswer";
        base.Description = "Renders an text input field, for short answers";
        Icon = "icon-autofill";
        DataType = FieldDataType.String;
        Category = "Simple";
        SortOrder = 10;
        ShowLabel = "True";
        FieldTypeViewName = "FieldType.Textfield.cshtml";
        EditView = "Umb.PropertyEditorUi.TextBox";
        PreviewView = "Forms.FieldPreview.TextBox";
    }

    public override List<Exception> ValidateSettings()
    {
        List<Exception> list = new List<Exception>();
        if (int.TryParse(MaximumLength, out var result) && result > 255)
        {
            list.Add(new Exception("The maxmimum length setting for the 'short answer' field cannot be more than 255 characters."));
        }

        return list;
    }

    public override IEnumerable<string> ValidateField(Form form, Field field, IEnumerable<object> postedValues, HttpContext context, IPlaceholderParsingService placeholderParsingService, IFieldTypeStorage fieldTypeStorage)
    {
        List<string> list = base.ValidateField(form, field, postedValues, context, placeholderParsingService, fieldTypeStorage).ToList();
        if (!TryGetMaximumLength(field, out var maximumLength))
        {
            maximumLength = 255;
        }

        string text = postedValues.FirstOrDefault()?.ToString();
        if (!string.IsNullOrEmpty(text) && text.Length > maximumLength)
        {
            list.Add($"The value provided exceeds {maximumLength} characters.");
        }

        return list;
    }

    private bool TryGetMaximumLength(Field field, out int maximumLength)
    {
        if (!field.Settings.TryGetValue("MaximumLength", out string value))
        {
            maximumLength = -1;
            return false;
        }

        return int.TryParse(value, out maximumLength);
    }
}
```