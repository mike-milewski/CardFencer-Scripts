using UnityEngine;

public class TreasureChestManager : MonoBehaviour
{
    [SerializeField]
    private TreasureData treasureData;

    [SerializeField]
    private TreasureChest[] treasureChests;

    [SerializeField]
    private bool dontCheckChests;

    private int stageIndex;

    public TreasureChest[] _TreasureChests
    {
        get
        {
            return treasureChests;
        }
        set
        {
            treasureChests = value;
        }
    }

    public TreasureData _TreasureData => treasureData;

    public int StageIndex => stageIndex;

    private void Awake()
    {
        stageIndex = NodeManager.instance.TreasureChestIndex;

        int treasureAmount = 0;

        if(!dontCheckChests)
        {
            if (stageIndex > -1)
            {
                for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].stageTreasures[stageIndex].treasures.Length; i++)
                {
                    if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].stageTreasures[stageIndex].treasures[i] == 1)
                    {
                        treasureChests[i].SetOpenChest();
                    }
                    else
                    {
                        treasureAmount++;
                        MenuController.instance._FieldTreasurePanel.SetTreasureChestAmount(treasureAmount);
                    }
                }
            }
            else
            {
                switch(stageIndex)
                {
                    case (-1):
                        for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[0].treasures.Length; i++)
                        {
                            if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[0].treasures[i] == 1)
                            {
                                treasureChests[i].SetOpenChest();
                            }
                            else
                            {
                                treasureAmount++;
                                MenuController.instance._FieldTreasurePanel.SetTreasureChestAmount(treasureAmount);
                            }
                        }
                        break;
                    case (-2):
                        for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[1].treasures.Length; i++)
                        {
                            if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[1].treasures[i] == 1)
                            {
                                treasureChests[i].SetOpenChest();
                            }
                            else
                            {
                                treasureAmount++;
                                MenuController.instance._FieldTreasurePanel.SetTreasureChestAmount(treasureAmount);
                            }
                        }
                        break;
                    case (-3):
                        for (int i = 0; i < treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[2].treasures.Length; i++)
                        {
                            if (treasureData.worldTreasures[NodeManager.instance.WorldIndex].secretStageTreasures[2].treasures[i] == 1)
                            {
                                treasureChests[i].SetOpenChest();
                            }
                            else
                            {
                                treasureAmount++;
                                MenuController.instance._FieldTreasurePanel.SetTreasureChestAmount(treasureAmount);
                            }
                        }
                        break;
                }
            }
        }
    }
}