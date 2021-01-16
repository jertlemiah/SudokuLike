using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable, CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField]
    public int size = 9;
    [SerializeField]
    public string puzzleName = "puzzlename";
    [SerializeField]
    public List<ButtonData> buttonDataList = new List<ButtonData>();
}

[Serializable]
public class ButtonData
{
    [SerializeField]
    public bool isGiven = false;
    [SerializeField]
    public int index = 1,
        regionNum = 1,
        correctValue = 0,
        currentValue = 0;
}

/*newButtonData.index = button.idSelf;
            newButtonData.regionNum = button.regionNum;
            newButtonData.isGiven = button.isGiven;
            newButtonData.correctValue = button.correctValue;
 * */
