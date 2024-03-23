using UnityEngine;

public enum FountainPowers { HpIncrease, CpIncrease, StrengthIncrease, DefenseIncrease, CardReward, StickerReward };

[System.Serializable]
public class FountainPower
{
    public FountainPowers fountainPowers;

    public MainCharacterStats mainCharacterStats;

    public int moonStonesRequired, statPointIncrease;

    public string powerInformation;

    public StickerInformation stickerInfo;

    public CardTemplate cardTemplate;

    public void GainFountainPower()
    {
        switch(fountainPowers)
        {
            case (FountainPowers.HpIncrease):
                mainCharacterStats.maximumHealth += statPointIncrease;
                mainCharacterStats.currentPlayerHealth = mainCharacterStats.maximumHealth;
                break;
            case (FountainPowers.CpIncrease):
                mainCharacterStats.maximumCardPoints += statPointIncrease;
                mainCharacterStats.currentPlayerCardPoints = mainCharacterStats.maximumCardPoints;
                break;
            case (FountainPowers.StrengthIncrease):
                mainCharacterStats.strength += statPointIncrease;
                break;
            case (FountainPowers.DefenseIncrease):
                mainCharacterStats.defense += statPointIncrease;
                break;
        }
    }
}

[CreateAssetMenu(fileName = "Fountain", menuName = "ScriptableObjects/Fountain", order = 1)]
public class FountainData : ScriptableObject
{
    public FountainPower[] fountainPower;

    public int powerIndex, maxFountainLevel;
}