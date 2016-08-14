using UnityEngine;
using System.Collections;

/// <summary>
/// Generate a level.
/// </summary>
public class LevelGenerator : MonoBehaviour {
    private const float FLOOR_Y_OFFSET = -0.5f;

    public int _Seed;
    public GameObject _Wall;
    public GameObject _Floor;
    public int _Depth;
    public int _Width;

    public bool _Active = true;

    public GameObject[][] Level;

	// Use this for initialization
	public void Awake () {
        Random.InitState(_Seed);

        if (_Active) GenerateLevelStructure();
	}

    // Generate a simple level structure.
    // No specific algorithm is used to make a level make sense.
    // Every block has a 60/40 chance to be either a floor or a wall.
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
    }
}
