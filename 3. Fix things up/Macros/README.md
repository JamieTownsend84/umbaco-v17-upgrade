# Macros - [Examples](./examples.md)

Macros are another feature we've known the removal of for a few years probably! There are two main parts to this that you are probably using. 

## Macro Partial Views
The first is calling `RenderMacro` directly in a template

```csharp
Umbraco.RenderMacroAsync("macro", model)
```

The process for this one isn't too bad, depending of course how many macros you have and how complicated they are. 

1) Create an element with the same properties as the Macro
2) Create a new `PartialView` that accepts the new element
3) Copy the contents of the `MacroPartialView` into the new `PartialView`
4) Update your templates to call `RenderPartialAsync` instead and with the new model

## RTE Macros
The second is by inserting a macro into a RTE. 

The initial process is the same, by creating the element with matching properties. But there are some additional steps needed.

1) Edit the RTE data type to allow a block of the new element
2) Create a `PartialView` in the `Partials/richtext/Components` folder - and update the content as above

However, unlike the templates approach this one is more of a challenge and risks losing data as the RTE uses special syntax in its body to tell the system to render a Macro or a Block. So any RTEs you have will need to be updated with the new Block Syntax replacing the Macro - if you don't have many instances it may be easier to just manually fix this, HOWEVER - if you have lots then you'll want to look at a more automated way such as `uSync.Migrations`, `Umbraco Deploy` or `Custom Migrations` depending on at what stage you want this to happen - before/after the initial V17 migration.

Thankfully some documentation exists with full code examples for reference.

https://docs.umbraco.com/umbraco-cms/tutorials/migrating-macros

https://joe.gl/ombek/blog/migrating-rte-macros/
