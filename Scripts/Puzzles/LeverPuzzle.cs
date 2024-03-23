using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    private Animator leverAnimator, gateToOpenAnimator;

    public void TriggerLever()
    {
        leverAnimator.Play("Lever");
    }

    public void TriggerGate()
    {
        gateToOpenAnimator.Play("OpenGate");
    }
}