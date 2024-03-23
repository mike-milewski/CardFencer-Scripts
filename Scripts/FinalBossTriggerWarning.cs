using UnityEngine;

public class FinalBossTriggerWarning : MonoBehaviour
{
    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private PlayerFieldSwordAndShield playerField;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private GameObject verticalLayoutObject;

    [SerializeField]
    private Animator finalBossWarningAnimator, confirmAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            playerField.enabled = false;

            AudioManager.instance.PlaySoundEffect(AudioManager.instance.OpenMenuAudio);

            other.GetComponent<PlayerFieldSwordAndShield>().ResetEquipment();

            other.GetComponent<PlayerMovement>().PlayerAnimator.Play("IdlePose");

            other.GetComponent<PlayerMovement>().enabled = false;

            InputManager.instance.ForceCursorOn = true;

            MenuController.instance.CanToggleMenu = false;

            InputManager.instance.FirstSelectedObject = confirmAnimator.gameObject;
            InputManager.instance.SetSelectedObject(null);

            cameraFollow.enabled = false;

            verticalLayoutObject.SetActive(true);

            finalBossWarningAnimator.Play("Open");

            confirmAnimator.Play("Normal");
        }
    }

    public void ConfirmWarning()
    {
        cameraFollow.enabled = true;

        playerMovement.enabled = true;

        playerField.enabled = true;

        InputManager.instance.SetSelectedObject(null);

        gameObject.SetActive(false);
    }
}