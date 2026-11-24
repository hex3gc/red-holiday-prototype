using System;
using System.Collections.Generic;

namespace RedHoliday;

/// <summary>
///     A condition based on the value of an Axis. 'Above' means the Axis must meet or exceed the given value to return true, and vice versa.
/// </summary>
public class AxisCondition : IRedCondition
{
    private RedAxis RedAxis;
    private float Value;
    private bool Above;
    public bool Met()
    {
        if (Above && RedAxis.Value >= Value)
        {
            return true;
        }
        else if (!Above && RedAxis.Value <= Value)
        {
            return true;
        }

        return false;
    }
}