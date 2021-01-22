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

    [SerializeField]
    public string currentState = "None";
    [SerializeField]
    public bool isCorrectValue = false, isHighlighted = false, isSelected = false;

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
    public GameObject UiSelector, UiSelected, UiPressed, UiHighlighted, UiIncorrect;

    private void Start()
    {
        UpdateStateVisuals();
    }

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

    private void UpdateStateVisuals()
    {
        //isCorrectValue = false, isHighlighted = false, isSelected = false;
        //_____Handle if digit is correct or not_____
        if (mainText.text.Length > 0 && !isCorrectValue)
        {
            // Turn the incorrect filter on
            if (UiIncorrect != null)
                UiIncorrect.SetActive(true);
            else
                Debug.Log("Button number " + idSelf + " does not have UiIncorrect setup correctly.");
            // Update the state text
            currentState = "Incorrect";
        }
        else
        {
            // Turn the incorrect filter off
            if (UiIncorrect != null)
                UiIncorrect.SetActive(false);
            else
                Debug.Log("Button number " + idSelf + " does not have UiIncorrect setup correctly.");
            // Update the state text
            if (mainText.text.Length > 0 && isCorrectValue)
                currentState = "Correct";
            else
                currentState = "No Value";
        }

        currentState = currentState + " and ";

        //_____Turn OFF highighted and selected filters_____
        if (UiHighlighted != null)
            UiHighlighted.SetActive(false);
        else
            Debug.Log("Button number " + idSelf + " does not have UiHighlighted setup correctly.");
        if (UiSelected != null)
            UiSelected.SetActive(false);
        else
            Debug.Log("Button number " + idSelf + " does not have UiSelected setup correctly.");

        // Turn on highlight filter
        if (isHighlighted)
        {
            Debug.Log("Turned highlighted filter on");
            if (UiHighlighted != null)
                UiHighlighted.SetActive(true);
            else
                Debug.Log("Button number " + idSelf + " does not have UiHighlighted setup correctly.");

            currentState = currentState + "Highlighted";
        }
        // Turn on selected filter
        else if (isSelected)
        {
            Debug.Log("Turned selected filter on");
            if (UiSelected != null)
                UiSelected.SetActive(true);
            else
                Debug.Log("Button number " + idSelf + " does not have UiSelected setup correctly.");
            currentState = currentState + "Selected";
        }
        else
        {
            // Both chould remain off
            currentState = currentState + "Not Selected or Highlighted";
        }

    }

    public void ChangeStates(bool isCorrectValue, bool isHighlighted, bool isSelected)
    {
        this.isCorrectValue = isCorrectValue;
        this.isHighlighted = isHighlighted;
        this.isSelected = isSelected;
        UpdateStateVisuals();
    }

    public void OnClick()
    {
        Debug.Log("Button onClick called");
        
        GameGridController.Instance.AddSelectedCells(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameGridController.Instance.AddSelectedCells(this);
        isSelected = true;
        ChangeStates(isCorrectValue, isHighlighted, true);
    }

    public void Deselect()
    {
        ChangeStates(isCorrectValue, isHighlighted, isSelected = false);
    }

    public void ResetCell()
    {
        currentValue = 0;
        correctValue = 0;
        isGiven = false;
        mainText.text = "";
        ChangeStates(false, false, false);
    }

    public void EraseCell()
    {
        if (!isGiven)
        {
            mainText.text = "";
            ChangeStates(false, isHighlighted, isSelected);
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
        ChangeStates((currentValue == correctValue), isHighlighted, isSelected);
    }

    public void UpdateMainText(bool writeGiven)
    {
        if (writeGiven || !isGiven)
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
