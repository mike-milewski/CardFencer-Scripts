using UnityEngine;

public class MultiWispPuzzle : MonoBehaviour
{
    [SerializeField]
    private Gate gate;

    [SerializeField]
    private MultiWispPuzzle oppositePuzzle;

    private bool wasTriggered;

    public void TriggerPuzzle()
    {
        wasTriggered = true;

        CheckTriggers();
    }

    private void CheckTriggers()
    {
        if(wasTriggered && oppositePuzzle.wasTriggered)
        {
            gate.OpenGatesRoutine();
        }
    }
}