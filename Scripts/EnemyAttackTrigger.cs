using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    [SerializeField]
    private EnemyFieldAI enemyAi;

    [SerializeField]
    private FieldEnemyAnimator fieldEnemy;

    [SerializeField]
    private Collider attackCollider;

    [SerializeField]
    private ParticleSystem attackParticle;

    [SerializeField]
    private AudioSource audioSource;

    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            var particle = Instantiate(attackParticle);

            particle.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.8f, other.transform.position.z);

            PlaySound();

            attackCollider.enabled = false;

            if(!MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Hindsight))
            {
                GameManager.instance.IsAnAmbush = true;
            }

            GameManager.instance.AttackParticle = particle.gameObject;

            GameManager.instance.EnemyObject = fieldEnemy.Enemy;

            fieldEnemy.Enemy.LoadEnemiesForGameManager();

            MenuController.instance.CanToggleMenu = false;

            GameManager.instance.EnterBattle();
        }
    }

    private void PlaySound()
    {
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