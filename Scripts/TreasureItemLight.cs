using UnityEngine;

public class TreasureItemLight : MonoBehaviour
{
    [SerializeField]
    private GameObject lightObject;

    public void EnableLightObject()
    {
        lightObject.SetActive(true);
    }
}