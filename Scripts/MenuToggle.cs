using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public void SetCanToggle()
    {
        MenuController.instance.ToggleMenuOpenedAnimationEvent();
    }

    public void SetCanToggleAnimationEvent()
    {
        MenuController.instance.SetOpenedMenuAnimationEvent();
    }

    public void SetCanOpenMenu()
    {
        MenuController.instance.SetCanOpen();
    }
}