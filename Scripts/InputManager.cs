using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Steamworks;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;

    [SerializeField]
    private Selectable[] selectablesToRemove;

    [SerializeField]
    private Button[] buttonsWithIllegalSelectables;

    [SerializeField]
    private GameObject firstObjectSelected;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private GameObject currentSelectedObject;

    [SerializeField]
    private bool isInBattle;

    private bool controllerPluggedIn, hasFocus, hasSetCurrentSelectedObject, hasSetEventObjectToNull, forceCursorOn;

    private string controllerName;

    private Scene scene;

    public EventSystem _EventSystem => eventSystem;

    public bool ControllerPluggedIn
    {
        get
        {
            return controllerPluggedIn;
        }
        set
        {
            controllerPluggedIn = value;
        }
    }

    public string ControllerName => controllerName;

    public GameObject FirstSelectedObject
    {
        get => firstObjectSelected;
        set => firstObjectSelected = value;
    }

    public bool ForceCursorOn
    {
        get => forceCursorOn;
        set => forceCursorOn = value;
    }

    public GameObject CurrentSelectedObject
    {
        get => currentSelectedObject;
        set => currentSelectedObject = value;
    }

    public bool HasFocus => hasFocus;

    private void Start()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        #endregion

        scene = SceneManager.GetActiveScene();

        if(CheckForController())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if(scene.name.Contains("Field"))
            {
                if(SteamUtils.IsSteamRunningOnSteamDeck())
                {
                    if (NodeManager.instance.HasWorldMapTutorial)
                    {
                        WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

                        if (!NewGameChecker.instance.IsANewGame)
                        {
                            StartCoroutine(SearchForUnlockedStage(worldMapMovement));
                        }
                        else
                        {
                            firstObjectSelected = worldMapMovement.LevelNodes[0].gameObject;
                        }

                        if (!NodeManager.instance.WorldBossDefeated)
                            eventSystem.SetSelectedGameObject(firstObjectSelected);
                    }
                    else
                    {
                        eventSystem.SetSelectedGameObject(null);
                    }
                }
                else
                {
                    if (PlayerPrefs.HasKey("WorldMapTutorial"))
                    {
                        WorldMapMovement worldMapMovement = FindObjectOfType<WorldMapMovement>();

                        if (!NewGameChecker.instance.IsANewGame)
                        {
                            StartCoroutine(SearchForUnlockedStage(worldMapMovement));
                        }
                        else
                        {
                            firstObjectSelected = worldMapMovement.LevelNodes[0].gameObject;
                        }

                        if (!NodeManager.instance.WorldBossDefeated)
                            eventSystem.SetSelectedGameObject(firstObjectSelected);
                    }
                    else
                    {
                        eventSystem.SetSelectedGameObject(null);
                    }
                }
            }
            else
            {
                if(firstObjectSelected != null)
                   eventSystem.SetSelectedGameObject(firstObjectSelected);
            }
        }

        hasFocus = true;

        if(selectablesToRemove.Length > 0)
           CheckSelectables();
    }

    private IEnumerator SearchForUnlockedStage(WorldMapMovement worldMapMovement)
    {
        yield return new WaitForSeconds(0.15f);

        switch (NodeManager.instance.CurrentNodeIndex)
        {
            case -1:
                firstObjectSelected = worldMapMovement.LevelNodes[4].gameObject;
                break;
            case -2:
                firstObjectSelected = worldMapMovement.LevelNodes[5].gameObject;
                break;
            case -3:
                firstObjectSelected = worldMapMovement.LevelNodes[6].gameObject;
                break;
            default:
                firstObjectSelected = worldMapMovement.LevelNodes[NodeManager.instance.CurrentNodeIndex].gameObject;
                break;
        }

        firstObjectSelected.GetComponent<StageInformation>().ShowInfoOnControllerSelect();

        SetSelectedObject(firstObjectSelected);
    }

    private void CheckSelectables()
    {
        for(int i = 0; i < selectablesToRemove.Length; i++)
        {
            for(int j = 0; j < buttonsWithIllegalSelectables.Length; j++)
            {
                if(!buttonsWithIllegalSelectables[j].interactable)
                {
                    Navigation nav = selectablesToRemove[i].navigation;

                    if(nav.selectOnUp != null)
                    {
                        if (nav.selectOnUp == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnUp = null;
                        }
                    }
                    if(nav.selectOnDown != null)
                    {
                        if (nav.selectOnDown.gameObject == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnDown = null;
                        }
                    }
                    if(nav.selectOnLeft != null)
                    {
                        if (nav.selectOnLeft.gameObject == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnLeft = null;
                        }
                    }
                    if(nav.selectOnRight != null)
                    {
                        if (nav.selectOnRight.gameObject == buttonsWithIllegalSelectables[j].gameObject)
                        {
                            nav.selectOnRight = null;
                        }
                    }

                    selectablesToRemove[i].navigation = nav;
                }
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        hasFocus = focus;
    }

    public void ResetSelectedObject()
    {
        SetSelectedObject(null);
    }

    private void Update()
    {
        if (CheckForController())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (!MenuController.instance.OpenedMenu)
            {
                if (!controllerPluggedIn)
                {
                    if(firstObjectSelected != null)
                    {
                        eventSystem.SetSelectedGameObject(currentSelectedObject != null ? currentSelectedObject : firstObjectSelected);
                        controllerPluggedIn = true;
                    }
                }

                if (!hasFocus)
                {
                    if(firstObjectSelected != null)
                    {
                        eventSystem.SetSelectedGameObject(currentSelectedObject != null ? currentSelectedObject : firstObjectSelected);
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    if(firstObjectSelected != null)
                    {
                        eventSystem.SetSelectedGameObject(currentSelectedObject != null ? currentSelectedObject : firstObjectSelected);
                    }
                }
            }
            else
            {
                if(!controllerPluggedIn)
                {
                    controllerPluggedIn = true;
                }

                if (!hasSetCurrentSelectedObject)
                {
                    SetSelectedGameObject();

                    hasSetEventObjectToNull = false;

                    hasSetCurrentSelectedObject = true;
                }

                if (Input.GetMouseButton(0))
                {
                    eventSystem.SetSelectedGameObject(currentSelectedObject != null ? currentSelectedObject : MenuController.instance.FirstSelectedObject);
                }
            }

            if(!hasSetCurrentSelectedObject)
            {
                SetSelectedGameObject();

                hasSetEventObjectToNull = false;

                hasSetCurrentSelectedObject = true;
            }

            if (!scene.name.Contains("Field") && !scene.name.Contains("Main"))
            {
                if(!string.IsNullOrEmpty(controllerName))
                {
                    if (controllerName == "xbox")
                    {
                        eventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "XboxHorizontal";
                        eventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "XboxVertical";
                    }
                    else
                    {
                        eventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "Ps4Horizontal";
                        eventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "Ps4Vertical";
                    }
                }
            }

            if(!string.IsNullOrEmpty(controllerName))
            {
                if(controllerName == "xbox")
                {
                    eventSystem.GetComponent<StandaloneInputModule>().submitButton = "SubmitXbox";
                }
                else
                {
                    eventSystem.GetComponent<StandaloneInputModule>().submitButton = "Submit";
                }
            }
        }
        else
        {
            if(scene.name.Contains("Field") || scene.name.Contains("Main") || MenuController.instance.OpenedMenu)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(!isInBattle)
            {
                if (!scene.name.Contains("Field") && !scene.name.Contains("Main") && !MenuController.instance.OpenedMenu)
                {
                    if (forceCursorOn)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    else
                    {
                        if (GameManager.instance.IsTutorial)
                        {
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }
                        else
                        {
                            Cursor.lockState = CursorLockMode.Locked;
                            Cursor.visible = false;
                        }
                    }
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if(!scene.name.Contains("Field") && !scene.name.Contains("Main"))
            {
                eventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "KeyBoardHorizontal";
                eventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "KeyBoardVertical";
            }

            eventSystem.GetComponent<StandaloneInputModule>().submitButton = "Submit";

            controllerPluggedIn = false;
            hasSetCurrentSelectedObject = false;

            if(!hasSetEventObjectToNull)
            {
                eventSystem.SetSelectedGameObject(null);

                hasSetEventObjectToNull = true;
            }
        }
    }

    public void SetSelectedObject(GameObject selected)
    {
        currentSelectedObject = selected;

        if(controllerPluggedIn || SteamUtils.IsSteamRunningOnSteamDeck())
        {
            eventSystem.SetSelectedGameObject(selected);
        } 
    }

    public void SetSelectedObjectMenu()
    {
        if(MenuController.instance.OpenedMenu)
        {
            currentSelectedObject = MenuController.instance.FirstSelectedObject;

            if (controllerPluggedIn)
            {
                if(ExitArea.instance != null)
                {
                    if (ExitArea.instance.ExitAreaAnimator.GetComponent<ExitAreaPanel>().ExitAreaPanelOpen)
                    {
                        eventSystem.SetSelectedGameObject(ExitArea.instance.ExitNoAnimator.gameObject);
                    }
                    else
                    {
                        eventSystem.SetSelectedGameObject(MenuController.instance.FirstSelectedObject);
                    }
                }
            }  
        }
    }

    private void SetSelectedGameObject()
    {
        if(currentSelectedObject != null)
           eventSystem.SetSelectedGameObject(currentSelectedObject);
    }

    public void SetSelectedLevelNode(WorldMapMovement worldMapMovement)
    {
        currentSelectedObject = worldMapMovement.LevelNodes[worldMapMovement.CurrentNodeIndex].gameObject;

        eventSystem.SetSelectedGameObject(currentSelectedObject);
    }

    public bool CheckForController()
    {
        bool isControllerPluggedIn = false;

        string[] controllerNames = Input.GetJoystickNames();

        for(int i = 0; i < controllerNames.Length; i++)
        {
            if(string.IsNullOrEmpty(controllerNames[i]))
            {
                isControllerPluggedIn = false;
            }
            else
            {
                isControllerPluggedIn = true;
                if (controllerNames[i].Contains("Xbox") || controllerNames[i].Contains("Windows"))
                {
                    controllerName = "xbox";
                }
                else
                {
                    controllerName = "playStation";
                }
            }
        }

        return isControllerPluggedIn;
    }

    public void CheckForSteamDeck()
    {
        if(SteamUtils.IsSteamRunningOnSteamDeck())
        {
            bool result = SteamUtils.ShowFloatingGamepadTextInput(EFloatingGamepadTextInputMode.k_EFloatingGamepadTextInputModeModeSingleLine, 0, 0, 500, 200);
        }
    }
}