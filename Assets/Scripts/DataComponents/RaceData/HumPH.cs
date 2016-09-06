using UnityEngine;
using System.Collections;

// Human race placeholder class
// Real races will be loaded from a data file in the future.
public class HumPH : IRace {

	public string RaceName
	{
		get { return "Human"; }
	}

	public int HPPerLevel
	{
		get { return 0; }
	}

	public int MPPerLevel
	{
		get { return 0; }
	}
}
