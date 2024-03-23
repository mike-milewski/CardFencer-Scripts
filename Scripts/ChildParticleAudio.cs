using UnityEngine;

public class ChildParticleAudio : MonoBehaviour
{
    [SerializeField]
    private CardParticleAudio cardParticleAudio;

    public void CheckChildAudio()
    {
        if(!cardParticleAudio.CheckIfAudioIsPlaying())
        {
            cardParticleAudio.CheckAudio();
        }
    }
}