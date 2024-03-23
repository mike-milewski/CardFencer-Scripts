using UnityEngine;

public class PedestalGem : MonoBehaviour
{
    [SerializeField]
    private GemHolder gemHolder;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            gemHolder.IncrementGemCount(1);

            GameManager.instance.CreateItemPickUpParticle(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z));

            Destroy(gameObject);
        }
    }
}