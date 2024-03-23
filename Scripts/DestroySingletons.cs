using UnityEngine;

public class DestroySingletons : MonoBehaviour
{
    private void Awake()
    {
        RemoveAllSingletons();
    }

    private void RemoveAllSingletons()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if(gameManager != null)
           Destroy(gameManager.gameObject);

        MenuController menuController = FindObjectOfType<MenuController>();

        if (menuController != null)
            menuController.UICamera.SetActive(false);
    }
}