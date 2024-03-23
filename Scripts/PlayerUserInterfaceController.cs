using UnityEngine;
using TMPro;
using Steamworks;

public class PlayerUserInterfaceController : MonoBehaviour
{
    [SerializeField]
    private Animator playerInformation, stageObjectiveAnimator, secretObjectiveAnimator;

    [SerializeField]
    private TextMeshProUGUI stageObjText, secretObjText;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private float lifeTime = 2, lifeTimeToReduce;

    public CanvasGroup _CanvasGroup
    {
        get
        {
            return canvasGroup;
        }
        set
        {
            canvasGroup = value;
        }
    }

    private bool gotRecoveryItem;

    public bool GotRecoveryItem
    {
        get
        {
            return gotRecoveryItem;
        }
        set
        {
            gotRecoveryItem = value;
        }
    }

    private void Update()
    {
        if(!gotRecoveryItem)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetButton("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            if (!SteamOverlayPause.instance.IsPaused)
                                playerInformation.Play("Show");
                        }
                        else if (Input.GetButtonUp("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            if (!SteamOverlayPause.instance.IsPaused)
                                playerInformation.Play("Hide");
                        }

                        if (SteamOverlayPause.instance.IsPaused)
                        {
                            if (playerInformation.GetCurrentAnimatorStateInfo(0).IsName("Show"))
                            {
                                playerInformation.Play("Hide");
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButton("XboxPlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            if (!SteamOverlayPause.instance.IsPaused)
                                playerInformation.Play("Show");
                        }
                        else if (Input.GetButtonUp("XboxPlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            if (!SteamOverlayPause.instance.IsPaused)
                                playerInformation.Play("Hide");
                        }

                        if (SteamOverlayPause.instance.IsPaused)
                        {
                            if (playerInformation.GetCurrentAnimatorStateInfo(0).IsName("Show"))
                            {
                                playerInformation.Play("Hide");
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButton("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                    {
                        if (!SteamOverlayPause.instance.IsPaused)
                            playerInformation.Play("Show");
                    }
                    else if (Input.GetButtonUp("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                    {
                        if (!SteamOverlayPause.instance.IsPaused)
                            playerInformation.Play("Hide");
                    }

                    if (SteamOverlayPause.instance.IsPaused)
                    {
                        if (playerInformation.GetCurrentAnimatorStateInfo(0).IsName("Show"))
                        {
                            playerInformation.Play("Hide");
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space) && !MenuController.instance.OpenedMenu)
                {
                    if (!SteamOverlayPause.instance.IsPaused)
                        playerInformation.Play("Show");
                }
                else if (Input.GetKeyUp(KeyCode.Space) && !MenuController.instance.OpenedMenu)
                {
                    if (!SteamOverlayPause.instance.IsPaused)
                        playerInformation.Play("Hide");
                }

                if (SteamOverlayPause.instance.IsPaused)
                {
                    if (playerInformation.GetCurrentAnimatorStateInfo(0).IsName("Show"))
                    {
                        playerInformation.Play("Hide");
                    }
                }
            }
        }
        else
        {
            lifeTimeToReduce -= Time.deltaTime;
            if(lifeTimeToReduce <= 0)
            {
                HidePlayerInformation(false);

                gotRecoveryItem = false;
            }
        }

        ToggleObjectivesPanel();
    }

    private void ToggleObjectivesPanel()
    {
        if(stageObjectiveAnimator != null && stageObjText.text.Length > 0)
        {
            if (InputManager.instance.ControllerPluggedIn)
            {
                if (InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetButton("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            stageObjectiveAnimator.gameObject.SetActive(true);

                            if (!SteamOverlayPause.instance.IsPaused)
                                stageObjectiveAnimator.Play("ShowObj");
                        }
                        else if (Input.GetButtonUp("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            stageObjectiveAnimator.gameObject.SetActive(true);

                            if (!SteamOverlayPause.instance.IsPaused)
                                stageObjectiveAnimator.Play("HideObj");
                        }

                        if (SteamOverlayPause.instance.IsPaused)
                        {
                            if (stageObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                            {
                                stageObjectiveAnimator.Play("HideObj");
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButton("XboxPlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            stageObjectiveAnimator.gameObject.SetActive(true);

                            if (!SteamOverlayPause.instance.IsPaused)
                                stageObjectiveAnimator.Play("ShowObj");
                        }
                        else if (Input.GetButtonUp("XboxPlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            stageObjectiveAnimator.gameObject.SetActive(true);

                            if (!SteamOverlayPause.instance.IsPaused)
                                stageObjectiveAnimator.Play("HideObj");
                        }

                        if (SteamOverlayPause.instance.IsPaused)
                        {
                            if (stageObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                            {
                                stageObjectiveAnimator.Play("HideObj");
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButton("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                    {
                        stageObjectiveAnimator.gameObject.SetActive(true);

                        if (!SteamOverlayPause.instance.IsPaused)
                            stageObjectiveAnimator.Play("ShowObj");
                    }
                    else if (Input.GetButtonUp("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                    {
                        stageObjectiveAnimator.gameObject.SetActive(true);

                        if (!SteamOverlayPause.instance.IsPaused)
                            stageObjectiveAnimator.Play("HideObj");
                    }

                    if (SteamOverlayPause.instance.IsPaused)
                    {
                        if (stageObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                        {
                            stageObjectiveAnimator.Play("HideObj");
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Space) && !MenuController.instance.OpenedMenu)
                {
                    stageObjectiveAnimator.gameObject.SetActive(true);

                    if (!SteamOverlayPause.instance.IsPaused)
                        stageObjectiveAnimator.Play("ShowObj");
                }
                else if (Input.GetKeyUp(KeyCode.Space) && !MenuController.instance.OpenedMenu)
                {
                    stageObjectiveAnimator.gameObject.SetActive(true);

                    if (!SteamOverlayPause.instance.IsPaused)
                        stageObjectiveAnimator.Play("HideObj");
                }

                if (SteamOverlayPause.instance.IsPaused)
                {
                    if (stageObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                    {
                        stageObjectiveAnimator.Play("HideObj");
                    }
                }
            }
        }

        if(secretObjectiveAnimator != null && secretObjText.text.Length > 0)
        {
            if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.SecretSeeker) || GameManager.instance._StageObjectives.ClearedSecretObjective)
            {
                if (InputManager.instance.ControllerPluggedIn)
                {
                    if (InputManager.instance.ControllerName == "xbox")
                    {
                        if(SteamUtils.IsSteamRunningOnSteamDeck())
                        {
                            if (Input.GetButton("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                            {
                                secretObjectiveAnimator.gameObject.SetActive(true);

                                if (!SteamOverlayPause.instance.IsPaused)
                                    secretObjectiveAnimator.Play("ShowObj");
                            }
                            else if (Input.GetButtonUp("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                            {
                                secretObjectiveAnimator.gameObject.SetActive(true);

                                if (!SteamOverlayPause.instance.IsPaused)
                                    secretObjectiveAnimator.Play("HideObj");
                            }

                            if (SteamOverlayPause.instance.IsPaused)
                            {
                                if (secretObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                                {
                                    secretObjectiveAnimator.Play("HideObj");
                                }
                            }
                        }
                        else
                        {
                            if (Input.GetButton("XboxPlayerInfo") && !MenuController.instance.OpenedMenu)
                            {
                                secretObjectiveAnimator.gameObject.SetActive(true);

                                if (!SteamOverlayPause.instance.IsPaused)
                                    secretObjectiveAnimator.Play("ShowObj");
                            }
                            else if (Input.GetButtonUp("XboxPlayerInfo") && !MenuController.instance.OpenedMenu)
                            {
                                secretObjectiveAnimator.gameObject.SetActive(true);

                                if (!SteamOverlayPause.instance.IsPaused)
                                    secretObjectiveAnimator.Play("HideObj");
                            }

                            if (SteamOverlayPause.instance.IsPaused)
                            {
                                if (secretObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                                {
                                    secretObjectiveAnimator.Play("HideObj");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButton("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            secretObjectiveAnimator.gameObject.SetActive(true);

                            if (!SteamOverlayPause.instance.IsPaused)
                                secretObjectiveAnimator.Play("ShowObj");
                        }
                        else if (Input.GetButtonUp("Ps4PlayerInfo") && !MenuController.instance.OpenedMenu)
                        {
                            secretObjectiveAnimator.gameObject.SetActive(true);

                            if (!SteamOverlayPause.instance.IsPaused)
                                secretObjectiveAnimator.Play("HideObj");
                        }

                        if(SteamOverlayPause.instance.IsPaused)
                        {
                            if (secretObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                            {
                                secretObjectiveAnimator.Play("HideObj");
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.Space) && !MenuController.instance.OpenedMenu)
                    {
                        secretObjectiveAnimator.gameObject.SetActive(true);

                        if (!SteamOverlayPause.instance.IsPaused)
                            secretObjectiveAnimator.Play("ShowObj");
                    }
                    else if (Input.GetKeyUp(KeyCode.Space) && !MenuController.instance.OpenedMenu)
                    {
                        secretObjectiveAnimator.gameObject.SetActive(true);

                        if (!SteamOverlayPause.instance.IsPaused)
                            secretObjectiveAnimator.Play("HideObj");
                    }

                    if (SteamOverlayPause.instance.IsPaused)
                    {
                        if (secretObjectiveAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShowObj"))
                        {
                            secretObjectiveAnimator.Play("HideObj");
                        }
                    }
                }
            }
        }
    }

    public void ShowPlayerInformation()
    {
        lifeTimeToReduce = lifeTime;

        playerInformation.Play("Show");
    }

    public void HidePlayerInformation(bool includeObjectives)
    {
        playerInformation.Play("Hide");

        if(includeObjectives)
        {
            if (stageObjectiveAnimator != null)
            {
                if(stageObjectiveAnimator.gameObject.activeSelf)
                   stageObjectiveAnimator.Play("HideObj");
            }

            if (secretObjectiveAnimator != null)
            {
                if (MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.SecretSeeker) || GameManager.instance._StageObjectives.ClearedSecretObjective)
                {
                    if(secretObjectiveAnimator.gameObject.activeSelf)
                       secretObjectiveAnimator.Play("HideObj");
                }
            }
        }
    }

    public void IdlePlayerInformation()
    {
        playerInformation.Play("Idle");
    }
}