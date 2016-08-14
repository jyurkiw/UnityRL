using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
    private const float FLOOR_Y_OFFSET = -0.5f;

    public int _Seed;
    public GameObject _Wall;
    public GameObject _Floor;
    public int _Depth;
    public int _Width;

    private GameObject[][] Level;

	// Use this for initialization
	public void Awake () {
        Random.InitState(_Seed);

        GenerateLevelStructure();
	}

    private void GenerateLevelStructure()
    {
        Level = new GameObject[_Depth][];
        for (int i = 0; i < _Depth; i++)
        {
            Level[i] = new GameObject[_Width];
            for (int j = 0; j < _Width; j++)
            {
                Vector3 local = new Vector3(j, 0, i);
                bool isFloor = false;

                if ((int)(Random.value * 100) > 50) Level[i][j] = Instantiate(_Wall);
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
