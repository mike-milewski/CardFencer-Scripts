using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 offSet;

    private Vector3 newPosition;

    private Quaternion cameraTurn;

    [SerializeField]
    private float smoothFactor;

    [SerializeField]
    private bool isMap;

    private void Update()
    {
        RotateCamera();

        if(isMap)
        {
            newPosition = player.position + offSet;

            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

            transform.LookAt(player);
        }
        else
        {
            newPosition = player.position + offSet;

            transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

            transform.LookAt(player);
        }
    }

    private void RotateCamera()
    {
        if(!GameManager.instance.IsTutorial)
        {
            if (SteamOverlayPause.instance.IsPaused) return;

            float cameraSensitivity = MenuController.instance._SettingsMenu.CameraSensitivitySlider.value > 0 ? MenuController.instance._SettingsMenu.CameraSensitivitySlider.value * 10 : 1;

            if(playerMovement.HasControllerInput)
            {
                if(InputManager.instance.ControllerName == "xbox")
                {
                    cameraTurn = Quaternion.AngleAxis(Input.GetAxis("XboxAnalog X") * cameraSensitivity, Vector3.up);
                }
                else
                {
                    cameraTurn = Quaternion.AngleAxis(Input.GetAxis("Ps4Analog X") * cameraSensitivity, Vector3.up);
                }
            }
            else
            {
                cameraTurn = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cameraSensitivity, Vector3.up);
            }

            offSet = cameraTurn * offSet;
        }
    }
}