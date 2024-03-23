using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    private BattleEnemy battleEnemy;

    public BattleEnemy _BattleEnemy => battleEnemy;
}