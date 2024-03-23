using UnityEngine;

public class PlayerCreditArmor : MonoBehaviour
{
    [SerializeField]
    private PlayerMenuInfo playerMenuInfo;

    [SerializeField]
    private GameObject noviceArmor, veteranArmor, eliteArmor;

    private void Awake()
    {
        switch(playerMenuInfo.armorIndex)
        {
            case 0:
                noviceArmor.SetActive(true);
                veteranArmor.SetActive(false);
                eliteArmor.SetActive(false);
                break;
            case 1:
                noviceArmor.SetActive(false);
                veteranArmor.SetActive(true);
                eliteArmor.SetActive(false);
                break;
            case 2:
                noviceArmor.SetActive(false);
                veteranArmor.SetActive(false);
                eliteArmor.SetActive(true);
                break;
        }
    }
}