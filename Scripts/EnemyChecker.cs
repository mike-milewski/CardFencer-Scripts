using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private BattleTerrain battleTerrain;

    [SerializeField]
    private Transform forestEnemies, desertEnemies, arcticEnemies, graveyardEnemies, castleEnemies;

    private Transform enemyParent;

    [SerializeField]
    private int enemiesInBattle;

    private void CheckActiveTerrain()
    {
        if(battleTerrain.ForestTerrain.activeSelf)
        {
            enemyParent = forestEnemies;
        }
        else if(battleTerrain.DesertTerrain.activeSelf)
        {
            enemyParent = desertEnemies;
        }
        else if(battleTerrain.ArcticTerrain.activeSelf)
        {
            enemyParent = arcticEnemies;
        }
        else if(battleTerrain.GraveyardTerrain.activeSelf)
        {
            enemyParent = graveyardEnemies;
        }
        else
        {
            enemyParent = castleEnemies;
        }
    }

    public void EnableEnemies()
    {
        CheckActiveTerrain();

        foreach(BattleEnemy battleEnemy in enemyParent.GetComponentsInChildren<BattleEnemy>(true))
        {
            for(int i = 0; i < GameManager.instance.EnemiesToLoad.Count; i++)
            {
                if (battleEnemy._EnemyStats == GameManager.instance.EnemiesToLoad[i] && battleEnemy.gameObject.activeSelf)
                {
                    Instantiate(battleEnemy, enemyParent);

                    battleSystem.Enemies.Remove(battleEnemy);
                    battleSystem.Enemies.Insert(0, battleEnemy);
                }

                if (battleEnemy._EnemyStats == GameManager.instance.EnemiesToLoad[i])
                {
                    battleEnemy.gameObject.SetActive(true);
                }
            }
        }
    }
}