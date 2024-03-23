using Steamworks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private TutorialChecker tutorialChecker;

    [SerializeField]
    private Transform tutorialButtonParent;

    [SerializeField]
    private Slider backgroundMusicSlider, soundEffectsSlider, cameraSensitivitySlider;

    [SerializeField]
    private GameObject toggleCheckmark, tutorialPanel;

    [SerializeField]
    private Selectable settingsButton, cardsButton, tutorialButton, cardsSelectDown;

    [SerializeField]
    private List<Selectable> tutorialButtons;

    public List<Selectable> TutorialButtons => tutorialButtons;

    public TutorialChecker _TutorialChecker => tutorialChecker;

    public GameObject ToggleCheckMark
    {
        get => toggleCheckmark;
    }

    public Slider BackgroundMusicSlider
    {
        get
        {
            return backgroundMusicSlider;
        }
        set
        {
            backgroundMusicSlider = value;
        }
    }

    public Slider SoundEffectsSlider
    {
        get
        {
            return soundEffectsSlider;
        }
        set
        {
            soundEffectsSlider = value;
        }
    }

    public Slider CameraSensitivitySlider
    {
        get
        {
            return cameraSensitivitySlider;
        }
        set
        {
            cameraSensitivitySlider = value;
        }
    }

    private void Start()
    {
        CurrentSettings();
    }

    public void SetDefaultButtonList()
    {
        if(tutorialButtonParent.childCount > 0)
        {
            for(int i = 0; i < tutorialButtonParent.childCount; i++)
            {
                if(tutorialButtonParent.GetChild(i).gameObject.activeSelf)
                {
                    tutorialButtons.Add(tutorialButtonParent.GetChild(i).GetComponent<Selectable>());
                }
            }
            SetSettingsButtonNavigations();
        }
    }

    public void SetDefaultTutorialNavs()
    {
        if(tutorialPanel.activeSelf)
        {
            Navigation settingsNav = settingsButton.navigation;
            Navigation cardsNav = cardsButton.navigation;
            Navigation tutorialNav = tutorialButton.navigation;

            settingsNav.selectOnDown = tutorialChecker.NextInfoButton;
            cardsNav.selectOnDown = tutorialChecker.NextInfoButton;
            tutorialNav.selectOnDown = tutorialChecker.NextInfoButton;

            settingsButton.navigation = settingsNav;
            cardsButton.navigation = cardsNav;
            tutorialButton.navigation = tutorialNav;
        }
    }

    public void SetSettingsButtonNavigations()
    {
        if(tutorialButtons.Count > 0)
        {
            for(int i = 0; i < tutorialButtons.Count; i++)
            {
                Navigation nav = tutorialButtons[i].GetComponent<Selectable>().navigation;

                if(i == 0)
                {
                    if(tutorialButtons.Count > 1)
                    {
                        nav.selectOnRight = tutorialButtons[i + 1].GetComponent<Selectable>();
                        nav.selectOnLeft = tutorialButtons[tutorialButtons.Count - 1].GetComponent<Selectable>();
                    }
                }
                else if(i >= tutorialButtons.Count - 1)
                {
                    nav.selectOnRight = tutorialButtons[0].GetComponent<Selectable>();
                    nav.selectOnLeft = tutorialButtons[i - 1].GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnRight = tutorialButtons[i + 1].GetComponent<Selectable>();
                    nav.selectOnLeft = tutorialButtons[i - 1].GetComponent<Selectable>();
                }

                tutorialButtons[i].GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    public void DisableTutorialButtons()
    {
        foreach (Button button in tutorialButtonParent.GetComponentsInChildren<Button>())
        {
            button.gameObject.SetActive(false);
        }

        tutorialChecker.BattleInformation.Clear();
        tutorialChecker.PlayStationBattleInfo.Clear();
        tutorialChecker.XboxBattleInfo.Clear();
    }

    public void RePositionButtonParentChildren()
    {
        if(tutorialButtons.Count > 1)
        {
            for(int i = 0; i < tutorialButtons.Count; i++)
            {
                for(int j = 0; j < tutorialButtonParent.childCount; j++)
                {
                    if(tutorialButtonParent.GetChild(j).name == tutorialButtons[i].name)
                    {
                        tutorialButtonParent.GetChild(j).transform.SetSiblingIndex(i);
                    }
                }
            }
        }
    }

    public void AdjustSettingsButtonNavigations()
    {
        Navigation cardNav = cardsButton.navigation;

        cardNav.selectOnDown = backgroundMusicSlider;

        cardsButton.navigation = cardNav;

        Navigation tutorialNav = tutorialButton.navigation;

        tutorialNav.selectOnDown = backgroundMusicSlider;

        tutorialButton.navigation = tutorialNav;

        Navigation settingsNav = settingsButton.navigation;

        settingsNav.selectOnDown = backgroundMusicSlider;

        settingsButton.navigation = settingsNav;
    }

    public void AdjustCardButtonNavigations()
    {
        Navigation settingsNav = settingsButton.navigation;

        settingsNav.selectOnDown = cardsSelectDown;

        settingsButton.navigation = settingsNav;

        Navigation tutorialNav = tutorialButton.navigation;

        tutorialNav.selectOnDown = cardsSelectDown;

        tutorialButton.navigation = tutorialNav;

        Navigation cardNav = cardsButton.navigation;

        cardNav.selectOnDown = cardsSelectDown;

        cardsButton.navigation = cardNav;
    }

    public void AdjustTutorialButtonNavigations()
    {
        Navigation cardNav = cardsButton.navigation;

        cardNav.selectOnDown = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;

        cardsButton.navigation = cardNav;

        Navigation settingsNav = settingsButton.navigation;

        settingsNav.selectOnDown = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;

        settingsButton.navigation = settingsNav;

        Navigation tutorialNav = tutorialButton.navigation;

        tutorialNav.selectOnDown = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;

        tutorialButton.navigation = tutorialNav;

        Navigation nextInfoButtonNav = tutorialChecker.NextInfoButton.navigation;

        nextInfoButtonNav.selectOnDown = tutorialButtons[0];

        tutorialChecker.NextInfoButton.navigation = nextInfoButtonNav;

        Navigation prevInfoButtonNav = tutorialChecker.PreviousInfoButtton.navigation;

        prevInfoButtonNav.selectOnDown = tutorialButtons[0];

        tutorialChecker.PreviousInfoButtton.navigation = prevInfoButtonNav;

        AdjustTutorialButtonListNavigations();
    }

    public void AdjustTutorialButtonListNavigations()
    {
        foreach(Selectable button in tutorialButtons)
        {
            Navigation nav = button.navigation;

            nav.selectOnUp = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;

            button.navigation = nav;
        }

        Navigation nextButtonNav = tutorialChecker.NextInfoButton.navigation;

        nextButtonNav.selectOnLeft = null;
        nextButtonNav.selectOnRight = null;

        tutorialChecker.NextInfoButton.navigation = nextButtonNav;

        Navigation prevButtonNav = tutorialChecker.PreviousInfoButtton.navigation;

        prevButtonNav.selectOnLeft = tutorialChecker.NextInfoButton;
        prevButtonNav.selectOnRight = tutorialChecker.NextInfoButton;

        tutorialChecker.PreviousInfoButtton.navigation = prevButtonNav;
    }

    public void AdjustButtonNavigations()
    {
        foreach (Selectable button in tutorialButtons)
        {
            Navigation nav = button.navigation;

            nav.selectOnUp = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;

            button.navigation = nav;
        }

        Navigation cardsNav = cardsButton.navigation;
        Navigation settingsNav = settingsButton.navigation;
        Navigation tutorialNav = tutorialButton.navigation;
        Navigation previousTutorialNav = tutorialChecker.PreviousInfoButtton.navigation;

        cardsNav.selectOnDown = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;
        settingsNav.selectOnDown = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;
        tutorialNav.selectOnDown = tutorialChecker.NextInfoButton.interactable ? tutorialChecker.NextInfoButton : tutorialChecker.PreviousInfoButtton;
        previousTutorialNav.selectOnRight = null;
        previousTutorialNav.selectOnLeft = null;

        cardsButton.navigation = cardsNav;
        settingsButton.navigation = settingsNav;
        tutorialButton.navigation = tutorialNav;
        tutorialChecker.PreviousInfoButtton.navigation = previousTutorialNav;
    }

    private void CurrentSettings()
    {
        if(PlayerPrefs.HasKey("BackgroundAudio"))
        {
            backgroundMusicSlider.value = PlayerPrefs.GetFloat("BackgroundAudio");
            AudioManager.instance.BackgroundMusic.volume = backgroundMusicSlider.value;
        }
        else
        {
            AudioManager.instance.BackgroundMusic.volume = 1;
        }

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffects");
            AudioManager.instance.SoundEffects.volume = soundEffectsSlider.value;
        }
        else
        {
            AudioManager.instance.SoundEffects.volume = 1;
        }

        if (PlayerPrefs.HasKey("CameraSense"))
        {
            cameraSensitivitySlider.value = PlayerPrefs.GetFloat("CameraSense");
        }

        if(!SteamUtils.IsSteamRunningOnSteamDeck())
        {
            var windowedMode = PlayerPrefs.HasKey("Windowed") ? PlayerPrefs.GetInt("Windowed") : 1;

            if (windowedMode == 1)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                toggleCheckmark.SetActive(true);

                int width = Screen.currentResolution.width;
                int height = Screen.currentResolution.height;

                Screen.SetResolution(width, height, true);
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.fullScreen = false;
                toggleCheckmark.SetActive(false);

                Screen.SetResolution(1900, 1080, false);
            }
        }
    }

    public void SetBackgroundAudioVolume()
    {
        PlayerPrefs.SetFloat("BackgroundAudio", backgroundMusicSlider.value);

        SaveSettings();
    }

    public void SetSoundEffectsVolume()
    {
        PlayerPrefs.SetFloat("SoundEffects", soundEffectsSlider.value);

        SaveSettings();
    }

    public void SetCameraSensitivity()
    {
        PlayerPrefs.SetFloat("CameraSense", cameraSensitivitySlider.value);

        SaveSettings();
    }

    public void ToggleWindowed()
    {
        if (PlayerPrefs.HasKey("Windowed"))
        {
            if (toggleCheckmark.activeSelf)
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.fullScreen = false;
                PlayerPrefs.SetInt("Windowed", 0);
                toggleCheckmark.SetActive(false);

                Screen.SetResolution(1900, 1080, false);
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                PlayerPrefs.SetInt("Windowed", 1);
                toggleCheckmark.SetActive(true);

                int width = Screen.currentResolution.width;
                int height = Screen.currentResolution.height;

                Screen.SetResolution(width, height, true);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Windowed", 0);
        }

        SaveSettings();
    }

    public void CheckControllerOnToggle(Toggle toggle)
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if(toggle.transition == Selectable.Transition.ColorTint)
            {
                ColorBlock colorBlock = toggle.colors;

                colorBlock.selectedColor = new Color(0.5566038f, 0.5566038f, 0.5566038f, 1);

                toggle.colors = colorBlock;
            }
        }
        else
        {
            if (toggle.transition == Selectable.Transition.ColorTint)
            {
                ColorBlock colorBlock = toggle.colors;

                colorBlock.selectedColor = new Color(1, 1, 1, 1);

                toggle.colors = colorBlock;
            }
        }
    }

    public void SaveSettings()
    {
        MenuController.instance.SetSettingsToggleValues(PlayerPrefs.HasKey("BackgroundAudio") ? PlayerPrefs.GetFloat("BackgroundAudio") : 1, PlayerPrefs.HasKey("SoundEffects") ? 
                                                        PlayerPrefs.GetFloat("SoundEffects") : 1, PlayerPrefs.HasKey("CameraSense") ? PlayerPrefs.GetFloat("CameraSense") : 1, 
                                                        PlayerPrefs.HasKey("Windowed") ? PlayerPrefs.GetInt("Windowed") : 1);
    }
}