# AHSW.EventLog.Contracts

Shared interfaces, log entry models, enums, and failure logging extensions for the [EventLog library](https://github.com/cat-begemot/event-log). Application layers can reference this package independently of the logging implementation and Entity Framework Core.

## Installation

```sh
dotnet add package AHSW.EventLog.Contracts --version 1.0.0
```

Targets .NET 10 and has no package dependencies.

## Public API

Version 1.0.0 contains:

| Namespace | Types and purpose |
|---|---|
| `AHSW.EventLog.Contracts.Interfaces` | `IEventLogService`, `IEventLogScope`, and `IApplicationRepository` for consuming event logging and implementing repository integration. |
| `AHSW.EventLog.Contracts.Interfaces.Configurators` | `IEntityLogConfiguration`, `IEntityLogConfigurator`, and `ITypeDescriptionsConfigurator` for logging and description callbacks. |
| `AHSW.EventLog.Contracts.Interfaces.Entities` | `IReadOnlyEntity`. |
| `AHSW.EventLog.Contracts.Models.Entities` | `EventLogEntry` and `EntityLogEntry`. |
| `AHSW.EventLog.Contracts.Models.Entities.Abstract` | `PkEntity`, `ReadOnlyEntity`, and `PropertyLogEntry`. |
| `AHSW.EventLog.Contracts.Models.Entities.PropertyLogEntries` | Typed property log entries for Boolean, date/time, decimal, double, integer, and string values. |
| `AHSW.EventLog.Contracts.Models.Enums` | `ActionType` and `EventStatus`. |
| `AHSW.EventLog.Contracts.Models` | `FailureInfo`. |
| `AHSW.EventLog.Contracts.Extensions` | `EventLogEntryExtensions.AddFailureInfo` for recording handled failures and exception details. |

For service implementation, dependency injection registration, startup configuration, and Entity Framework Core mappings, install [AHSW.EventLog](https://www.nuget.org/packages/AHSW.EventLog), which includes this package as a dependency. Concrete configuration helpers and description-table entities remain in that package.

## Migration from AHSW.EventLog

The extraction is a source and binary breaking change. Shared types now use `AHSW.EventLog.Contracts` namespaces and reside in the Contracts assembly. Update imports and project references, then recompile all consumers.

`CreateEventScopeAndRun` callbacks now receive `IEventLogScope<TEventType, TEntityType, TPropertyType>`. `SaveAndLogEntitiesAsync` configuration callbacks now receive `IEntityLogConfiguration<TEventType, TEntityType, TPropertyType>`. Update explicitly typed callbacks and custom service implementations to match. Import `AHSW.EventLog.Contracts.Extensions` for `AddFailureInfo`.

See the [migration guide](https://github.com/cat-begemot/event-log#migrating-to-the-contracts-package) for namespace mappings and the [EventLog documentation](https://github.com/cat-begemot/event-log#readme) for configuration and usage examples.

## License

MIT. The package includes the repository's LICENSE file.
