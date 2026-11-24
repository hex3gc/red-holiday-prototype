using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RedHoliday.Nodes;

/// <summary>
/// 	Displays one of the choices from a Phrase
/// </summary>
public partial class NPhraseDisplayButton : TextureButton
{
    private RedChoice _redChoice;
    private NPhraseDisplay _nPhraseDisplay;

    private TextureRect _colorOverlayTextureRect;
    private Label _label;

	public override void _Ready()
    {
        _colorOverlayTextureRect = GetNode<TextureRect>("%ColorOverlayTextureRect");
        _label = GetNode<Label>("%Label");

        Pressed += OnButtonPressed;

        _label.Text = _redChoice.Text;

        int highest = 0;
        Color finalColor = new Color("#999999", 0.5f);
        foreach (RedAxisBump redAxisBump in _redChoice.AxisBumps)
        {
            Color axisCol = new Color(redAxisBump.Positive ? redAxisBump.RedAxis.PosColor : redAxisBump.RedAxis.NegColor, 0.6f);
            switch(redAxisBump.BumpStrength)
            {
                case RedAxis.BumpStrength.small:
                    if (1 > highest) { finalColor = axisCol; }
                    highest = 1;
                    break;
                case RedAxis.BumpStrength.medium:
                    if (2 > highest) { finalColor = axisCol; }
                    highest = 2;
                    break;
                case RedAxis.BumpStrength.large:
                    if (3 > highest) { finalColor = axisCol; }
                    highest = 3;
                    break;
            }
        }

        _colorOverlayTextureRect.Modulate = finalColor;
    }

    public void SetData(RedChoice redChoice, NPhraseDisplay nPhraseDisplay)
    {
        _redChoice = redChoice;
        _nPhraseDisplay = nPhraseDisplay;
    }

    // EVENTS
    
    public void OnButtonPressed()
    {
        SFXPlayer.Play_FullClick();

        foreach (RedAxisBump redAxisBump in _redChoice.AxisBumps)
        {
            redAxisBump.Bump();
        }

        _nPhraseDisplay.Advance(_redChoice.GetNextPhrase());
    }
}
