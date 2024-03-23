using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip rightFoot, leftFoot;

    public void RightFoot()
    {
        PlayAudioClip(rightFoot);
    }

    public void LeftFoot()
    {
        PlayAudioClip(leftFoot);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (AudioManager.instance.SoundEffects.volume > 0)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }  
    }
}