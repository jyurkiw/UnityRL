using UnityEngine;
using System.Collections;

public enum StairType { UP, DOWN };

public class StairController : MonoBehaviour {
	public StairType _StairType;
	private bool canTrigger = true;

	public void Awake()
	{
		// Get the player controller
		PlayerController playerController = FindObjectOfType<PlayerController>();

		if (_StairType == StairType.UP && playerController.State == PlayerState.MOVING_DOWN ||
			_StairType == StairType.DOWN && playerController.State == PlayerState.MOVING_UP)
			canTrigger = false;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (canTrigger)
		{
			PlayerController player = FindObjectOfType<PlayerController>();
			bool changeLevel = false;

			if (_StairType == StairType.DOWN)
				changeLevel = player.ActiveLevel.SetNextSeed();
			else changeLevel = player.ActiveLevel.SetPreviousSeed();

			if (changeLevel)
			{
				if (_StairType == StairType.DOWN)
					player.State = PlayerState.MOVING_DOWN;
				else player.State = PlayerState.MOVING_UP;

				player.ActiveLevel.Init();
				player.CancelPathfindingAndMovement();
			}
			else
			{
				Debug.Log("Going back to town.");
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		canTrigger = true;
	}
}
