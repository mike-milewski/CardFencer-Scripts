using UnityEngine;
using Steamworks;

public class ExpTest : MonoBehaviour
{
    public MainCharacterStats mainCharacterStats;

    public float nextToLevel = 10;
    public int level = 1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;

        if (level < 10)
        {
            nextToLevel = Mathf.Round((nextToLevel + level) * 1.40f);
        }
        else
        {
            float newExp = Mathf.Round(nextToLevel / 50);
            nextToLevel += newExp + level;
        }

        Debug.Log("Level: " + level + " Next To: " + nextToLevel);
    }

    private void ActuallyLevelUp()
    {
        if (SteamManager.Initialized)
        {
            int level = mainCharacterStats.level;

            SteamUserStats.GetStat("ACH_PLAYER_LEVEL", out level);
            mainCharacterStats.level++;
            level++;
            SteamUserStats.SetStat("ACH_PLAYER_LEVEL", level);
            SteamUserStats.StoreStats();

            if (level >= mainCharacterStats.maximumLevel)
            {
                SteamUserStats.GetAchievement("ACH_MAX_POTENTIAL", out bool achievementCompleted);

                if (!achievementCompleted)
                {
                    SteamUserStats.SetAchievement("ACH_MAX_POTENTIAL");
                    SteamUserStats.StoreStats();
                }
            }

            Debug.Log(level);
        }
    }
}