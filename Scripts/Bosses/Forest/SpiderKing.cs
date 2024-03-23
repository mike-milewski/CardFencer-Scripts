
public class SpiderKing : BattleEnemy
{
    public void WalkRightAudio()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.SpiderKingRightLeg);
    }

    public void WalkLeftAudio()
    {
        AudioManager.instance.PlaySoundEffect(AudioManager.instance.SpiderKingLeftLeg);
    }
}