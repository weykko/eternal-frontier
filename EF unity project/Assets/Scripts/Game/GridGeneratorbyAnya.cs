using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneratorbyAnya : MonoBehaviour
{
    public GameObject cubePrefab; // Префаб куба
    public int gridSize = 10; // Размер сетки
    public float spacing = 1.1f; // Расстояние между кубами

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                // Вычисляем позицию каждого куба
                Vector3 position = new Vector3(x * spacing, 0, z * spacing);

                // Создаем куб
                Instantiate(cubePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
