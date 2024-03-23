using UnityEngine;
using TMPro;

public class BattleNumber : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private TextMeshProUGUI battleNumberText;

    [SerializeField]
    private Color damageColor, healColorHP, healColorCP;

    public TextMeshProUGUI BattleNumberText
    {
        get
        {
            return battleNumberText;
        }
        set
        {
            battleNumberText = value;
        }
    }

    public void ApplyDamageColor()
    {
        battleNumberText.color = damageColor;

        PlayAnimation();
    }

    public void ApplyHpHealColor()
    {
        battleNumberText.color = healColorHP;

        PlayAnimation();
    }

    public void ApplyCpHealColor()
    {
        battleNumberText.color = healColorCP;

        PlayAnimation();
    }

    private void PlayAnimation()
    {
        animator.Play("BattleNumber", -1, 0);
    }
}