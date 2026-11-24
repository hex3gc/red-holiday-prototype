using Godot;
using System;

namespace RedHoliday;

/// <summary>
/// 	Central class for playing sounds
/// </summary>
public static class AmbiencePlayer
{
    public static void SoundFromString(string str)
    {
        switch (str.ToLower())
        {
            case "fuzz":
            case "bgfuzz":
                Play_Fuzz();
                break;
            case "aircon":
                Play_AirCon();
                break;
            case "lander":
                Play_Lander();
                break;
            case "musicmorning":
                Play_MusicMorning();
                break;
            default:
                break;
        }
    }
    public static void Play_Fuzz()
    {
        GameState.MainNode.AmbiencePlayer.Stream = GD.Load<AudioStream>("res://Sounds/bgfuzz.wav");
        GameState.MainNode.AmbiencePlayer.Play();
    }
    public static void Play_AirCon()
    {
        GameState.MainNode.AmbiencePlayer.Stream = GD.Load<AudioStream>("res://Sounds/aircon.wav");
        GameState.MainNode.AmbiencePlayer.Play();
    }
    public static void Play_Lander()
    {
        GameState.MainNode.AmbiencePlayer.Stream = GD.Load<AudioStream>("res://Sounds/lander.wav");
        GameState.MainNode.AmbiencePlayer.Play();
    }
    public static void Play_MusicMorning()
    {
        GameState.MainNode.AmbiencePlayer.Stream = GD.Load<AudioStream>("res://Sounds/musicMorning.wav");
        GameState.MainNode.AmbiencePlayer.Play();
    }
}
