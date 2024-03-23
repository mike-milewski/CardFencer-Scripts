using UnityEngine;

public class MultiLeverPuzzle : MonoBehaviour
{
    [SerializeField]
    private Animator leverAnimator, gateToOpenAnimator;

    [SerializeField]
    private MultiLeverPuzzle[] leversToTrigger;

    [SerializeField]
    private int amountOfLeversToTrigger;

    private int triggeredLeversIndex;

    public void PlayAnimation()
    {
        if (leverAnimator.GetComponent<GatePuzzle>())
        {
            leverAnimator.Play("DungeonLever");
        }
        else
        {
            leverAnimator.Play("Lever");
        }
    }

    public void TriggerGate()
    {
        triggeredLeversIndex++;

        for (int i = 0; i < leversToTrigger.Length; i++)
        {
            leversToTrigger[i].triggeredLeversIndex++;
        }

        if (triggeredLeversIndex >= amountOfLeversToTrigger)
        {
            if(leverAnimator.GetComponent<GatePuzzle>())
            {
                leverAnimator.GetComponent<GatePuzzle>().OpenGate();
            }
            else if(leverAnimator.GetComponent<IceBlockade>())
            {
                leverAnimator.GetComponent<IceBlockade>().RemoveBlockades();
            }
            else
            {
                gateToOpenAnimator.Play("OpenGate");
            }
        }
    }
}