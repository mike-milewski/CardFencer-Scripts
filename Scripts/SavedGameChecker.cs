using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavedGameChecker : MonoBehaviour
{
    [SerializeField]
    private Selectable newGameSelectable, exitGameSelectable, loadGameSelectable;

    [SerializeField]
    private Button loadGameButton;

    [SerializeField]
    private Image loadGameImage;

    [SerializeField]
    private TextMeshProUGUI loadGameText;

    [SerializeField]
    private SceneNameToLoad sceneNameToLoad;

    private void Awake()
    {
        if (SaveSystem.CheckSaveFile())
        {
            loadGameButton.interactable = true;
            loadGameButton.GetComponent<Image>().raycastTarget = true;

            Navigation newGameNav = newGameSelectable.navigation;
            Navigation exitGameNav = exitGameSelectable.navigation;

            newGameNav.selectOnRight = loadGameSelectable;
            newGameNav.selectOnLeft = loadGameSelectable;

            exitGameNav.selectOnUp = loadGameSelectable;
            exitGameNav.selectOnDown = loadGameSelectable;

            newGameSelectable.navigation = newGameNav;
            exitGameSelectable.navigation = exitGameNav;

            loadGameImage.color = new Color(1, 1, 1, 1);
            loadGameText.color = new Color(1, 1, 1, 1);
        }
        else
        {
            loadGameImage.color = new Color(1, 1, 1, 0.7f);
            loadGameText.color = new Color(1, 1, 1, 0.7f);
        }
    }

    public void SetSceneToLoad()
    {
        MenuController.instance._GameHandler.Load();

        switch (NodeManager.instance.SceneName)
        {
            case "ForestField":
                sceneNameToLoad.SceneToLoad = "ForestField";
                break;
            case "DesertField":
                sceneNameToLoad.SceneToLoad = "DesertField";
                break;
            case "ArcticField":
                sceneNameToLoad.SceneToLoad = "ArcticField";
                break;
            case "GraveyardField":
                sceneNameToLoad.SceneToLoad = "GraveyardField";
                break;
            case "CastleField":
                sceneNameToLoad.SceneToLoad = "CastleField";
                break;
        }

        sceneNameToLoad.FadeOutScene();
    }

    public void ReEnableMenuController()
    {
        if(!MenuController.instance.gameObject.activeSelf)
        {
            MenuController.instance.gameObject.SetActive(true);
        }
    }
}