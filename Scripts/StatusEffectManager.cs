using UnityEngine;
using UnityEngine.UI;

public class StatusEffectManager : MonoBehaviour
{
    [SerializeField]
    private StatusEffects statusEffectPrefab;

    [SerializeField]
    private BattlePlayer battlePlayer;

    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Sprite burnsImmuneSprite, poisonImmuneSprite, paralysisImmuneSprite, undeadImmuneSprite, imperviousSprite, resurrectSprite, paralysisSprite, poisonSprite, burnsSprite, hpRegen, cpRegen, enrage, thorns, 
                   strengthUp, defenseUp, shadowFormSprite, freezeImmuneSprite, freezeSprite;

    public Sprite BurnsImmuneSprite => burnsImmuneSprite;

    public Sprite PoisionImmuneSprite => poisonImmuneSprite;

    public Sprite ParalysisImmuneSprite => paralysisImmuneSprite;

    public Sprite FreezeImmuneSprite => freezeImmuneSprite;

    public Sprite UndeadImmuneSprite => undeadImmuneSprite;

    public Sprite ImperviousSprite => imperviousSprite;

    public Sprite ResurrectSprite => resurrectSprite;

    public Sprite ParalysisSprite => paralysisSprite;

    public Sprite PoisonSprite => poisonSprite;

    public Sprite BurnsSprite => burnsSprite;

    public Sprite FreezeSprite => freezeSprite;

    public Sprite HpRegen => hpRegen;

    public Sprite CpRegen => cpRegen;

    public Sprite Enrage => enrage;

    public Sprite Thorns => thorns;

    public Sprite StrengthUp => strengthUp;

    public Sprite DefenseUp => defenseUp;

    public Sprite ShadowFormSprite => shadowFormSprite;

    public void CreateStatusEffect(StatusEffect effect, string statusName, int statusTime, int statusBoost, bool shouldCheckStatus, Sprite statusSprite, bool showPanel)
    {
        var status = Instantiate(statusEffectPrefab, battlePlayer.StatusEffectHolder.transform);

        if(showPanel)
        {
            battlePlayer.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
        }

        battlePlayer.StatusEffectText.text = statusName;

        status.Target = battlePlayer.gameObject;
        status._statusEffect = effect;
        status.SetStatusParticle();
        status.StatusTime = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? statusTime * 2 : statusTime;
        status.StatusChangePercentage = statusBoost;
        if(MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch))
        {
            status.StatusTimeText.text = statusTime > 0 ? (statusTime * 2).ToString() : "";
        }
        else
        {
            status.StatusTimeText.text = statusTime > 0 ? statusTime.ToString() : "";
        }
        status.StatusEffectImage.sprite = statusSprite;
        status.ShouldCheckStatus = shouldCheckStatus;
        status.CheckStatusChange();

        AdjustStatusEffectNavigationsForPlayer();

