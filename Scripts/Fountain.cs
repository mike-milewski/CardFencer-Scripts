using UnityEngine;
using TMPro;
using Steamworks;

public class Fountain : MonoBehaviour
{
    [SerializeField]
    private FountainMenu fountainMenu;

    [SerializeField]
    private Animator messagePromptAnimator;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private TextMeshProUGUI messagePromptText;

    [SerializeField]
    private AudioSource audioSource;

    private bool canOpenMenu, openedFountainMenu;

    public bool OpenedFountainMenu
    {
        get => openedFountainMenu;
        set => openedFountainMenu = value;
    }

    private void Update()
    {
        if(canOpenMenu && !MenuController.instance.OpenedMenu && !SteamOverlayPause.instance.IsPaused)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetAxisRaw("SteamChest") == 1)
                        {
                            if (!openedFountainMenu)
                            {
                                fountainMenu.OpenMenu();
                                openedFountainMenu = true;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("XboxOpenChest"))
                        {
                            if (!openedFountainMenu)
                            {
                                fountainMenu.OpenMenu();
                                openedFountainMenu = true;
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4OpenChest"))
                    {
                        if (!openedFountainMenu)
                        {
                            fountainMenu.OpenMenu();
                            openedFountainMenu = true;
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!openedFountainMenu)
                    {
                        fountainMenu.OpenMenu();
                        openedFountainMenu = true;
                    }
                }
            }
        }

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            canOpenMenu = true;

            messagePromptAnimator.Play("FadeIn", -1, 0);

            if (playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "R2");
                    }
                    else
                    {
                        messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "RB");
                    }
                }
                else
                {
                    messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "E");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            canOpenMenu = false;

            messagePromptAnimator.Play("FadeOut", -1, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            if (playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "R2");
                }
                else
                {
                    messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Fountain\n Press " + string.Format("\"{0}\"", "E");
            }
        }
    }
}