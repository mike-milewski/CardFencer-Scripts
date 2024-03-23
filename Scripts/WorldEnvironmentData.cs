using UnityEngine;

[CreateAssetMenu(fileName = "WorldEnvironment", menuName = "ScriptableObjects/WorldEnvironment", order = 1)]
public class WorldEnvironmentData : ScriptableObject
{
    public bool changedDay, changedWeather;
}