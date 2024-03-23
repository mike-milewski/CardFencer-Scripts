using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    BattleSystem battleSystem = null;

    [SerializeField]
    private BattlePlayer battlePlayer;

    [SerializeField]
    private ParticleSystem basicAttackParticle, healParticle;

    private bool dealsDoubleDamage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.GetComponent<BattleEnemy>())
        {
            BattleEnemy battleEnemy = other.transform.parent.GetComponent<BattleEnemy>();

            battleEnemy.AttackInterrupted = true;

            if(battlePlayer.HasCounterRegenStatus)
            {
                battlePlayer.HealHealth(1, true);

                healParticle.gameObject.SetActive(true);
                healParticle.Play();
                healParticle.transform.position = new Vector3(battlePlayer.transform.position.x, battlePlayer.transform.position.y, battlePlayer.transform.position.z);

                healParticle.GetComponent<CardParticleAudio>().CheckAudio();
            }

            if(battlePlayer.HasBlindRage)
            {
                dealsDoubleDamage = true;
            }
            else
            {
                dealsDoubleDamage = false;
            }

            if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Pierce))
            {
                if(battleSystem == null)
                {
                    battleSystem = FindObjectOfType<BattleSystem>();
                }

                battleEnemy.TakeDamage(dealsDoubleDamage ? battlePlayer.DamageValue() * 2 : battlePlayer.DamageValue(), true, true, true);
            }
            else
            {
                battleEnemy.TakeDamage(dealsDoubleDamage ? battlePlayer.DamageValue() * 2 : battlePlayer.DamageValue(), false, true, true);
            }

            basicAttackParticle.gameObject.SetActive(true);

            basicAttackParticle.gameObject.transform.position = new Vector3(battleEnemy.transform.position.x, battleEnemy.transform.position.y + battleEnemy._EnemyStats.hitAnimationOffsetY, 
                                                                            battleEnemy.transform.position.z);

            basicAttackParticle.Play();

            if (!basicAttackParticle.GetComponent<AudioSource>().isPlaying)
                basicAttackParticle.GetComponent<CardParticleAudio>().CheckAudio();

            battlePlayer.DisableWeaponCollider();
        }
    }
}
