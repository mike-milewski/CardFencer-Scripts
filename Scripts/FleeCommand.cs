using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class FleeCommand : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private BattleResults battleResults;

    [SerializeField]
    private BattlePlayer battlePlayer;

    [SerializeField]
    private Animator fleeCommandAnimator;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Transform escapePoint;

    [SerializeField]
    private TextMeshProUGUI fleePromptText;

    [SerializeField]
    private Image fleeGauge;

    [SerializeField]
    private Slider fleeNotch;

    [SerializeField]
    private Button fleeButton;

    [SerializeField]
    private GameObject fleeTextObject;

    [SerializeField]
    private float fleeSpeed, runAwaySpeed;

    [SerializeField][Header("Flee Gauge Random Start Values")]
    private float startingGaugeMin;
    [SerializeField]
    private float startingGaugeMax;

    private bool canNotchMove, escapeFailed, isEscaping, fleeNotchMovingForward, fleeNotchMovingBackward, perfectEscape;

    private Coroutine notchRoutine, fleeRoutine;

    private Vector3 escapeDistance;

    private Quaternion playerRotation;

    private void Start()
    {
        if(GameManager.instance.IsBossFight || MenuController.instance._StickerPowerManager.CheckStartOfBattleStickerPower(StickerPower.Enrage))
        {
            fleeButton.interactable = false;
            fleeButton.GetComponent<Image>().raycastTarget = false;
            fleeTextObject.SetActive(true);
        }
        else
        {
            fleeButton.interactable = true;
            fleeButton.GetComponent<Image>().raycastTarget = true;
            fleeTextObject.SetActive(false);
        }
    }

    public void BeginFlee()
    {
        battleSystem._InputManager.SetSelectedObject(null);
        battleSystem._InputManager._EventSystem.sendNavigationEvents = false;
        battleSystem.NavagationDisabled = true;

        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.PerfectEscape))
        {
            battleSystem._StickerPowerHolder.CreateStickerMessage(StickerPower.PerfectEscape);
            StartCoroutine("EscapeArtist");

            battleSystem.DisableBattleUIInteracteability();
            battleSystem.DeselectTargets(true);

            battleSystem.ResetAttackCommandAnimation();
            battleSystem.ResetItemCardAnimation();
            battleSystem.ResetSelectedCardAnimation();
        }
        else
        {
            fleeCommandAnimator.Play("Flee");

            escapeFailed = false;

            isEscaping = true;

            notchRoutine = StartCoroutine("WaitToStopNotch");

            float randomStart = Random.Range(startingGaugeMin, startingGaugeMax);

            fleeGauge.fillAmount = randomStart;

            fleeNotch.value = Random.Range(0f, 1f);

            fleeNotchMovingForward = true;
            fleeNotchMovingBackward = false;

            battleSystem.DisableBattleUIInteracteability();
            battleSystem.DeselectTargets(true);

            battleSystem.ResetAttackCommandAnimation();
            battleSystem.ResetItemCardAnimation();
            battleSystem.ResetSelectedCardAnimation();
        }
    }

    private void Update()
    {
        if(canNotchMove)
        {
            if (InputManager.instance.ControllerPluggedIn)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    if (Input.GetButtonDown("XboxAttack"))
                    {
                        fleeGauge.fillAmount += fleeSpeed;
                    }

                    fleePromptText.text = "A";
                }
                else
                {
                    if (Input.GetButtonDown("Ps4Attack"))
                    {
                        fleeGauge.fillAmount += fleeSpeed;
                    }

                    fleePromptText.text = "X";
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    fleeGauge.fillAmount += fleeSpeed;
                }

                fleePromptText.text = "C";
            }
        }

        if (canNotchMove)
        {
            if (fleeGauge.fillAmount > 0)
                fleeGauge.fillAmount -= Time.deltaTime / 20;

            MoveFleeNotch();
        }

        if(canvasGroup.alpha > 0 || perfectEscape)
        {
            if (!escapeFailed)
            {
                AnimatePlayerEscaping();
            }
        }
    }

    private void SetFleeNotch()
    {
        if(isEscaping)
           canNotchMove = true;
    }

    private void MoveFleeNotch()
    {
        if(canNotchMove)
        {
            if(fleeNotchMovingForward)
            {
                fleeNotch.value += Time.deltaTime;
                if(fleeNotch.value >= 1)
                {
                    fleeNotchMovingBackward = true;
                    fleeNotchMovingForward = false;
                }
            }

            if (fleeNotchMovingBackward)
            {
                fleeNotch.value -= Time.deltaTime;
                if (fleeNotch.value <= 0)
                {
                    fleeNotchMovingForward = true;
                    fleeNotchMovingBackward = false;
                }
            }
        }
    }

    private IEnumerator WaitToStopNotch()
    {
        float t = 0;

        while(t < 5)
        {
            t += Time.deltaTime;
            yield return null;
        }
        canNotchMove = false;

        CheckNotchPosition();
    }

    private void CheckNotchPosition()
    {
        if(fleeNotch.value >= 9.5f)
        {
            fleeNotch.value = 1;
        }

        if(fleeGauge.fillAmount >= fleeNotch.value)
        {
            SuccessfulEscape();
        }
        else
        {
            escapeFailed = true;
            StartCoroutine("WaitToReturnToPosition");
        }
    }

    private void SuccessfulEscape()
    {
        fleeRoutine = StartCoroutine("RunAway");

        fleeCommandAnimator.Play("Reverse");

        battlePlayer.HideStatsBar();

        isEscaping = false;
    }

    private IEnumerator EscapeArtist()
    {
        battlePlayer.HideStatsBar();

        perfectEscape = true;

        yield return new WaitForSeconds(1f);

        StartCoroutine("RunAway");
    }

    public void CharacterFleeAction()
    {
        battlePlayer._Animator.Play("Jump");
    }

    private void AnimatePlayerEscaping()
    {
        escapeDistance = new Vector3(escapePoint.position.x - battlePlayer.transform.position.x, 0, escapePoint.position.z - battlePlayer.transform.position.z).normalized;

        playerRotation = Quaternion.LookRotation(escapeDistance);

        battlePlayer.transform.rotation = Quaternion.Slerp(battlePlayer.transform.rotation, playerRotation, 5 * Time.deltaTime).normalized;
    }

    private IEnumerator WaitToReturnToPosition()
    {
        while(battlePlayer._Animator.speed > 0.3f)
        {
            battlePlayer._Animator.speed -= Time.deltaTime;

            yield return null;
        }
        battlePlayer._Animator.speed = 1;

        battlePlayer._Animator.Play("Idle_Battle");

        yield return new WaitForSeconds(0.5f);

        fleeCommandAnimator.Play("Reverse");

        battlePlayer._Animator.Play("ReturnJump");

        while (Mathf.Abs(battlePlayer.transform.rotation.y) > 0.01f)
        {
            battlePlayer.transform.rotation = Quaternion.Slerp(battlePlayer.transform.rotation, battlePlayer.DefaultRotation, 5 * Time.deltaTime).normalized;

            yield return new WaitForFixedUpdate();
        }

        battlePlayer.transform.rotation = battlePlayer.DefaultRotation;

        isEscaping = false;

        battleSystem.EnemyTurn();
        battleSystem.ResetEnemyStatusIndex();
        battleSystem.CheckEnemyStatus();

        StopAllCoroutines();
    }

    private IEnumerator RunAway()
    {
        float t = 0;

        while(t < 2f)
        {
            t += Time.deltaTime;

            battlePlayer.transform.position += escapeDistance * runAwaySpeed * Time.deltaTime;
            yield return null;
        }
        battlePlayer._Animator.Play("Idle_Battle");
        battleResults.ExitBattle();
    }
}