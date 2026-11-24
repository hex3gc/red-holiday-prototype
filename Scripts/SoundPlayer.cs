using Godot;
using System;

namespace RedHoliday;

/// <summary>
/// 	Central class for playing sounds
/// </summary>
public static class SoundPlayer
{
	public static void Play_Click()
	{
		GameState.MainNode.SFXPlayer.Stream = GD.Load<AudioStream>("res://Sounds/click.wav");
		GameState.MainNode.SFXPlayer.Play();
	}
    public static void Play_FullClick()
	{
		GameState.MainNode.SFXPlayer.Stream = GD.Load<AudioStream>("res://Sounds/fullClick.wav");
		GameState.MainNode.SFXPlayer.Play();
	}
}
