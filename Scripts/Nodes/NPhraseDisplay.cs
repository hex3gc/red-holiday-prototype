using Godot;
using Godot.Collections;
using GodotPlugins.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RedHoliday.Nodes;

/// <summary>
/// 	Displays the text of a Phrase, and is clickable to advance to the next Phrase. Can also contain Choice buttons.
/// </summary>
public partial class NPhraseDisplay : Control
{
    private RedPhrase _currentPhrase;
    private bool _concatting;
    private List<char> _concatCurrentChars = new();
    private List<char> _concatRemainingChars = new();
    private float _concatInterval = 0.005f;
    private float _concatTimer = 0f;

    private VBoxContainer _phraseDisplayVBoxContainer;
    private PanelContainer _mainPanelContainer;
    private TextureRect _mainPanelContainerTexture;
    private MarginContainer _mainMarginContainer;
    private VBoxContainer _mainVBoxContainer;
    private Label _mainLabel;
    private ScrollContainer _choicesVBoxScrollContainer;
    private VBoxContainer _choicesVBoxContainer;
    private Label _debugLabel;
    private Label _nameLabel;

    public override void _Ready()
    {
        _phraseDisplayVBoxContainer = GetNode<VBoxContainer>("%PhraseDisplayVBoxContainer");
        _mainPanelContainer = GetNode<PanelContainer>("%MainPanelContainer");
        _mainPanelContainerTexture = GetNode<TextureRect>("%MainPanelContainerTexture");
        _mainMarginContainer = GetNode<MarginContainer>("%MainMarginContainer");
        _mainVBoxContainer = GetNode<VBoxContainer>("%MainVBoxContainer");
        _mainLabel = GetNode<Label>("%MainLabel");
        _choicesVBoxScrollContainer = GetNode<ScrollContainer>("%ChoicesVBoxScrollContainer");
        _choicesVBoxContainer = GetNode<VBoxContainer>("%ChoicesVBoxContainer");
        _debugLabel = GetNode<Label>("%DebugLabel");
        _nameLabel = GetNode<Label>("%NameLabel");

        _mainPanelContainer.GuiInput += OnMainPanelContainerGuiInput;
    }
    public override void _Process(double delta)
    {
        if (_currentPhrase == null)
        {
            return;
        }

        _concatTimer += (float)delta;
        
        if (_concatting == true && _concatTimer >= _concatInterval)
        {
            _concatTimer = 0f;
            if (_concatRemainingChars.Count > 0)
            {
                _concatCurrentChars.Add(_concatRemainingChars.First());
                _concatRemainingChars.RemoveAt(0);
                _mainLabel.Text = new string(_concatCurrentChars.ToArray());
            }
            else
            {
                _concatting = false;
                _concatRemainingChars.Clear();
                if (_currentPhrase.ChoiceLabels != null && _currentPhrase.ChoiceLabels.Count > 0)
                {
                    Grow();
                    PopulateChoices();
                }
            }
        }
    }

