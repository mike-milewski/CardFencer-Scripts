using UnityEngine;

[System.Serializable]
public class WorldMoonStones
{
    public StageMoonStones[] stageMoonstones, secretStageMoonstones;
}

[System.Serializable]
public class StageMoonStones
{
    public int[] moonStones;
}

[CreateAssetMenu(fileName = "Moonstones", menuName = "ScriptableObjects/Moonstones", order = 1)]
public class MoonstoneData : ScriptableObject
{
    public WorldMoonStones[] worldMoonStones;
}