using UnityEngine;

public class WillOWispAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnEnable()
    {
        animator.Play("FireBallPingPong");
    }
}