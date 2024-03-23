using UnityEngine;

public class RankBonusAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private RankBonusBattleInformation[] rankBonusInformation;

    public void PlayBonusParticle()
    {
        for(int i = 0; i < rankBonusInformation.Length; i++)
        {
            rankBonusInformation[i].PlayParticle();
        }
    }
}