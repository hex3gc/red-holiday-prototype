using System;
using System.Collections.Generic;
using System.Linq;

namespace RedHoliday;

/// <summary>
///     A small, optional story beat. Contains a sequence of Phrases with localized branching.
/// </summary>
public class RedConversation
{
    public string Label { get; private set; }
    public int Priority { get; private set; }
    public List<string> PhraseLabels { get; private set; }
    public List<string> NextConversationLabels { get; private set; }
    public List<IRedCondition> Conditions { get; private set; }
    public bool First { get; private set; }
    public bool Last { get; private set; }

    public RedConversation(string label, int priority, List<string> phraseLabels, List<string> nextConversationLabels, List<IRedCondition> conditions = default, bool first = false, bool last = false)
    {
        Label = label;
        Priority = priority;
        PhraseLabels = phraseLabels;
        NextConversationLabels = nextConversationLabels;
        Conditions = conditions;
        First = first;
        Last = last;

        RedStoryZone.Conversations.Add(this);
    }
}