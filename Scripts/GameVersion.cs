using UnityEngine;
using TMPro;

public class GameVersion : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI versionText;

    private void Awake()
    {
        versionText.text = "Version: " + Application.version;
    }
}