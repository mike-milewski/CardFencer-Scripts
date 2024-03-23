using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;
using UnityEngine.EventSystems;

public class SteamOverlayPause : MonoBehaviour
{
    public static SteamOverlayPause instance = null;

    [SerializeField]
    private GameObject uiBlocker;

    private EventSystem eventSystem = null, battleEventSystem = null;

    private BattleSystem battleSystem = null;

    protected Callback<GameOverlayActivated_t> overlayIsOn;

    private bool isPaused;

    public bool IsPaused => isPaused;

    public EventSystem BattleEventSystem
    {
        get
        {
            return battleEventSystem;
        }
        set
        {
            battleEventSystem = value;
        }
    }

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    private void Start()
    {
        if(SteamManager.Initialized)
        {
            overlayIsOn = Callback<GameOverlayActivated_t>.Create(PauseGameIfSteamOverlayOn);
        }
    }

    private void Update()
    {
        CheckCallBacks();
    }

    private void PauseGameIfSteamOverlayOn(GameOverlayActivated_t callback)
    {
        Scene scene = SceneManager.GetActiveScene();

        if (callback.m_bActive == 0)
        {
            isPaused = false;

            uiBlocker.SetActive(false);

            if (!scene.name.Contains("Field") && !scene.name.Contains("Menu") && !scene.name.Contains("Battle"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (MenuController.instance.OpenedMenu)
                {
                    if (eventSystem != null)
                    {
                        eventSystem.sendNavigationEvents = true;
                    }

                    Time.timeScale = 0;
                }
                else
                {
                    if (GameManager.instance.IsEnteringBattle)
                    {
                        Time.timeScale = 0;
                    }
                    else
                    {
                        if (eventSystem != null)
                        {
                            eventSystem.sendNavigationEvents = true;
                        }

                        Time.timeScale = 1;
                    }
                }
            }
            else if(scene.name.Contains("Field"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (eventSystem != null)
                {
                    if(!NodeManager.instance.CompletedStage && !NodeManager.instance.UnlockedSecretStage && !NodeManager.instance.WorldBossDefeated)
                       eventSystem.sendNavigationEvents = true;
                }

                Time.timeScale = 1;
            }
            else if(scene.name.Contains("Menu"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (eventSystem != null)
                {
                    eventSystem.sendNavigationEvents = true;
                }

                Time.timeScale = 1;
            }
            else if(scene.name.Contains("Battle"))
            {
                if (battleSystem == null)
                {
                    BattleSystem _battleSystem = FindObjectOfType<BattleSystem>();

                    battleSystem = _battleSystem;
                }

                if (battleEventSystem == null)
                {
                    if(battleSystem != null)
                    {
                        EventSystem _eventSystem = battleSystem._InputManager._EventSystem;

                        battleEventSystem = _eventSystem;
                    }
                }

                if (battleSystem != null)
                {
                    if(!battleSystem.NavagationDisabled)
                    {
                        if(battleEventSystem != null)
                        {
                            battleEventSystem.sendNavigationEvents = true;
                        }
                    }
                }

                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            isPaused = true;

            uiBlocker.SetActive(true);

            if (!scene.name.Contains("Field") && !scene.name.Contains("Menu") && !scene.name.Contains("Battle"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (eventSystem != null)
                {
                    eventSystem.sendNavigationEvents = false;
                }
            }
            else if(scene.name.Contains("Field"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (eventSystem != null)
                {
                    eventSystem.sendNavigationEvents = false;
                }
            }
            else if(scene.name.Contains("Menu"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (eventSystem != null)
                {
                    eventSystem.sendNavigationEvents = false;
                }
            }

            if(scene.name.Contains("Boss"))
            {
                if (eventSystem == null)
                {
                    EventSystem _eventSystem = InputManager.instance._EventSystem;

                    eventSystem = _eventSystem;
                }

                if (eventSystem != null)
                {
                    eventSystem.sendNavigationEvents = false;
                }
            }

            if(scene.name.Contains("Battle"))
            {
                if (battleSystem == null)
                {
                    BattleSystem _battleSystem = FindObjectOfType<BattleSystem>();

                    battleSystem = _battleSystem;
                }

                if (battleEventSystem == null)
                {
                    if(battleSystem != null)
                    {
                        EventSystem _eventSystem = battleSystem._InputManager._EventSystem;

                        battleEventSystem = _eventSystem;
                    }
                }

                if (battleEventSystem != null)
                {
                    battleEventSystem.sendNavigationEvents = false;
                }
            }

            Time.timeScale = 0;
        }
    }

    private void CheckCallBacks()
    {
        SteamAPI.RunCallbacks();
    }
}