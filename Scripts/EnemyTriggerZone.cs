using UnityEngine;

public class EnemyTriggerZone : MonoBehaviour
{
    [SerializeField]
    private EnemyFieldAI enemyAI;

    [SerializeField]
    private SphereCollider triggerCollider;

    [SerializeField]
    private ParticleSystem foundTargetParticle;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool enteredTrigger;

    public ParticleSystem FoundTargetParticle => foundTargetParticle;

    public bool EnteredTrigger => enteredTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            if (CheckUndetectableTrigger()) return;

            enemyAI.StopWayPointRoutine();

            enteredTrigger = true;

            enemyAI.TimeToWaitToPursuePlayer = 0;
            enemyAI.TimeToAttackPlayer = 0;

            foundTargetParticle.Play();

            if(PlayerPrefs.HasKey("SoundEffects"))
            {
                audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
                audioSource.Play();
            }
            else
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            enemyAI.TimeToWaitToPursuePlayer = 0;
            enemyAI.TimeToAttackPlayer = 0;
            enemyAI.ResetAttackTriggerRoutine();

            enteredTrigger = false;
        }
    }

    private bool CheckUndetectableTrigger()
    {
        bool isUndetectable = false;

        if (MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Undetectable))
        {
            isUndetectable = true;
        }

        return isUndetectable;
    }

    public void DisableTrigger()
    {
        enteredTrigger = false;
        triggerCollider.enabled = false;
    }

    public void EnableTrigger()
    {
        if(enemyAI.CanMove)
           triggerCollider.enabled = true;
    }
}