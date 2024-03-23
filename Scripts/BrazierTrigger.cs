using UnityEngine;

public class BrazierTrigger : MonoBehaviour
{
    [SerializeField]
    private BrazierTrigger oppositeBrazier;

    [SerializeField]
    private IceBlockade iceBlockade;

    [SerializeField]
    private GameObject flameObject, lightObject, sparkleObject;

    private bool hasTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if(other.GetComponent<PlayerMovement>())
        {
            TriggerObject();
        }
    }

    private void TriggerObject()
    {
        hasTriggered = true;

        sparkleObject.SetActive(false);
        flameObject.SetActive(true);
        lightObject.SetActive(true);

        CheckOppositeBrazier();
    }

    private void CheckOppositeBrazier()
    {
        if(hasTriggered && oppositeBrazier.hasTriggered)
        {
            iceBlockade.RemoveBlockades();
        }
    }
}