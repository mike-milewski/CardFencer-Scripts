using UnityEngine;

public class FleeButton : MonoBehaviour
{
    [SerializeField]
    private AudioChecker audioChecker;

    [SerializeField]
    private AudioSource audioSource;

    public void ConrollerSelect()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            audioSource.clip = AudioManager.instance.CursorAudio;

            audioChecker.CheckSettingsVolume();
        }
    }
}