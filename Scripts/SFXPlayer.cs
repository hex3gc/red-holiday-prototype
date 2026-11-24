using Godot;
using System;

namespace RedHoliday;

/// <summary>
/// 	Central class for playing sounds
/// </summary>
public static class SFXPlayer
{
	public static void SoundFromString(string str)
	{
		switch(str.ToLower())
		{
			case "click":
				Play_Click();
				break;
			case "fullclick":
				Play_FullClick();
				break;
			case "landerthrust":
				Play_LanderThrust();
				break;
			case "landercrash":
				Play_LanderCrash();
				break;
			case "slidedoor":
				Play_SlideDoor();
				break;
			default:
				break;
		}
	}
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
	public static void Play_LanderThrust()
	{
		GameState.MainNode.SFXPlayer.Stream = GD.Load<AudioStream>("res://Sounds/landerThrust.wav");
		GameState.MainNode.SFXPlayer.Play();
	}
	public static void Play_LanderCrash()
	{
		GameState.MainNode.SFXPlayer.Stream = GD.Load<AudioStream>("res://Sounds/landerCrash.wav");
		GameState.MainNode.SFXPlayer.Play();
	}
	public static void Play_SlideDoor()
	{
		GameState.MainNode.SFXPlayer.Stream = GD.Load<AudioStream>("res://Sounds/slideDoor.wav");
		GameState.MainNode.SFXPlayer.Play();
	}
}
