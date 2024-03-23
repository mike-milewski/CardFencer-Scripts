using Steamworks;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonsToggle : MonoBehaviour
{
    [SerializeField]
    private Selectable closeMenuSelectable, cameraSensitivitySelectable;

    [SerializeField]
    private GameObject fullScreenSetting;

    private void Start()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            fullScreenSetting.SetActive(false);

            if (closeMenuSelectable != null)
            {
                Navigation closeMenuNav = closeMenuSelectable.GetComponent<Selectable>().navigation;

                closeMenuNav.selectOnUp = cameraSensitivitySelectable;

                closeMenuSelectable.navigation = closeMenuNav;

                Navigation cameraSenseNav = cameraSensitivitySelectable.GetComponent<Selectable>().navigation;

                cameraSenseNav.selectOnDown = closeMenuSelectable;

                cameraSensitivitySelectable.navigation = cameraSenseNav;
            }
            else
            {
                Navigation cameraSenseNav = cameraSensitivitySelectable.GetComponent<Selectable>().navigation;

                cameraSenseNav.selectOnDown = null;

                cameraSensitivitySelectable.navigation = cameraSenseNav;
            }
        }
    }
}