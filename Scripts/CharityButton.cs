using UnityEngine;

public class CharityButton : MonoBehaviour
{
    [SerializeField]
    private AudioChecker audioChecker;

    [SerializeField]
    private AudioSource audioSource;

    public void CheckController()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            audioSource.clip = AudioManager.instance.CursorAudio;

            audioChecker.CheckSettingsVolume();
        }
    }
}