using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/BossData", order = 1)]
public class BossData : ScriptableObject
{
    public bool forestBossDefeated, desertBossDefeated, arcticBossDefeated, graveBossDefeated, forestSecretBossDefeated, 
                desertSecretBossDefeated, arcticSecretBossDefeated, graveSecretBossDefeated, castleSecretBossDefeated;
}