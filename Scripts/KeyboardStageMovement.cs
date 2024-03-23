using UnityEngine;

[System.Serializable]
public class StageKeys
{
    [SerializeField]
    private KeyCode keyCode;

    [SerializeField]
    private StageInformation stageInformation;

    public KeyCode KeyCode => keyCode;

    public StageInformation _StageInformation => stageInformation;
}

public class KeyboardStageMovement : MonoBehaviour
{
    [SerializeField]
    private StageKeys[] stageKeys;

    [SerializeField]
    private StageInformation currentStage;

    [SerializeField]
    private WorldMapMovement worldMapMovement;

    [SerializeField]
    private InputManager inputManager;

    private KeyCode key;

    private void Update()
    {
        if (SteamOverlayPause.instance.IsPaused) return;

        if (inputManager.ControllerPluggedIn) return;

        if (worldMapMovement.Moving) return;

        if (worldMapMovement.OpenedEnterAreaPanel) return;

        if (worldMapMovement.UIBlocker.activeSelf) return;

        if (MenuController.instance.OpenedMenu) return;

        for (int i = 0; i < stageKeys.Length; i++)
        {
            if (!stageKeys[i]._StageInformation.Unlocked) return;

            MoveToStage(key, stageKeys[i]._StageInformation, stageKeys, i);
        }
    }

    private void MoveToStage(KeyCode pressedKey, StageInformation stageToMoveTo, StageKeys[] stageKey, int stageKeyIndex)
    {
        if (!currentStage.IsOnSameNode()) return;

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            key = KeyCode.RightArrow;
            
            if(key == stageKey[stageKeyIndex].KeyCode)
               stageToMoveTo.ButtonNodeController(false);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            key = KeyCode.LeftArrow;

            if (key == stageKey[stageKeyIndex].KeyCode)
                stageToMoveTo.ButtonNodeController(false);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            key = KeyCode.UpArrow;

            if (key == stageKey[stageKeyIndex].KeyCode)
                stageToMoveTo.ButtonNodeController(false);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            key = KeyCode.DownArrow;

            if (key == stageKey[stageKeyIndex].KeyCode)
                stageToMoveTo.ButtonNodeController(false);
        }
    }
}