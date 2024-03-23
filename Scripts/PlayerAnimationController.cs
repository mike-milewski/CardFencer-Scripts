using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private RuntimeAnimatorController defaultController, victoryController;

    public void ChangeController(bool victoryPose)
    {
        if(victoryPose)
        {
            playerAnimator.runtimeAnimatorController = victoryController;

            playerAnimator.Play("Victory");
        }
        else
        {
            playerAnimator.runtimeAnimatorController = defaultController;

            playerAnimator.Play("IdlePose");
        }
    }
}