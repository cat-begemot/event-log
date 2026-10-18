using AHSW.EventLog.Contracts.Interfaces;
using Bookstore.Sample.Configurations;
using Bookstore.Sample.Interfaces;

namespace Bookstore.Sample.Models;

internal class ResolvedServices
{
    public IEventLogService<EventType, EntityType, PropertyType> EventLog { get; init; }
    
    public IBookRepository BookRepository { get; init;}
    
    public IShelfRepository ShelfRepository { get; init;}
}