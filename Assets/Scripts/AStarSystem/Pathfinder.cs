using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PathNode {
    public int g;
    public Vector2 loc;
    public PathNode parent;
}

// Basis for our A* pathfinding system.
public class Pathfinder {
    public NavType[][] BlockGrid;
    public HeuristicFinder heuristic = new HeuristicFinder();

    // Build a Pathfinder object for the passed level.
    public Pathfinder(LevelGenerator level)
    {
        BlockGrid = new NavType[level.Level.Length][];

        // Build the roomgrid
        for (int i = 0; i < level.Level.Length; i++)
        {
            BlockGrid[i] = new NavType[level.Level[i].Length];

            for (int j = 0; j < level.Level[i].Length; j++)
                BlockGrid[i][j] = level.Level[i][j].GetComponent<NavType>();
        }
    }

    public Stack<Vector2> FindPath(Vector2 start, Vector2 destination)
    {
        Stack<Vector2> path = new Stack<Vector2>();
        // TODO: Replace list<pathnode> with a binary heap
        List<PathNode> open = new List<PathNode>();
        HashSet<Vector2> closed = new HashSet<Vector2>();

        PathNode current = new PathNode() { g = 0, loc = start, parent = null };
        open.Add(current);

        while (current.loc != destination && open.Count > 0)
        {
            open = open.OrderBy(o => o.g).ToList();
            current = open[0];
            open.RemoveAt(0);
            closed.Add(current.loc);

            // target square location storage vector
            Vector2 target;

            // Check adjacent squares for viability
            // Viable blocks are ones that...
            // 1) are in range of the BlockList
            // 2) Are not in the closed list
            // 4) Are not walls

            List<Vector2> adjacentBlocks = new List<Vector2>();
            // Check up
            target = new Vector2(current.loc.x, current.loc.y - 1);
            if (target.y >= 0 && !closed.Contains(target) && BlockGrid[(int)target.y][(int)target.x].blockType != BlockType.WALL)
                adjacentBlocks.Add(target);
            else if (target.y >= 0 && BlockGrid[(int)target.y][(int)target.x].blockType == BlockType.WALL)
                closed.Add(target);

            // Check left
            target = new Vector2(current.loc.x - 1, current.loc.y);
			if (target.x >= 0 && !closed.Contains(target) && BlockGrid[(int)target.y][(int)target.x].blockType != BlockType.WALL)
                adjacentBlocks.Add(target);
            else if (target.x >= 0 && BlockGrid[(int)target.y][(int)target.x].blockType == BlockType.WALL)
                closed.Add(target);

            // Check down
            target = new Vector2(current.loc.x, current.loc.y + 1);
			if (target.y < BlockGrid.Length && !closed.Contains(target) && BlockGrid[(int)target.y][(int)target.x].blockType != BlockType.WALL)
                adjacentBlocks.Add(target);
            else if (target.y < BlockGrid.Length && BlockGrid[(int)target.y][(int)target.x].blockType == BlockType.WALL)
                closed.Add(target);

            // Check right
            target = new Vector2(current.loc.x + 1, current.loc.y);
			if (target.x < BlockGrid[(int)target.y].Length && !closed.Contains(target) && BlockGrid[(int)target.y][(int)target.x].blockType != BlockType.WALL)
                adjacentBlocks.Add(target);
            else if (target.x < BlockGrid[(int)target.y].Length && BlockGrid[(int)target.y][(int)target.x].blockType == BlockType.WALL)
                closed.Add(target);

            foreach (Vector2 loc in adjacentBlocks)
                if (open.Where(o => o.loc == loc).Count() == 0)
                    open.Add(new PathNode() { g = current.g + BlockGrid[(int)loc.y][(int)loc.x].GValue, loc = loc, parent = current });
                else
                {
                    PathNode targetNode = open.Where(o => o.loc == loc).FirstOrDefault();

                    // check for reparantage-efficiency
					if (targetNode != null && current.g + BlockGrid[(int)loc.y][(int)loc.x].GValue < targetNode.g)
                    {
                        int tnIdx = open.IndexOf(targetNode);
						open[tnIdx].g = current.g + BlockGrid[(int)loc.y][(int)loc.x].GValue;
                        open[tnIdx].parent = current;
                    }
                }
        }

        if (current.loc == destination)
        {
            while (current.loc != start)
            {
                path.Push(current.loc);
                current = current.parent;
            }
		}
		else
		{
			// If we're in here, there was no valid path.
			path = new Stack<Vector2>();
		}

        return path;
    }
}
