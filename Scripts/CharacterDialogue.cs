using System.Collections;
using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    [SerializeField][TextArea]
    private string[] dialogue;

    [SerializeField]
    private DialogueTrigger dialogueTrigger;

    [SerializeField]
    private Animator dialogueBoxAnimator;

    [SerializeField]
    private TextMeshProUGUI dialogueBoxText;

    private Coroutine dialogueRoutine;

    private int dialogueIndex;

    private bool openedDialogue;

    public bool OpenedDialogue
    {
        get
        {
            return openedDialogue;
        }
        set
        {
            openedDialogue = value;
        }
    }

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if(openedDialogue)
        {
            if(InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxAttack"))
                    {
                        if (dialogueRoutine != null)
                        {
                            StopCoroutine(dialogueRoutine);
                            dialogueRoutine = null;
                        }

                        if (dialogueBoxText.text.Length == dialogue[dialogueIndex].Length)
                        {
                            dialogueIndex++;

                            if (dialogueIndex >= dialogue.Length)
                            {
                                EndDialogue();
                            }
                            else
                            {
                                dialogueRoutine = StartCoroutine(StartDialogue(dialogue[dialogueIndex]));
                            }
                        }
                        else
                        {
                            dialogueBoxText.text = dialogue[dialogueIndex];
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Attack"))
                    {
                        if (dialogueRoutine != null)
                        {
                            StopCoroutine(dialogueRoutine);
                            dialogueRoutine = null;
                        }

                        if (dialogueBoxText.text.Length == dialogue[dialogueIndex].Length)
                        {
                            dialogueIndex++;

                            if (dialogueIndex >= dialogue.Length)
                            {
                                EndDialogue();
                            }
                            else
                            {
                                dialogueRoutine = StartCoroutine(StartDialogue(dialogue[dialogueIndex]));
                            }
                        }
                        else
                        {
                            dialogueBoxText.text = dialogue[dialogueIndex];
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    if (dialogueRoutine != null)
                    {
                        StopCoroutine(dialogueRoutine);
                        dialogueRoutine = null;
                    }

                    if (dialogueBoxText.text.Length == dialogue[dialogueIndex].Length)
                    {
                        dialogueIndex++;

                        if (dialogueIndex >= dialogue.Length)
                        {
                            EndDialogue();
                        }
                        else
                        {
                            dialogueRoutine = StartCoroutine(StartDialogue(dialogue[dialogueIndex]));
                        }
                    }
                    else
                    {
                        dialogueBoxText.text = dialogue[dialogueIndex];
                    }
                }
            }
        }
    }

    public void BeginDialogueCoroutine()
    {
        dialogueBoxAnimator.Play("Show", -1, 0);

        dialogueRoutine = StartCoroutine(StartDialogue(dialogue[dialogueIndex]));

        openedDialogue = true;

        MenuController.instance.CanToggleMenu = false;

        if(gameObject.GetComponent<NpcMovement>())
        {
            gameObject.GetComponent<NpcMovement>().StopMovement();
        }
    }

    private IEnumerator StartDialogue(string stringToFill)
    {
        int i = 0;

        string newDialogueString = "";

        dialogueBoxText.text = "";

        while (i < dialogue[dialogueIndex].Length)
        {
            newDialogueString += stringToFill[i++];

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.ExpGainAudioClip);

            dialogueBoxText.text = newDialogueString;

            yield return new WaitForSeconds(0.05f);
        }
        newDialogueString = stringToFill;

        dialogueBoxText.text = newDialogueString;
    }

    private void EndDialogue()
    {
        dialogueBoxAnimator.Play("Hide", -1, 0);

        dialogueIndex = 0;

        openedDialogue = false;

        MenuController.instance.CanToggleMenu = true;

        dialogueTrigger.EnableControls();

        if(gameObject.GetComponent<NpcMovement>())
        {
            gameObject.GetComponent<NpcMovement>().ResumeMovement();
        }
        else
        {
            dialogueTrigger.RotateToDefaultPosition();
        }
    }

    private float DialogueSpeed()
    {
        float speed = 0;

        if(PlayerPrefs.HasKey("SlowDialogue"))
        {
            if(PlayerPrefs.GetInt("SlowDialogue") == 1)
            {
                speed = 0.8f;
            }
        }
        else if(PlayerPrefs.HasKey("MidDialogue"))
        {
            if (PlayerPrefs.GetInt("MidDialogue") == 1)
            {
                speed = 0.6f;
            }
        }
        else if(PlayerPrefs.HasKey("FastDialogue"))
        {
            if (PlayerPrefs.GetInt("FastDialogue") == 1)
            {
                speed = 0.35f;
            }
        }
        else
        {
            speed = 0.3f;
        }

        return speed;
    }
}