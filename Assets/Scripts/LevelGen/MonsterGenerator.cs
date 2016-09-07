using UnityEngine;
using System.Collections;

public class MonsterGenerator : MonoBehaviour {
	private LevelGenerator levelGenerator { get; set; }

	void Awake()
	{
		levelGenerator = GetComponent<LevelGenerator>();
	}


}
