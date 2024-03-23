using System.Collections;
using UnityEngine;

public class PreBattleAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private EnemyStats enemyStats;

    [SerializeField]
    private Animator bossAnimator;

    [SerializeField]
    private bool isFinalBoss;

    public void PlayBattleCryAudio()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.SpiderKingBattleCryAudio);
    }

    public void StartBattle()
    {
        bossAnimator.Play("Idle");

        if(isFinalBoss)
        {
            GameManager.instance.IsFinalBossFight = true;
        }
        else
        {
            GameManager.instance.IsBossFight = true;
        }

        StartCoroutine(WaitToBeginBattle());
    }

    private IEnumerator WaitToBeginBattle()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.EnemiesToLoad.Add(enemyStats);
        GameManager.instance.EnterBattle();
    }
}