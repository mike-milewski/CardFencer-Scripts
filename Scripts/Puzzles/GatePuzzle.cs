using UnityEngine;

public class GatePuzzle : MonoBehaviour
{
    [SerializeField]
    private Gate gate;

    public void OpenGate()
    {
        gate.OpenGatesRoutine();
    }

    public void CheckGateTrigger()
    {
        if(GetComponent<MultiLeverPuzzle>())
        {
            GetComponent<MultiLeverPuzzle>().TriggerGate();
        }
    }
}