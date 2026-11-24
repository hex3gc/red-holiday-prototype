using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace RedHoliday;

/// <summary>
///     Consumes a JSON file and generates story objects, placing them in the RedStoryZone
/// </summary>
public static class StoryGenerator
{
    public const string CONVERSATION_DIR = "res://Scripts/Eras/Encounters/Conversations/";

    public static void Init()
    {
        using DirAccess dir = DirAccess.Open(CONVERSATION_DIR);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir())
                {
                    GD.Print($"Loading file: " + fileName);
                    var file = FileAccess.Open(CONVERSATION_DIR + fileName, FileAccess.ModeFlags.Read);
                    if (file != null)
                    {
                        string content = file.GetAsText();
                        JSON2Conversation(content);
                    }
                    else
                    {
                        throw new System.Exception("Conversation file opened null: " + fileName);
                    }
                }

                fileName = dir.GetNext();
            }
        }
        else
        {
            throw new System.Exception("Conversation path missing");
        }
    }

    public static void JSON2Conversation(string j)
    {
        Json json = new Json();
        Error error = json.Parse(j);
        if (error == Error.Ok)
        {
            Dictionary conversationData = json.Data.AsGodotDictionary();
            Array phrases = conversationData["phrases"].AsGodotArray();
            List<string> phraseLabels = new();

            foreach (Variant phrase in phrases)
            {
                Dictionary phraseData = phrase.AsGodotDictionary();
                List<string> choiceLabels = new();

                if (phraseData.ContainsKey("choices"))
                {
                    Array choices = phraseData["choices"].AsGodotArray();
                    
                    foreach(Variant choice in choices)
                    {
                        Dictionary choiceData = choice.AsGodotDictionary();
                        List<RedAxisBump> axisBumps = new();

                        if (choiceData.ContainsKey("bumps"))
                        {
                            Array bumps = choiceData["bumps"].AsGodotArray();

                            foreach (Variant bump in bumps)
                            {
                                Dictionary bumpData = bump.AsGodotDictionary();
                                var tuple = GetAxis(bumpData["axis"].ToString());
                                RedAxis redAxis = tuple.Item1;
                                bool redAxisAlign = tuple.Item2;
                                RedAxis.BumpStrength bumpStrength = GetBumpStrength(bumpData["strength"].ToString());

                                RedAxisBump redAxisBump = new RedAxisBump
                                (
                                    redAxis,
                                    redAxisAlign,
                                    bumpStrength
                                );

                                axisBumps.Add(redAxisBump);
                            }
                        }

                        RedChoice redChoice = new RedChoice
                        (
                            phraseData["phrase"].AsString() + choiceData["choice"].AsString(),
                            choiceData["text"].AsString(),
                            choiceData["priority"].AsInt16(),
                            choiceData["next-phrases"].AsStringArray().ToList(),
                            axisBumps.Count > 0 ? axisBumps : new()
                        );

                        choiceLabels.Add(phraseData["phrase"].AsString() + choiceData["choice"].AsString());
                        RedStoryZone.Choices.Add(redChoice);
                    }
                }

                RedPhrase redPhrase = new
                (
                    phraseData["phrase"].AsString(),
                    phraseData["text"].AsString(),
                    GetSpeaker(phraseData["speaker"].AsString()),
                    phraseData["priority"].AsInt16(),
                    phraseData.ContainsKey("next-phrases") ? phraseData["next-phrases"].AsStringArray().ToList() : null,
                    choiceLabels.Count > 0 ? choiceLabels : new(),
                    // TODO conditions
                    first: phraseData.ContainsKey("first"),
                    last: phraseData.ContainsKey("last"),
                    bg: phraseData.ContainsKey("image") ? phraseData["image"].AsString() : "",
                    sfx: phraseData.ContainsKey("sfx") ? phraseData["sfx"].AsString() : "",
                    ambience: phraseData.ContainsKey("ambience") ? phraseData["ambience"].AsString() : ""
                );

                phraseLabels.Add(phraseData["phrase"].AsString());
                RedStoryZone.Phrases.Add(redPhrase);
            }

            RedConversation redConversation = new
            (
                conversationData["conversation"].AsString(),
                conversationData["priority"].AsInt16(),
                conversationData["next-conversations"].AsStringArray().ToList(),
                phraseLabels,
                // TODO conditions
                first: conversationData.ContainsKey("first"),
                last: conversationData.ContainsKey("last")
            );

            RedStoryZone.Conversations.Add(redConversation);
        }
        else
        {
            throw new System.Exception("Could not parse conversation JSON: " + json.GetErrorMessage());
        }
    }

    public static RedSpeaker GetSpeaker(string name)
    {
        switch(name.ToLower())
        {
            case "narrator":
                return RedSpeaker.Narrator;
            case "player":
                return RedSpeaker.Player;
            case "earl":
                return RedSpeaker.Earl;
            case "cal":
                return RedSpeaker.Cal;
            case "lace":
                return RedSpeaker.Lace;
            case "???":
            case "unknown":
                return RedSpeaker.Unknown;
            default:
                throw new System.ArgumentException("Invalid RedSpeaker supplied from JSON: " + name);
        }
    }

    public static System.Tuple<RedAxis, bool> GetAxis(string name)
    {
        switch(name.ToLower())
        {
            case "anxious":
                return new System.Tuple<RedAxis,bool>(GameState.AnxiousRelaxed, false);
            case "relaxed":
                return new System.Tuple<RedAxis,bool>(GameState.AnxiousRelaxed, true);
            case "honest":
                return new System.Tuple<RedAxis,bool>(GameState.HonestDishonest, false);
            case "dishonest":
                return new System.Tuple<RedAxis,bool>(GameState.HonestDishonest, true);
            case "patient":
                return new System.Tuple<RedAxis,bool>(GameState.PatientImpulsive, false);
            case "impulsive":
                return new System.Tuple<RedAxis,bool>(GameState.PatientImpulsive, true);
            case "shy":
                return new System.Tuple<RedAxis,bool>(GameState.ShyBold, false);
            case "bold":
                return new System.Tuple<RedAxis,bool>(GameState.ShyBold, true);
            case "kind":
                return new System.Tuple<RedAxis,bool>(GameState.KindMean, false);
            case "mean":
                return new System.Tuple<RedAxis,bool>(GameState.KindMean, true);
            default:
                throw new System.ArgumentException("Invalid RedAxis supplied from JSON: " + name);
        }
    }

    public static RedAxis.BumpStrength GetBumpStrength(string name)
    {
        switch(name.ToLower())
        {
            case "small":
                return RedAxis.BumpStrength.small;
            case "medium":
                return RedAxis.BumpStrength.medium;
            case "large":
                return RedAxis.BumpStrength.large;
            default:
                throw new System.ArgumentException("Invalid BumpStrength supplied from JSON: " + name);
        }
    }
}