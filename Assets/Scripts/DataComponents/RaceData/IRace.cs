using UnityEngine;
using System.Collections;

// The IRace interface is a contract for race data.
// All race data is either looked up from a data file,
// or calculated somehow.
public interface IRace {
	string RaceName { get; }

	int HPPerLevel { get; }
	int MPPerLevel { get; }
}
