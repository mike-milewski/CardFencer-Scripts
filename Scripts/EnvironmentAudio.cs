using UnityEngine;

public class EnvironmentAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void Update()
    {
        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
        }
        else
        {
            audioSource.volume = 1;
        }
    }
}