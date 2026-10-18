using System;
using AHSW.EventLog.Interfaces;

namespace AHSW.EventLog.Models;

internal record PropertyInfo<TEntity>(Func<TEntity, object> Getter, string Name) :
    IPropertyInfo;