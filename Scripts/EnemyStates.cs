using UnityEngine;

public enum EnemyActions { PhysicalAction, RangedAction, SupportAction, HealAction };
public enum ActionTarget { Player, SingleEnemy, MultiEnemy };
public enum ActionStatus { NONE, Burns, Paralysis, Poison, HpRegen, CpRegen, StrengthUp, DefenseUp, StrengthDown, DefenseDown, CrackedShield, DullBlade, Undead };

[System.Serializable]
public class EnemyAction
{
    [SerializeField]
    private string actionName, animatorName;

    [SerializeField]
    private EnemyActions enemyActions;

    [SerializeField]
    private ActionTarget actionTarget;

    [SerializeField]
    private ActionStatus actionStatus;

    [SerializeField]
    private Sprite statusEffectSprite = null;

    [SerializeField]
    private string statusEffectName;

    [SerializeField]
    private int actionStrength;

    [SerializeField]
    private int statusEffectDuration;

    [SerializeField]
    private int additionalAttacks;

    [SerializeField]
    private bool canBeBlocked, canBeCountered, shouldCheckStatus, isAProjectile;

    [SerializeField]
    private float projectileOffsetY;

    [SerializeField]
    private int attackIndex, statusBoostPercentage;

    public Sprite StatusEffectSprite
    {
        get
        {
            return statusEffectSprite;
        }
        set
        {
            statusEffectSprite = value;
        }
    }

    public EnemyActions _EnemyActions
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

    public ActionStatus _actionStatus
    {
        get
        {
            return actionStatus;
        }
        set
        {
            actionStatus = value;
        }
    }

    public ActionTarget _ActionTarget
    {
        get
        {
            return actionTarget;
        }
        set
        {
            actionTarget = value;
        }
    }

    public int ActionStrength
    {
        get
        {
            return actionStrength;
        }
        set
        {
            actionStrength = value;
        }
    }

    public string StatusEffectName
    {
        get
        {
            return statusEffectName;
        }
        set
        {
            statusEffectName = value;
        }
    }

    public int AttackIndex
    {
        get
        {
            return attackIndex;
        }
        set
        {
            attackIndex = value;
        }
    }

    public float ProjectileOffsetY
    {
        get
        {
            return projectileOffsetY;
        }
        set
        {
            projectileOffsetY = value;
        }
    }

    public int StatusEffectDuration
    {
        get
        {
            return statusEffectDuration;
        }
        set
        {
            statusEffectDuration = value;
        }
    }

    public int AdditionalAttacks
    {
        get
        {
            return additionalAttacks;
        }
        set
        {
            additionalAttacks = value;
        }
    }

    public int StatusBoostPercentage
    {
        get
        {
            return statusBoostPercentage;
        }
        set
        {
            statusBoostPercentage = value;
        }
    }

    public string ActionName
    {
        get
        {
            return actionName;
        }
        set
        {
            actionName = value;
        }
    }

    public string AnimatorName
    {
        get
        {
            return animatorName;
        }
        set
        {
            animatorName = value;
        }
    }

    public bool CanBeBlocked
    {
        get
        {
            return canBeBlocked;
        }
        set
        {
            canBeBlocked = value;
        }
    }

    public bool CanBeCountered
    {
        get
        {
            return canBeCountered;
        }
        set
        {
            canBeCountered = value;
        }
    }

    public bool ShouldCheckStatus
    {
        get
        {
            return shouldCheckStatus;
        }
        set
        {
            shouldCheckStatus = value;
        }
    }

    public bool IsAProjectile => isAProjectile;
}

public class EnemyStates : MonoBehaviour
{
    [SerializeField]
    private EnemyAction[] enemyAction;

    private int actionIndex;

    public EnemyAction[] _enemyAction
    {
        get
        {
            return enemyAction;
        }
        set
        {
            enemyAction = value;
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
}