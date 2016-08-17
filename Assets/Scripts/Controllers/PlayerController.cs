using UnityEngine;
using System.Collections.Generic;

public enum PlayerState { WAIT_FOR_INPUT, CAMERA_MOVING, MOVING_UP, MOVING_DOWN }

/// <summary>
/// Basic player controller for a 3d roguelike.
/// </summary>
public class PlayerController : MonoBehaviour {
    public Camera _PlayerCamera;
    public float _MaxCameraMoveSpeedPerSecond = 4f;
    public bool _DebugOn = false;

    public const string CLICK_SPHERE = "MarkerSphere";
    public const string PATH_SPHERE = "PathSphere";
    public const float CLICK_SPHERE_Y_OFFSET = 1f;

    private static GameObject CLICK_SPHERE_PREFAB;
    private static GameObject PATH_SPHERE_PREFAB;

    public PlayerState State { get; set; }
    private Vector3 MoveTarget { get; set; }
	private Stack<Vector2> MovePath { get; set; }

	private LevelGenerator _activeLevel;
	public LevelGenerator ActiveLevel
	{
		get
		{
			if (_activeLevel == null || !_activeLevel.enabled)
				_activeLevel = GameObject.FindObjectOfType<LevelGenerator>() as LevelGenerator;
			return _activeLevel;
		}
	}
	
    public void Awake()
    {
        CLICK_SPHERE_PREFAB = (GameObject)Resources.Load(CLICK_SPHERE);
        PATH_SPHERE_PREFAB = (GameObject)Resources.Load(PATH_SPHERE);

		MovePath = new Stack<Vector2>();
    }

	// Update is called once per frame
	public void Update () {
        // Mouse input capture (Left click navigation)
        // Use raycast from camera to determine what block the user is clicking on.
	    if (State == PlayerState.WAIT_FOR_INPUT && Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = _PlayerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform != null && LegalNavigationBlock(hit.transform))
                {
                    // Perform pathfinding.
                    MovePath = ActiveLevel.pathfinder.FindPath(new Vector2(transform.position.x, transform.position.z), new Vector2(hit.transform.position.x, hit.transform.position.z));

					if (MovePath.Count > 0)
					{
						// If debug mode is on, draw a click-sphere
						PlaceClickSphere(hit.transform);

						// If debug mode is on, draw the nav path
						PlaceNavSpheres(MovePath, hit.transform);

						// Set the initial move target
						MoveTarget = TranslatePathfindingToGrid(MovePath.Pop());

						// Set player state
						State = PlayerState.CAMERA_MOVING;
					}
                }
            }
        }

		if (State == PlayerState.WAIT_FOR_INPUT && Input.anyKeyDown)
		{
			Vector2 target = new Vector2(transform.position.x, transform.position.z);

			// Find the desired direciton of movment, and move the character
			// W
			if (Input.GetKeyDown(KeyCode.W) && ActiveLevel.IsTargetBlockLegalForMove(target + Vector2.up)) target = target + Vector2.up;
			// A
			else if (Input.GetKeyDown(KeyCode.A) && ActiveLevel.IsTargetBlockLegalForMove(target + Vector2.left)) target = target + Vector2.left;
			// S
			else if (Input.GetKeyDown(KeyCode.S) && ActiveLevel.IsTargetBlockLegalForMove(target + Vector2.down)) target = target + Vector2.down;
			// D
			else if (Input.GetKeyDown(KeyCode.D) && ActiveLevel.IsTargetBlockLegalForMove(target + Vector2.right)) target = target + Vector2.right;

			MoveTarget = new Vector3(target.x, transform.position.y, target.y);
			State = PlayerState.CAMERA_MOVING;
		}

        // Move the player to the move target
        // Camera movement is bound by the _MaxCameraMoveSpeedPerSecond value
		// Movement should follow the MovePath returned by the A* pathfinder
        if (State == PlayerState.CAMERA_MOVING)
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTarget, _MaxCameraMoveSpeedPerSecond * Time.deltaTime);

            // Check for movement completion and set state as appropriate.
			if (transform.position == MoveTarget && MovePath.Count > 0)
				MoveTarget = TranslatePathfindingToGrid(MovePath.Pop());
			else if(transform.position == MoveTarget && MovePath.Count == 0)
                State = PlayerState.WAIT_FOR_INPUT;
        }
	}

    // Is the passed transform a legal target for player navigation?
    private bool LegalNavigationBlock(Transform target)
    {
		BlockType targetType = target.GetComponent<NavType>().blockType;
		return targetType == BlockType.FLOOR || targetType == BlockType.STAIR;
    }

    // Set of drawn clickspheres.
    private HashSet<Transform> clickSpheres;

    // Place a click sphere
    // Do not place if the target is not a legal navigation block, or if
    // a sphere has already been drawn at that transform location.
    private void PlaceClickSphere(Transform target)
    {
        if (_DebugOn)
        {
            // the clickSpheres hashset is a kind of singleton
            if (clickSpheres == null) clickSpheres = new HashSet<Transform>();

            // Draw the clickSphere
            if (!clickSpheres.Contains(target))
            {
                clickSpheres.Add(target);
                GameObject clickSphere = Instantiate(CLICK_SPHERE_PREFAB);
                clickSphere.transform.position = new Vector3(target.position.x, target.position.y + CLICK_SPHERE_Y_OFFSET, target.position.z);
            }
        }
    }

	// List of navigation spheres for debugging
    private List<GameObject> navSpheres = new List<GameObject>();

	// Place navigation spheres if debug is on.
    private void PlaceNavSpheres(Stack<Vector2> path, Transform target)
    {
        if (_DebugOn)
        {
            // remove all existing pathing spheres
            for (int i = 0; i < navSpheres.Count; i++)
                Destroy(navSpheres[i]);

            navSpheres = new List<GameObject>();

			Stack<Vector2> tempPath = new Stack<Vector2>(new Stack<Vector2>(path));

			while (tempPath.Count > 1)
            {
				Vector2 pathStep = tempPath.Pop();
                GameObject navSphere = Instantiate(PATH_SPHERE_PREFAB);
                navSphere.transform.position = new Vector3(pathStep.x, target.position.y + CLICK_SPHERE_Y_OFFSET, pathStep.y);
                navSpheres.Add(navSphere);
            }
        }
    }

	// translate a pathfinding Vector2 to a Vector3 position.
	private Vector3 TranslatePathfindingToGrid(Vector2 node)
	{
		return new Vector3(node.x, 0, node.y);
	}

	// Cancel all pathfinding and movement.
	public void CancelPathfindingAndMovement()
	{
		MovePath.Clear();
		State = PlayerState.WAIT_FOR_INPUT;
	}
}
