using TMPro;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UI;
    [SerializeField] private TextMeshProUGUI GameOverSoulsUI;
    [SerializeField] private int curSouls = 0;
    private int spentSouls;
    private void Start()
    {
        UpdateUI();
    }

    public void AddSouls(int amount)
    {
        curSouls += amount;
        UpdateUI();
    }

    public bool SpendSouls(int amount)
    {
        if(curSouls >= amount)
        {
            curSouls -= amount;
            spentSouls += amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        UI.SetText("Souls:" + curSouls);
        GameOverSoulsUI.SetText("Souls:" + curSouls);
    }

    public int GetSouls()
    {
        return curSouls;
    }
    public int GetSpentSouls()
    {
        return spentSouls;
    }
}
