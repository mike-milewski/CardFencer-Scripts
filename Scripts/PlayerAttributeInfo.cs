using UnityEngine;
using TMPro;

public class PlayerAttributeInfo : MonoBehaviour
{
    [SerializeField]
    private Animator attributePanelAnimator;

    [SerializeField]
    private TextMeshProUGUI attributeTitle, attributeDescription;

    [SerializeField]
    private string title;

    [SerializeField][TextArea]
    private string description;

    public void ShowPanel()
    {
        attributeTitle.text = title;
        attributeDescription.text = description;

        attributePanelAnimator.Play("Show", -1, 0);
    }

    public void HidePanel()
    {
        attributePanelAnimator.Play("Hide", -1, 0);
    }
}