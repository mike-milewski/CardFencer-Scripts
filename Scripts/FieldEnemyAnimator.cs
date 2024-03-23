using UnityEngine;

public class FieldEnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private EnemyFieldAI enemy;

    [SerializeField]
    private EnemyTriggerZone enemyTriggerZone;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private Animator animator;

    public EnemyFieldAI Enemy => enemy;

    public EnemyTriggerZone _EnemyTriggerZone => enemyTriggerZone;

    public BoxCollider _BoxCollider => boxCollider;

    public Animator _animator => animator;
}