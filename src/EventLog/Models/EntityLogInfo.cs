using System;
using System.Collections.Generic;

namespace AHSW.EventLog.Models;

public record EntityLogInfo<TEntity, TPropertyType>(
    IEnumerable<TEntity> Entities,
    params TPropertyType[] Properties)
        where TEntity : class
        where TPropertyType : struct, Enum;
