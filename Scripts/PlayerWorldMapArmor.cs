using UnityEngine;

public class PlayerWorldMapArmor : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private GameObject noviceArmor, veteranArmor, eliteArmor;

    private void Start()
    {
        switch(playerMenuInfo.armorIndex)
        {
            case 1:
                noviceArmor.SetActive(false);
                veteranArmor.SetActive(true);
                break;
            case 2:
                noviceArmor.SetActive(false);
                eliteArmor.SetActive(true);
                break;
        }
    }
}