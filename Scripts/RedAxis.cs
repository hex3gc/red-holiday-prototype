using System;
using System.Collections.Generic;
using System.Linq;

namespace RedHoliday;

/// <summary>
///     Represents one of the player's personality Axes in the current GameState.
/// </summary>
public class RedAxis
{
    public enum BumpStrength
    {
        small,
        medium,
        large
    }

    private float _value = 0f;
	public float Value
	{
		get { return _value; }
		set { _value = Math.Clamp(value, -100f, 100f); }
	}
    public string NegName { get; private set; }
    public string PosName { get; private set; }
    public string NegColor { get; private set; }
    public string PosColor { get; private set; }

    public RedAxis(string negName, string posName, string negColor, string posColor)
    {
        NegName = negName;
        PosName = posName;
        NegColor = negColor;
        PosColor = posColor;
    }

    // Easier to move towards either axis if you're in the center
    public void Bump(bool positive, BumpStrength bumpStrength)
    {
        float baseModifier;
        string toast = positive ? PosName : NegName;
        switch (bumpStrength)
        {
            default:
            case BumpStrength.small:
                baseModifier = 2f;
                toast = "+" + toast;
                break;
            case BumpStrength.medium:
                baseModifier = 4f;
                toast = "++" + toast;
                break;
            case BumpStrength.large:
                baseModifier = 8f;
                toast = "+++" + toast;
                break;
        }
        float finalModifier = baseModifier * (1 + ((100 - Math.Abs(Value)) / 100f));

        if (positive)
        {
            Value = Value + finalModifier;
        }
        else
        {
            Value = Value - finalModifier;
        }

        GameState.MainNode.Toast(toast, positive? PosColor : NegColor, bumpStrength);
        GameState.MainNode.UpdateAllBars();
    }
}
