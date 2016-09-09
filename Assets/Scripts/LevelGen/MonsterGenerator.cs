using UnityEngine;
using System.Collections;

public class MonsterGenerator : MonoBehaviour {
	private LevelGenerator levelGenerator { get; set; }

	public void Awake()
	{
		levelGenerator = GetComponent<LevelGenerator>();
	}

	public void GenerateMonsters()
	{

	}
}
