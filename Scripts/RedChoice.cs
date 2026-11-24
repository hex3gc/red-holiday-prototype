using System;
using System.Collections.Generic;
using System.Linq;

namespace RedHoliday;

/// <summary>
///     A choice attached to a Phrase. If at least one exists on a Phrase, it'll override any 'next phrases'!
/// </summary>
public class RedChoice
{
    public string Label { get; private set; }
    public string Text { get; private set; }
    public int Priority { get; private set; }
    public List<string> NextPhraseLabels { get; private set; }
    public List<RedAxisBump> AxisBumps { get; private set; }
    public List<IRedCondition> Conditions { get; private set; }

    public RedChoice(string label, string text, int priority, List<string> nextPhraseLabels, List<RedAxisBump> axisBumps = default, List<IRedCondition> conditions = default)
    {
        Label = label;
        Text = text;
        Priority = priority;
        NextPhraseLabels = nextPhraseLabels;
        AxisBumps = axisBumps;
        Conditions = conditions;

        RedStoryZone.Choices.Add(this);
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
}