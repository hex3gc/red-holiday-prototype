using Godot;
using System;
using System.Linq;

namespace RedHoliday.Nodes;

/// <summary>
/// 	A simple text toast showing that a certain Axis has been altered.
/// </summary>
public partial class NBumpGradient : TextureRect
{
    private TextureRect _blackBg;

    private Color _color;
    private Color _colorTransparent;

	public void Init(string text, string color, RedAxis.BumpStrength bumpStrength)
    {
        _blackBg = GetNode<TextureRect>("%BlackBg");

        float transparency = 1f;
        switch (bumpStrength)
        {
            case RedAxis.BumpStrength.small:
                transparency = 0.1f;
                break;
            case RedAxis.BumpStrength.medium:
                transparency = 0.3f;
                break;
            case RedAxis.BumpStrength.large:
                transparency = 0.8f;
                break;
        }

        Color refColor = new Color(color);
        _color = new Color(refColor.R, refColor.G, refColor.B, transparency);
        _colorTransparent = new Color(refColor.R, refColor.G, refColor.B, 0);

        string align = text.Replace("+", "").ToLower();
        Texture = GD.Load<Texture2D>("res://Images/col-overlay-" + align + ".png");

        Modulate = _color;
        FadeOut();
    }

    public void FadeOut()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate", _colorTransparent, 1f);
        tween.TweenCallback(Callable.From(QueueFree));
        Tween tween2 = CreateTween();
        tween2.TweenProperty(_blackBg, "modulate", new Color(1, 1, 1, 0), 1f);
        tween2.TweenCallback(Callable.From(QueueFree));
    }
}
