using System.Collections;
using UnityEngine;
using TMPro;
using Steamworks;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private CharacterDialogue characterDialogue;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private Animator messagePromptAnimator;

    [SerializeField]
    private TextMeshProUGUI messagePromptText;

    [SerializeField]
    private SphereCollider sphereCollider;

    private Coroutine rotateRoutine, defaultRotationRoutine;

    private Quaternion characterRotation;

    private bool canStartDialogue;

    public bool CanStartDialogue
    {
        get
        {
            return canStartDialogue;
        }
        set
        {
            canStartDialogue = value;
        }
    }

    private void Awake()
    {
        characterRotation = transform.rotation;
    }

    private void Update()
    {
        if (canStartDialogue && !MenuController.instance.OpenedMenu && !SteamOverlayPause.instance.IsPaused)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if (InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        if (Input.GetAxisRaw("SteamChest") == 1)
                        {
                            characterDialogue.BeginDialogueCoroutine();

                            DisableControls();

                            canStartDialogue = false;
                        }
                    }
                    else
                    {
                        if (Input.GetButtonDown("XboxOpenChest"))
                        {
                            characterDialogue.BeginDialogueCoroutine();

                            DisableControls();

                            canStartDialogue = false;
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4OpenChest"))
                    {
                        characterDialogue.BeginDialogueCoroutine();

                        DisableControls();

                        canStartDialogue = false;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    characterDialogue.BeginDialogueCoroutine();

                    DisableControls();

                    canStartDialogue = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            canStartDialogue = true;

            messagePromptAnimator.Play("FadeIn", -1, 0);

            if (playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if(SteamUtils.IsSteamRunningOnSteamDeck())
                    {
                        messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "R2");
                    }
                    else
                    {
                        messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "RB");
                    }
                }
                else
                {
                    messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "E");
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
                    messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "R2");
                }
                else
                {
                    messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "R2");
                }
            }
            else
            {
                messagePromptText.text = "Talk\n Press " + string.Format("\"{0}\"", "E");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            canStartDialogue = false;

            messagePromptAnimator.Play("FadeOut", -1, 0);
        }
    }

    public void EnableControls()
    {
        playerMovement.enabled = true;
        cameraFollow.enabled = true;
        sphereCollider.enabled = true;
    }

    public void DisableControls()
    {
        playerMovement.PlayerAnimator.Play("IdlePose");
        messagePromptAnimator.Play("FadeOut", -1, 0);

        playerMovement.enabled = false;
        cameraFollow.enabled = false;

        sphereCollider.enabled = false;

        RotateToPlayer();
    }

    private void RotateToPlayer()
    {
        rotateRoutine = null;

        rotateRoutine = StartCoroutine("TurnToPlayer");
    }

    public void RotateToDefaultPosition()
    {
        defaultRotationRoutine = null;

        defaultRotationRoutine = StartCoroutine("ReturnToDefaultRotation");
    }

    private IEnumerator TurnToPlayer()
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            Vector3 targetDir = player.position - transform.position;

            float step = 5 * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);

            transform.rotation = Quaternion.LookRotation(newDir);

            yield return null;
        }
    }

    private IEnumerator ReturnToDefaultRotation()
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, characterRotation, 5 * Time.deltaTime);

            yield return null;
        }
        transform.rotation = characterRotation;
    }
}