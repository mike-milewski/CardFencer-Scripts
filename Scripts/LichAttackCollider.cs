using UnityEngine;

public class LichAttackCollider : MonoBehaviour
{
    [SerializeField]
    private SphereCollider[] attackColliders;

    public void EnableAttackColliders()
    {
        for(int i = 0; i < attackColliders.Length; i++)
        {
            attackColliders[i].enabled = true;
        }
    }

    public void DisableAttackColliders()
    {
        for (int i = 0; i < attackColliders.Length; i++)
        {
            attackColliders[i].enabled = false;
        }
    }
}