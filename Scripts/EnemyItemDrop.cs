using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField]
    private EnemyStats enemyStats;

    public void DropItem()
    {
        int rand = Random.Range(0, 100);

        if (enemyStats.dropsCard)
        {
            if (enemyStats.cardDrop == null) return;

            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.TreasureMagnet) ? rand <= enemyStats.dropChance + 25 : rand <= enemyStats.dropChance)
            {
                SpoilsManager.instance.AddItem(enemyStats.dropsCard, enemyStats.cardDrop, enemyStats.stickerDrop);
            }
        }
        else
        {
            if (enemyStats.stickerDrop == null) return;

            if (MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.TreasureMagnet) ? rand <= enemyStats.dropChance + 25 : rand <= enemyStats.dropChance)
            {
                SpoilsManager.instance.AddItem(enemyStats.dropsCard, enemyStats.cardDrop, enemyStats.stickerDrop);
            }
        }
    }
}