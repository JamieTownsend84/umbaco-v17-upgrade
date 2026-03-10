# Umbraco Forms - [Examples](./examples.md)

[License changes](../Umbraco%20License%20Changes/)

From my experience the upgrade of Umbraco forms from 13 to 17 is relatively pain free, depending of course on how much you have extended it. That said, most customisation previously was done using C# and that is still the same in most cases, such as custom workflows, field types etc.

But, and there is always a but with the new backoffice :)

As the backoffice presentation is handled by client-side components you may need to create a custom backoffice package which will define the `Preview` and `Editor` UI to use for your custom field types for example if one of the OOTB doesn't quite fit your requirements. If like me you aren't too fussed about the preview and 99% of the time do not allow editing of form submissions in the backoffice you may be able to get away without having to do this. Depends how complex the field type is and if preview/editing is important to you.

You can see an example on how to create the `Field Preview` and `Field Editor` via Lit/Web Components here: https://docs.umbraco.com/umbraco-forms/developer/extending/adding-a-fieldtype

## Property Editor UIs

One of the breaking changes is the change to the value you use to define a field/setting property view, this will affect any custom field types / workflow settings you have defined. 
I've included a couple of [examples here](./examples.md). 

For a full list of the values available see here: \
https://docs.umbraco.com/umbraco-forms/16.latest/developer/extending/adding-a-type/setting-types#built-in-setting-types

## Themes

Another breaking change I experienced was around form field conditions and form pagination. As we've jumped quite a few major versions I highly recommend re-aligning with the `default` theme, render and script partials, to ensure you are not missing anything. Umbraco Forms uses RCL these days so whereas you could just browse the default theme and compare it you'll need to download the theme from the Umbraco Form documentation website, you can find that here: https://docs.umbraco.com/umbraco-forms/developer/themes