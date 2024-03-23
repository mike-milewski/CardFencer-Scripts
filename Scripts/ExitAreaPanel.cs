using UnityEngine;

public class ExitAreaPanel : MonoBehaviour
{
    private ExitArea exitArea = null;

    [SerializeField]
    private bool exitAreaPanelOpen;

    public ExitArea _ExitArea
    {
        get
        {
            return exitArea;
        }
        set
        {
            exitArea = value;
        }
    }

    public bool ExitAreaPanelOpen
    {
        get
        {
            return exitAreaPanelOpen;
        }
        set
        {
            exitAreaPanelOpen = value;
        }
    }

    public void ExitTheArea()
    {
        if(exitArea != null)
        {
            exitArea.CheckIfExitCompletesStage();
        }
    }

    public void MoveAwayFromCollider()
    {
        if(exitArea != null)
        {
            exitArea.MovePlayerAwayFromCollider();
        }

        exitAreaPanelOpen = false;

        MenuController.instance.CanToggleMenu = true;
    }
}