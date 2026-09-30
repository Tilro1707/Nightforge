using TMPro;
using UnityEngine;

public class RunTimer : MonoBehaviour
{
    [SerializeField] private SoulManager soulManager;

    [Header("UI")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI niceWords;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI wastedText;

    [Header("Run")]
    [SerializeField] private float runDuration = 300f;

    private float remainingTime;
    private bool runEnded;

    private void Start()
    {
        remainingTime = runDuration;
        endPanel.SetActive(false);
        UpdateUI();
    }

    private void Update()
    {
        if (runEnded)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            UpdateUI();
            EndRun();
            return;
        }

        UpdateUI();
    }

    private void EndRun()
    {
        runEnded = true;

        int score = soulManager.GetSouls();
        int wasted = soulManager.GetSpentSouls();

        scoreText.SetText("SCORE\n" + score);
        wastedText.SetText("SOULS WASTED\n" + wasted);
        if (wasted == 0)
        {
            niceWords.SetText("HACKING.");
        }
        else if (wasted <= 15)
        {
            niceWords.SetText("WTF HOW.");
        }
        else if (wasted <= 30)
        {
            niceWords.SetText("ABSURD.");
        }
        else if (wasted <= 60)
        {
            niceWords.SetText("GODLIKE.");
        }
        else if (wasted <= 90)
        {
            niceWords.SetText("VERY NICE.");
        }
        else if (wasted <= 120)
        {
            niceWords.SetText("PRETTY GOOD.");
        }
        else if (wasted <= 150)
        {
            niceWords.SetText("SOLID.");
        }
        else if (wasted <= 180)
        {
            niceWords.SetText("NOT BAD.");
        }
        else if (wasted <= 210)
        {
            niceWords.SetText("COULD BE WORSE.");
        }
        else if (wasted <= 240)
        {
            niceWords.SetText("YOU LIKE UPGRADES.");
        }
        else if (wasted <= 270)
        {
            niceWords.SetText("SOULS ARE TEMPORARY.");
        }
        else
        {
            niceWords.SetText("BE BETTER.");
        }

        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void UpdateUI()
    {
        int totalSeconds = Mathf.CeilToInt(remainingTime);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.SetText($"{minutes:00}:{seconds:00}");
    }
}