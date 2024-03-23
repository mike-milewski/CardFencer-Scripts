using UnityEngine;

public class SwordEnemyCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEnemyParticlePrefab;

    [SerializeField]
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FieldEnemyAnimator>())
        {
            var particle = Instantiate(hitEnemyParticlePrefab);

            particle.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 0.5f, other.transform.position.z);

            PlaySound();

            GameManager.instance.EnemyObject = other.GetComponent<FieldEnemyAnimator>().Enemy;

            GameManager.instance.AttackParticle = particle.gameObject;

            other.GetComponent<FieldEnemyAnimator>().Enemy.LoadEnemiesForGameManager();

            other.GetComponent<BoxCollider>().enabled = false;

            MenuController.instance.CanToggleMenu = false;

            GameManager.instance.IsAPreemptiveStrike = true;

            GetComponent<BoxCollider>().enabled = false;

            GameManager.instance.EnterBattle();
        }
    }

    private void PlaySound()
    {
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