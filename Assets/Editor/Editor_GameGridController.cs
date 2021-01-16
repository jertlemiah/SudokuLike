using UnityEngine;
using UnityEditor;
//using co

[CustomEditor(typeof(GameGridController))]
[CanEditMultipleObjects]
public class Editor_GameGridController : Editor
{
    public override void OnInspectorGUI()
    {
        GameGridController myTarget = (GameGridController)target;

        DrawDefaultInspector();
        //EditorGUILayout.LabelField("Repopulate Fate Reaons");
        GUILayout.Label("Editor Buttons");
        //GUIStyle.
        //Editor.
        if (GUILayout.Button("Get Regions"))
        {
            myTarget.GetRegions();
        }
        if (GUILayout.Button("Update Disjoint Set"))
        {
            myTarget.UpdateDisjointSet();
        }
        if (GUILayout.Button("Update Cell Walls"))
        {
            myTarget.UpdateCellWalls();
        }
        if (GUILayout.Button("Clear Board"))
        {
            myTarget.ClearBoard();
        }

        if (GUILayout.Button("GenerateLevelData()"))
        {
            myTarget.GenerateLevelData();
        }
        //Editor.
        //GUIStyle.
        if (GUILayout.Button("Load Level Data"))
        {
            myTarget.LoadLevelData(myTarget.levelToLoad);
        }
        //GUILayout.


        if (GUILayout.Button("UpdateAllButtonText()"))
        {
            myTarget.UpdateAllButtonText();
        }
    }
}