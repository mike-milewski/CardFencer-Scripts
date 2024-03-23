using UnityEngine;

public class FieldCardManager : MonoBehaviour
{
    [SerializeField]
    private FieldCardData fieldCardData;

    [SerializeField]
    private CardPickUp[] cardPickUp;

    private int stageIndex, currentWorldIndex;

    public CardPickUp[] _CardPickUp
    {
        get
        {
            return cardPickUp;
        }
        set
        {
            cardPickUp = value;
        }
    }

    public FieldCardData _FieldCardData => fieldCardData;

    public int StageIndex => stageIndex;

    private void Awake()
    {
        stageIndex = NodeManager.instance.CurrentNodeIndex - 1;

        currentWorldIndex = NodeManager.instance.WorldIndex;

        int fieldCardAmount = 0;

        if (stageIndex > -1)
        {
            for (int i = 0; i < fieldCardData.worldCards[currentWorldIndex].stageCards[stageIndex].fieldCards.Length; i++)
            {
                if (fieldCardData.worldCards[currentWorldIndex].stageCards[stageIndex].fieldCards[i] == 1)
                {
                    cardPickUp[i].gameObject.SetActive(false);
                }
                else
                {
                    fieldCardAmount++;

                    MenuController.instance._FieldTreasurePanel.SetFieldCardsAmount(fieldCardAmount);
                }
            }
        }
        else
        {
            switch(NodeManager.instance.CurrentNodeIndex)
            {
                case (-1):
                    for (int i = 0; i < fieldCardData.worldCards[currentWorldIndex].secretStageCards[0].fieldCards.Length; i++)
                    {
                        if (fieldCardData.worldCards[currentWorldIndex].secretStageCards[0].fieldCards[i] == 1)
                        {
                            cardPickUp[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            fieldCardAmount++;

                            MenuController.instance._FieldTreasurePanel.SetFieldCardsAmount(fieldCardAmount);
                        }
                    }
                    break;
                case (-2):
                    for (int i = 0; i < fieldCardData.worldCards[currentWorldIndex].secretStageCards[1].fieldCards.Length; i++)
                    {
                        if (fieldCardData.worldCards[currentWorldIndex].secretStageCards[1].fieldCards[i] == 1)
                        {
                            cardPickUp[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            fieldCardAmount++;

                            MenuController.instance._FieldTreasurePanel.SetFieldCardsAmount(fieldCardAmount);
                        }
                    }
                    break;
                case (-3):
                    for (int i = 0; i < fieldCardData.worldCards[currentWorldIndex].secretStageCards[2].fieldCards.Length; i++)
                    {
                        if (fieldCardData.worldCards[currentWorldIndex].secretStageCards[2].fieldCards[i] == 1)
                        {
                            cardPickUp[i].gameObject.SetActive(false);
                        }
                        else
                        {
                            fieldCardAmount++;

                            MenuController.instance._FieldTreasurePanel.SetFieldCardsAmount(fieldCardAmount);
                        }
                    }
                    break;
            }
        }
    }
}