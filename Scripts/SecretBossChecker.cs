using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretBossChecker : MonoBehaviour
{
    [SerializeField]
    private BossData bossData;

    [SerializeField]
    private GameObject bossObject;

    private bool bossDefeated;

    public bool BossDefeated => bossDefeated;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name)
        {
            case "Secret_Wood_3":
                if(bossData.forestSecretBossDefeated)
                {
                    bossObject.SetActive(false);
                    bossDefeated = true;
                }
                break;
            case "Secret_Desert_2":
                if (bossData.desertSecretBossDefeated)
                {
                    bossObject.SetActive(false);
                    bossDefeated = true;
                }
                break;
            case "Secret_Arctic_3":
                if (bossData.arcticSecretBossDefeated)
                {
                    bossObject.SetActive(false);
                    bossDefeated = true;
                }
                break;
            case "Secret_Graveyard_3":
                if (bossData.graveSecretBossDefeated)
                {
                    bossObject.SetActive(false);
                    bossDefeated = true;
                }
                break;
            case "Secret_Castle_3":
                if (bossData.castleSecretBossDefeated)
                {
                    bossObject.SetActive(false);
                    bossDefeated = true;
                }
                break;
        }
    }
}