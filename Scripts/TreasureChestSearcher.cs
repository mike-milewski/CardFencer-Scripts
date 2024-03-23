using Steamworks;
using System.Security.Cryptography;
using UnityEngine;

public class TreasureChestSearcher : MonoBehaviour
{
    private TreasureChest treasureChest = null;

    [SerializeField]
    private PlayerMovement playerMovement;

    private bool canOpenChest;

    public TreasureChest _TreasureChest
    {
        get
        {
            return treasureChest;
        }
        set
        {
            treasureChest = value;
        }
    }

    public bool CanOpenChest
    {
        get
        {
            return canOpenChest;
        }
        set
        {
            canOpenChest = value;
        }
    }

    private void Update()
    {
        if(canOpenChest && !MenuController.instance.OpenedMenu)
        {
            if(playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetAxisRaw("SteamChest") == 1)
                        {
                            treasureChest.OpenChest();

                            treasureChest._EnableTreasureChest.InteractableParticle.Stop();
                            treasureChest._EnableTreasureChest.InteractableParticle.gameObject.SetActive(false);

                            canOpenChest = false;
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("XboxOpenChest"))
                        {
                            treasureChest.OpenChest();

                            treasureChest._EnableTreasureChest.InteractableParticle.Stop();
                            treasureChest._EnableTreasureChest.InteractableParticle.gameObject.SetActive(false);

                            canOpenChest = false;
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4OpenChest"))
                    {
                        treasureChest.OpenChest();

                        treasureChest._EnableTreasureChest.InteractableParticle.Stop();
                        treasureChest._EnableTreasureChest.InteractableParticle.gameObject.SetActive(false);

                        canOpenChest = false;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    treasureChest.OpenChest();

                    treasureChest._EnableTreasureChest.InteractableParticle.Stop();
                    treasureChest._EnableTreasureChest.InteractableParticle.gameObject.SetActive(false);

                    canOpenChest = false;
                }
            }
        }
    }
}