using System;
using System.Threading.Tasks;
using AHSW.EventLog.Contracts.Interfaces.Configurators;
using AHSW.EventLog.Contracts.Models.Entities;

namespace AHSW.EventLog.Contracts.Interfaces;

public interface IEventLogScope<TEventType, TEntityType, TPropertyType>
    where TEventType : struct, Enum
    where TEntityType : struct, Enum
    where TPropertyType : struct, Enum
{
    EventLogEntry<TEventType, TEntityType, TPropertyType> EventLogEntry { get; }

    /// <summary>
    /// Executes a repository action and records logs according to the passed configuration
    /// </summary>
    /// <param name="repositoryActionAsync">A repository action which will be executed and logged</param>
    /// <param name="optionsBuilder"></param>
    Task SaveAndLogEntitiesAsync(Func<Task> repositoryActionAsync,
        Action<IEntityLogConfiguration<TEventType, TEntityType, TPropertyType>> optionsBuilder = null);
}