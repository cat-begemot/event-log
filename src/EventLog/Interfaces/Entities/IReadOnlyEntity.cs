using System;

namespace AHSW.EventLog.Interfaces.Entities;

public interface IReadOnlyEntity
{
    int? CreatedBy { get; set; }

    DateTime CreatedAt { get; set; }
}