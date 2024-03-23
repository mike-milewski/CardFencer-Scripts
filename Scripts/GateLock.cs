using UnityEngine;

public class GateLock : MonoBehaviour
{
    [SerializeField]
    private GemHolder gemHolder;

    [SerializeField]
    private Gate gate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            if (gemHolder.GemsHeld <= 0) return;

            gemHolder.DecrementGemCount(1);

            OpenGate();

            gameObject.SetActive(false);
        }
    }

    private void OpenGate()
    {
        gate.OpenGatesRoutine();
    }
}