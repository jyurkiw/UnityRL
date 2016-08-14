using UnityEngine;
using System.Collections.Generic;

public enum PlayerState { WAIT_FOR_INPUT, CAMERA_MOVING }

/// <summary>
/// Basic player controller for a 3d roguelike.
/// </summary>
public class PlayerController : MonoBehaviour {
    public Camera _PlayerCamera;
    public float _MaxCameraMoveSpeedPerSecond = 4f;
    public bool _DebugOn = false;

    public const string CLICK_SPHERE = "MarkerSphere";
    public const float CLICK_SPHERE_Y_OFFSET = 1f;

    public PlayerState State { get; set; }
    private Vector3 MoveTarget { get; set; }
	
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
                    // Set player state
                    State = PlayerState.CAMERA_MOVING;

                    // Set the player's move target
                    MoveTarget = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);

                    // if debug mode is on, draw a click-sphere
                    PlaceClickSphere(hit.transform);
                }
            }
        }

        // Move the player to the move target
        // Camera movement is bound by the _MaxCameraMoveSpeedPerSecond value
        if (State == PlayerState.CAMERA_MOVING)
        {
            transform.position = Vector3.MoveTowards(transform.position, MoveTarget, _MaxCameraMoveSpeedPerSecond * Time.deltaTime);

            // Check for movement completion and set state as appropriate.
            if (transform.position == MoveTarget)
                State = PlayerState.WAIT_FOR_INPUT;
        }
	}

    // Is the passed transform a legal target for player navigation?
    private bool LegalNavigationBlock(Transform target)
    {
        return target.GetComponent<NavType>().blockType == BlockType.FLOOR;
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
                GameObject clickSpherePrefab = (GameObject)Resources.Load(CLICK_SPHERE);
                GameObject clickSphere = Instantiate(clickSpherePrefab);
                clickSphere.transform.position = new Vector3(target.position.x, target.position.y + CLICK_SPHERE_Y_OFFSET, target.position.z);
            }
        }
    }
}
