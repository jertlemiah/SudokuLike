using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController_GridNumber : ButtonController
{
    [SerializeField]
    public bool turnOutlineLeftOn, turnOutlineRightOn, turnOutlineTopOn, turnOutlineBottomOn;

    [SerializeField]
    public GameObject outlineLeftObj, outlineRightObj, outlineTopObj, outlineBottomObj;

    [SerializeField]
    public int currentValue = 0, correctValue = 1;

    [SerializeField]
    public int idSelf, regionNum = 1, rowNum = 1, colNum = 1,
        idLeft, idRight, idTop, idBottom;

    [SerializeField]
    public TMP_Text mainText;

    [SerializeField]
    public bool givenDigit = false;

    public void OnClick()
    {
        Debug.Log("Button onClick called");
        
            GameGridController.Instance.AddSelectedCells(this);
    }

    public void UpdateMainText(int newValue)
    {
        if (!givenDigit)
        {
            if (newValue == 0)
                mainText.text = "";
            else
                mainText.text = newValue.ToString();
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
