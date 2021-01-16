using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor;

public class ButtonController_GridNumber : MonoBehaviour, ISelectHandler
{
    //[Header("Cell Values")]
    [SerializeField]
    public int currentValue, correctValue, regionNum = 1;

    //[Header("")]
    //EditorStyles.foldout
    //GUIStyle.foldout
    //[Header("Visual Objects")]
    [SerializeField]
    public bool turnOutlineLeftOn, turnOutlineRightOn, turnOutlineTopOn, turnOutlineBottomOn;

    [SerializeField]
    public GameObject outlineLeftObj, outlineRightObj, outlineTopObj, outlineBottomObj;

    

    [SerializeField]
    public int idSelf, rowNum = 1, colNum = 1,
        idLeft, idRight, idTop, idBottom;

    [SerializeField]
    public TMP_Text mainText;

    [SerializeField]
    public bool isGiven = false;

    [SerializeField]
    public GameObject UiSelector, UiSelected, UiPressed;

    public void LoadButtonData(ButtonData buttonData)
    {
        correctValue = buttonData.correctValue;
        currentValue = buttonData.currentValue;


        isGiven = buttonData.isGiven;

        if (isGiven)
            currentValue = correctValue;
        else
            currentValue = buttonData.currentValue;

        regionNum = buttonData.regionNum;

        UpdateMainText(true);
    }

    public void OnClick()
    {
        Debug.Log("Button onClick called");
        
            GameGridController.Instance.AddSelectedCells(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameGridController.Instance.AddSelectedCells(this);
        if (UiSelected != null)
            UiSelected.SetActive(true);
    }

    public void Deselect()
    {
        if (UiSelected != null)
            UiSelected.SetActive(false);
    }

    public void EraseCell()
    {
        if (!isGiven)
        {
            mainText.text = "";
        }
        else
        {
            Debug.Log("Cannot erase a given digit");
        }
    }
    /* need to fix this so that I can internally update the cell and externally update it
     * 
     * 
     */

    public void UpdateValue(int newValue)
    {
        currentValue = newValue;
        UpdateMainText(false);
    }

    public void UpdateMainText(bool writeGiven)
    {
        if (!isGiven || writeGiven)
        {
            if (currentValue == 0)
                mainText.text = "";
            else
                mainText.text = currentValue.ToString();
        }
        else
        {
            Debug.Log("Cannot change the value of a given digit");
        }        
    }

    public void UpdateCellWalls()
    {
        /*_____Left_____*/
        if (turnOutlineLeftOn && outlineLeftObj)
            outlineLeftObj.SetActive(true);
        else if (outlineLeftObj)
            outlineLeftObj.SetActive(false);

        /*_____Right_____*/
        if (turnOutlineRightOn && outlineRightObj)
            outlineRightObj.SetActive(true);
        else if (outlineRightObj)
            outlineRightObj.SetActive(false);

        /*_____Top_____*/
        if (turnOutlineTopOn && outlineTopObj)
            outlineTopObj.SetActive(true);
        else if (outlineTopObj)
            outlineTopObj.SetActive(false);

        /*_____Bottom_____*/
        if (turnOutlineBottomOn && outlineBottomObj)
            outlineBottomObj.SetActive(true);
        else if (outlineBottomObj)
            outlineBottomObj.SetActive(false);
    }
}
