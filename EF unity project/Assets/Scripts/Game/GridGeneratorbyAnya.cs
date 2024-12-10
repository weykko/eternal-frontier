using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneratorbyAnya : MonoBehaviour
{
    public GameObject cubePrefab; // ������ ����
    public int gridSize = 10; // ������ �����
    public float spacing = 1.1f; // ���������� ����� ������

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
                // ��������� ������� ������� ����
                Vector3 position = new Vector3(x * spacing, 0, z * spacing);

                // ������� ���
                Instantiate(cubePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
