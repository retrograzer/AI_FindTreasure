using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridSetup : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject playerPrefab;
    public Transform gridLayoutParent;
    public int rowCount, columnCount;
    public Color wallColor = Color.gray;
    public Color treasureColor = Color.yellow;
    public int wallPercent = 1;

    FlexibleGridLayout gridLayout;
    List<CellBehavior> allCurrentCells = new List<CellBehavior>();
    CellBehavior currentPlayer;

    private void Awake()
    {
        gridLayout = gridLayoutParent.GetComponent<FlexibleGridLayout>();
    }

    void Start()
    {
        CreateNewGrid();
    }

    //Clean this whole thing out of GetComponents
    void CreateNewGrid ()
    {
        gridLayout.rows = rowCount;
        gridLayout.columns = columnCount;

        allCurrentCells.Clear();

        for (int colNum = 0; colNum < columnCount; colNum++)
        {
            for (int rowNum = 0; rowNum < rowCount; rowNum++)
            {
                GameObject newCellGO = Instantiate(cellPrefab, Vector2.zero, Quaternion.identity);
                CellBehavior newCell = newCellGO.GetComponent<CellBehavior>();

                //Is this thing a wall?
                if (Random.Range(1, 100) <= wallPercent)
                {
                    newCell.IsWall = true;
                    newCell.SetCellColor(wallColor);
                }

                allCurrentCells.Add(newCell);
            }
        }

        // Set the location of the Treasure on the grid
        int rngTreasureLocation = Random.Range(0, allCurrentCells.Count);

        while (allCurrentCells[rngTreasureLocation].IsWall)
        {
            rngTreasureLocation = Random.Range(0, allCurrentCells.Count);
        }

        allCurrentCells[rngTreasureLocation].IsTreasure = true;
        allCurrentCells[rngTreasureLocation].SetCellColor(treasureColor);

        // Set all the cells parent to the grid layout so that they auto-align
        foreach (CellBehavior index in allCurrentCells)
        {
            index.transform.SetParent(gridLayoutParent);
        }

        //Set the location of the player on the grid
        int rngPlayerLocation = Random.Range(0, allCurrentCells.Count);

        while (allCurrentCells[rngPlayerLocation].IsWall || allCurrentCells[rngPlayerLocation].IsTreasure)
        {
            rngPlayerLocation = Random.Range(0, allCurrentCells.Count);
        }

        currentPlayer = allCurrentCells[rngPlayerLocation];
        currentPlayer.SetCellColor(Color.green);
    }

    public void ReloadLevel () { SceneManager.LoadScene(0); }
}
