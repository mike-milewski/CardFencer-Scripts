using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Coffee.UIEffects;
using Steamworks;

public class RankAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private BattleResults battleResults;

    [SerializeField]
    private UIShiny uiShiny;

    [SerializeField]
    private UIShadow uiShadow;

    [SerializeField]
    private Animator rankTextAnimator, rankedUpTextAnimator, rankBonusAnimator;

    [SerializeField]
    private AudioSource rankUpAudioSource, rankRewardsAudioSource;

    [SerializeField]
    private ParticleSystem rankUpParticle;

    [SerializeField]
    private Image rankImage;

    [SerializeField]
    private TextMeshProUGUI rankText, rankedUpText;

    public void CheckRank()
    {
        rankUpParticle.Play();

        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            rankUpAudioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            rankUpAudioSource.Play();
        }
        else
        {
            rankUpAudioSource.Play();
        }

        StartCoroutine("RankUp");
    }

    private IEnumerator RankUp()
    {
        StartCoroutine(battleResults.WaitToShowStatsUpgrade());

        yield return new WaitForSeconds(0.1f);
        SetRankImageSprite();
        yield return new WaitForSeconds(0.5f);
        PlayRankText();
        yield return new WaitForSeconds(0.5f);
        rankBonusAnimator.Play("RankBonus");

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            rankRewardsAudioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            rankRewardsAudioSource.Play();
        }
        else
        {
            rankRewardsAudioSource.Play();
        }
    }

    private void SetRankImageSprite()
    {
        rankedUpTextAnimator.Play("RankedUp");

        playerMenuInfo.rankIndex++;

        battleResults.SetRankBonusInfo();

        rankText.text = playerMenuInfo.rankName[playerMenuInfo.rankIndex];

        rankImage.sprite = playerMenuInfo.rankSprites[playerMenuInfo.rankIndex];

        rankImage.color = new Color(1, 1, 1, 1);

        uiShiny.enabled = true;
        uiShadow.enabled = true;

        switch(playerMenuInfo.rankIndex)
        {
            case 0:
                if(SteamManager.Initialized)
                {
                    SteamUserStats.GetAchievement("ACH_BRONZE_BADGE", out bool achievementCompleted);

                    if (!achievementCompleted)
                    {
                        SteamUserStats.SetAchievement("ACH_BRONZE_BADGE");
                        SteamUserStats.StoreStats();
                    }
                }
                break;
            case 1:
                if (SteamManager.Initialized)
                {
                    SteamUserStats.GetAchievement("ACH_SILVER_HONOR", out bool achievementCompleted);

                    if (!achievementCompleted)
                    {
                        SteamUserStats.SetAchievement("ACH_SILVER_HONOR");
                        SteamUserStats.StoreStats();
                    }
                }
                break;
            case 2:
                if (SteamManager.Initialized)
                {
                    SteamUserStats.GetAchievement("ACH_GOLD_STATUS", out bool achievementCompleted);

                    if (!achievementCompleted)
                    {
                        SteamUserStats.SetAchievement("ACH_GOLD_STATUS");
                        SteamUserStats.StoreStats();
                    }
                }
                break;
            case 3:
                if (SteamManager.Initialized)
                {
                    SteamUserStats.GetAchievement("ACH_PLATINUM_PLAQUE", out bool achievementCompleted);

                    if (!achievementCompleted)
                    {
                        SteamUserStats.SetAchievement("ACH_PLATINUM_PLAQUE");
                        SteamUserStats.StoreStats();
                    }
                }
                break;
            case 4:
                if (SteamManager.Initialized)
                {
                    SteamUserStats.GetAchievement("ACH_BRILLIANT_FENCER", out bool achievementCompleted);

                    if (!achievementCompleted)
                    {
                        SteamUserStats.SetAchievement("ACH_BRILLIANT_FENCER");
                        SteamUserStats.StoreStats();
                    }
                }
                break;
        }
    }

    private void PlayRankText()
    {
        rankTextAnimator.Play("Rank");
        RankTextColor();
    }

    private void RankTextColor()
    {
        rankText.enableVertexGradient = true;

        VertexGradient textGradient = rankText.colorGradient;

        switch (playerMenuInfo.rankIndex)
        {
            case (0):
                textGradient.bottomLeft = MenuController.instance.BronzeRank.colorGradient.bottomLeft;
                textGradient.bottomRight = MenuController.instance.BronzeRank.colorGradient.bottomRight;
                textGradient.topLeft = MenuController.instance.BronzeRank.colorGradient.topLeft;
                textGradient.topRight = MenuController.instance.BronzeRank.colorGradient.topRight;
                break;
            case (1):
                textGradient.bottomLeft = MenuController.instance.SilverRank.colorGradient.bottomLeft;
                textGradient.bottomRight = MenuController.instance.SilverRank.colorGradient.bottomRight;
                textGradient.topLeft = MenuController.instance.SilverRank.colorGradient.topLeft;
                textGradient.topRight = MenuController.instance.SilverRank.colorGradient.topRight;
                break;
            case (2):
                textGradient.bottomLeft = MenuController.instance.GoldRank.colorGradient.bottomLeft;
                textGradient.bottomRight = MenuController.instance.GoldRank.colorGradient.bottomRight;
                textGradient.topLeft = MenuController.instance.GoldRank.colorGradient.topLeft;
                textGradient.topRight = MenuController.instance.GoldRank.colorGradient.topRight;
                break;
            case (3):
                textGradient.bottomLeft = MenuController.instance.DiamondRank.colorGradient.bottomLeft;
                textGradient.bottomRight = MenuController.instance.DiamondRank.colorGradient.bottomRight;
                textGradient.topLeft = MenuController.instance.DiamondRank.colorGradient.topLeft;
                textGradient.topRight = MenuController.instance.DiamondRank.colorGradient.topRight;
                break;
            case (4):
                textGradient.bottomLeft = MenuController.instance.BrilliantRank.colorGradient.bottomLeft;
                textGradient.bottomRight = MenuController.instance.BrilliantRank.colorGradient.bottomRight;
                textGradient.topLeft = MenuController.instance.BrilliantRank.colorGradient.topLeft;
                textGradient.topRight = MenuController.instance.BrilliantRank.colorGradient.topRight;
                break;
        }

        rankText.colorGradient = textGradient;
    }
}