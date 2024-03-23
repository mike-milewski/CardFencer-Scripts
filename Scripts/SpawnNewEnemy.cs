using System.Collections;
using UnityEngine;

public class SpawnNewEnemy : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private BattleEnemy enemyToSpawn;

    [SerializeField]
    private GameObject spawnParticleObject;

    public void SpawnEnemy()
    {
        battleSystem.AddEnemyToBattle(enemyToSpawn);

        spawnParticleObject.SetActive(true);

        spawnParticleObject.transform.position = new Vector3(enemyToSpawn.transform.position.x, spawnParticleObject.transform.position.y, enemyToSpawn.transform.position.z);

        StartCoroutine(WaitToStartEnemyTurn());
    }

    private IEnumerator WaitToStartEnemyTurn()
    {
        yield return new WaitForSeconds(2.5f);

        enemyToSpawn.StartEnemyTurn();

        gameObject.SetActive(false);
    }
}