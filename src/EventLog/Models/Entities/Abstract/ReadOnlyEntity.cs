using System;

namespace AHSW.EventLog.Models.Entities.Abstract;

public abstract class ReadOnlyEntity : PkEntity
{
    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}