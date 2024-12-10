using UnityEngine;

public class MovePivot : MonoBehaviour
{
    void Start()
    {
        // �������� ������ ���, ����� ��� pivot �������� � ������
        Vector3 currentBounds = GetObjectBounds();
        Vector3 pivotOffset = currentBounds / 2f;

        // ���������� ������ � ������ ������
        transform.position -= pivotOffset;
    }

    Vector3 GetObjectBounds()
    {
        // �������� ������� �������, ���� ��� MeshRenderer
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            return renderer.bounds.size;
        }
        return Vector3.zero;
    }
}
