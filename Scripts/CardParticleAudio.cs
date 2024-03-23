using UnityEngine;

public class CardParticleAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool dontPlayOnEnable;

    public bool DontPlayOnEnable
    {
        get => dontPlayOnEnable;
        set => dontPlayOnEnable = value;
    }

    private void OnEnable()
    {
        if(!dontPlayOnEnable)
            CheckAudio();
    }

    public void CheckAudio()
    {
        if (!audioSource.gameObject.activeInHierarchy) return;

        if (PlayerPrefs.HasKey("SoundEffects"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("SoundEffects");
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
        }
    }

    public bool CheckIfAudioIsPlaying()
    {
        return audioSource.isPlaying;
    }
}