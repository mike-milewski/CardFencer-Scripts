using UnityEngine;

public class MoveableObjectTriggerZone : MonoBehaviour
{
    [SerializeField]
    private Animator gateAnimator = null;

    [SerializeField]
    private bool disableObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InteractableObject>())
        {
            other.GetComponent<InteractableObject>().DisableRigidbody();

            if(gateAnimator != null)
            {
                gateAnimator.Play("Open");
            }

            if(disableObject)
            {
                GetComponent<BoxCollider>().enabled = false;
            } 
        }
    }
}