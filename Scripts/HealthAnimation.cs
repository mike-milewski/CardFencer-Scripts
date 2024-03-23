using UnityEngine;

public class HealthAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public Animator _animator
    {
        get
        {
            return animator;
        }
        set
        {
            animator = value;
        }
    }
}