    public void Start(RedConversation conversation = null)
    {
        if (conversation == null)
        {
            conversation = RedStoryZone.Conversations.Find(c => c.First == true);
        }

        _currentPhrase = RedStoryZone.Phrases.Find(p => p.First == true);
        InitPhrase();
    }
    public void Advance()
    {
        if (_currentPhrase.Last == true)
        {
            GameState.MainNode.Finish();
            return;
        }

        _currentPhrase = _currentPhrase.GetNextPhrase();
        InitPhrase();
    }
    public void Advance(RedPhrase redPhrase)
    {
        if (_currentPhrase.Last == true)
        {
            GameState.MainNode.Finish();
            return;
        }

        _currentPhrase = redPhrase;
        InitPhrase();
    }
    public void InitPhrase()
    {
        Array<Node> vboxChildren = _choicesVBoxContainer.GetChildren();
        for (int i = 0; i < vboxChildren.Count; i++)
        {
            Shrink();
            vboxChildren[i].QueueFree();
        }
        
        _concatCurrentChars.Clear();
        _concatRemainingChars = _currentPhrase.Text.ToCharArray().ToList();
        _concatting = true;

        Tuple<string,string> nameDisplay = _currentPhrase.GetSpeakerDisplay();
        _nameLabel.Text = nameDisplay.Item1;
        _nameLabel.LabelSettings.FontColor = new Color(nameDisplay.Item2);
        _mainPanelContainerTexture.Modulate = new Color(nameDisplay.Item2);

        SFXPlayer.SoundFromString(_currentPhrase.Sfx);
        AmbiencePlayer.SoundFromString(_currentPhrase.Ambience);

        /*
        string nextPhrases = "";
        string nextChoices = "";

        if (_currentPhrase.NextPhraseLabels != null)
        {
            foreach (string phrase in _currentPhrase.NextPhraseLabels)
            {
                nextPhrases = nextPhrases + phrase + "   ";
            }
        }
        if (_currentPhrase.ChoiceLabels != null)
        {
            foreach (string choice in _currentPhrase.ChoiceLabels)
            {
                nextChoices = nextChoices + choice + "   ";
            }
        }
        */

        if (_currentPhrase.Bg != "")
        {
            Texture2D bgTexture = GD.Load<Texture2D>("res://Images/bg/" + _currentPhrase.Bg + ".png");
            if (bgTexture != null)
            {
                GameState.MainNode.SetBg(bgTexture);
            }
            else
            {
                GD.PrintErr("Could not load bg image: " + _currentPhrase.Bg);
            }
        }

        //_debugLabel.Text = "Phrase: " + _currentPhrase.Label + "   " + "| Next phrases: " + nextPhrases + "| Next choices: " + nextChoices;
    }
    public void Grow()
    {
        _phraseDisplayVBoxContainer.Size = new Vector2(1200f, 600f);
        _phraseDisplayVBoxContainer.Position = new Vector2(0f, -400f);
        _choicesVBoxScrollContainer.SizeFlagsStretchRatio = 1.2f;
        _nameLabel.Visible = false;
    }
    public void Shrink()
    {
        _phraseDisplayVBoxContainer.Size = new Vector2(1200f, 200f);
        _phraseDisplayVBoxContainer.Position = new Vector2(0f, 0f);
        _choicesVBoxScrollContainer.SizeFlagsStretchRatio = 0f;
        _nameLabel.Visible = true;
    }
    public void PopulateChoices()
    {
        foreach (string choice in _currentPhrase.ChoiceLabels)
        {
            RedChoice redChoice = RedStoryZone.Choices.Find(p => p.Label == choice);
            if (redChoice != null)
            {
                Node node = GD.Load<PackedScene>("res://Scenes/PhraseDisplayButton.tscn").Instantiate();
                NPhraseDisplayButton nPhraseDisplayButton = node as NPhraseDisplayButton;
                nPhraseDisplayButton.SetData(redChoice, this);
                _choicesVBoxContainer.AddChild(nPhraseDisplayButton);
            }
        }
    }

    // EVENTS
    
    public void OnMainPanelContainerGuiInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton && inputEvent.IsPressed())
		{
			InputEventMouseButton inputEventMouseButton = inputEvent as InputEventMouseButton;
			if (inputEventMouseButton.ButtonIndex == MouseButton.Left)
			{
                if (_concatting)
                {
                    SFXPlayer.Play_Click();
                    _concatCurrentChars.AddRange(_concatRemainingChars);
                    _mainLabel.Text = new string(_concatCurrentChars.ToArray());

                    _concatRemainingChars.Clear();
                    _concatting = false;
                    if (_currentPhrase.ChoiceLabels != null && _currentPhrase.ChoiceLabels.Count > 0)
                    {
                        Grow();
                        PopulateChoices();
                    }
                }
                else if (_currentPhrase.ChoiceLabels == null || _currentPhrase.ChoiceLabels.Count <= 0)
                {
                    SFXPlayer.Play_Click();
                    Advance();
                }
			}
		}
    }
}
