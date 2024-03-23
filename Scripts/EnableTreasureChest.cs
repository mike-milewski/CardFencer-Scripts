using UnityEngine;
using TMPro;
using Steamworks;

public class EnableTreasureChest : MonoBehaviour
{
    [SerializeField]
    private TreasureChestSearcher treasureChestSearcher;

    [SerializeField]
    private TreasureChest treasureChest;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private TextMeshProUGUI messagePromptText;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private Animator messagePromptAnimator;

    [SerializeField]
    private AudioSource interactableParticleAudioSource;

    [SerializeField]
    private ParticleSystem interactableParticle, interactableChild, interactableChildTwo;

    public ParticleSystem InteractableParticle => interactableParticle;

    public Animator MessagePromptAnimator => messagePromptAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCollision>())
        {
            AllowInteraction();

            messagePromptAnimator.Play("FadeIn");

            if(playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "R2");
                    }
                    else
                    {
                        messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "RB");
                    }
                }
                else
                {
                    messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "E");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCollision>())
        {
            DisallowInteraction();

            messagePromptAnimator.Play("FadeOut");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlayerCollision>())
        {
            if(playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "R2");
                    }
                    else
                    {
                        messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "RB");
                    }
                }
                else
                {
                    messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Chest\n Press " + string.Format("\"{0}\"", "E");
            }
        }
    }

    private void AllowInteraction()
    {
        treasureChestSearcher._TreasureChest = treasureChest;

        treasureChestSearcher.CanOpenChest = true;

        interactableParticle.gameObject.SetActive(true);

        if(PlayerPrefs.HasKey("SoundEffects"))
        {
            interactableParticleAudioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            interactableParticleAudioSource.Play();
        }
        else
        {
            interactableParticleAudioSource.Play();
        }

        PlayInteractableParticle(true);

        interactableParticle.Play();
    }

    private void DisallowInteraction()
    {
        treasureChestSearcher._TreasureChest = null;

        treasureChestSearcher.CanOpenChest = false;

        PlayInteractableParticle(false);
    }

    private void PlayInteractableParticle(bool canLoop)
    {
        ParticleSystem.MainModule mainParticle = interactableParticle.main;

        mainParticle.loop = canLoop;

        ParticleSystem.MainModule mainParticle2 = interactableChild.main;

        mainParticle2.loop = canLoop;

        ParticleSystem.MainModule mainParticle3 = interactableChildTwo.main;

        mainParticle3.loop = canLoop;
    }
}
