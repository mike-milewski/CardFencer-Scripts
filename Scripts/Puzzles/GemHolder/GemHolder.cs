using UnityEngine;

public class GemHolder : MonoBehaviour
{
    [SerializeField]
    private int gemsHeld;

    public int GemsHeld => gemsHeld;

    public void IncrementGemCount(int value)
    {
        gemsHeld += value;
    }

    public void DecrementGemCount(int value)
    {
        gemsHeld -= value;
    }
}