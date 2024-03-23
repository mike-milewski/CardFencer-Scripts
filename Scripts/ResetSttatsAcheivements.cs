using UnityEngine;
using Steamworks;

public class ResetSttatsAcheivements : MonoBehaviour
{
    private void Start()
    {
        if(SteamManager.Initialized)
        {
            SteamUserStats.ResetAllStats(true);
        }
    }
}