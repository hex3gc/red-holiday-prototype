using System;
using System.Collections.Generic;
using RedHoliday.Nodes;
using static RedHoliday.Globals;

namespace RedHoliday;

/// <summary>
/// 	Central object for keeping track of the current game's status.
/// </summary>
public static class GameState
{
	public static NMain MainNode;

	// Axes
	public static RedAxis KindMean = new("KIND", "MEAN", KIND_MAIN_COLOR, MEAN_MAIN_COLOR);
	public static RedAxis ShyBold = new("SHY", "BOLD", SHY_MAIN_COLOR, BOLD_MAIN_COLOR);
	public static RedAxis HonestDishonest = new("HONEST", "DISHONEST", HONEST_MAIN_COLOR, DISHONEST_MAIN_COLOR);
	public static RedAxis PatientImpulsive = new("PATIENT", "IMPULSIVE", PATIENT_MAIN_COLOR, IMPULSIVE_MAIN_COLOR);
	public static RedAxis AnxiousRelaxed = new("ANXIOUS", "RELAXED", ANXIOUS_MAIN_COLOR, RELAXED_MAIN_COLOR);

	// Tags
	public static List<string> Tags = new();
}
