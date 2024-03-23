using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum ButtonDirection { UP, DOWN, LEFT, RIGHT };

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private ButtonDirection buttonDirection;

    [SerializeField]
    private Selectable selectable;

    [SerializeField]
    private EventSystem eventSystem;

    private void Update()
    {
        if (InputManager.instance.ControllerPluggedIn)
        {
            if(Input.GetAxisRaw("Ps4Vertical") == -1)
            {
                if (eventSystem.currentSelectedGameObject == selectable.gameObject)
                {
                    if (selectable.navigation.mode == Navigation.Mode.Explicit)
                    {
                        switch (buttonDirection)
                        {
                            case (ButtonDirection.UP):
                                eventSystem.SetSelectedGameObject(selectable.navigation.selectOnUp.gameObject);
                                InputManager.instance.CurrentSelectedObject = eventSystem.currentSelectedGameObject;
                                break;
                            case (ButtonDirection.DOWN):
                                eventSystem.SetSelectedGameObject(selectable.navigation.selectOnDown.gameObject);
                                InputManager.instance.CurrentSelectedObject = eventSystem.currentSelectedGameObject;
                                break;
                            case (ButtonDirection.LEFT):
                                eventSystem.SetSelectedGameObject(selectable.navigation.selectOnLeft.gameObject);
                                InputManager.instance.CurrentSelectedObject = eventSystem.currentSelectedGameObject;
                                break;
                            case (ButtonDirection.RIGHT):
                                eventSystem.SetSelectedGameObject(selectable.navigation.selectOnRight.gameObject);
                                InputManager.instance.CurrentSelectedObject = eventSystem.currentSelectedGameObject;
                                break;
                        }
                    }
                }
            }
        }
        else return;
    }
}