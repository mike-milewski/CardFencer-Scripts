using UnityEngine;

[System.Serializable]
public class WorldTreasures
{
    public StageTreasures[] stageTreasures, secretStageTreasures;
}

[System.Serializable]
public class StageTreasures
{
    public int[] treasures;
}

[CreateAssetMenu(fileName = "Treasures", menuName = "ScriptableObjects/Treasures", order = 1)]
public class TreasureData : ScriptableObject
{
    public WorldTreasures[] worldTreasures;
}