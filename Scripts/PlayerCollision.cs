using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerFieldSwordAndShield playerField;

    [SerializeField]
    private PlayerUserInterfaceController userInterfaceController;

    [SerializeField]
    private PlayerInformation playerInformation;

    [SerializeField]
    private CapsuleCollider capsuleCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(!playerField.IsUsingSword)
        {
            if (other.GetComponent<FieldEnemyAnimator>() && !other.GetComponent<HealingFieldItem>() && !other.GetComponent<ExitArea>())
            {
                GameManager.instance.EnemyObject = other.GetComponent<FieldEnemyAnimator>().Enemy;

                other.GetComponent<FieldEnemyAnimator>().Enemy.LoadEnemiesForGameManager();

                other.GetComponent<FieldEnemyAnimator>().Enemy.HasAttacked = false;

                other.GetComponent<FieldEnemyAnimator>()._EnemyTriggerZone.DisableTrigger();

                other.GetComponent<FieldEnemyAnimator>()._EnemyTriggerZone.FoundTargetParticle.Stop();

                other.GetComponent<BoxCollider>().enabled = false;

                MenuController.instance.CanToggleMenu = false;

                if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Foresight))
                {
                    GameManager.instance.IsAPreemptiveStrike = true;
                }

                GameManager.instance.EnterBattle();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<HealingFieldItem>())
        {
            HealingFieldItem healing = collision.collider.GetComponent<HealingFieldItem>();

            userInterfaceController.ShowPlayerInformation();

            userInterfaceController.GotRecoveryItem = true;

            healing.Recover();

            playerInformation.UpdatePlayerInformation();

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.HealingPickUpAudio);

            GameManager.instance.PlayHealParticle(healing);

            Destroy(collision.collider.gameObject);
        }
    }
}