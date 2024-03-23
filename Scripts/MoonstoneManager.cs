using UnityEngine;

public class MoonstoneManager : MonoBehaviour
{
    [SerializeField]
    private MoonstoneData moonstoneData;

    [SerializeField]
    private EnemyFieldAI[] enemyFieldAI;

    public EnemyFieldAI[] _EnemyFieldAI
    {
        get
        {
            return enemyFieldAI;
        }
        set
        {
            enemyFieldAI = value;
        }
    }

    private int stageIndex, currentWorldIndex;

    public MoonstoneData _MoonStoneData => moonstoneData;

    public int StageIndex => stageIndex;

    private void Awake()
    {
        stageIndex = NodeManager.instance.MoonStoneStageIndex;

        currentWorldIndex = NodeManager.instance.WorldIndex;

        int moonStoneAmount = 0;

        if (stageIndex > -1)
        {
            if(moonstoneData.worldMoonStones[currentWorldIndex].stageMoonstones.Length > 0)
            {
                for (int i = 0; i < moonstoneData.worldMoonStones[currentWorldIndex].stageMoonstones[stageIndex].moonStones.Length; i++)
                {
                    if (moonstoneData.worldMoonStones[currentWorldIndex].stageMoonstones[stageIndex].moonStones[i] == 1)
                    {
                        if(enemyFieldAI.Length > 0)
                        {
                            if (moonstoneData.worldMoonStones[currentWorldIndex].stageMoonstones[stageIndex].moonStones.Length > enemyFieldAI.Length)
                            {
                                switch (enemyFieldAI.Length)
                                {
                                    case 1:
                                        if (i == enemyFieldAI[0].MoonStoneIndex)
                                        {
                                            enemyFieldAI[0].HasMoonStone = false;
                                            enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                        }
                                        break;
                                    case 2:
                                        if (i == enemyFieldAI[0].MoonStoneIndex)
                                        {
                                            enemyFieldAI[0].HasMoonStone = false;
                                            enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                        }
                                        if (i == enemyFieldAI[1].MoonStoneIndex)
                                        {
                                            enemyFieldAI[1].HasMoonStone = false;
                                            enemyFieldAI[1].MoonStoneSymbol.SetActive(false);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                enemyFieldAI[i].HasMoonStone = false;
                                enemyFieldAI[i].MoonStoneSymbol.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        moonStoneAmount++;

                        MenuController.instance._FieldTreasurePanel.SetFieldMoonStoneAmount(moonStoneAmount);
                    }
                }
            }
        }
        else
        {
            switch(stageIndex)
            {
                case (-1):
                    if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones.Length > 0)
                    {
                        for (int i = 0; i < moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[0].moonStones.Length; i++)
                        {
                            if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[0].moonStones[i] == 1)
                            {
                                if (enemyFieldAI.Length > 0)
                                {
                                    if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[0].moonStones.Length > enemyFieldAI.Length)
                                    {
                                        switch (enemyFieldAI.Length)
                                        {
                                            case 1:
                                                if (i == enemyFieldAI[0].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[0].HasMoonStone = false;
                                                    enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                                }
                                                break;
                                            case 2:
                                                if (i == enemyFieldAI[0].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[0].HasMoonStone = false;
                                                    enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                                }
                                                if (i == enemyFieldAI[1].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[1].HasMoonStone = false;
                                                    enemyFieldAI[1].MoonStoneSymbol.SetActive(false);
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        enemyFieldAI[i].HasMoonStone = false;
                                        enemyFieldAI[i].MoonStoneSymbol.SetActive(false);
                                    }
                                }
                            }
                            else
                            {
                                moonStoneAmount++;

                                MenuController.instance._FieldTreasurePanel.SetFieldMoonStoneAmount(moonStoneAmount);
                            }
                        }
                    }
                    break;
                case (-2):
                    if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones.Length > 0)
                    {
                        for (int i = 0; i < moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[1].moonStones.Length; i++)
                        {
                            if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[1].moonStones[i] == 1)
                            {
                                if (enemyFieldAI.Length > 0)
                                {
                                    if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[1].moonStones.Length > enemyFieldAI.Length)
                                    {
                                        switch (enemyFieldAI.Length)
                                        {
                                            case 1:
                                                if (i == enemyFieldAI[0].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[0].HasMoonStone = false;
                                                    enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                                }
                                                break;
                                            case 2:
                                                if (i == enemyFieldAI[0].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[0].HasMoonStone = false;
                                                    enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                                }
                                                if (i == enemyFieldAI[1].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[1].HasMoonStone = false;
                                                    enemyFieldAI[1].MoonStoneSymbol.SetActive(false);
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        enemyFieldAI[i].HasMoonStone = false;
                                        enemyFieldAI[i].MoonStoneSymbol.SetActive(false);
                                    }
                                }
                            }
                            else
                            {
                                moonStoneAmount++;

                                MenuController.instance._FieldTreasurePanel.SetFieldMoonStoneAmount(moonStoneAmount);
                            }
                        }
                    }
                    break;
                case (-3):
                    if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones.Length > 0)
                    {
                        for (int i = 0; i < moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[2].moonStones.Length; i++)
                        {
                            if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[2].moonStones[i] == 1)
                            {
                                if (enemyFieldAI.Length > 0)
                                {
                                    if (moonstoneData.worldMoonStones[currentWorldIndex].secretStageMoonstones[2].moonStones.Length > enemyFieldAI.Length)
                                    {
                                        switch (enemyFieldAI.Length)
                                        {
                                            case 1:
                                                if (i == enemyFieldAI[0].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[0].HasMoonStone = false;
                                                    enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                                }
                                                break;
                                            case 2:
                                                if (i == enemyFieldAI[0].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[0].HasMoonStone = false;
                                                    enemyFieldAI[0].MoonStoneSymbol.SetActive(false);
                                                }
                                                if (i == enemyFieldAI[1].MoonStoneIndex)
                                                {
                                                    enemyFieldAI[1].HasMoonStone = false;
                                                    enemyFieldAI[1].MoonStoneSymbol.SetActive(false);
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        enemyFieldAI[i].HasMoonStone = false;
                                        enemyFieldAI[i].MoonStoneSymbol.SetActive(false);
                                    }
                                }
                            }
                            else
                            {
                                moonStoneAmount++;

                                MenuController.instance._FieldTreasurePanel.SetFieldMoonStoneAmount(moonStoneAmount);
                            }
                        }
                    }
                    break;
            }
        }
    }
}