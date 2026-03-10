## Newtonsoft.Json

It has been migrated to `System.Text.Json` and is one of the changes I incorrectly assumed wasn't too much of a big deal. To quote the documentation:

> Although this sounds like it's not a big change, it’s one of the most breaking changes on the backend. Whereas Newtonsoft.Json was flexible and error-tolerant by default, System.Text.Json is strict but more secure by default. You can therefore run into things that will not be serialized.

Which is exactly what we've found, be it custom data types, property editors, API controllers or even Property Value Converters you will want to either migrate these to `System.Text.Json` or at a minimum - do full regression testing to find any potential issues. 

I've included a couple of examples for `Property Value Converters`.