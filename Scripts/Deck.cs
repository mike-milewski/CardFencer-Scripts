using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "ScriptableObjects/Deck", order = 1)]
public class Deck : ScriptableObject
{
    public List<CardTemplate> cardTemplate = new List<CardTemplate>();

    public int minimumDeckLimit, maxDeckLimit;
}