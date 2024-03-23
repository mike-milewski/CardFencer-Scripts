using UnityEngine;

public class MenuButtonAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool isInMenu;

    public void PlayAudioWithController()
    {
        if(InputManager.instance.ControllerPluggedIn)
        {
            if(isInMenu)
            {
                audioSource.clip = AudioManager.instance.CursorAudio;

                if (MenuController.instance.OpenedMenu)
                {
                    if (PlayerPrefs.HasKey("SoundEffects"))
                    {
                        audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
                    }
                    else
                    {
                        audioSource.volume = 1.0f;
                    }
                    
                    audioSource.Play();
                }
            }
        }
    }
}