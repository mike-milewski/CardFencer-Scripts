using UnityEngine;

public class BossEventReset : MonoBehaviour
{
    [SerializeField]
    private Animator bossAnimator;

    [SerializeField]
    private PlayerUserInterfaceController interfaceConttroller;

    [SerializeField]
    private CameraFollow cameraFollow;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private PlayerFieldSwordAndShield playerFieldSwordAndShield;

    [SerializeField]
    private BoxCollider bossEventTrigger;

    public void ResetBossEvent()
    {
        bossAnimator.Play("Idle");
        bossEventTrigger.enabled = true;
        playerFieldSwordAndShield.enabled = true;
        interfaceConttroller.enabled = true;
        playerMovement.enabled = true;
        cameraFollow.enabled = true;

        GameManager.instance.EnemiesToLoad.Clear();

        MenuController.instance.CanToggleMenu = true;
    }
}