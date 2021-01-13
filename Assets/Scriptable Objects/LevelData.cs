using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField]
    public int size = 9;
    [SerializeField]
    public List<ButtonData> buttonDataList = new List<ButtonData>();
}

[System.Serializable]
public class ButtonData
{
    [SerializeField]
    public int index, regionNum;
    [SerializeField]
    public bool isGiven = false;
    [SerializeField]
    public int correctValue = 1;
}
