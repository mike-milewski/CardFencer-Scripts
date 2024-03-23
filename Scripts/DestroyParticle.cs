using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public void SetDestroyTime(float t)
    {
        Destroy(gameObject, t);
    }
}