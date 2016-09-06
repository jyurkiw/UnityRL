using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	public string _CharacterName { get; set; }
	public IRace _Race { get; set; }
	public bool Dirty { get; set; }

	public PlayerInfo()
	{
		Dirty = false;
	}

	// Set the player race.
	// Default does nothing.
	public void SetRace(string race)
	{
		switch (race)
		{
			case "Human":
				_Race = new HumPH();
				break;
			default:
				break;
		}
	}
}
