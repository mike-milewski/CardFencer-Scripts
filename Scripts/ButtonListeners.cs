using UnityEngine;
using UnityEngine.UI;

public class ButtonListeners : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip selectedAudioClip = null;

    private void Start()
    {
        AttachListener();
    }

    private void PlayAudio()
    {
        if (selectedAudioClip != null)
        {
            audioSource.clip = selectedAudioClip;

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
    }

    public void PlayAudioAnimationEvent(AudioClip clip)
    {
        if (button.interactable)
        {
            audioSource.clip = clip;

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
    }

    public void AttachListener()
    {
        button.onClick.AddListener(PlayAudio);
    }
}