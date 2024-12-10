using UnityEngine;

public class MovePivot : MonoBehaviour
{
    void Start()
    {
        // Сдвигаем объект так, чтобы его pivot оказался в центре
        Vector3 currentBounds = GetObjectBounds();
        Vector3 pivotOffset = currentBounds / 2f;

        // Перемещаем объект с учетом сдвига
        transform.position -= pivotOffset;
    }

    Vector3 GetObjectBounds()
    {
        // Получаем границы объекта, если это MeshRenderer
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            return renderer.bounds.size;
        }
        return Vector3.zero;
    }
}
