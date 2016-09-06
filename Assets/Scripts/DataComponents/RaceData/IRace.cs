using UnityEngine;
using System.Collections;

public interface IRace {
	string RaceName { get; }

	int HPPerLevel { get; }
	int MPPerLevel { get; }
}
