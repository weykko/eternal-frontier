using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Grid : MonoBehaviour {
    private GridCell[,] cells;
    public int towerPosX;
    public int towerPosY;
    public Texture2D map;
    public Texture2D resourceMap;
    public GameObject cellPrefab;
    public GameObject TowerPrefab;
    public GameObject[] treePrefabs;
    private Quaternion rotationNoise;
    private Color firstHeight;
    private Color secondHeight;
    private Color thirdHeight;
    private readonly float colorTolerance = 0.01f;

    private void Start()
    {
        firstHeight = new Color(0.188f, 0.188f, 0.188f, 1);
        secondHeight = new Color(0.506f, 0.506f, 0.506f, 1);
        thirdHeight = Color.white;
        
        rotationNoise = Quaternion.Euler(0, Random.Range(0, 360), 0);
        
        cells = new GridCell[map.width, map.height];
        
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {
                int yOffset = 0;
                Color pixelColor = map.GetPixel(i, j);
                if (AreColorsSimilar(pixelColor, firstHeight, colorTolerance))
                {
                    yOffset = 1;
                } else if (AreColorsSimilar(pixelColor, secondHeight, colorTolerance))
                {
                    yOffset = 2;
                } else if (AreColorsSimilar(pixelColor, thirdHeight, colorTolerance))
                {
                    yOffset = 3;
                }
                GameObject newCell = Instantiate(cellPrefab, new Vector3(2 * i, 2 * yOffset, 2 * j), new Quaternion(0, 0, 0, 0), transform);
                newCell.name = "Cell" + i + "," + j;
                cells[i, j] = newCell.GetComponent<GridCell>();
            }
        }
        Vector3 yTreesOffset = new Vector3(0, 1, 0);
        for (int i = 0; i < resourceMap.width; i++)
        {
            for (int j = 0; j < resourceMap.height; j++)
            {
                rotationNoise = Quaternion.Euler(0, Random.Range(0, 360), 0);
                Color pixelColor = resourceMap.GetPixel(i, j);
                if (AreColorsSimilar(pixelColor, Color.black, colorTolerance))
                {
                    GridCell existingCell = cells[i,j];
                    GameObject tree = Instantiate(treePrefabs[Random.Range(0, 6)], existingCell.transform.position + yTreesOffset, rotationNoise, existingCell.transform);
                    existingCell.IsOccupied = true;
                    existingCell.Occupant = tree;
                }
            }
        }
        GridCell midCell = cells[towerPosX, towerPosY];
        GameObject tower = Instantiate(TowerPrefab, midCell.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

// Iterate through the 7x7 block (from -3 to +3 around the tower's center)
        for (int i = -3; i <= 3; i++)
        {
            for (int j = -3; j <= 3; j++)
            {
                int cellX = towerPosX + i;
                int cellY = towerPosY + j;

                // Check bounds to avoid accessing cells outside the grid
                if (cellX >= 0 && cellX < map.width && cellY >= 0 && cellY < map.height)
                {
                    // If the cell is occupied, clear it
                    if (cells[cellX, cellY].IsOccupied)
                    {
                        Destroy(cells[cellX, cellY].Occupant);
                        cells[cellX, cellY].IsOccupied = false;
                    }

                    // Mark the cell as occupied by the tower
                    cells[cellX, cellY].IsOccupied = true;
                    cells[cellX, cellY].Occupant = tower;
                }
            }
        }

        
    }
    private bool AreColorsSimilar(Color color1, Color color2, float tolerance)
    {
        return Mathf.Abs(color1.r - color2.r) <= tolerance &&
               Mathf.Abs(color1.g - color2.g) <= tolerance &&
               Mathf.Abs(color1.b - color2.b) <= tolerance;
    }
}