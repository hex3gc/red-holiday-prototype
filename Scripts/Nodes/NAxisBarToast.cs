using Godot;
using System;
using System.Linq;

namespace RedHoliday.Nodes;

/// <summary>
/// 	A simple text toast showing that a certain Axis has been altered.
/// </summary>
public partial class NAxisBarToast : Label
{
    public float deathTimer = 0f;

	public void Init(string text, string color)
    {
        Random rng = new Random();
        SetPosition(new Vector2(rng.Next(0, 100), rng.Next(40, 100)));

        Text = text;
        LabelSettings.FontColor = new Color(color);
        LabelSettings.FontSize += text.Count(c => c == '+') * 10;
    }

    public override void _Process(double delta)
    {
        deathTimer += (float)delta;
        SetPosition(new Vector2(Position.X, Position.Y - deathTimer * 1));

        if (deathTimer >= 2f)
        {
            QueueFree();
        }
    }
}
