using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace RedHoliday;

/// <summary>
///     The smallest narrative element. The text (and, optionally, the choices) of a Phrase are displayed on screen.
/// </summary>
public class RedPhrase
{
    public string Label { get; private set; }
    public string Text { get; private set; }
    public RedSpeaker Speaker { get; private set; }
    public int Priority { get; private set; }
    public List<string> NextPhraseLabels { get; private set; }
    public List<IRedCondition> Conditions { get; private set; }
    public List<string> ChoiceLabels { get; private set; }
    public bool First { get; private set; }
    public bool Last { get; private set; }
    public string Bg { get; private set; }
    public string Sfx { get; private set; }
    public string Ambience { get; private set; }

    public RedPhrase(string label, string text, RedSpeaker speaker, int priority, List<string> nextPhraseLabels, List<string> choiceLabels = default, List<IRedCondition> conditions = default, bool first = false, bool last = false, string bg = "", string sfx = "", string ambience = "")
    {
        Label = label;
        Text = text;
        Speaker = speaker;
        Priority = priority;
        NextPhraseLabels = nextPhraseLabels;
        Conditions = conditions;
        ChoiceLabels = choiceLabels;
        First = first;
        Last = last;
        Bg = bg;
        Sfx = sfx;
        Ambience = ambience;

        RedStoryZone.Phrases.Add(this);
    }

    public RedPhrase GetNextPhrase() // TODO implement priority
    {
        foreach (string label in NextPhraseLabels)
        {
            RedPhrase candidate = RedStoryZone.Phrases.Find(p => p.Label == label);
            if (candidate != null)
            {
                return candidate;
            }
        }

        throw new ArgumentException("No next phrase found for " + Label);
    }

    public Tuple<string,string> GetSpeakerDisplay()
    {
        switch (Speaker)
        {
            case RedSpeaker.Cal:
                return new Tuple<string,string>("CAL", Globals.BOLD_MAIN_COLOR);
            case RedSpeaker.Earl:
                return new Tuple<string, string>("EARL", Globals.PATIENT_MAIN_COLOR);
            case RedSpeaker.Lace:
                return new Tuple<string, string>("LACE", Globals.ANXIOUS_MAIN_COLOR);
            case RedSpeaker.Player:
            case RedSpeaker.Unknown:
                return new Tuple<string, string>("???", "#999999");
            case RedSpeaker.Narrator:
            default:
                return new Tuple<string, string>("", "#999999");
        }
    }
}