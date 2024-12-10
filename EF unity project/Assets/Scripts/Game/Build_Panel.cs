using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    public BuildingsGrid BuildingsGrid;
    public Building TurretPrefab;
    public Building UpgradedTurretPrefab;
    public Building TowerPrefab;

    public void ClickOnTurret()
    {
        // �������� ������� ������ ������ � ��� ���������� ������
        BuildingsGrid.StartPlacingBuilding(TurretPrefab, UpgradedTurretPrefab);
    }

    public void ClickOnTower()
    {
        // � ����� ������ �� �����, �������� ������ ���� ������
        BuildingsGrid.StartPlacingBuilding(TowerPrefab, null);
    }
}
