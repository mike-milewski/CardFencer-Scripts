using UnityEngine;

public class PerfectBlock : MonoBehaviour
{
    [SerializeField]
    private BoxCollider boxCollider;

    public BoxCollider _BoxCollider => boxCollider;
}