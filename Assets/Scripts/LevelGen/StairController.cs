using UnityEngine;
using System.Collections;

public enum StairType { UP, DOWN };

public class StairController : MonoBehaviour {
	public StairType _StairType;
	private bool canTrigger = true;

	private const string PLAYER_OBJECT_NAME = "PlayerModel";

	public void Awake()
	{
		if (_StairType == StairType.UP) canTrigger = false;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (canTrigger)
		{
			PlayerController player = FindObjectOfType<PlayerController>();
			bool changeLevel = false;

			if (_StairType == StairType.DOWN)
				changeLevel = player.activeLevel.SetNextSeed();
			else changeLevel = player.activeLevel.SetPreviousSeed();

			if (changeLevel)
			{
				player.CancelPathfindingAndMovement();
				player.activeLevel.Init();
			}
			else
			{
				Debug.Log("Going back to town.");
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (_StairType == StairType.UP) canTrigger = true;
	}
}
