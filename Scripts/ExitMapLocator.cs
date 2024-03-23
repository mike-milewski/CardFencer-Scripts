using UnityEngine;

public class ExitMapLocator : MonoBehaviour
{
    [SerializeField]
    private Transform exitTransform;

    private void FixedUpdate()
    {
        var lookPos = exitTransform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);
    }
}