using UnityEngine;
using TMPro;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private TextMeshProUGUI level, health, cardPoints, stickerPoints, money, moonStone;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private bool restoreStats;

    private void Start()
    {
        if(restoreStats)
        {
            RestorePlayerStats();
        }
        
        UpdatePlayerInformation();
    }

    public void UpdatePlayerInformation()
    {
        level.text = "L<size=15>v:</size> " + mainCharacterStats.level.ToString();
        health.text = mainCharacterStats.currentPlayerHealth + "/" + "<size=17>" + mainCharacterStats.maximumHealth + "</size>";
        cardPoints.text = mainCharacterStats.currentPlayerCardPoints + "/" + "<size=17>" + mainCharacterStats.maximumCardPoints + "</size>";
        stickerPoints.text = mainCharacterStats.currentPlayerStickerPoints + "/" + "<size=17>" + mainCharacterStats.maximumStickerPoints + "</size>";
        money.text = mainCharacterStats.money.ToString();
        moonStone.text = "<size=15>x</size> " + mainCharacterStats.moonStone;
    }

    private void RestorePlayerStats()
    {
        mainCharacterStats.currentPlayerHealth = mainCharacterStats.maximumHealth;
        mainCharacterStats.currentPlayerCardPoints = mainCharacterStats.maximumCardPoints;
    }

    public void ToggleObject(bool toggle, float alpha)
    {
        canvasGroup.interactable = toggle;

        canvasGroup.blocksRaycasts = toggle;

        canvasGroup.alpha = alpha;
    }
}