using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{
    [SerializeField]
    private InteractableObject interactableObject;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private GameObject objectToSpawn, blockadeToRemove;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InteractableObject>())
        {
            interactableObject.DisableRigidbody();

            if(GetComponent<GatePuzzle>())
            {
                GetComponent<GatePuzzle>().OpenGate();
            }
            else
            {
                if (blockadeToRemove.GetComponent<Gate>())
                {
                    blockadeToRemove.GetComponent<Gate>().OpenGatesRoutine();
                }
                else
                {
                    objectToSpawn.SetActive(true);
                    blockadeToRemove.SetActive(false);
                }
            }

            boxCollider.enabled = false;
        }
    }
}