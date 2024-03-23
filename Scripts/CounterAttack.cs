using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CounterAttack : MonoBehaviour
{
    private BattlePlayer battlePlayer;

    [SerializeField]
    private Slider counterNotch;

    [SerializeField]
    private GameObject filledCounterThresholdObject;

    [SerializeField]
    private RectTransform counterThresholdObject, thresholdFrame;

    [SerializeField]
    private Animator counterAttackAnimator, missedAttackAnimator;

    [SerializeField]
    private float sliderPingPongTime;

    private bool isCounterAttacking, hasMiracleCounter, reachedMaxValue, hasMissedAttack;

    private Coroutine missedAttackRoutine;

    public BattlePlayer _BattlePlayer
    {
        get
        {
            return battlePlayer;
        }
        set
        {
            battlePlayer = value;
        }
    }

    public bool IsCounterAttacking
    {
        get
        {
            return isCounterAttacking;
        }
        set
        {
            isCounterAttacking = value;
        }
    }

    public float SliderPingPongTime
    {
        get
        {
            return sliderPingPongTime;
        }
        set
        {
            sliderPingPongTime = value;
        }
    }

    private void Update()
    {
        if (isCounterAttacking)
        {
            if(!hasMissedAttack)
            {
                if (counterNotch.value <= 0)
                {
                    reachedMaxValue = false;
                }

                if (!reachedMaxValue)
                {
                    if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Focus))
                    {
                        counterNotch.value += Time.deltaTime * 1.1f;
                    }
                    else
                    {
                        counterNotch.value += Time.deltaTime * sliderPingPongTime;
                    }

                    if (counterNotch.value >= 1)
                    {
                        reachedMaxValue = true;
                    }
                }
                else
                {
                    if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Focus))
                    {
                        counterNotch.value -= Time.deltaTime * 1.1f;
                    }
                    else
                    {
                        counterNotch.value -= Time.deltaTime * sliderPingPongTime;
                    }
                }
            }
        } 
    }

    public void StartNotch()
    {
        counterAttackAnimator.Play("CounterAttack", -1, 0);

        reachedMaxValue = false;

        isCounterAttacking = true;

        counterNotch.value = 0.5f;
    }

    public void StopNotch()
    {
        counterAttackAnimator.Play("Reverse", -1, 0);

        IsCounterAttacking = false;
    }

    public void CheckMiracleCounterPower()
    {
        if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.MiracleCounter))
        {
            if (battlePlayer.MiracleCounterIndex > 2)
            {
                hasMiracleCounter = false;

                counterThresholdObject.gameObject.SetActive(true);
                filledCounterThresholdObject.SetActive(false);
            }
            else
            {
                hasMiracleCounter = true;

                counterThresholdObject.gameObject.SetActive(false);
                filledCounterThresholdObject.SetActive(true);
            }
        }
        else
        {
            hasMiracleCounter = false;

            counterThresholdObject.gameObject.SetActive(true);
            filledCounterThresholdObject.SetActive(false);
        }
    }

    public bool CanCounter()
    {
        bool canCounter = false;

        if(hasMiracleCounter)
        {
            canCounter = true;
        }

        if(!hasMiracleCounter)
        {
            if(battlePlayer.HasDullBladeStatus ||battlePlayer.HasBlindRage)
            {
                if(counterNotch.value >= 0.339f && counterNotch.value <= 0.647f && !hasMissedAttack)
                {
                    canCounter = true;
                }
            }
            else
            {
                if(counterNotch.value >= 0.2f && counterNotch.value <= 0.78f && !hasMissedAttack)
                {
                    canCounter = true;
                }
            }
        }

        return canCounter;
    }

    public void SetSuccessNotch()
    {
        if(battlePlayer.HasDullBladeStatus || battlePlayer.HasBlindRage)
        {
            counterThresholdObject.sizeDelta = new Vector2(56.28f, counterThresholdObject.GetComponent<RectTransform>().sizeDelta.y);

            counterThresholdObject.anchoredPosition = new Vector2(0, 0);

            thresholdFrame.sizeDelta = new Vector2(56.291f, thresholdFrame.sizeDelta.y);

            thresholdFrame.anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            counterThresholdObject.sizeDelta = new Vector2(108.44f, counterThresholdObject.GetComponent<RectTransform>().sizeDelta.y);

            counterThresholdObject.anchoredPosition = new Vector2(0, 0);

            thresholdFrame.sizeDelta = new Vector2(110.972f, thresholdFrame.sizeDelta.y);

            thresholdFrame.anchoredPosition = new Vector2(0, 0);
        }
    }

    private IEnumerator MissedAttack()
    {
        missedAttackAnimator.Play("MissedAttack", -1, 0);

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.MissedAttackAudio);

        hasMissedAttack = true;

        yield return new WaitForSeconds(1f);

        missedAttackAnimator.Play("Idle", -1, 0);

        hasMissedAttack = false;

        missedAttackRoutine = null;
    }

    public void StartMissedAttackRoutine()
    {
        if(missedAttackRoutine == null)
        {
            missedAttackRoutine = StartCoroutine(MissedAttack());
        }
    }
}