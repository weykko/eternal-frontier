using System;
using UnityEngine;

public class Grid : MonoBehaviour {
    private GridCell[,] cells;
    public Texture2D map;
    public GameObject cellPrefab;

    private void Start()
    {
        cells = new GridCell[map.width, map.height];
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {
                Color pixelColor = map.GetPixel(i, j);
                if (pixelColor.grayscale == 0)
                {
                    GameObject newCell = Instantiate(cellPrefab, transform);
                    newCell.transform.position = new Vector3(i, 0, j);
                    cells[i, j] = newCell.GetComponent<GridCell>();
                }
                else
                {
                    Debug.Log(pixelColor.grayscale);
                }
                
            }
        }
    }
}