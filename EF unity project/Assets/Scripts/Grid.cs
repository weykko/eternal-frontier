using System;
using Pathfinding.Examples;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour {
    private GridCell[,] cells;
    public Texture2D map;
    public Texture2D resourceMap;
    public GameObject cellPrefab;
    public GameObject[] treePrefabs;
    private Quaternion rotationNoise;

    private void Start()
    {
        rotationNoise = Quaternion.Euler(0, Random.Range(0, 360), 0);
        
        
        cells = new GridCell[map.width, map.height];
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {   
                
                Color pixelColor = map.GetPixel(i, j);
                if (pixelColor.grayscale == 0)
                {
                    GameObject newCell = Instantiate(cellPrefab, new Vector3(2 * i, 0, 2 * j), new Quaternion(0, 0, 0, 0), transform);
                    cells[i, j] = newCell.GetComponent<GridCell>();
                }
                //затычка
                else
                {
                    GameObject newCell = Instantiate(cellPrefab, new Vector3(2 * i, 2, 2 * j), new Quaternion(0, 0, 0, 0), transform);
                    cells[i, j] = newCell.GetComponent<GridCell>();
                }
            }
        }
        Vector3 y_pos = new Vector3(0, 1, 0);
        for (int i = 0; i < resourceMap.width; i++)
        {
            for (int j = 0; j < resourceMap.height; j++)
            {
                rotationNoise = Quaternion.Euler(0, Random.Range(0, 360), 0);
                Color pixelColor = resourceMap.GetPixel(i, j);
                if (pixelColor.grayscale == 0)
                {
                    GridCell existingCell = cells[i,j];
                    GameObject tree = Instantiate(treePrefabs[Random.Range(0, 6)], existingCell.transform.position + y_pos, rotationNoise, existingCell.transform);
                }
            }
        }
    }
}