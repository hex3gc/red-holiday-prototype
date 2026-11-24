using System;
using System.Collections.Generic;

namespace RedHoliday;

/// <summary>
///     Any testable condition which returns true or false.
/// </summary>
public interface IRedCondition
{
    public bool Met();
}