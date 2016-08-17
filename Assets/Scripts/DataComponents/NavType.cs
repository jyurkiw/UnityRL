using UnityEngine;
using System.Collections;

public enum BlockType { FLOOR, WALL, STAIR };

/// <summary>
/// Nav Type data component.
/// Has no functionality except to store navigation data about a block.
/// </summary>
public class NavType : MonoBehaviour {
    public BlockType blockType;

	public int GValue
	{
		get
		{
			switch(blockType)
			{
				case BlockType.FLOOR: return 1;
				case BlockType.WALL: return int.MaxValue;
				case BlockType.STAIR: return 100;
				default: return 1;
			}
		}
	}
}
