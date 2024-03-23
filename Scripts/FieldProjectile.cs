using UnityEngine;

public class FieldProjectile : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hitParticle;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Collider sphereCollider;

    private EnemyFieldAI enemyFieldAI;

    private FieldEnemyAnimator fieldEnemyAnimator;

    public EnemyFieldAI _EnemyFieldAI
    {
        get
        {
            return enemyFieldAI;
        }
        set
        {
            enemyFieldAI = value;
        }
    }

    public FieldEnemyAnimator _FieldEnemyAnimator
    {
        get
        {
            return fieldEnemyAnimator;
        }
        set
        {
            fieldEnemyAnimator = value;
        }
    }

    private void OnEnable()
    {
        Destroy(gameObject, 2f);
    }

    private void OnDisable()
    {
        var hit = Instantiate(hitParticle, transform.position, Quaternion.identity);

        hit.gameObject.AddComponent<DestroyParticle>();
        hit.gameObject.GetComponent<DestroyParticle>().SetDestroyTime(2f);
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            EnterBatteOnContact(other.transform.position);
        }
    }

    private void EnterBatteOnContact(Vector3 particleHitPosition)
    {
        sphereCollider.enabled = false;

        AudioManager.instance.PlaySoundEffect(AudioManager.instance.PunchAudio);

        Instantiate(hitParticle, new Vector3(particleHitPosition.x, particleHitPosition.y + 0.6f, particleHitPosition.z), Quaternion.identity);

        GameManager.instance.EnemyObject = fieldEnemyAnimator.Enemy;

        fieldEnemyAnimator.Enemy.LoadEnemiesForGameManager();
        fieldEnemyAnimator.Enemy.HasAttacked = false;
        fieldEnemyAnimator._EnemyTriggerZone.DisableTrigger();
        fieldEnemyAnimator._EnemyTriggerZone.FoundTargetParticle.Stop();
        fieldEnemyAnimator.GetComponent<BoxCollider>().enabled = false;

        MenuController.instance.CanToggleMenu = false;

        if (!MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.Hindsight))
        {
            GameManager.instance.IsAnAmbush = true;
        }

        GameManager.instance.EnterBattle();

        Destroy(gameObject);
    }
}