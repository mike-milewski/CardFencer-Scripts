using UnityEngine;

public class SetObjectForSubMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSet, mainMenuObjectToSet;

    [SerializeField]
    private bool isDeckMenu, isStickerMenu, isRankMenu, isBestiaryMenu, isSettingsMenu, ignoreEvents;

    private bool canSetObject;

    public void SetObject()
    {
        if (!InputManager.instance.ControllerPluggedIn) return;

        if(!ignoreEvents)
        {
            if (!canSetObject)
            {
                if (isDeckMenu)
                {
                    DeckMenu deckMenu = GetComponent<DeckMenu>();

                    if (PlayerPrefs.HasKey("CardTutorial"))
                    {
                        InputManager.instance.SetSelectedObject(deckMenu.DeckParent.GetChild(0).gameObject);
                    }
                }
                if (isStickerMenu)
                {
                    StickerMenu stickerMenu = GetComponent<StickerMenu>();

                    if (PlayerPrefs.HasKey("StickerTutorial"))
                    {
                        InputManager.instance.SetSelectedObject(stickerMenu.StickerParent.childCount <= 0 ? objectToSet : stickerMenu.StickerParent.GetChild(0).gameObject);
                    }
                }
                if (isRankMenu)
                {
                    InputManager.instance.SetSelectedObject(objectToSet);
                }
                if (isBestiaryMenu)
                {
                    InputManager.instance.SetSelectedObject(objectToSet);
                }
                if (isSettingsMenu)
                {
                    InputManager.instance.SetSelectedObject(objectToSet);
                }

                canSetObject = true;
            }
        }
    }

    public void SetMainMenuObject()
    {
        if(!ignoreEvents)
        {
            if (canSetObject)
            {
                InputManager.instance.SetSelectedObject(mainMenuObjectToSet);

                canSetObject = false;
            }
        }
    }
}