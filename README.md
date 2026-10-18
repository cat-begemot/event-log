[![Create release and publish NuGet](https://github.com/cat-begemot/event-log/actions/workflows/release-and-publish.yml/badge.svg)](https://github.com/cat-begemot/event-log/actions/workflows/release-and-publish.yml)
![LastNuGetVersion](https://img.shields.io/nuget/v/AHSW.EventLog)
![NugetDownloads](https://img.shields.io/nuget/dt/AHSW.EventLog)

<a href="https://github.com/cat-begemot/event-log/tree/master/src/EventLog">
	<img width="111" height="111" src="https://github.com/cat-begemot/event-log/blob/master/images/logo.png"/>
</a>

## Event Log
EventLog is a library that identifies application events, records diagnostic and statistical information about them, and stores them in a relational database for further analysis. Within the scope of an application event, it has the ability to track the state of application domain models.

The concept behind using EventLog is to gather information about application activity and domain model mutations for various purposes. Some possible examples of its use include:
- Monitoring the health state of a long-running application
- Identifying performance issues
- Being aware of user activity in the system, tracking who is responsible for changing any property of domain models
- Keeping a history of domain model changes
- Collecting statistics on user controls and application features to improve the UI
- Analyzing what happens in the application through API invocation and data changes for internal investigations

## Table of Contents

* [Event Log](#event-log)
* [Table of Contents](#table-of-contents)
* [NuGet Packages](#nuget-packages)
* [Migrating to the Contracts package](#migrating-to-the-contracts-package)
* [Usage Example in Code](#1-usage-example-in-code)
* [Database Result Output](#2-database-result-output)
* [Sample Project](#3-sample-project)
* [How to use](#4-how-to-use)
  * [Define TEventType, TEntityType, and TPropertyType empty enums](#41-define-teventtype-tentitytype-and-tpropertytype-empty-enums)
  * [Embed EventLog tables to the application database context](#42-embed-eventlog-tables-to-the-application-database-context)
  * [Add ObservableProperties static class (optional)](#43-add-observableproperties-static-class-optional)
  * [Register EventLog service](#44-register-eventlog-service)
  * [Configure EventLog service on the application startup](#45-configure-eventlog-service-on-the-application-startup)
  * [Add EventLog recording](#46-add-eventlog-recording)
* [Use SQL queries for log investigation](#5-use-sql-queries-for-log-investigation)
* [Use log tables in any way in code for extend application functionality](#6-use-log-tables-in-any-way-in-code-for-extend-application-functionality)

## NuGet Packages

Both packages target .NET 10 and use the MIT license.

| Package | Purpose |
|---|---|
| [AHSW.EventLog](https://www.nuget.org/packages/AHSW.EventLog) | Event logging implementation and Entity Framework Core integration. Includes a dependency on AHSW.EventLog.Contracts. |
| [AHSW.EventLog.Contracts](https://www.nuget.org/packages/AHSW.EventLog.Contracts) | Shared service, scope, repository, and logging configuration interfaces; log entry models; enums; and failure logging extensions. Its initial version is 1.0.0 and it has no package dependencies. |

Install the implementation to use EventLog in an application:

```sh
dotnet add package AHSW.EventLog
```

The Contracts package is installed automatically as a dependency. It can also be referenced independently:

```sh
dotnet add package AHSW.EventLog.Contracts --version 1.0.0
```

Application layers that consume `IEventLogService` can reference Contracts independently of EF Core. The application startup and persistence layers still need AHSW.EventLog to configure the service, register it with dependency injection, and map log tables. The examples below use the implementation package.

The implementation package includes this README; the Contracts package includes its own [README](src/EventLog.Contracts/README.md). Both include the repository's LICENSE file. The release pipeline packs and publishes both packages through the existing EventLog release process; a new Contracts version alone does not trigger a release.

## Migrating to the Contracts package

The extraction changes public namespaces, assemblies, and callback signatures. It is a source and binary breaking change: update references and imports, then recompile all consumers. Treat an upgrade containing this extraction as a breaking release.

| Types | Previous namespace | Current namespace |
|---|---|---|
| `IEventLogService`, `IApplicationRepository` | `AHSW.EventLog.Interfaces` | `AHSW.EventLog.Contracts.Interfaces` |
| `IReadOnlyEntity` | `AHSW.EventLog.Interfaces.Entities` | `AHSW.EventLog.Contracts.Interfaces.Entities` |
| `IEntityLogConfigurator`, `ITypeDescriptionsConfigurator` | `AHSW.EventLog.Interfaces.Configurators` | `AHSW.EventLog.Contracts.Interfaces.Configurators` |
| `EventLogEntry`, `EntityLogEntry` | `AHSW.EventLog.Models.Entities` | `AHSW.EventLog.Contracts.Models.Entities` |
| `PkEntity`, `ReadOnlyEntity`, `PropertyLogEntry` | `AHSW.EventLog.Models.Entities.Abstract` | `AHSW.EventLog.Contracts.Models.Entities.Abstract` |
| Typed property log entries | `AHSW.EventLog.Models.Entities.PropertyLogEntries` | `AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries` |
| `ActionType`, `EventStatus` | `AHSW.EventLog.Models.Enums` | `AHSW.EventLog.Contracts.Models.Enums` |
| `FailureInfo` | `AHSW.EventLog.Models` | `AHSW.EventLog.Contracts.Models` |
| `EventLogEntryExtensions` (`AddFailureInfo`) | `AHSW.EventLog.Extensions` | `AHSW.EventLog.Contracts.Extensions` |
| `IEventLogConfigurator`, `IEntityConfigurator`, `IPropertyConfigurator` | `AHSW.EventLog.Interfaces.Configurators` | `AHSW.EventLog.Interfaces.Configuration` |
| Concrete configuration classes | `AHSW.EventLog.Models.Configurations` | `AHSW.EventLog.Models.Configuration` |
| `BaseDescriptiveEntity` | `AHSW.EventLog.Models.Entities.Abstract` | `AHSW.EventLog.Models.Entities.Abstracts` |

`CreateEventScopeAndRun` now passes `IEventLogScope<TEventType, TEntityType, TPropertyType>` to its callback instead of `EventLogScope<TEventType, TEntityType, TPropertyType>`. `SaveAndLogEntitiesAsync` now passes `IEntityLogConfiguration<TEventType, TEntityType, TPropertyType>` instead of the concrete `EntityLogConfiguration`. Update explicitly typed delegates, method parameters, and custom service implementations accordingly; lambdas with inferred parameter types can retain their existing structure.

Import `AHSW.EventLog.Contracts.Extensions` to call `AddFailureInfo`. Keep `AHSW.EventLog.Extensions` where you use the `AddEventLog` registration extension. Concrete configuration helpers and description-table entities remain in the implementation package.

## 1. Usage Example in Code  
As an example, imagine the following code is an API endpoint that creates a domain model, **Book**, and saves it in storage.  
As a wrapper, **EventLog** records the ```AddBooksOnShelf``` event, adds some detailed information to the ```EventLogEntry```, executes the initial repository method while simultaneously adding an ```EntityLogEntry``` related to the ```EventLogEntry```, and records the ```Book``` property value states.  

```cs
var book = GetBookEntity();

// Create event scope. For example, it can be API endpoint
await services.EventLog.CreateEventScopeAndRun(
    // Define event type
    EventType.AddBooksOnShelf,
    async eventLogScope =>
    {
	// Addtional usefule data can be added to the event log record 
	eventLogScope.EventLogEntry.CreatedBy = userId;
	eventLogScope.EventLogEntry.Details = "Adding a book";

	// Trackable object is changed
	book.Title = "Book Title - Rev1";

	// Gathering object delta changing and save object and then event
	// IMPORTANT: data modification must be completed before this invocation
	await eventLogScope.SaveAndLogEntitiesAsync(
	    // Invoke object saving by EF data context 
	    () => services.BookRepository.AddOrUpdateAsync(book),
	    options => options
		// Define tracked objects and define tracked properties
		.AddEntityLogging(
		    new[] { book },
		    () => new[] { PropertyType.BookTitle, PropertyType.BookLikeCount }));
    });
```

## 2. Database Result Output  
As a result, **EventLog** table data can be viewed and analysed using [SQL queries](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts).
Expand below text to see the examples.  

<details>
<summary>2.1. Analyse all recorded application events</summary>

#### [ShowAllEvents.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/ShowAllEvents.sql)

| **Id** | **Initiator** | **EventType** | **Status** | **CreatedAt** | **DurationInMs** | **Details** | **FailureDetails** |
|---|---|---|---|---|---|---|---|
| 8 | 3 | Update books | Successful | 2025-04-05 13:22:59.2916081 | 2 | No one observable property is not changed |  |
| 7 | 3 | Update books | Successful | 2025-04-05 13:22:59.2836144 | 5 | Observable property is changed |  |
| 6 | 3 | Add books | Successful | 2025-04-05 13:22:59.2754546 | 7 | Adding a book |  |
| 5 | 3 | Add books | Successful | 2025-04-05 13:22:59.2566239 | 16 | Adding a shelf and 2 books |  |
| 4 | 3 | Add books | Successful | 2025-04-05 13:22:59.2509625 | 3 | Adding a book |  |
| 3 | 3 | Add books | Successful | 2025-04-05 13:22:59.1869002 | 63 | Adding a book |  |
| 2 | 3 | Add shelf | Successful | 2025-04-05 13:22:59.0944167 | 91 | Adding a shelf |  |
| 1 | 3 | Add books | Successful | 2025-04-05 13:22:58.9072985 | 185 | Adding a shelf and 2 books |  |
</details>

<details>
<summary>2.2. Analyse application event statistics</summary>

#### [ShowAllEvents.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/Statistics.sql)

| **EventType** | **TotalCount** | **ErrorCount** | **MedianInMs** | **MeanInMs** |
|---|---|---|---|---|
| Add books | 7 | 2 | 63 | 81 |
| Add shelf | 1 | 0 | 92 | 92 |
| Update books | 2 | 0 | 4 | 4 |
</details>

<details>
<summary>2.3. Track application domain model properties changing</summary>
	
#### [ShowAllEvents.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/TrackPropertyChanges.sql)

| **CreatedAt** | **Action** | **EntityId** | **Entity** | **Property** | **Value** | **ValueType** | **InitiatorId** | **Event** |
|---|---|---|---|---|---|---|---|---|
| 2025-04-05 13:22:59 | Create | 8 | Book | Title | EventLog Manual - 43 Edition | String | 3 | Add books |
| 2025-04-05 13:22:59 | Create | 8 | Book | Published | 2025-04-05 13:22:59.2968132 | DateTime | 3 | Add books |
| 2025-04-05 13:22:59 | Create | 8 | Book | IsAvailable | 1 | Bool | 3 | Add books |
| 2025-04-05 13:22:59 | Create | 8 | Book | Condition | 1 | Int32 | 3 | Add books |
| 2025-04-05 13:22:59 | Create | 8 | Book | Labels | 3 | Int32 | 3 | Add books |
| 2025-04-05 13:22:59 | Create | 8 | Book | LikeCount | 96 | Int32 | 3 | Add books |
| 2025-04-05 13:22:59 | Create | 8 | Book | Price | 43.0 | Double | 3 | Add books |
| 2025-04-05 13:22:59 | Update | 7 | Book | Title | EventLog Manual - 63 Edition - Revision_1 | String | 3 | Update books |
| 2025-04-05 13:22:59 | Update | 7 | Book | Published | 2025-04-05 13:22:59.2832816 | DateTime | 3 | Update books |
| 2025-04-05 13:22:59 | Update | 7 | Book | FirstSale | 2025-04-06 13:22:59.2832817 | DateTime | 3 | Update books |
</details>

## 3. Sample Project
[Bookstore](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample) is a sample console application to demonstrate the way of configuring EventLog and the most common use cases.  

## 4. How to use

### 4.1. Define TEventType, TEntityType, and TPropertyType empty enums
These enums will be filled later, when required event and property logging is added to the code. It is desirable to follow the next suggestions.
* 4.1.1. Explicitly numerate enum members to avoid damaging the log in the future
```cs
// Preferable to use the short underlying type to keep EventLog data compact 
internal enum EventType : short
{
    // Books = 1000
    AddBooksOnShelf = 1001, // Explicit number is assigned to each enum member
    UpdateBooksOnShelf = 1002,
    
    // Shelves = 2000
    AddShelf = 2001,
    DeleteShelf = 2002,
}
```
* 4.1.2. Group enum members to simplify further maintenance and extends types
```cs
// Preferable to use the short underlying type to keep EventLog data compact 
internal enum PropertyType : short
{
    // Book = 1000 - All properties grouped by entity
    BookTitle = 1001,
    BookPublished = 1002,
    BookIsAvailable = 1003,
    BookLikeCount = 1004,
    BookPrice = 1005,
    BookFirstSale = 1006,
    BookCondition = 1007,
    BookLabels = 1008,
    
    // Shelf = 2000
    ShelfHeight = 2001
```
* 4.1.3. Make consistency numeration between TEntityType and TPropertyType
```cs
// Preferable to use the short underlying type to keep EventLog data compact 
internal enum EntityType ; short
{
    Book = 1000, // PropertyType has Book entity group that starts with 1000 as well
    Shelf = 2000
}
```

### 4.2. Embed EventLog tables to the application database context
* 4.2.1. Add `modelBuilder.ApplyEventLogConfigurations<EventType, EntityType, PropertyType>();` to the overriden OnModelCreating method in the application database context class. Make sure it goes first in the settings in roder to apply all database schemes correctly
```cs
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyEventLogConfigurations<EventType, EntityType, PropertyType>(); // Must be first
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookstoreDbContext).Assembly);
    }
```
* 4.2.2. Create and apply the new migration with EventLog convfigurations

### 4.3. Add ObservableProperties static class (optional)
Predefine methods with observable property collections to enable convenient and consistent usage across the application.

```cs
internal static class ObservableProperties
{
    public static PropertyType[] GetForBookEntity() =>
        new []
        {
            PropertyType.BookTitle,
            PropertyType.BookCondition,
        };
    
    public static PropertyType[] GetForShelfEntity() =>
        new []
        {
            PropertyType.ShelfHeight
        };
}
```

### 4.4. Register EventLog service
```cs
services.AddEventLog<BookstoreDbContext, EventType, EntityType, PropertyType>();
```

### 4.5. Configure EventLog service on the application startup
Setting up enum member custom names is a convenient way to make look of SQL query results more user-friendly.

All observable properties and appropriate entities must be resigtered in the service configuration.

See more detail example in the [Program.cs](https://github.com/cat-begemot/event-log/blob/master/src/Bookstore.Sample/Program.cs).

```cs
EventLogService<EventType, EntityType, PropertyType>.Configure(
	configurationBuilder => configurationBuilder
	    // Optional customization
	    .UseCustomTypeDescriptions(context,
		options => options
		    // Customize event type descriptions
		    .AddEventTypeDescription(EventType.AddBooksOnShelf, "Add books on a shelf")
		    .AddEventTypeDescription(EventType.UpdateBooksOnShelf, "Update books on a shelf")
		    // Customize entity type descriptions
		    .AddEntityTypeDescription(EntityType.Book, "Book")
		    .AddEntityTypeDescription(EntityType.Shelf, "Shelf")
		    // Customize ptoperty type descriptions
		    .AddPropertyTypeDescription(PropertyType.BookTitle, "Title")
		    .AddPropertyTypeDescription(PropertyType.ShelfHeight, "Height")
	    )
	    .RegisterEntity<BookEntity>(EntityType.Book, x => ((BookEntity)x).Id,
		options => options
		    .RegisterProperty(PropertyType.BookTitle,
			x => x.Title, nameof(BookEntity.Title))
	    )
	    .RegisterEntity<ShelfEntity>(EntityType.Shelf, x => ((ShelfEntity)x).Id,
		options => options
		    .RegisterProperty(PropertyType.ShelfHeight,
			x => x.Height, nameof(ShelfEntity.Height))
	    )
    );
});
```

### 4.6. Add EventLog recording
All entity changes must be made before SaveAndLogEntitiesAsync() invocation. Inside this method entities must be only updated in repository and saves.

See more examples in the [Program.cs](https://github.com/cat-begemot/event-log/blob/master/src/Bookstore.Sample/Program.cs).

```cs
        var book = CreateBookEntity();

        await services.EventLog.CreateEventScopeAndRun(
            // Specify event logging level here
            EventType.AddBooksOnShelf,
            async eventLogScope =>
            {
                eventLogScope.EventLogEntry.CreatedBy = userId;
                eventLogScope.EventLogEntry.Details = "Adding a book";
            
                await eventLogScope.SaveAndLogEntitiesAsync(
                    // All entity changes must be made before SaveAndLogEntitiesAsync() invocation
                    () => services.BookRepository.AddOrUpdateAsync(book),
                    options => options
                        // Specify entity level logging here
                        .AddEntityLogging(
                            // Specify property level logging here
                            new[] { book }, ObservableProperties.GetForBookEntity)
                );
            }
        );
```

## 5. Use SQL queries for log investigation
All samples of SQL queries can be found [here](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts):
- [ShowAllEvents.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/ShowAllEvents.sql)
- [ShowAllFailedEvents.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/ShowAllFailedEvents.sql)
- [TrackPropertyChanges.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/TrackPropertyChanges.sql)
- [EventStatistics.sql](https://github.com/cat-begemot/event-log/tree/master/src/Bookstore.Sample/Scripts/EventStatistics.sql)

## 6. Use log tables in any way in code for extend application functionality