        battleSystem.CheckCharityButtonNavigation();
    }

    public void CreateEnemyStatusEffect(BattleEnemy battleEnemy, StatusEffect effect, string statusName, int statusTime, int statusBoost, bool shouldCheckStatus, Sprite statusSprite, bool isNegativeEffect, bool showPanel)
    {
        var status = Instantiate(statusEffectPrefab, battleEnemy.StatusEffectHolder.transform);

        if(showPanel)
        {
            battleEnemy.StatusEffectText.transform.parent.GetComponent<Animator>().Play("StatusText", -1, 0);
        }

        battleEnemy.StatusEffectText.text = statusName;

        status.Target = battleEnemy.gameObject;
        status._statusEffect = effect;
        status.SetStatusEffectNameText();
        status.StatusNameText.text = statusName;
        status.StatusChangePercentage = statusBoost;
        status.SetStatusParticle();
        if(isNegativeEffect)
        {
            status.StatusTime = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? statusTime * 2 : statusTime;

            status.StatusTimeText.text = MenuController.instance._StickerPowerManager.CheckMidBattleStickerPower(StickerPower.Stretch) ? (statusTime * 2).ToString() : statusTime.ToString();
        }
        else
        {
            status.StatusTime = statusTime;
            status.StatusTimeText.text = statusTime > 0 ? statusTime.ToString() : "";
        }
        status.StatusEffectImage.sprite = statusSprite;
        status.ShouldCheckStatus = shouldCheckStatus;
        status.CheckStatusChange();

        if(status._statusEffect == StatusEffect.Burns || status._statusEffect == StatusEffect.Poison)
        {
            status.transform.SetAsFirstSibling();
        }

        battleSystem.CheckCharityButtonNavigation();
    }

    public void AdjustStatusEffectNavigationsForPlayer()
    {
        if(battlePlayer.StatusEffectHolder.transform.childCount > 0)
        {
            for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
            {
                StatusEffects status = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();

                Navigation nav = status.GetComponent<Selectable>().navigation;

                nav.selectOnRight = null;
                nav.selectOnLeft = null;

                nav.selectOnDown = battleSystem.CharityButton.interactable ? battleSystem.CharityButton : battleSystem.DefaultAttackAnimator.GetComponent<Selectable>();

                if (i == 0)
                {
                    if (battlePlayer.StatusEffectHolder.transform.childCount == 1)
                    {
                        switch(battleSystem.Enemies.Count)
                        {
                            case (1):
                                if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                }
                                break;
                            case (2):
                                if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                }
                                else if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount <= 0 && battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                }
                                else
                                {
                                    nav.selectOnRight = null;
                                }
                                break;
                            case (3):
                                if (battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                }
                                else if (battleSystem.Enemies[1].StatusEffectHolder.transform.childCount <= 0 && battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                }
                                else if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount <= 0 && battleSystem.Enemies[2].StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                }
                                else
                                {
                                    nav.selectOnRight = null;
                                }
                                break;
                        }
                    }
                    else
                    {
                        nav.selectOnRight = battlePlayer.StatusEffectHolder.transform.GetChild(i + 1).GetComponent<Selectable>();
                    }
                }
                else if(i >= battlePlayer.StatusEffectHolder.transform.childCount - 1)
                {
                    switch (battleSystem.Enemies.Count)
                    {
                        case (1):
                            if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                            {
                                nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                            }
                            break;
                        case (2):
                            if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                            {
                                nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                            }
                            else if(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount <= 0 && battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                            {
                                nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                            }
                            else
                            {
                                nav.selectOnRight = null;
                            }
                            break;
                        case (3):
                            if (battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                            {
                                nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                            }
                            else if (battleSystem.Enemies[1].StatusEffectHolder.transform.childCount <= 0 && battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                            {
                                nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                            }
                            else if(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount <= 0 && battleSystem.Enemies[2].StatusEffectHolder.transform.childCount > 0)
                            {
                                nav.selectOnRight = battleSystem.Enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                            }
                            else
                            {
                                nav.selectOnRight = null;
                            }
                            break;
                    }

                    nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(i - 1).GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnRight = battlePlayer.StatusEffectHolder.transform.GetChild(i + 1).GetComponent<Selectable>();
                    nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(i - 1).GetComponent<Selectable>();
                }

                status.GetComponent<Selectable>().navigation = nav;
            }
        }
    }

    public void AdjustStatusEffectNavigationsForEnemies()
    {
        for(int i = 0; i < battleSystem.Enemies.Count; i++)
        {
            if(battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 0)
            {
                for(int j = 0; j < battleSystem.Enemies[i].StatusEffectHolder.transform.childCount; j++)
                {
                    StatusEffects status = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();

                    Navigation nav = status.GetComponent<Selectable>().navigation;

                    nav.selectOnRight = null;
                    nav.selectOnLeft = null;

                    nav.selectOnDown = battleSystem.CharityButton.interactable ? battleSystem.CharityButton : battleSystem.DefaultAttackAnimator.GetComponent<Selectable>();

                    switch (battleSystem.Enemies.Count)
                    {
                        case (1):
                            if(j == 0)
                            {
                                if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                                {
                                    nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(battlePlayer.StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                }

                                if(battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 1)
                                {
                                    nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                                }
                            }
                            else if(j >= battleSystem.Enemies[i].StatusEffectHolder.transform.childCount - 1)
                            {
                                nav.selectOnLeft = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j - 1).GetComponent<Selectable>();
                            }
                            else
                            {
                                nav.selectOnLeft = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j - 1).GetComponent<Selectable>();
                                nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                            }
                            break;
                        case (2):
                            if (j == 0)
                            {
                                if(i == 0)
                                {
                                    if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                    }

                                    if(battleSystem.Enemies[i].StatusEffectHolder.transform.childCount == 1)
                                    {
                                        if(battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                    }
                                    else
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                                    }
                                }
                                if(i == 1)
                                {
                                    if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnLeft = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                    }
                                    else
                                    {
                                        if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                    }

                                    if(battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 1)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                                    }
                                }
                            }
                            else if (j >= battleSystem.Enemies[i].StatusEffectHolder.transform.childCount - 1)
                            {
                                nav.selectOnLeft = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j - 1).GetComponent<Selectable>();

                                if(i == 0)
                                {
                                    if(battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                    }
                                }
                                if(i == 1)
                                {
                                    if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                    }
                                }
                            }
                            else
                            {
                                nav.selectOnLeft = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j - 1).GetComponent<Selectable>();
                                nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                            }
                            break;
                        case (3):
                            if (j == 0)
                            {
                                if(i == 0)
                                {
                                    if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(battlePlayer.StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                    }

                                    if (battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 1)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                                    }

                                    if(battleSystem.Enemies[i].StatusEffectHolder.transform.childCount == 1)
                                    {
                                        if (battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                        else if(battleSystem.Enemies[2].StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnRight = battleSystem.Enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                    }
                                }
                                if(i == 1)
                                {
                                    if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnLeft = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                    }
                                    else
                                    {
                                        if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(battlePlayer.StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                        }
                                    }

                                    if (battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 1)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                                    }

                                    if (battleSystem.Enemies[i].StatusEffectHolder.transform.childCount == 1)
                                    {
                                        if (battleSystem.Enemies[2].StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnRight = battleSystem.Enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                        else if(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                    }
                                }
                                if(i == 2)
                                {
                                    if (battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnLeft = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(battleSystem.Enemies[1].StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                    }
                                    else if(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnLeft = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(battleSystem.Enemies[0].StatusEffectHolder.transform.childCount - 1).GetComponent<Selectable>();
                                    }
                                    else
                                    {
                                        if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnLeft = battlePlayer.StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                    }

                                    if(battleSystem.Enemies[i].StatusEffectHolder.transform.childCount > 1)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                                    }

                                    if (battleSystem.Enemies[i].StatusEffectHolder.transform.childCount == 1)
                                    {
                                        if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                        {
                                            nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                        }
                                    }
                                }
                            }
                            else if (j >= battleSystem.Enemies[i].StatusEffectHolder.transform.childCount - 1)
                            {
                                nav.selectOnLeft = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j - 1).GetComponent<Selectable>();

                                if(i == 0)
                                {
                                    if(battleSystem.Enemies[1].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[1].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                    }
                                }
                                if(i == 1)
                                {
                                    if (battleSystem.Enemies[2].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[2].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                    }
                                }
                                if(i == 2)
                                {
                                    if (battleSystem.Enemies[0].StatusEffectHolder.transform.childCount > 0)
                                    {
                                        nav.selectOnRight = battleSystem.Enemies[0].StatusEffectHolder.transform.GetChild(0).GetComponent<Selectable>();
                                    }
                                }
                            }
                            else
                            {
                                nav.selectOnLeft = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j - 1).GetComponent<Selectable>();
                                nav.selectOnRight = battleSystem.Enemies[i].StatusEffectHolder.transform.GetChild(j + 1).GetComponent<Selectable>();
                            }
                            break;
                    }

                    status.GetComponent<Selectable>().navigation = nav;
                }
            }
        }
    }
}