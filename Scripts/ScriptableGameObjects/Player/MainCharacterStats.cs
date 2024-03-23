using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCharacter", menuName = "ScriptableObjects/MainCharacter", order = 1)]
public class MainCharacterStats : ScriptableObject
{
    public string playerName;

    public int level, maximumLevel, currentPlayerHealth, maximumHealth, healthLimit, currentPlayerCardPoints, maximumCardPoints, cardPointLimit, currentPlayerStickerPoints, maximumStickerPoints, stickerPointLimit, 
               strength, defense, maximumMoney, moonStone, maximumMoonstone;

    public float currentExp, nextExpToLevel, money, moveSpeed, increasedMoveSpeed, increasedSprintSpeed, cardPointHealPercentage;

    public float guardGauge;

    public List<CardTemplate> startingCards = new List<CardTemplate>();

    public List<CardTemplate> currentCards = new List<CardTemplate>();

    public CardTemplate startingItem;
}
