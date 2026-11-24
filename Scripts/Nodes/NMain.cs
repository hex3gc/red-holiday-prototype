using Godot;
using System;
using static RedHoliday.Globals;

namespace RedHoliday.Nodes;

/// <summary>
/// 	Game entry node with initialization logic.
/// </summary>
public partial class NMain : Node
{
	private TextureRect _bg;
	private NAxisBar _kindMeanAxisBar;
	private NAxisBar _shyBoldAxisBar;
	private NAxisBar _patientImpulsiveAxisBar;
	private NAxisBar _honestDishonestAxisBar;
	private NAxisBar _anxiousRelaxedAxisBar;
	private NTagDisplay _tagDisplay;
	private NPhraseDisplay _phraseDisplay;
	public AudioStreamPlayer AmbiencePlayer
	{
		get { return _ambiencePlayer; }
		set { _ambiencePlayer = value; }
	}
	private AudioStreamPlayer _ambiencePlayer;
	public AudioStreamPlayer SFXPlayer
	{
		get { return _sfxPlayer; }
		set { _sfxPlayer = value; }
	}
	private AudioStreamPlayer _sfxPlayer;
	private Label _demoEndText;

	public override void _Ready()
	{
		GameState.MainNode = this;
		_kindMeanAxisBar = GetNode<NAxisBar>("%KindMeanAxisBar");
		_shyBoldAxisBar = GetNode<NAxisBar>("%ShyBoldAxisBar");
		_patientImpulsiveAxisBar = GetNode<NAxisBar>("%PatientImpulsiveAxisBar");
		_honestDishonestAxisBar = GetNode<NAxisBar>("%HonestDishonestAxisBar");
		_anxiousRelaxedAxisBar = GetNode<NAxisBar>("%AnxiousRelaxedAxisBar");
		_tagDisplay = GetNode<NTagDisplay>("%TagDisplay");
		_phraseDisplay = GetNode<NPhraseDisplay>("%PhraseDisplay");
		_bg = GetNode<TextureRect>("%Bg");
		_ambiencePlayer = GetNode<AudioStreamPlayer>("%AmbiencePlayer");
		_sfxPlayer = GetNode<AudioStreamPlayer>("%SFXPlayer");
		_demoEndText = GetNode<Label>("%DemoEndText");

		// Init story
		StoryGenerator.Init();

		// Setup Axis and Tag debug UI
		_kindMeanAxisBar.InitFromAxis(GameState.KindMean, "KIND", "MEAN", KIND_MAIN_COLOR, MEAN_MAIN_COLOR);
		_shyBoldAxisBar.InitFromAxis(GameState.ShyBold, "SHY", "BOLD", SHY_MAIN_COLOR, BOLD_MAIN_COLOR);
		_patientImpulsiveAxisBar.InitFromAxis(GameState.PatientImpulsive, "PATIENT", "IMPULSIVE", PATIENT_MAIN_COLOR, IMPULSIVE_MAIN_COLOR);
		_honestDishonestAxisBar.InitFromAxis(GameState.HonestDishonest, "HONEST", "DISHONEST", HONEST_MAIN_COLOR, DISHONEST_MAIN_COLOR);
		_anxiousRelaxedAxisBar.InitFromAxis(GameState.AnxiousRelaxed, "ANXIOUS", "RELAXED", ANXIOUS_MAIN_COLOR, RELAXED_MAIN_COLOR);
		UpdateAllBars();

		_tagDisplay.Update();

		// Start phrase loop
		_phraseDisplay.Start();
	}
	public override void _Process(double delta)
	{
		
	}

	public void Toast(string text, string color, RedAxis.BumpStrength bumpStrength)
	{
		Node node = GD.Load<PackedScene>("res://Scenes/AxisBarToast.tscn").Instantiate();
		NAxisBarToast nAxisBarToast = node as NAxisBarToast;
		nAxisBarToast.Init(text, color);
		AddChild(nAxisBarToast);

		Node node2 = GD.Load<PackedScene>("res://Scenes/BumpGradient.tscn").Instantiate();
		NBumpGradient nBumpGradient = node2 as NBumpGradient;
		nBumpGradient.Init(text, color, bumpStrength);
		AddChild(nBumpGradient);
	}
	public void UpdateAllBars()
	{
		_kindMeanAxisBar.UpdateBars();
		_shyBoldAxisBar.UpdateBars();
		_honestDishonestAxisBar.UpdateBars();
		_patientImpulsiveAxisBar.UpdateBars();
		_anxiousRelaxedAxisBar.UpdateBars();
	}
	public void SetBg(Texture2D texture)
	{
		_bg.Texture = texture;
	}
	public void Finish()
	{
		_kindMeanAxisBar.Visible = true;
		_shyBoldAxisBar.Visible = true;
		_honestDishonestAxisBar.Visible = true;
		_patientImpulsiveAxisBar.Visible = true;
		_anxiousRelaxedAxisBar.Visible = true;
		_demoEndText.Visible = true;
		_phraseDisplay.Visible = false;
	}
}
