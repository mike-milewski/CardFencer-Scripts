using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapController : MonoBehaviour
{
    [SerializeField]
    private GameObject worldOneObject, worldTwoObject, worldThreeObject, worldFourObject, worldFiveObject;

    public void CheckCurrentWorld()
    {
        Scene scene = SceneManager.GetActiveScene();

        switch(scene.name)
        {
            case ("ForestField"):
                worldOneObject.SetActive(false);
                break;
            case ("DesertField"):
                worldTwoObject.SetActive(false);
                break;
            case ("ArcticField"):
                worldThreeObject.SetActive(false);
                break;
            case ("GraveyardField"):
                worldFourObject.SetActive(false);
                break;
            case ("CastleField"):
                worldFiveObject.SetActive(false);
                break;
        }
    }
}