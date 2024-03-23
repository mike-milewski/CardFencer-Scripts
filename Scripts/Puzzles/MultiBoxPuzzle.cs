using UnityEngine;

public class MultiBoxPuzzle : MonoBehaviour
{
    [SerializeField]
    private MultiBoxPuzzle oppositeBoxPuzzle;

    [SerializeField]
    private InteractableObject interactableObject;

    [SerializeField]
    private BoxCollider boxCollider;

    [SerializeField]
    private Animator gateAnimator;

    [SerializeField]
    private GameObject blockadeToRemove, objectToSpawn;

    private bool isBoxPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            interactableObject.DisableRigidbody();

            boxCollider.enabled = false;

            isBoxPlaced = true;

            if(isBoxPlaced && oppositeBoxPuzzle.isBoxPlaced)
            {
                if(GetComponent<GatePuzzle>())
                {
                    GetComponent<GatePuzzle>().OpenGate();
                }
                else if(GetComponent<IceBlockade>())
                {
                    GetComponent<IceBlockade>().RemoveBlockades();
                }
                else if(objectToSpawn != null)
                {
                    blockadeToRemove.SetActive(false);
                    objectToSpawn.SetActive(true);
                }
                else
                {
                    gateAnimator.Play("OpenGate");
                }
            }
        }
    }
}