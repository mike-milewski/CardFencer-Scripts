using UnityEngine;

[System.Serializable]
public class WorldCards
{
    public StageCards[] stageCards, secretStageCards;
}

[System.Serializable]
public class StageCards
{
    public int[] fieldCards;
}

[CreateAssetMenu(fileName = "FieldCard", menuName = "ScriptableObjects/FieldCard", order = 1)]
public class FieldCardData : ScriptableObject
{
    public WorldCards[] worldCards;
}