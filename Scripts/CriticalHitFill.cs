using UnityEngine;
using UnityEngine.UI;

public class CriticalHitFill : MonoBehaviour
{
    [SerializeField]
    private Image criticalFillImage;

    [SerializeField]
    private float fillSpeed;

    private void OnEnable()
    {
        criticalFillImage.fillAmount = 0;
    }

    private void Update()
    {
        criticalFillImage.fillAmount += fillSpeed;
    }
}