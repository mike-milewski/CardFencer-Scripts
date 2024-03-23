using UnityEngine;
using UnityEngine.SceneManagement;

public class BossChecker : MonoBehaviour
{
    [SerializeField]
    private BossData bossData;

    [SerializeField]
    private StageObjectives stageObjectives;

    [SerializeField]
    private GameObject bossObject, bossTriggerObject;

    private Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();

        switch(scene.name)
        {
            case "Forest_Boss":
                if(bossData.forestBossDefeated)
                {
                    stageObjectives._Objectives = Objectives.NONE;
                    bossObject.SetActive(false);
                    bossTriggerObject.SetActive(false);
                }
                break;
            case "Desert_Boss":
                if (bossData.desertBossDefeated)
                {
                    stageObjectives._Objectives = Objectives.NONE;
                    bossObject.SetActive(false);
                    bossTriggerObject.SetActive(false);
                }
                break;
            case "Arctic_Boss":
                if (bossData.arcticBossDefeated)
                {
                    stageObjectives._Objectives = Objectives.NONE;
                    bossObject.SetActive(false);
                    bossTriggerObject.SetActive(false);
                }
                break;
            case "Graveyard_Boss":
                if (bossData.graveBossDefeated)
                {
                    stageObjectives._Objectives = Objectives.NONE;
                    bossObject.SetActive(false);
                    bossTriggerObject.SetActive(false);
                }
                break;
        }
    }
}