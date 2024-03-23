using UnityEngine;

public class AudioChecker : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public void CheckSettingsVolume()
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1;
        }

        audioSource.Play();
    }
}