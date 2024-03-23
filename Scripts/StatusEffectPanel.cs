using UnityEngine;
using TMPro;

public class StatusEffectPanel : MonoBehaviour
{
    [SerializeField]
    private Animator statusPanelAnimator;

    [SerializeField]
    private TextMeshProUGUI statusNameText, statusText;

    public TextMeshProUGUI StatusNameText
    {
        get
        {
            return statusNameText;
        }
        set
        {
            statusNameText = value;
        }
    }

    public TextMeshProUGUI StatusText
    {
        get
        {
            return statusText;
        }
        set
        {
            statusText = value;
        }
    }

    public void ShowStatusEffectPanel()
    {
        statusPanelAnimator.Play("OnPointerStatusPanel", -1, 0);
    }

    public void HideStatusEffectPanel()
    {
        statusPanelAnimator.Play("Reverse", -1, 0);
    }
}