using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Building[,] grid;
    private Building flyingBuilding;
    private Building upgradePrefab; // Префаб для замены
    private Camera mainCamera;

    private float cellSize;
    private Vector3 planeOrigin;
    private bool isRotated = false;

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
        float planeSize = transform.localScale.x * 10f;
        cellSize = planeSize / GridSize.x;
        planeOrigin = transform.position - new Vector3(planeSize / 2, 0, planeSize / 2);
    }

    public void StartPlacingBuilding(Building buildingPrefab, Building replacementPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
        upgradePrefab = replacementPrefab; // Сохраняем ссылку на префаб для замены
        isRotated = false;
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                Vector3 localPosition = worldPosition - planeOrigin;
                int x = Mathf.FloorToInt(localPosition.x / cellSize);
                int y = Mathf.FloorToInt(localPosition.z / cellSize);

                Vector2Int buildingSize = isRotated
                    ? new Vector2Int(flyingBuilding.Size.y, flyingBuilding.Size.x)
                    : flyingBuilding.Size;

                bool available = true;

                if (x < 0 || x > GridSize.x - buildingSize.x) available = false;
                if (y < 0 || y > GridSize.y - buildingSize.y) available = false;

                if (available && IsPlaceTaken(x, y, buildingSize)) available = false;

                flyingBuilding.transform.position = new Vector3(x * cellSize, 0, y * cellSize) + planeOrigin;
                flyingBuilding.transform.rotation = Quaternion.Euler(0, isRotated ? 90 : 0, 0);

                flyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y, buildingSize);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    isRotated = !isRotated;
                }
            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }

        if (upgradePrefab != null)
        {
            Vector3 position = flyingBuilding.transform.position;
            Quaternion rotation = flyingBuilding.transform.rotation;
            Destroy(flyingBuilding.gameObject);
            flyingBuilding = Instantiate(upgradePrefab, position, rotation);
        }

        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }
}
