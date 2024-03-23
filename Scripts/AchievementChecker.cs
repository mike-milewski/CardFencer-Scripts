using UnityEngine;
using Steamworks;

public class AchievementChecker : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    private void Awake()
    {
        CheckRankAchievement();
        CheckCardAchievement();
        CheckBestiaryAchievement();
    }

    private void CheckRankAchievement()
    {
        switch (playerMenuInfo.rankIndex)
        {
            case 0:
                if (SteamManager.Initialized)
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

    private void CheckCardAchievement()
    {
        MenuController.instance._CardCollection.CheckCardMasterAchievement();
    }

    private void CheckBestiaryAchievement()
    {
        MenuController.instance._BestiaryMenu.CheckBestiaryAchievement();
    }
}