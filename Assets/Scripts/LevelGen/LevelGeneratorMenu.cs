using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Assets.Scripts.LevelGen
{
    public class LevelGeneratorMenu : EditorWindow
    {
        public int seed, x, y, z, width, depth;
        GameObject walls, floors;


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

            seed = EditorGUILayout.IntField("Seed", 0);

            GUILayout.Space(10f);

            x = EditorGUILayout.IntField("X", 0);
            y = EditorGUILayout.IntField("Y", 0);
            z = EditorGUILayout.IntField("Z", 0);

            GUILayout.Space(10f);

            width = EditorGUILayout.IntField("Width", 10);
            depth = EditorGUILayout.IntField("Depth", 10);
            
            walls = (GameObject)EditorGUILayout.ObjectField("Wall Prefab", walls, typeof(GameObject), false);
            floors = (GameObject)EditorGUILayout.ObjectField("Floor Prefab", floors, typeof(GameObject), false);

            GUILayout.Space(10f);

            if (GUILayout.Button("Generate Level In Editor")) {
                GameObject inEditorLevel = (GameObject)Resources.Load("LevelMarker");
                inEditorLevel = Instantiate(inEditorLevel);
                LevelGenerator generator = inEditorLevel.GetComponent<LevelGenerator>();
                generator._Active = false;

                inEditorLevel.transform.position = new Vector3(x, y, z);
                generator._Depth = depth;
                generator._Width = width;
                generator._Floor = floors;
                generator._Wall = walls;
                generator._Seed = seed;

                generator.GenerateLevelStructure();
            }
        }
    }
}
