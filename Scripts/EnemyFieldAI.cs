using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldAI : MonoBehaviour
{
    [SerializeField]
    private List<EnemyStats> enemies = new List<EnemyStats>();

    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private FieldEnemyAnimator fieldEnemy;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private EnemyAttackTrigger enemyAttackTrigger;

    [SerializeField]
    private EnemyTriggerZone enemyTriggerZone;

    [SerializeField]
    private MoonstoneManager moonstoneManager = null;

    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private ParticleSystem deathParticle;

    [SerializeField]
    private Material alphaMaterial;

    [SerializeField]
    private GameObject targetSymbol = null, moonStoneSymbol = null, projectileObject = null;

    [SerializeField]
    private int moonStoneIndex, enemyCheckPointIndex;

    [SerializeField]
    private float moveSpeed, rotationSpeed, attackDistance, deathParticleOffsetY;

    [SerializeField]
    private bool canMove, hasMoonStone, isSecretObjective, isOptionalBoss, isDead;

    private float timeToPursuePlayer, timeToAttackPlayer;

    private bool moving, isIdle, hasAttacked, waitingToMove;

    private int wayPointIndex;

    public GameObject TargetSymbol => targetSymbol;

    public GameObject MoonStoneSymbol => moonStoneSymbol;

    public int EnemyCheckPointIndex => enemyCheckPointIndex;

    public int MoonStoneIndex => moonStoneIndex;

    public bool IsSecretObjective => isSecretObjective;

    public bool IsOptionalBoss => isOptionalBoss;

    public bool CanMove => canMove;

    public bool HasMoonStone
    {
        get
        {
            return hasMoonStone;
        }
        set
        {
            hasMoonStone = value;
        }
    }

    public float TimeToWaitToPursuePlayer
    {
        get
        {
            return timeToPursuePlayer;
        }
        set
        {
            timeToPursuePlayer = value;
        }
    }

    public float TimeToAttackPlayer
    {
        get
        {
            return timeToAttackPlayer;
        }
        set
        {
            timeToAttackPlayer = 0;
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
        }
    }

    public bool HasAttacked
    {
        get
        {
            return hasAttacked;
        }
        set
        {
            hasAttacked = value;
        }
    }

    public List<EnemyStats> Enemies
    {
        get
        {
            return enemies;
        }
        set
        {
            enemies = value;
        }
    }

    public FieldEnemyAnimator FieldEnemyAnimator => fieldEnemy;

    public Animator EnemyAnimator => enemyAnimator;

    public BoxCollider _BoxCollider
    {
        get
        {
            return boxCollider;
        }
        set
        {
            boxCollider = value;
        }
    }

    private Vector3 direction;

    private Quaternion lookDirection;

    private Coroutine enemyTriggerRoutine, enemyAttackRoutine, wayPointsRoutine;

    private void OnEnable()
    {
        if (isDead) return;

        if(wayPointsRoutine != null)
        {
            StopCoroutine(wayPointsRoutine);
            wayPointsRoutine = null;
        }
        waitingToMove = false;

        if (MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.SecretSeeker))
            ShowTargetSymbol();
    }

    private void Awake()
    {
        if (isDead) return;

        wayPointIndex = 0;

        if(this.enabled)
        {
            if (canMove)
            {
                enemyTriggerZone.enabled = true;
            }
            else
            {
                enemyTriggerZone.enabled = false;
            }
        }

        if(hasMoonStone)
        {
            ShowMoonStone();
        }
    }

    private void Update()
    {
        if(!isDead)
        {
            if(canMove)
            {
                if (!enemyTriggerZone.EnteredTrigger)
                {
                    if (moving)
                    {
                        isIdle = true;

                        direction = new Vector3(wayPoints[wayPointIndex].position.x - transform.position.x, 0, wayPoints[wayPointIndex].position.z - transform.position.z).normalized;

                        lookDirection = Quaternion.LookRotation(direction);

                        transform.position += direction * moveSpeed * Time.deltaTime;

                        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime).normalized;
                    }

                    float distanceToDestination = Vector3.Distance(transform.position, wayPoints[wayPointIndex].position);

                    if (distanceToDestination < 2)
                    {
                        moving = false;

                        if (isIdle)
                            enemyAnimator.Play("Idle");
                    }
                    else
                    {
                        enemyAnimator.Play("Move");
                        moving = true;
                    }
                }
                else
                {
                    if (!hasAttacked)
                    {
                        direction = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z).normalized;

                        lookDirection = Quaternion.LookRotation(direction);

                        timeToPursuePlayer += Time.deltaTime;

                        float distanceToDestination = Vector3.Distance(transform.position, player.position);

                        if (timeToPursuePlayer >= 2 && distanceToDestination >= attackDistance)
                        {
                            enemyAnimator.Play("Move");

                            isIdle = false;

                            moving = true;

                            transform.position += direction * moveSpeed * Time.deltaTime;
                        }
                        else
                        {
                            isIdle = true;

                            moving = false;

                            enemyAnimator.Play("Idle");
                        }

                        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime).normalized;

                        float distanceFromWayPoint = Vector3.Distance(transform.position, wayPoints[wayPointIndex].position);

                        if (distanceFromWayPoint > 15)
                        {
                            StartTriggerRoutine();
                        }

                        if (distanceToDestination < attackDistance)
                        {
                            timeToAttackPlayer += Time.deltaTime;

                            if (timeToAttackPlayer >= 1)
                            {
                                enemyAnimator.Play("AttackField");

                                fieldEnemy._BoxCollider.enabled = false;

                                hasAttacked = true;
                            }
                        }
                    }
                    else
                    {
                        direction = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z).normalized;

                        lookDirection = Quaternion.LookRotation(direction);

                        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime).normalized;
                    }
                }
            }
            else
            {
                enemyAnimator.Play("Idle");
            }
        }
    }

    public void ShowTargetSymbol()
    {
        if (!gameObject.activeSelf) return;

        if (!IsSecretObjective) return;

        if (isDead) return;

        targetSymbol.GetComponent<ParticleSystem>().Play();
    }

    public void HideTargetSymbol()
    {
        if (!gameObject.activeSelf) return;

        if (!IsSecretObjective) return;

        if (isDead) return;

        targetSymbol.GetComponent<ParticleSystem>().Stop();
    }

    private void ShowMoonStone()
    {
        moonStoneSymbol.SetActive(true);
    }

    public void StartTriggerRoutine()
    {
        enemyTriggerRoutine = null;

        enemyTriggerRoutine = StartCoroutine(EnableEnemyTriggerRoutine());
    }

    public void SpawnProjectileAttack()
    {
        var projectile = Instantiate(projectileObject, new Vector3(enemyAttackTrigger.transform.position.x, enemyAttackTrigger.transform.position.y, enemyAttackTrigger.transform.position.z), transform.rotation);

        projectile.SetActive(true);

        FieldProjectile fieldProjectile = projectile.GetComponent<FieldProjectile>();

        fieldProjectile._EnemyFieldAI = this;
        fieldProjectile._FieldEnemyAnimator = fieldEnemy;
    }

    public void EnableAlternativeAttack()
    {
        projectileObject.SetActive(true);

        projectileObject.GetComponent<ParticleSystem>().Play();
    }

    private IEnumerator EnableEnemyTriggerRoutine()
    {
        enemyTriggerZone.DisableTrigger();
        yield return new WaitForSeconds(2);
        enemyTriggerZone.EnableTrigger();
    }

    public void StartAttackTriggerRoutine()
    {
        enemyAttackRoutine = null;

        timeToAttackPlayer = 0;

        fieldEnemy._BoxCollider.enabled = true;

        enemyAttackRoutine = StartCoroutine("ResetAttackTrigger");
    }

    public void ResetAttackTriggerRoutine()
    {
        enemyAttackRoutine = null;

        hasAttacked = false;

        timeToAttackPlayer = 0;
    }

    private IEnumerator ResetAttackTrigger()
    {
        enemyAnimator.Play("Idle");
        yield return new WaitForSeconds(1f);
        hasAttacked = false;
    }

    public void EnableAttackCollider()
    {
        enemyAttackTrigger.EnableAttackCollider();
    }

    public void DisableAttackCollider()
    {
        enemyAttackTrigger.DisableAttackCollider();
    }

    public void SwitchToLookAnimation()
    {
        if(canMove)
        {
            if (!enemyTriggerZone.EnteredTrigger)
            {
                if (!moving)
                {
                    isIdle = false;
                    enemyAnimator.Play("Look");
                }
            }
        }
    }

    public void IncrementWayPointIndex()
    {
        if(!enemyTriggerZone.EnteredTrigger)
        {
            moving = true;

            wayPointIndex++;
            if (wayPointIndex >= wayPoints.Length)
            {
                wayPointIndex = 0;
            }
        }
    }

    public void StartWayPointCoroutine()
    {
        if(canMove)
        {
            if (!waitingToMove && !enemyTriggerZone.EnteredTrigger)
            {
                wayPointsRoutine = null;

                wayPointsRoutine = StartCoroutine("WaitToMoveToWayPoint");

                waitingToMove = true;
            }
            else if (enemyTriggerZone.EnteredTrigger)
            {
                if (wayPointsRoutine != null)
                {
                    StopCoroutine(wayPointsRoutine);

                    wayPointsRoutine = null;
                } 

                waitingToMove = false;
            }
        }
    }

    public void StopWayPointRoutine()
    {
        if (wayPointsRoutine != null)
        {
            StopCoroutine(wayPointsRoutine);

            wayPointsRoutine = null;
        }  

        waitingToMove = false;
    }

    private IEnumerator WaitToMoveToWayPoint()
    {
        yield return new WaitForSeconds(1.2f);
        IncrementWayPointIndex();
        waitingToMove = false;
    }

    public void FadeOutMaterial()
    {
        deathParticle.gameObject.SetActive(true);

        deathParticle.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + deathParticleOffsetY, transform.position.z);

        deathParticle.Play();

        AudioSource _audioSource = deathParticle.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            _audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            _audioSource.Play();
        }
        else
        {
            _audioSource.Play();
        }

        skinnedMeshRenderer.material = alphaMaterial;
        skinnedMeshRenderer.receiveShadows = false;
        skinnedMeshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        StartCoroutine("BeginFadeOut");
    }

    private IEnumerator BeginFadeOut()
    {
        float alpha = skinnedMeshRenderer.material.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;

            skinnedMeshRenderer.material.color = new Color(skinnedMeshRenderer.material.color.a, skinnedMeshRenderer.material.color.g, skinnedMeshRenderer.material.color.g, alpha);

            yield return new WaitForFixedUpdate();
        }
        alpha = 0;

        skinnedMeshRenderer.enabled = false;

        gameObject.SetActive(false);
    }

    public void SetMoonStoneData()
    {
        if(moonstoneManager != null)
        {
            if (NodeManager.instance.CurrentNodeIndex > -1)
            {
                moonstoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].stageMoonstones[moonstoneManager.StageIndex].moonStones[moonStoneIndex] = 1;
            }
            else
            {
                switch (NodeManager.instance.CurrentNodeIndex)
                {
                    case (-1):
                        moonstoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[0].moonStones[moonStoneIndex] = 1;
                        break;
                    case (-2):
                        moonstoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[1].moonStones[moonStoneIndex] = 1;
                        break;
                    case (-3):
                        moonstoneManager._MoonStoneData.worldMoonStones[NodeManager.instance.WorldIndex].secretStageMoonstones[2].moonStones[moonStoneIndex] = 1;
                        break;
                }
            }
        }
    }

    public void LoadEnemiesForGameManager()
    {
        foreach(EnemyStats enemyStats in enemies)
        {
            GameManager.instance.EnemiesToLoad.Add(enemyStats);
        }
    }

    public void PlayWalkAudio(AudioClip audioClip)
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1.0f;
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}