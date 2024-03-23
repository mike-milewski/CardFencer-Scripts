using UnityEngine;
using TMPro;

public class PlayerLevelTextAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private MainCharacterStats mainCharacterStats;

    [SerializeField]
    private ParticleSystem levelUpParticle;

    [SerializeField]
    private TextMeshProUGUI playerLevelText;

    public void IncrementLevel()
    {
        playerLevelText.text = mainCharacterStats.level.ToString();

        levelUpParticle.gameObject.SetActive(true);
        levelUpParticle.Play();
    }
}