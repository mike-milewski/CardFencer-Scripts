using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BattleTerrain : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume;

    [SerializeField]
    private GameObject forestTerrain, desertTerrain, arcticTerrain, graveyardTerrain, castleTerrain, rainParticle, snowParticle, fogParticle, forestFireFlies;

    [SerializeField]
    private Light directionalLight;

    [Header("Post Process Profiles")]
    [SerializeField]
    private PostProcessProfile forestProfile;
    [SerializeField]
    private PostProcessProfile desertProfile;
    [SerializeField]
    private PostProcessProfile arcticProfile;
    [SerializeField]
    private PostProcessProfile graveyardProfile;
    [SerializeField]
    private PostProcessProfile castleProfile;

    public GameObject ForestTerrain => forestTerrain;

    public GameObject DesertTerrain => desertTerrain;

    public GameObject ArcticTerrain => arcticTerrain;

    public GameObject GraveyardTerrain => graveyardTerrain;

    public GameObject CastleTerrain => castleTerrain;

    private void Awake()
    {
        string sceneName = GameManager.instance.CurrentScene;

        if(sceneName.Contains("Forest") || sceneName.Contains("Woods"))
        {
            forestTerrain.SetActive(true);

            postProcessVolume.profile = forestProfile;
        }
        else if(sceneName.Contains("Desert"))
        {
            forestTerrain.SetActive(false);
            desertTerrain.SetActive(true);

            if(!GameManager.instance._WorldEnvironmentData.changedDay)
            {
                postProcessVolume.profile = desertProfile;
            }
            else
            {
                postProcessVolume.profile = forestProfile;
            }
        }
        else if(sceneName.Contains("Arctic"))
        {
            forestTerrain.SetActive(false);
            arcticTerrain.SetActive(true);

            postProcessVolume.profile = arcticProfile;
        }
        else if(sceneName.Contains("Graveyard"))
        {
            forestTerrain.SetActive(false);
            graveyardTerrain.SetActive(true);

            postProcessVolume.profile = graveyardProfile;
        }
        else if(sceneName.Contains("Castle"))
        {
            forestTerrain.SetActive(false);
            castleTerrain.SetActive(true);

            postProcessVolume.profile = castleProfile;
        }

        if(GameManager.instance._WorldEnvironmentData.changedDay)
        {
            directionalLight.intensity = 0.4f;

            if(sceneName.Contains("Forest") || sceneName.Contains("Woods"))
            {
                if(!GameManager.instance._WorldEnvironmentData.changedWeather)
                {
                    forestFireFlies.SetActive(true);
                }
            }
        }

        if (GameManager.instance._WorldEnvironmentData.changedWeather)
        {
            if(sceneName.Contains("Forest") || sceneName.Contains("Woods"))
            {
                rainParticle.SetActive(true);
            }
            else if(sceneName.Contains("Arctic"))
            {
                snowParticle.SetActive(true);
            }
            else if(sceneName.Contains("Graveyard"))
            {
                fogParticle.SetActive(true);
            }
        }
    }
}