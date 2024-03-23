using UnityEngine;

public class PuzzleReset : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InteractableObject>())
        {
            var particle = Instantiate(particlePrefab, new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z), Quaternion.Euler(-90, 0, 0));

            if (PlayerPrefs.HasKey("SoundEffects"))
            {
                particle.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
                particle.GetComponent<AudioSource>().Play();
            }
            else
            {
                particle.GetComponent<AudioSource>().volume = 1;
                particle.GetComponent<AudioSource>().Play();
            }

            particle.AddComponent<DestroyParticle>();
            particle.GetComponent<DestroyParticle>().SetDestroyTime(2f);

            other.transform.position = other.GetComponent<InteractableObject>().DefaultPosition;
        }
    }
}