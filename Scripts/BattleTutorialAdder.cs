using System.Collections.Generic;
using UnityEngine;

public class BattleTutorialAdder : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    [Header("Battle Info One")]
    private List<string> informationOne, playStationInfoOne, xboxInfoOne, steamDeckInfoOne;

    [SerializeField]
    [TextArea]
    [Header("Battle Info Two")]
    private List<string> informationTwo, playStationInfoTwo, xboxInfoTwo, steamDeckInfoTwo;

    [SerializeField]
    [TextArea]
    [Header("Battle Info Three")]
    private List<string> informationThree, playStationInfoThree, xboxInfoThree, steamDeckInfoThree;

    public void AddBattleTutorialInfo(int battleIndex)
    {
        switch(battleIndex)
        {
            case 0:
                for (int i = 0; i < informationOne.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.BattleInformation.Add(informationOne[i]);
                }

                for (int i = 0; i < playStationInfoOne.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.PlayStationBattleInfo.Add(playStationInfoOne[i]);
                }

                for (int i = 0; i < xboxInfoOne.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.XboxBattleInfo.Add(xboxInfoOne[i]);
                }

                for (int i = 0; i < steamDeckInfoOne.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.SteamDeckBattleInfo.Add(steamDeckInfoOne[i]);
                }
                break;
            case 1:
                for (int i = 0; i < informationTwo.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.BattleInformation.Add(informationTwo[i]);
                }

                for (int i = 0; i < playStationInfoTwo.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.PlayStationBattleInfo.Add(playStationInfoTwo[i]);
                }

                for (int i = 0; i < xboxInfoTwo.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.XboxBattleInfo.Add(xboxInfoTwo[i]);
                }

                for (int i = 0; i < steamDeckInfoTwo.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.SteamDeckBattleInfo.Add(steamDeckInfoTwo[i]);
                }
                break;
            case 2:
                for (int i = 0; i < informationThree.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.BattleInformation.Add(informationThree[i]);
                }

                for (int i = 0; i < playStationInfoThree.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.PlayStationBattleInfo.Add(playStationInfoThree[i]);
                }

                for (int i = 0; i < xboxInfoThree.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.XboxBattleInfo.Add(xboxInfoThree[i]);
                }

                for (int i = 0; i < steamDeckInfoThree.Count; i++)
                {
                    MenuController.instance._SettingsMenu._TutorialChecker.SteamDeckBattleInfo.Add(steamDeckInfoThree[i]);
                }
                break;
        }
    }
}