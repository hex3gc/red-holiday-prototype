using Godot;
using System;
using System.Linq;

namespace RedHoliday.Nodes;

/// <summary>
/// 	A debug textbox showing existing tags.
/// </summary>
public partial class NTagDisplay : Label
{
	public void Update()
	{
		Text = Concat();
	}

	public static string Concat()
	{
		string ret = "TAGS:";
		bool first = true;

		foreach (string tag in GameState.Tags)
		{
			if (first)
			{
				ret = ret + " " + tag;
				first = false;
			}
			else
			{
				ret = ret + ", " + tag;
			}
		}

		return ret;
	}
}
