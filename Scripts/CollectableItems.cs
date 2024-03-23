using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    [SerializeField]
    private StageObjectives stageObjective;

    [SerializeField]
    private bool isSecretObjective;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            GameManager.instance.CreateItemPickUpParticle(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z));

            if(!isSecretObjective)
            {
                stageObjective.CheckCollectObjectiveMain();
            }
            else
            {
                stageObjective.CheckCollectObjectiveSecret();
            }

            Destroy(gameObject);
        }
    }
}