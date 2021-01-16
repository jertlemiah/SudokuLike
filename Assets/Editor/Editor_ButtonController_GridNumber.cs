using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(ButtonController_GridNumber))]
[CanEditMultipleObjects]
public class Editor_ButtonController_GridNumber : Editor
{
    private bool showVisualObjectsFoldout;
    private bool showProgrammingProperties;
    private bool defaultDraw;

    public override void OnInspectorGUI()
    {
        ButtonController_GridNumber myTarget = (ButtonController_GridNumber)target;

        GUILayout.Space(10f);
        GUILayout.Label("Cell details", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.IntField("Cell Index", myTarget.idSelf);
        EditorGUILayout.IntField("Column Number", myTarget.colNum);
        EditorGUILayout.IntField("Row Number", myTarget.rowNum);
        EditorGUI.EndDisabledGroup();

        GUILayout.Space(10f);
        GUILayout.Label("Cell Properties to be set", EditorStyles.boldLabel);
        myTarget.currentValue = EditorGUILayout.IntField("Current Value", myTarget.currentValue);
        myTarget.correctValue = EditorGUILayout.IntField("Correct Value", myTarget.correctValue);
        myTarget.isGiven = EditorGUILayout.Toggle("Is Given Digit", myTarget.isGiven);
        myTarget.regionNum = EditorGUILayout.IntField("Region number", myTarget.regionNum);

        GUILayout.Space(10f);
        showVisualObjectsFoldout = EditorGUILayout.Foldout(showVisualObjectsFoldout, "Button Objects");
        if (showVisualObjectsFoldout)
        {
            myTarget.mainText = (TMP_Text)EditorGUILayout.ObjectField("Cell Text", myTarget.mainText, typeof(TMP_Text), false);

            GUILayout.Space(5f);

            myTarget.outlineLeftObj = (GameObject)EditorGUILayout.ObjectField("Outline Left Object", myTarget.outlineLeftObj, typeof(GameObject), false);   
            myTarget.outlineRightObj = (GameObject)EditorGUILayout.ObjectField("Outline Right Object", myTarget.outlineRightObj, typeof(GameObject), false);
            myTarget.outlineTopObj = (GameObject)EditorGUILayout.ObjectField("Outline Top Object", myTarget.outlineTopObj, typeof(GameObject), false);
            myTarget.outlineBottomObj = (GameObject)EditorGUILayout.ObjectField("Outline Bottom Object", myTarget.outlineBottomObj, typeof(GameObject), false);

            GUILayout.Space(5f);

            myTarget.UiSelector = (GameObject)EditorGUILayout.ObjectField("Selector ui", myTarget.UiSelector, typeof(GameObject), false);
            myTarget.UiSelected = (GameObject)EditorGUILayout.ObjectField("Selected ui", myTarget.UiSelected, typeof(GameObject), false);
            myTarget.UiPressed = (GameObject)EditorGUILayout.ObjectField("Pressed ui", myTarget.UiPressed, typeof(GameObject), false);
        }
        // UiSelector, UiSelected, UiPressed;
        //turnOutlineLeftOn, turnOutlineRightOn, turnOutlineTopOn, turnOutlineBottomOn;
        GUILayout.Space(5f);
        showProgrammingProperties = EditorGUILayout.Foldout(showProgrammingProperties, "Programming Properties");
        if (showProgrammingProperties)
        {
            EditorGUI.BeginDisabledGroup(true);
            //myTarget.mainText.text = EditorGUILayout.TextField("Cell Text", myTarget.mainText.text);

            //GUILayout.Space(5f);

            myTarget.idLeft = EditorGUILayout.IntField("Index of Left cell", myTarget.idLeft);
            myTarget.idRight = EditorGUILayout.IntField("Index of Right cell", myTarget.idRight);
            myTarget.idTop = EditorGUILayout.IntField("Index of Top cell", myTarget.idTop);
            myTarget.idBottom = EditorGUILayout.IntField("Index of Bottom cell", myTarget.idBottom);

            GUILayout.Space(5f);

            myTarget.turnOutlineLeftOn = EditorGUILayout.Toggle("Turn Left Outline On", myTarget.turnOutlineLeftOn);
            myTarget.turnOutlineRightOn = EditorGUILayout.Toggle("Turn Right Outline On", myTarget.turnOutlineRightOn);
            myTarget.turnOutlineTopOn = EditorGUILayout.Toggle("Turn Top Outline On", myTarget.turnOutlineTopOn);
            myTarget.turnOutlineBottomOn = EditorGUILayout.Toggle("Turn Bottom Outline On", myTarget.turnOutlineBottomOn);
            //EditorGUILayout.ObjectField("Outline Right Object", myTarget.outlineRightObj, typeof(GameObject), false);
            //EditorGUILayout.ObjectField("Outline Top Object", myTarget.outlineTopObj, typeof(GameObject), false);
            //EditorGUILayout.ObjectField("Outline Bottom Object", myTarget.outlineBottomObj, typeof(GameObject), false);

            //GUILayout.Space(5f);

            //EditorGUILayout.ObjectField("Selector ui", myTarget.UiSelector, typeof(GameObject), false);
            //EditorGUILayout.ObjectField("Selected ui", myTarget.UiSelected, typeof(GameObject), false);
            //EditorGUILayout.ObjectField("Pressed ui", myTarget.UiPressed, typeof(GameObject), false);
            EditorGUI.EndDisabledGroup();
        }
        //idSelf, rowNum = 1, colNum = 1,
        //idLeft, idRight, idTop, idBottom;
        defaultDraw = EditorGUILayout.Foldout(defaultDraw, "Default Draw Settings");
        if (defaultDraw)
        {
            DrawDefaultInspector();
        }
        
        //EditorGUILayout.LabelField("Repopulate Fate Reaons");
        //GUILayout.Label("Editor Buttons");
        //GUIStyle.
        //Editor.
        //if (GUILayout.Button("Get Regions"))
        //{
        //    myTarget.GetRegions();
        //}
    }
}
