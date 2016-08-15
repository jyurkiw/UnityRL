using UnityEngine;
using System.Collections;


public class HeuristicFinder {
    public float[][] hValueGrid;

    // Calculate all h-values for every block in the grid.
    // Wall blocks are automatically set to int.maxvalue so that
    // they will be automatically ignored.
    // NOTE: This means that F will need to be a long/double type in the
    // A* implementation, or we will run into overflow issues.
    public void BuildHeuristics(NavType[][] blockGrid, Vector2 destination)
    {
        hValueGrid = new float[blockGrid.Length][];

        for (int y = 0; y < blockGrid.Length; y++)
        {
            hValueGrid[y] = new float[blockGrid[y].Length];

            // calculate heuristics using the manhattan method, restricted to non-diagonal movement
            // (because there is no diagonal movement allowed in the game).
            for (int x = 0; x < blockGrid[y].Length; x++)
                if (blockGrid[y][x].blockType == BlockType.FLOOR)
                    hValueGrid[y][x] = (Mathf.Max(y, destination.y) - Mathf.Min(y, destination.y)) + (Mathf.Max(x, destination.x) - Mathf.Min(x, destination.x));
                else hValueGrid[y][x] = float.MaxValue;
        }
    }
}
