using UnityEngine;
using TMPro;
using Steamworks;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private ShopMenu shopMenu;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Animator messagePromptAnimator;

    [SerializeField]
    private TextMeshProUGUI messagePromptText;

    private bool canOpenShop, openedShopMenu;

    public bool OpenedShopMenu
    {
        get => openedShopMenu;
        set => openedShopMenu = value;
    }

    private void Update()
    {
        if(canOpenShop && !MenuController.instance.OpenedMenu && !SteamOverlayPause.instance.IsPaused)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetAxisRaw("SteamChest") == 1)
                        {
                            if (!openedShopMenu)
                            {
                                shopMenu.OpenShop();
                                openedShopMenu = true;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("XboxOpenChest"))
                        {
                            if (!openedShopMenu)
                            {
                                shopMenu.OpenShop();
                                openedShopMenu = true;
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4OpenChest"))
                    {
                        if (!openedShopMenu)
                        {
                            shopMenu.OpenShop();
                            openedShopMenu = true;
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!openedShopMenu)
                    {
                        shopMenu.OpenShop();
                        openedShopMenu = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            canOpenShop = true;

            messagePromptAnimator.Play("FadeIn", -1, 0);

            if (playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "R2");
                    }
                    else
                    {
                        messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "RB");
                    }
                }
                else
                {
                    messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "E");
            }
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
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "R2");
                    }
                    else
                    {
                        messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "RB");
                    }
                }
                else
                {
                    messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Shop\n Press " + string.Format("\"{0}\"", "E");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            canOpenShop = false;

            messagePromptAnimator.Play("FadeOut", -1, 0);
        }
    }
}