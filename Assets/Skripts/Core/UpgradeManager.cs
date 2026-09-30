using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private SoulManager soulManager;
    [Header("Fire")]
    [SerializeField] private TextMeshProUGUI fireLevelText;
    [SerializeField] private TextMeshProUGUI fireCostText;
    private int fireLevel = 0;
    [Header("Water")]
    [SerializeField] private TextMeshProUGUI waterLevelText;
    [SerializeField] private TextMeshProUGUI waterCostText;
    private int waterLevel = 0;
    [Header("Air")]
    [SerializeField] private TextMeshProUGUI airLevelText;
    [SerializeField] private TextMeshProUGUI airCostText;
    private int airLevel = 0;
    private int maxLevel = 3;
    private int fireCost = 0;
    private int waterCost = 0;
    private int airCost = 0;
    private float costMultiplier = 2f;
    private bool firstUpgradeFree = true;

    private void Start()
    {
        UpdateFireUI();
        UpdateWaterUI();
        UpdateAirUI();
    }

    public void UpgradeFire()
    {
        if(fireLevel == maxLevel)
        {
            return;
        }
        if (soulManager.SpendSouls(fireCost))
        {
            fireLevel++;
            if (firstUpgradeFree)
            {
                fireCost = 15;
                waterCost = 15;
                airCost = 15;
            }

            if (fireLevel < 3)
            {
                fireCost = (int)(fireCost * costMultiplier);
            }

            firstUpgradeFree = false;

            UpdateWaterUI();
            UpdateFireUI();
            UpdateAirUI();
        }
    }
    public void UpgradeWater()
    {
        if (waterLevel == maxLevel)
        {
            return;
        }
        if (soulManager.SpendSouls(waterCost))
        {
            waterLevel++;
            if (firstUpgradeFree)
            {
                fireCost = 15;
                waterCost = 15;
                airCost = 15;
            }

            if (waterLevel < 3)
            {
                waterCost = (int)(waterCost * costMultiplier);
            }

            firstUpgradeFree = false;

            UpdateWaterUI();
            UpdateFireUI();
            UpdateAirUI();
        }
    }
    public void UpgradeAir()
    {
        if (airLevel == maxLevel)
        {
            return;
        }
        if (soulManager.SpendSouls(airCost))
        {
            airLevel++;
            if (firstUpgradeFree)
            {
                fireCost = 15;
                waterCost = 15;
                airCost = 15;
            }

            if (airLevel < 3)
            {
                airCost = (int)(airCost * costMultiplier);
            }

            firstUpgradeFree = false;

            UpdateWaterUI();
            UpdateFireUI();
            UpdateAirUI();
        }
    }

    public void UpdateFireUI()
    {
        fireLevelText.SetText("Level " + fireLevel + "/3");
        if(firstUpgradeFree)
        {
            fireCostText.SetText("FREE");
        }
        else if (fireLevel < 3)
        {
            fireCostText.SetText(fireCost + " Souls");
        }
        else
        {
            fireCostText.SetText("MAX");
        }
    }
    public void UpdateWaterUI()
    {
        waterLevelText.SetText("Level " + waterLevel + "/3");
        if (firstUpgradeFree)
        {
            waterCostText.SetText("FREE");
        }
        else if (waterLevel < 3)
        {
            waterCostText.SetText(waterCost + " Souls");
        }
        else
        {
            waterCostText.SetText("MAX");
        }
    }
    public void UpdateAirUI()
    {
        airLevelText.SetText("Level " + airLevel + "/3");
        if (firstUpgradeFree)
        {
            airCostText.SetText("FREE");
        }
        else if (airLevel < 3)
        {
            airCostText.SetText(airCost + " Souls");
        }
        else
        {
            airCostText.SetText("MAX");
        }
    }

    public int getFireLevel()
    {
        return fireLevel;
    }
    public int getWaterLevel()
    {
        return waterLevel;
    }
    public int getAirLevel()
    {
        return airLevel;
    }

    public int GetLevel(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Fire:
                return fireLevel;
            case ElementType.Water:
                return waterLevel;
            case ElementType.Air:
                return airLevel;
        }
        return 0;
    }
}
