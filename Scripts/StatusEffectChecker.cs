using System.Collections.Generic;
using UnityEngine;

public class StatusEffectChecker : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private BattleEnemy battleEnemy;

    [SerializeField]
    private List<EnemyAction> enemyActions = new List<EnemyAction>();

    private BattleEnemy _battleEnemy;

    private int enemyIndex, actionIndex, enemiesWithStatusEffects, enemiesWithLowHealth;

    public int EnemyIndex
    {
        get
        {
            return enemyIndex;
        }
        set
        {
            enemyIndex = value;
        }
    }

    public int ActionIndex
    {
        get
        {
            return actionIndex;
        }
        set
        {
            actionIndex = value;
        }
    }

    public List<EnemyAction> _EnemyActions
    {
        get
        {
            return enemyActions;
        }
        set
        {
            enemyActions = value;
        }
    }

    public BattleEnemy _BattleEnemy => _battleEnemy;

    private void Awake()
    {
        AddActions();
    }

    private void AddActions()
    {
        enemyActions.Clear();

        foreach (EnemyAction action in battleEnemy._enemyAction)
        {
            enemyActions.Add(action);

            action.ActionName = action.ActionName;
            action.AdditionalAttacks = action.AdditionalAttacks;
            action.AnimatorName = action.AnimatorName;
            action.StatusBoostPercentage = action.StatusBoostPercentage;
            action.StatusEffectDuration = action.StatusEffectDuration;
            action._ActionTarget = action._ActionTarget;
            action.StatusEffectSprite = action.StatusEffectSprite;
            action.AttackIndex = action.AttackIndex;
            action.CanBeBlocked = action.CanBeBlocked;
            action.CanBeCountered = action.CanBeCountered;
            action.StatusEffectName = action.StatusEffectName;
            action.ProjectileOffsetY = action.ProjectileOffsetY;
            action.ShouldCheckStatus = action.ShouldCheckStatus;
            action._EnemyActions = action._EnemyActions;
        }
    }

    public void ResetActions()
    {
        enemyActions.Clear();

        foreach (EnemyAction action in battleEnemy._enemyAction)
        {
            enemyActions.Add(action);

            action.ActionName = action.ActionName;
            action.AdditionalAttacks = action.AdditionalAttacks;
            action.AnimatorName = action.AnimatorName;
            action.StatusBoostPercentage = action.StatusBoostPercentage;
            action.StatusEffectDuration = action.StatusEffectDuration;
            action._ActionTarget = action._ActionTarget;
            action.StatusEffectSprite = action.StatusEffectSprite;
            action.AttackIndex = action.AttackIndex;
            action.ActionStrength = action.ActionStrength;
            action.CanBeBlocked = action.CanBeBlocked;
            action.CanBeCountered = action.CanBeCountered;
            action.StatusEffectName = action.StatusEffectName;
            action.ProjectileOffsetY = action.ProjectileOffsetY;
            action.ShouldCheckStatus = action.ShouldCheckStatus;
            action._EnemyActions = action._EnemyActions;
        }

        CheckEnemyAction();
    }

    public void CheckEnemyAction()
    {
        enemyIndex = 0;
        enemiesWithStatusEffects = 0;
        enemiesWithLowHealth = 0;

        for(int i = 0; i < enemyActions.Count; i++)
        {
            EnemyAction action = enemyActions[i];
            if(action._EnemyActions == EnemyActions.SupportAction)
            {
                if(CheckActionStatusEffects(action._ActionTarget, action))
                {
                    enemyActions.RemoveAt(i);
                    i--;
                }
                
            }
            if(action._EnemyActions == EnemyActions.HealAction)
            {
                if (!CheckEnemyHealth(action._ActionTarget, action))
                {
                    enemyActions.RemoveAt(i);
                    i--;
                }
            }
        }

        actionIndex = Random.Range(0, enemyActions.Count);
    }

    private bool CheckActionStatusEffects(ActionTarget target, EnemyAction actions)
    {
        bool hasStatus = false;

        if(target == ActionTarget.Player)
        {
            BattlePlayer battlePlayer = battleSystem._battlePlayer;
            if (battlePlayer.StatusEffectHolder.transform.childCount > 0)
            {
                for (int i = 0; i < battlePlayer.StatusEffectHolder.transform.childCount; i++)
                {
                    StatusEffects statusEffects = battlePlayer.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                    if (actions._actionStatus == (ActionStatus)statusEffects._statusEffect)
                    {
                        hasStatus = true;
                    }
                }
            }
            else hasStatus = false;
        }
        else if(target == ActionTarget.MultiEnemy)
        {
            if (battleSystem.Enemies.Count > 1)
            {
                for (int i = 0; i < battleSystem.Enemies.Count; i++)
                {
                    BattleEnemy battleEnemy = battleSystem.Enemies[i];

                    if(battleEnemy.CurrentHealth > 0)
                    {
                        if (battleEnemy.StatusEffectHolder.transform.childCount > 0)
                        {
                            for (int j = 0; j < battleEnemy.StatusEffectHolder.transform.childCount; j++)
                            {
                                StatusEffects statusEffects = battleEnemy.StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();
                                if (actions._actionStatus == (ActionStatus)statusEffects._statusEffect)
                                {
                                    enemiesWithStatusEffects++;
                                }
                            }
                        }
                    }
                }

                if(battleSystem.Enemies.Count == 3)
                {
                    if (enemiesWithStatusEffects < 2)
                    {
                        hasStatus = false;
                    }
                    else hasStatus = true;
                }
                else if(battleSystem.Enemies.Count == 2)
                {
                    if (enemiesWithStatusEffects < 1)
                    {
                        hasStatus = false;
                    }
                    else hasStatus = true;
                }
            }
            else
            {
                BattleEnemy battleEnemy = battleSystem.Enemies[0];

                _battleEnemy = battleEnemy;

                if(battleEnemy.CurrentHealth > 0)
                {
                    if (battleEnemy.StatusEffectHolder.transform.childCount > 0)
                    {
                        for (int i = 0; i < battleEnemy.StatusEffectHolder.transform.childCount; i++)
                        {
                            StatusEffects statusEffects = battleEnemy.StatusEffectHolder.transform.GetChild(i).GetComponent<StatusEffects>();
                            if (actions._actionStatus == (ActionStatus)statusEffects._statusEffect)
                            {
                                battleEnemy.HasTheStatusEffect = true;
                                return hasStatus = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            foreach(BattleEnemy enemy in battleSystem.Enemies)
            {
                if(enemy.CurrentHealth > 0)
                {
                    if(enemy.StatusEffectHolder.transform.childCount <= 0)
                    {
                        hasStatus = false;
                        _battleEnemy = enemy;

                        break;
                    }
                    else
                    {
                        for (int j = 0; j < enemy.StatusEffectHolder.transform.childCount; j++)
                        {
                            StatusEffects status = enemy.StatusEffectHolder.transform.GetChild(j).GetComponent<StatusEffects>();
                            if (actions._actionStatus == (ActionStatus)status._statusEffect)
                            {
                                enemy.HasTheStatusEffect = true;
                                hasStatus = true;
                            }
                            else
                            {
                                hasStatus = false;
                                _battleEnemy = enemy;

                                break;
                            }
                        }
                    }
                }
            }
        }

        return hasStatus;
    }

    private bool CheckEnemyHealth(ActionTarget target, EnemyAction actions)
    {
        bool needsHealing = false;

        if(target == ActionTarget.MultiEnemy)
        {
            if(battleSystem.Enemies.Count > 1)
            {
                for(int i = 0; i < battleSystem.Enemies.Count; i++)
                {
                    BattleEnemy enemy = battleSystem.Enemies[i];
                    if(enemy.CurrentHealth <= enemy.MaxHealth / 3 && enemy.CurrentHealth > 0)
                    {
                        enemiesWithLowHealth++;
                    }
                }

                if(battleSystem.Enemies.Count == 3)
                {
                    if (enemiesWithLowHealth > 2)
                    {
                        needsHealing = true;
                    }
                    else needsHealing = false;
                }

                if(battleSystem.Enemies.Count == 2)
                {
                    if (enemiesWithLowHealth > 1)
                    {
                        needsHealing = true;
                    }
                    else needsHealing = false;
                }
            }
            else
            {
                if (battleEnemy.CurrentHealth <= battleEnemy.MaxHealth / 3)
                {
                    needsHealing = true;
                }
                else needsHealing = false;
            }
        }
        else if(target == ActionTarget.SingleEnemy)
        {
            for(int i = 0; i < battleSystem.Enemies.Count; i++)
            {
                BattleEnemy enemy = battleSystem.Enemies[i];
                if(enemy.CurrentHealth <= enemy.MaxHealth / 3 && enemy.CurrentHealth > 0)
                {
                    enemyIndex = enemy.IndexedEnemy;

                    return needsHealing = true;
                }
            }
        }

        return needsHealing;
    }
}