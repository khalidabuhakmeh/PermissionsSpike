# Permissions Spike

Working with Brock Allen, we talked about a permissions framework that was policy
based. This is an initial spike that has the following:

- It can register policies
- It gets the Principal from a factory which is added to a context
- The **best** matching policy is found, then executed
- This supports the **imperitive** approach, but could easily be retrofitted into MVC or WebAPI.
- This could easily be modified to support an IoC container (which I would prefer)
- It validates a **Principal** based on their claims.

## Known Issues

- Not sure the interface is completely correct
- The creation of all the policies could be cached
- The matching could be moved to the metadata
- Dependency injection / IoC creation of models would be better as to inject dependencies like a database.
- The use of a **static** might make some cringe, so I'd like to support both a **static** and **non-static** approach.
