using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameGridController))]
[CanEditMultipleObjects]
public class Editor_GameGridController : Editor
{
    public override void OnInspectorGUI()
    {
        GameGridController myTarget = (GameGridController)target;

        DrawDefaultInspector();
        //EditorGUILayout.LabelField("Repopulate Fate Reaons");
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
        
    }
}