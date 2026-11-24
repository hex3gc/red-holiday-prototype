using System;
using System.Collections.Generic;

namespace RedHoliday;

/// <summary>
///     A condition based on the presence of a Tag. 'Exists' means the Tag must exist to return true, and vice versa.
/// </summary>
public class TagCondition : IRedCondition
{
    private string Value;
    private bool Exists;
    public bool Met()
    {
        if (Exists && GameState.Tags.Contains(Value))
        {
            return true;
        }
        else if (!Exists && !GameState.Tags.Contains(Value))
        {
            return true;
        }

        return false;
    }
}