## Newtonsoft.Json - [Examples](./examples.md)

`Newtonsoft.Json` has been replaced by `System.Text.Json` and is one of the changes I incorrectly assumed wasn't too much of a big deal. To quote the documentation:

> Although this sounds like it's not a big change, it's one of the most breaking changes on the backend. Whereas Newtonsoft.Json was flexible and error-tolerant by default, System.Text.Json is strict but more secure by default. You can therefore run into things that will not be serialized.

Which is exactly what we've found. Be it custom data types, property editors, API controllers, or Property Value Converters, you will want to either migrate these to `System.Text.Json` or at a minimum - do full regression testing to find any potential issues.

I've included a couple of examples for `Property Value Converters`.
