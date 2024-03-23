using UnityEngine;
using TMPro;

public class SaveGameTextAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI saveGameText;

    public void SetSaveGameTextEvent()
    {
        saveGameText.text = "Save Game";
    }

    public void SetSavedGameTextEvent()
    {
        saveGameText.text = "Saved!";
    }
}