using Godot;
using System;

namespace RedHoliday.Nodes;

/// <summary>
/// 	A debug bar associated with an existing Axis.
/// </summary>
public partial class NAxisBar : Control
{
	private RedAxis _axis;
	private string _leftName;
	private string _rightName;

	private TextureProgressBar _leftBar;
	private Label _leftLabel;
	private TextureProgressBar _rightBar;
	private Label _rightLabel;
	private Button _barButton;

	public override void _Ready()
	{
		_leftBar = GetNode<TextureProgressBar>("%LeftBar");
		_leftLabel = GetNode<Label>("%LeftLabel");
		_rightBar = GetNode<TextureProgressBar>("%RightBar");
		_rightLabel = GetNode<Label>("%RightLabel");
		_barButton = GetNode<Button>("%BarButton");

		_barButton.GuiInput += OnBarButtonGuiInput;
	}

	public void InitFromAxis(RedAxis axis, string leftName, string rightName, string leftColorHex, string rightColorHex)
	{
		_axis = axis;
		_leftName = leftName;
		_rightName = rightName;

		_leftLabel.Text = _leftName + ": " + (_axis.Value < 0 ? Math.Abs(_axis.Value) : 0);
		_rightLabel.Text = _rightName + ": " + (_axis.Value > 0 ? Math.Abs(_axis.Value) : 0);

		_leftBar.TintProgress = new Color(leftColorHex);
		_rightBar.TintProgress = new Color(rightColorHex);
	}

	public void UpdateBars()
	{
		if (_axis.Value < 0)
		{
			_leftBar.Value = Math.Abs(_axis.Value);
			_rightBar.Value = 0f;
		}
		else
		{
			_leftBar.Value = 0f;
			_rightBar.Value = Math.Abs(_axis.Value);
		}

		_leftLabel.Text = _leftName + ": " + (_axis.Value < 0 ? Math.Abs(_axis.Value) : 0);
		_rightLabel.Text = _rightName + ": " + (_axis.Value > 0 ? Math.Abs(_axis.Value) : 0);
	}

	// EVENTS

	public void OnBarButtonGuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseButton && inputEvent.IsPressed())
		{
			InputEventMouseButton inputEventMouseButton = inputEvent as InputEventMouseButton;
			SFXPlayer.Play_FullClick();
			if (inputEventMouseButton.ButtonIndex == MouseButton.Left)
			{
				_axis.Bump(true, RedAxis.BumpStrength.medium);
			}
			else if (inputEventMouseButton.ButtonIndex == MouseButton.Right)
			{
				_axis.Bump(false, RedAxis.BumpStrength.medium);
			}
		}
	}
}
