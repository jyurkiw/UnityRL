using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generate a level.
/// </summary>
public class LevelGenerator : MonoBehaviour {
    private const float FLOOR_Y_OFFSET = -0.5f;
    
    public int _Seed;
    public GameObject _Wall;
    public GameObject _Floor;
	public GameObject _UpStair;
	public GameObject _DownStair;
    public int _Depth;
    public int _Width;

    public GameObject[][] Level;
    public Pathfinder pathfinder;

	public Vector2 UpStairLoc { get; set; }
	public Vector2 DownStairLoc { get; set; }

	private LinkedList<int> seeds = new LinkedList<int>();
	private PlayerController playerController;

	// Use this for initialization
	public void Awake () {
		playerController = FindObjectOfType<PlayerController>();
		Random.InitState(_Seed);

		playerController.State = PlayerState.MOVING_DOWN;
        Init();
		playerController.State = PlayerState.WAIT_FOR_INPUT;
	}

	// Initialize the floor based on the _Seed value.
    public void Init()
    {
		if (Level != null)
			for (int i = 0; i < Level.Length; i++)
				for (int j = 0; j < Level[i].Length; j++)
					Destroy(Level[i][j]);

		Level = null;

		GenerateLevelStructure();
        pathfinder = new Pathfinder(this);
    }

    // Generate a simple level structure.
    // No specific algorithm is used to make a level make sense.
    // Every block has a 60/40 chance to be either a floor or a wall.
	// Set one random floor tile to be a stair up.
	// Set one random floor tile to be a stair down.
    public void GenerateLevelStructure()
    {
        Level = new GameObject[_Depth][];

        for (int i = 0; i < _Depth; i++)
        {
            Level[i] = new GameObject[_Width];
            for (int j = 0; j < _Width; j++)
            {
                Vector3 local = new Vector3(j, 0, i);
                bool isFloor = false;

                if ((int)(Random.value * 100) > 60) Level[i][j] = Instantiate(_Wall);
                else
                {
                    Level[i][j] = Instantiate(_Floor);
                    isFloor = true;
                }

                if (isFloor) local.y = FLOOR_Y_OFFSET;

                Level[i][j].transform.parent = transform;
                Level[i][j].transform.localPosition = local;
            }
        }

		// Turn a random tile into a down-stair
		int dsY = Random.Range(0, Level.Length);
		int dsX = Random.Range(0, Level[dsY].Length);
		Vector3 stairPos = new Vector3(dsX, 0, dsY);
		DownStairLoc = new Vector2(dsX, dsY);

		Destroy(Level[dsY][dsX]);
		Level[dsY][dsX] = Instantiate(_DownStair);
		Level[dsY][dsX].transform.localPosition = stairPos;

		// Set the position of the player controller
		if (playerController.State == PlayerState.MOVING_UP)
			playerController.transform.position = new Vector3(dsX, 0, dsY);

		dsY = Random.Range(0, Level.Length);
		dsX = Random.Range(0, Level[dsY].Length);
		stairPos = new Vector3(dsX, 1.2f, dsY);
		UpStairLoc = new Vector2(dsX, dsY);

		Destroy(Level[dsY][dsX]);
		Level[dsY][dsX] = Instantiate(_UpStair);
		Level[dsY][dsX].transform.localPosition = stairPos;

		// Set the position of the player controller
		if (playerController.State == PlayerState.MOVING_DOWN)
			playerController.transform.position = new Vector3(dsX, 0, dsY);
    }

	// Store the previous seed value, and generate a new one.
	// Make sure to reset random after generating the next seed,
	// or the first floor will not be the same when you come back
	// from the second for the first time.
	public bool SetNextSeed()
	{
		seeds.AddLast(_Seed);
		Random.InitState(_Seed);
		_Seed = Random.Range(0, int.MaxValue);
		Random.InitState(_Seed);

		return true;
	}

	// Move back to the previous seed value.
	public bool SetPreviousSeed()
	{
		if (seeds.Count == 0) return false;

		_Seed = seeds.Last.Value;
		seeds.RemoveLast();
		Random.InitState(_Seed);
		return true;
	}
}
