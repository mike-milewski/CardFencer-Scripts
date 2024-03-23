using UnityEngine;

public class DisableStageObjectivePanel : MonoBehaviour
{
    public void DisablePanel()
    {
        gameObject.SetActive(false);
    }

    public void CheckObjectActive()
    {
        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("HideObj"))
        {
            gameObject.SetActive(false);
        }
    }
}