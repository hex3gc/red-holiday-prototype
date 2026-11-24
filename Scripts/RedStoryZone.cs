using System;
using System.Collections.Generic;
using System.Linq;

namespace RedHoliday;

/// <summary>
///     Construction site for the entire story. Hard-coded for prototyping purposes.
/// </summary>
public static class RedStoryZone
{
    public static List<RedConversation> Conversations = new();
    public static List<RedPhrase> Phrases = new();
    public static List<RedChoice> Choices = new();

    /*
    public static void Init()
    {
        // Waking up, reflecting
        RedConversation WAKEUP = new
        (
            "WAKEUP",
            0,
            [],
            first: true,
            last: true
        );

        // A - Initial waking up
        #region
        {
            RedPhrase WAKEUP_A1 = new
            (
                "WAKEUP_A1",
                "Your head is still pounding. A rough landing.\n\nAbout twelve hours ago, you became a denizen of the RED HOLIDAY.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A2"],
                first: true
            );
            RedPhrase WAKEUP_A2 = new
            (
                "WAKEUP_A2",
                "On the Martian surface, sleep can feel like you're floating.\n\nWhen you last were truly floating - that quiet year in interplanetary space - you were given a steady 1G to keep you feeling spry. The 0.38G you're experiencing now might explain why you feel so lethargic.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A3"]
            );
            RedPhrase WAKEUP_A3 = new
            (
                "WAKEUP_A3",
                "A little less than twelve hours ago, you were brought to this bed by the arm, stumbling and dizzy. You think names were exchanged with the one carrying you, briefly, but you don't remember what his was.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A4"]
            );
            RedPhrase WAKEUP_A4 = new
            (
                "WAKEUP_A4",
                "At least you still remember your own.\n\n(TODO enter your name prompt)",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A5"]
            );
            RedPhrase WAKEUP_A5 = new
            (
                "WAKEUP_A5",
                "Names aside, you seem to remember faces. Four faces, but five people.\n\nThey've probably been waiting for you to get out of bed for a while.",
                RedSpeaker.Narrator,
                0,
                [],
                ["WAKEUP_A5_C1", "WAKEUP_A5_C2", "WAKEUP_A5_C3", "WAKEUP_A5_C4"]
            );
                RedChoice WAKEUP_A5_C1 = new
                (
                    "WAKEUP_A5_C1",
                    "I could still use more sleep.",
                    0,
                    ["WAKEUP_B1"],
                    [
                        new RedAxisBump
                        (
                            GameState.AnxiousRelaxed,
                            true,
                            RedAxis.BumpStrength.small
                        )
                    ]
                );
                RedChoice WAKEUP_A5_C2 = new
                (
                    "WAKEUP_A5_C2",
                    "I'll stay put. Thinking about introductions turns my stomach...",
                    1,
                    ["WAKEUP_B1"],
                    [
                        new RedAxisBump
                        (
                            GameState.ShyBold,
                            false,
                            RedAxis.BumpStrength.small
                        ),
                        new RedAxisBump
                        (
                            GameState.AnxiousRelaxed,
                            false,
                            RedAxis.BumpStrength.medium
                        )
                    ]
                );
                RedChoice WAKEUP_A5_C3 = new
                (
                    "WAKEUP_A5_C3",
                    "I'll go. I am curious to learn their names...",
                    2,
                    ["WAKEUP_A6"],
                    [
                        new RedAxisBump
                        (
                            GameState.KindMean,
                            false,
                            RedAxis.BumpStrength.small
                        ),
                    ]
                );
                RedChoice WAKEUP_A5_C4 = new
                (
                    "WAKEUP_A5_C4",
                    "Just get up and get it over with.",
                    3,
                    ["WAKEUP_A6"],
                    [
                        new RedAxisBump
                        (
                            GameState.ShyBold,
                            true,
                            RedAxis.BumpStrength.small
                        ),
                        new RedAxisBump
                        (
                            GameState.PatientImpulsive,
                            true,
                            RedAxis.BumpStrength.small
                        ),
                    ]
                );
            RedPhrase WAKEUP_A6 = new
            (
                "WAKEUP_A6",
                "When you roll to the open side of the bed you feel an ache in your legs, your hips, and then all over. The pounding in your head gets worse after it leaves the pillow.\n\nSlow going, but you end up at a sink with a mirror. Judging by your luggage being on the floor, this room's yours, and so are the toiletries.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A7"]
            );
            RedPhrase WAKEUP_A7 = new
            (
                "WAKEUP_A7",
                "The mirror's surface is spotless and so is everything else. Your space clothes on the other hand are stale sweat rags, wrinkled from all the stirring you did in the night, yet not with a scent as overpoweringly sour as your breath. You could've sworn you felt cleaner back in the capsule.\n\nThe least you can do is brush your teeth. The toothpaste on Mars is a refreshing mint.",
                RedSpeaker.Narrator,
                0,
                ["TODO"]
            );
        }
        #endregion

        // B - Wake up path; stay in bed
        #region
        {
            RedPhrase WAKEUP_B1 = new
            (
                "WAKEUP_B1",
                "So, you stay in bed.\n\nYou hoped sleep would come easy, but it does not. An ache begins to creep through your body and your head is still pounding, and you begin to wish that the blankets were warmer.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_B2"]
            );
            RedPhrase WAKEUP_B2 = new
            (
                "WAKEUP_B2",
                "You also need to pee.",
                RedSpeaker.Narrator,
                0,
                [],
                ["WAKEUP_B2_C1", "WAKEUP_B2_C2"]
            );
                RedChoice WAKEUP_B2_C1 = new
                (
                    "WAKEUP_B2_C1",
                    "No, not yet... it's comfy.",
                    0,
                    ["WAKEUP_B3"],
                    [
                        new RedAxisBump
                        (
                            GameState.AnxiousRelaxed,
                            true,
                            RedAxis.BumpStrength.medium
                        ),
                    ]
                );
                RedChoice WAKEUP_B2_C2 = new
                (
                    "WAKEUP_B2_C2",
                    "Oh, fine.",
                    1,
                    ["WAKEUP_B6"],
                    []
                );
            RedPhrase WAKEUP_B3 = new
            (
                "WAKEUP_B3",
                "Time passes. Despite your relative weightlessness, you feel so heavy in the bed, and despite your frail condition, you can eke out some comfort from this moment.\n\n...are you sure about not going to the bathroom? The last chance you got was over half a day ago.",
                RedSpeaker.Narrator,
                0,
                [],
                ["WAKEUP_B3_C1", "WAKEUP_B3_C2"]
            );
                RedChoice WAKEUP_B3_C1 = new
                (
                    "WAKEUP_B3_C1",
                    "I'm not leaving this bed. Never!",
                    0,
                    ["WAKEUP_B4"],
                    [
                        new RedAxisBump
                        (
                            GameState.AnxiousRelaxed,
                            true,
                            RedAxis.BumpStrength.large
                        ),
                    ]
                );
                RedChoice WAKEUP_B3_C2 = new
                (
                    "WAKEUP_B3_C2",
                    "Okay, fine!",
                    1,
                    ["WAKEUP_B6"],
                    []
                );
            RedPhrase WAKEUP_B4 = new
            (
                "WAKEUP_B4",
                "Good luck with that.\n\nTime passes once again and, unfortunately, your head and your body and your bladder begin to stress so much that relaxing becomes impossible.\n\nEventually your reflexes take over and force you to move.",
                RedSpeaker.Narrator,
                0,
                [],
                ["WAKEUP_B4_C1"]
            );
                RedChoice WAKEUP_B4_C1 = new
                (
                    "WAKEUP_B4_C1",
                    "No...",
                    0,
                    ["WAKEUP_B5"],
                    []
                );
            RedPhrase WAKEUP_B5 = new
            (
                "WAKEUP_B5",
                "Yes.\n\nIt turns out that Martian toilets are a luxury compared to interplanetary ones. Toilet paper is no longer a tightly rationed resource, so you feel a particularly great deal of relief.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A7"]
            );
            RedPhrase WAKEUP_B6 = new
            (
                "WAKEUP_B6",
                "Good.\n\nIt turns out that Martian toilets are a luxury compared to interplanetary ones. Toilet paper is no longer a tightly rationed resource, so you feel a particularly great deal of relief.",
                RedSpeaker.Narrator,
                0,
                ["WAKEUP_A7"]
            );
        }
        #endregion
    }
    */
}