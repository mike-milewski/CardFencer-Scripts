using UnityEngine;
using UnityEngine.SceneManagement;

public class HealingObjectsSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject healingFieldItemPrefab;

    [SerializeField]
    private int healingItemsToSpawn;

    [SerializeField]
    private float healSpawnOffset;

    public void SpawnHealingItems()
    {
        if(MenuController.instance._StickerPowerManager.CheckFieldStickerPower(StickerPower.ResourcefulSpoils))
        {
            StickerPowerHolder.instance.CreateStickerMessage(StickerPower.ResourcefulSpoils);
            for (int i = 0; i < healingItemsToSpawn + 1; i++)
            {
                var healing = Instantiate(healingFieldItemPrefab, new Vector3(transform.position.x, transform.position.y + healSpawnOffset, transform.position.z), Quaternion.identity);

                if (GameManager.instance._WorldEnvironmentData.changedDay)
                {
                    healing.GetComponent<HealingFieldItem>().EnableLights();
                }
            }
        }
        else
        {
            for (int i = 0; i < healingItemsToSpawn; i++)
            {
                var healing = Instantiate(healingFieldItemPrefab, new Vector3(transform.position.x, transform.position.y + healSpawnOffset, transform.position.z), Quaternion.identity);

                if(GameManager.instance._WorldEnvironmentData.changedDay)
                {
                    healing.GetComponent<HealingFieldItem>().EnableLights();
                }
            }
        }
    }
}