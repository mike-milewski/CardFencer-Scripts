using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSelect, objectToSelectOnClose;

    [SerializeField]
    private bool ignoreEvents, doesntResetObject;

    private bool canToggle, canSetObjectOnClose;

    private Scene scene;

    public GameObject ObjectToSelectOnClose
    {
        get => objectToSelect;
        set => objectToSelectOnClose = value;
    }

    public void MenuToggle()
    {
        if(!ignoreEvents)
        {
            if (canToggle)
            {
                if (MenuController.instance != null)
                {
                    if(!MenuController.instance.OpenedMenu)
                        MenuController.instance.CanToggleMenu = true;
                }   

                canToggle = false;
            }
        }
    }

    public void SetCanToggle()
    {
        SetObjectForInputManager();

        if(!ignoreEvents)
            canToggle = true;
    }

    public void DestoryObject()
    {
        Destroy(transform.parent.gameObject);
    }

    public void SetObjectForInputManager()
    {
        if(!canSetObjectOnClose)
        {
            scene = SceneManager.GetActiveScene();

            if(objectToSelect != null)
            {
                if(scene.name.Contains("Battle"))
                {
                    BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                    battleSystem._InputManager.SetSelectedObject(objectToSelect);
                }
                else
                {
                    InputManager.instance.SetSelectedObject(objectToSelect);
                }
            }

            canSetObjectOnClose = true;
        }
    }

    public void SetObjectOnClose()
    {
        if(canSetObjectOnClose)
        {
            scene = SceneManager.GetActiveScene();

            if(objectToSelectOnClose != null)
            {
                if(scene.name.Contains("Battle"))
                {
                    BattleSystem battleSystem = FindObjectOfType<BattleSystem>();

                    battleSystem._InputManager.SetSelectedObject(objectToSelectOnClose);
                }
                else
                {
                    InputManager.instance.SetSelectedObject(objectToSelectOnClose);
                }
            }
            else
            {
                if(!doesntResetObject)
                   InputManager.instance.SetSelectedObject(null);
            }

            canSetObjectOnClose = false;
        } 
    }

    public void ResetSelectedObject()
    {
        InputManager.instance.SetSelectedObject(null);
    }
}