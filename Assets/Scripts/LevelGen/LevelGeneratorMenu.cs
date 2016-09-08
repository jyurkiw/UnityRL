using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Assets.Scripts.LevelGen
{
    public class LevelGeneratorMenu : EditorWindow
    {
        public int seed, x = 0, y = 0, z = 0, width, depth;
        GameObject walls, floors, stairsUp, stairsDown;


        // Add menu named "Generate Level In-Editor" to the Level Gen menu
        [MenuItem("Level Gen/Generate Level In-Editor")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            LevelGeneratorMenu window = (LevelGeneratorMenu)EditorWindow.GetWindow(typeof(LevelGeneratorMenu));
            window.Show();
        }

        // Create a gui to collect data, and build an in-editor level with the passed attributes.
        // Intended for debugging.
        void OnGUI()
        {
            GUILayout.Label("Generate Level In-Place", EditorStyles.boldLabel);

            seed = EditorGUILayout.IntField("Seed", seed);

            GUILayout.Space(10f);

            x = EditorGUILayout.IntField("X", x);
            y = EditorGUILayout.IntField("Y", y);
            z = EditorGUILayout.IntField("Z", z);

            GUILayout.Space(10f);

            width = EditorGUILayout.IntField("Width", width);
            depth = EditorGUILayout.IntField("Depth", depth);
            
            walls = (GameObject)EditorGUILayout.ObjectField("Wall Prefab", walls, typeof(GameObject), false);
            floors = (GameObject)EditorGUILayout.ObjectField("Floor Prefab", floors, typeof(GameObject), false);
			stairsUp = (GameObject)EditorGUILayout.ObjectField("Stairs Up Prefab", stairsUp, typeof(GameObject), false);
			stairsDown = (GameObject)EditorGUILayout.ObjectField("Stairs Down Prefab", stairsDown, typeof(GameObject), false);

            GUILayout.Space(10f);

            if (GUILayout.Button("Generate Level In Editor")) {
                GameObject inEditorLevel = (GameObject)Resources.Load("LevelMarker");
                inEditorLevel = Instantiate(inEditorLevel);
                LevelGenerator generator = inEditorLevel.GetComponent<LevelGenerator>();

                inEditorLevel.transform.position = new Vector3(x, y, z);
                generator._Depth = depth;
                generator._Width = width;
                generator._Floor = floors;
                generator._Wall = walls;
				generator._UpStair = stairsUp;
				generator._DownStair = stairsDown;
                generator._Seed = seed;
				generator.InEditor = true;

                generator.GenerateLevelStructure();
            }
        }
    }
}
