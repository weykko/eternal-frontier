using UnityEngine;

public class BuildPanel : MonoBehaviour
{
    public BuildingsGrid BuildingsGrid;
    public Building TurretPrefab;
    public Building UpgradedTurretPrefab;
    public Building TowerPrefab;

    public void ClickOnTurret()
    {
        // Передаем базовый префаб турели и его улучшенную версию
        BuildingsGrid.StartPlacingBuilding(TurretPrefab, UpgradedTurretPrefab);
    }

    public void ClickOnTower()
    {
        // У башни замены не будет, передаем только один префаб
        BuildingsGrid.StartPlacingBuilding(TowerPrefab, null);
    }
}
