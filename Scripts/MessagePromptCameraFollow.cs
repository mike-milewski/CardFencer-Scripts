using UnityEngine;

public class MessagePromptCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, cam.transform.rotation, 5 * Time.deltaTime).normalized;
    }
}