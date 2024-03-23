using UnityEngine;
using UnityEngine.UI;

public class FindNewGameChecker : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private bool setNewGame;

    private void Start()
    {
        button.onClick.AddListener(ButtonListener);
    }

    private void ButtonListener()
    {
        NewGameChecker newGameChecker = FindObjectOfType<NewGameChecker>();

        newGameChecker.IsANewGame = setNewGame;
    }
}