using UnityEngine;

public class ChangeCursorImage : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}