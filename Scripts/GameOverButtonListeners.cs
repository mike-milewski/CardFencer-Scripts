using UnityEngine;
using UnityEngine.UI;

public class GameOverButtonListeners : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private bool shouldReturnToCheckPoint;

    private void Awake()
    {
        if(shouldReturnToCheckPoint)
        {
            ReturnToCheckPoint();
        }
        else
        {
            RetryBattleButton();
        }
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void ReturnToCheckPoint()
    {
        button.onClick.AddListener(GameManager.instance.WaitToLoadFieldSceneButton);
    }

    private void RetryBattleButton()
    {
        button.onClick.AddListener(GameManager.instance.RetryBattle);
    }
}