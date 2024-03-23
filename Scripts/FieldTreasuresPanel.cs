using UnityEngine;
using TMPro;

public class FieldTreasuresPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI fieldTreasuresText, fieldCardsText, fieldMoonstoneText;

    private int treasureChestAmount, fieldCardAmount, fieldMoonstoneAmount;

    public void SetTreasureChestAmount(int treasures)
    {
        treasureChestAmount = treasures;

        fieldTreasuresText.text = "<size=15>x</size> " + treasures;
    }

    public void SetFieldCardsAmount(int cards)
    {
        fieldCardAmount = cards;

        fieldCardsText.text = "<size=15>x</size> " + cards;
    }

    public void SetFieldMoonStoneAmount(int moonStones)
    {
        fieldMoonstoneAmount = moonStones;

        fieldMoonstoneText.text = "<size=15>x</size> " + moonStones;
    }

    public void DecrementFieldCardText()
    {
        fieldCardAmount--;

        fieldCardsText.text = "<size=15>x</size> " + fieldCardAmount;
    }

    public void DecrementFieldTreasuresText()
    {
        treasureChestAmount--;

        fieldTreasuresText.text = "<size=15>x</size> " + treasureChestAmount;
    }

    public void DecrementFieldMoonstoneText()
    {
        fieldMoonstoneAmount--;

        fieldMoonstoneText.text = "<size=15>x</size> " + fieldMoonstoneAmount;
    }

    public void ResetTreasuresText()
    {
        treasureChestAmount = 0;
        fieldCardAmount = 0;
        fieldMoonstoneAmount = 0;

        fieldCardsText.text = "<size=15>x</size> 0";
        fieldTreasuresText.text = "<size=15>x</size> 0";
        fieldMoonstoneText.text = "<size=15>x</size> 0";

        gameObject.SetActive(false);
    }
}