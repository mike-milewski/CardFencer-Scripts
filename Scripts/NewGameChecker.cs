using UnityEngine;

public class NewGameChecker : MonoBehaviour
{
    public static NewGameChecker instance = null;

    [SerializeField]
    private bool isANewGame;

    public bool IsANewGame
    {
        get
        {
            return isANewGame;
        }
        set
        {
            isANewGame = value;
        }
    }

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }
}