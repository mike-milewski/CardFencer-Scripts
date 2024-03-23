using UnityEngine;

public class StatusBarScreenFitter : MonoBehaviour
{
    [SerializeField]
    private RectTransform statusBarRectTransform;

    private void OnEnable()
    {
        SetStatusBarPosition();
    }

    private void SetStatusBarPosition()
    {
        float screenSize = Screen.width / Screen.height;

        if(screenSize == 1)
        {
            statusBarRectTransform.anchoredPosition = new Vector2(-304.2f, statusBarRectTransform.anchoredPosition.y);
        }

        Debug.Log(screenSize);
    }
}