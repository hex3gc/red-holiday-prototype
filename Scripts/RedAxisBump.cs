using System;
using System.Collections.Generic;
using System.Linq;

namespace RedHoliday;

/// <summary>
///     An object representing a bump in an Axis.
/// </summary>
public class RedAxisBump
{
    public RedAxis RedAxis { get; private set; }
    public bool Positive { get; private set; }
    public RedAxis.BumpStrength BumpStrength { get; private set; }

    public RedAxisBump(RedAxis redAxis, bool positive, RedAxis.BumpStrength bumpStrength)
    {
        RedAxis = redAxis;
        Positive = positive;
        BumpStrength = bumpStrength;
    }

    public void Bump()
    {
        RedAxis.Bump(Positive, BumpStrength);
    }
}