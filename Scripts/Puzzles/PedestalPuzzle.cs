using UnityEngine;

public class PedestalPuzzle : MonoBehaviour
{
    [SerializeField]
    private GemHolder gemHolder;

    [SerializeField]
    private PedestalPuzzle oppositePedestal;

    [SerializeField]
    private Animator gateAnimator;

    [SerializeField]
    private SphereCollider sphereCollider;

    [SerializeField]
    private GameObject gemObject;

    [SerializeField]
    private bool hasItemPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if(hasItemPlaced) return;

        if(gateAnimator.GetCurrentAnimatorStateInfo(0).IsName("OpenGate")) return;

        if (!other.GetComponent<PlayerMovement>()) return;

        if(gemHolder.GemsHeld > 0)
        {
            gemHolder.DecrementGemCount(1);

            gemObject.SetActive(true);

            hasItemPlaced = true;

            sphereCollider.enabled = false;

            GameManager.instance.CreateItemPickUpParticle(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
        }

        if(hasItemPlaced && oppositePedestal.hasItemPlaced)
        {
            gateAnimator.Play("OpenGate");

            CheckGateAudio();
        }
    }

    private void CheckGateAudio()
    {
        if (gateAnimator.GetComponent<AudioSource>())
        {
            if (PlayerPrefs.HasKey("SoundEffects"))
            {
                gateAnimator.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundEffects");
            }
            else
            {
                gateAnimator.GetComponent<AudioSource>().volume = 1.0f;
            }

            gateAnimator.GetComponent<AudioSource>().Play();
        }
    }
}