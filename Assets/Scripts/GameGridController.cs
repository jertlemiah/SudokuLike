using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridController : Singleton<GameGridController>
{
    //[SerializeField]
    //private List<ButtonController_GridNumber> allButtons = new List<ButtonController_GridNumber>();
    private ButtonController_GridNumber[] allButtons;
    [SerializeField]
    public Dictionary<int,List<ButtonController_GridNumber>> allRegions = new Dictionary<int, List<ButtonController_GridNumber>>();
    [SerializeField]
    public DisjSets disjointSet;
    [SerializeField]
    public GameObject gridLayoutGroup;
    //private List<int> knownRegionNums = new List<int>();
    [SerializeField]
    public int size = 9; // This is a generic size used for width, height, & region size

    [SerializeField]
    public List<ButtonController_GridNumber> selectedCells = new List<ButtonController_GridNumber>();
    [SerializeField]
    public string toolState = "";

    // Start is called before the first frame update
    void Start()
    {
        // Create a disjoint set of width*height for all the cells
        //disjointSet = new DisjSets(size * size);
        //allButtons.Clear();
        //foreach (List<ButtonController_GridNumber> region in allRegions)
        //{
        //    foreach(ButtonController_GridNumber button in region)
        //    {
        //        allButtons.Add(button);
        //    }
        //}
        //if(allRegions.Values.l)
            GetRegions();
        UpdateDisjointSet();
        UpdateCellWalls();
    }

    public void ClearBoard()
    {
        Debug.Log("ClearBoard called");
        Debug.Log("allButtons.length: " + allButtons.Length);
        foreach (ButtonController_GridNumber button in allButtons)
        {
            button.UpdateMainText(0);
        }
    }

    public void UpdateSelectedCells(int newValue)
    {
        Debug.Log("UpdateSelectedCells called with value "+newValue);
        foreach (ButtonController_GridNumber cell in selectedCells)
        {
            cell.UpdateMainText(newValue);
        }
    }

    public void AddSelectedCells(ButtonController_GridNumber newCell)
    {
        if (toolState != "MultiSelect")
            ClearSelectedCells();
        selectedCells.Add(newCell);
    }

    public void ClearSelectedCells()
    {
        selectedCells.Clear();
    }

    public void GetRegions()
    {
        Debug.Log("GetRegions called");
        //knownRegionNums.Clear();
        allRegions.Clear();
        int i = 0;
        allButtons = gridLayoutGroup.GetComponentsInChildren<ButtonController_GridNumber>();
        //Debug.Log("Size of allButtons " + allButtons.Length);
        foreach (ButtonController_GridNumber button in allButtons)
        {
            //Debug.Log("ID " + button.regionNum);
            button.idSelf = i;
            // If the region doesn't exist yet, create a new region List
            if (!allRegions.ContainsKey(button.regionNum))
            {
                // Create a new regionList, add this current button to that list, then add the list to allRegions
                List<ButtonController_GridNumber> newList = new List<ButtonController_GridNumber>();
                newList.Add(button);
                allRegions.Add(button.regionNum, newList);
            }
            // else, add this button to the existing region
            else
            {
                allRegions[(button.regionNum)].Add(button);
            }
            i++;
        }
    }

    // Update is called once per frame
    public void UpdateDisjointSet()
    {
        Debug.Log("UpdateDisjointSet called");
        // Create a disjoint set of width*height for all the cells
        disjointSet = new DisjSets(size * size);
        /*
         * for each cell in a region, union those cells
         */
        foreach (KeyValuePair<int, List<ButtonController_GridNumber>> entry in allRegions)
        {
            List<ButtonController_GridNumber> region = entry.Value;
            ButtonController_GridNumber firstButton = region[0];
            foreach (ButtonController_GridNumber button in region)
            {
                //disjointSet.Union();
                //Debug.Log("disjointSet.Find(firstButton.idSelf): " + disjointSet.Find(firstButton.idSelf));
                //Debug.Log("disjointSet.Find(button.idSelf): " + disjointSet.Find(button.idSelf));
                // If the roots are different, remove wall, otherwise ignore
                if (disjointSet.Find(firstButton.idSelf) != disjointSet.Find(button.idSelf))
                {
                    // Union the roots of the two squares
                    disjointSet.Union(disjointSet.Find(firstButton.idSelf), disjointSet.Find(button.idSelf));
                }
                else
                {
                    //System.out.printf("\nNot removing wall %d", randomWall.index);
                }
            }
        }
    }

    public void UpdateCellWalls()
    {
        Debug.Log("UpdateCellWalls called");
        /*/
         * For each cell in a region
         *      if wall is adjacent to another cell in the region, turn that wall off
         *      else (it is a wall touching another region) turn that wall on
         */
        foreach (KeyValuePair<int, List<ButtonController_GridNumber>> entry in allRegions)
        {
            List<ButtonController_GridNumber> region = entry.Value;
            //Debug.Log("Region size: " + region.Count);
            foreach (ButtonController_GridNumber button in region)
            {
                // If the root of this set is equal to the root of the adjacent set, then they are in the same set

                /*____Left_____*/
                
                // if on the right edge, set idRight to -1
                if ((button.idSelf + 1) % size == 1)
                    button.idLeft = -1;
                else
                    button.idLeft = button.idSelf - 1;
                //Debug.Log("idLeft" + button.idLeft);
                if (button.idLeft == -1 || (disjointSet.Find(button.idSelf) != disjointSet.Find(button.idLeft)))
                    button.turnOutlineLeftOn = true;
                else
                    button.turnOutlineLeftOn = false;

                /*____Right_____*/
                // if on the right edge, set idRight to -1
                if ((button.idSelf + 1) % size == 0)
                    button.idRight = -1;
                else
                    button.idRight = button.idSelf + 1;
                //Debug.Log("idRight" + button.idRight);
                if (button.idRight == -1 || (disjointSet.Find(button.idSelf) != disjointSet.Find(button.idRight)))
                    button.turnOutlineRightOn = true;
                else
                    button.turnOutlineRightOn = false;

                /*____Top_____*/
                // if on the top row, set idTop to -1
                if (button.idSelf < size)
                    button.idTop = -1;
                else
                    button.idTop = button.idSelf - size;
                //Debug.Log("idTop" + button.idTop);
                if (button.idTop == -1 || (disjointSet.Find(button.idSelf) != disjointSet.Find(button.idTop)))
                    button.turnOutlineTopOn = true;
                else
                    button.turnOutlineTopOn = false;

                /*____Bottom_____*/
                // if on the top row, set idTop to -1
                if (button.idSelf > (disjointSet.GetArrLength() - size - 1))
                    button.idBottom = -1;
                else
                    button.idBottom = button.idSelf + size;
                //Debug.Log("idBottom" + button.idBottom);
                if (button.idBottom == -1 || (disjointSet.Find(button.idSelf) != disjointSet.Find(button.idBottom)))
                    button.turnOutlineBottomOn = true;
                else
                    button.turnOutlineBottomOn = false;

                button.UpdateCellWalls();

                button.rowNum = (button.idSelf / size) + 1;
                button.colNum = (button.idSelf % size) + 1;
            }
        }
    }


}
