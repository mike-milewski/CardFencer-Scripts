using UnityEngine;

public class EnemySkillParticle : MonoBehaviour
{
    [SerializeField]
    private GameObject particleObject;

    [SerializeField]
    private Transform enemyTransform;

    [SerializeField]
    private bool moveParticlePosition;

    private bool playedAudio;

    public void EnableParticleObject()
    {
        if(moveParticlePosition)
        {
            particleObject.transform.position = new Vector3(enemyTransform.position.x, particleObject.transform.position.y, enemyTransform.position.z);
        } 

        particleObject.SetActive(true);

        if (!playedAudio)
        {
            if (particleObject.GetComponent<CardParticleAudio>())
            {
                particleObject.GetComponent<CardParticleAudio>().DontPlayOnEnable = false;
                particleObject.GetComponent<CardParticleAudio>().CheckAudio();
            }

            playedAudio = true;
        }
    }
}