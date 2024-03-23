using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidBody;

    private Vector3 center;

    private void Awake()
    {
        rigidBody.centerOfMass = center;
    }
}