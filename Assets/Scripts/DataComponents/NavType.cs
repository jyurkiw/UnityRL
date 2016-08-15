using UnityEngine;
using System.Collections;

public enum BlockType { FLOOR, WALL, STAIR };

/// <summary>
/// Nav Type data component.
/// Has no functionality except to store navigation data about a block.
/// </summary>
public class NavType : MonoBehaviour {
    public BlockType blockType;
}
