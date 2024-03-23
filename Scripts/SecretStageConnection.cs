using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stages
{
    [SerializeField]
    private List<Transform> stageInformations = new List<Transform>();

    public List<Transform> StageInformations => stageInformations;
}

public class SecretStageConnection : MonoBehaviour
{
    [SerializeField]
    private Stages[] stages;

    [SerializeField]
    private WorldMapMovement worldMapMovement;

    [SerializeField]
    private StageInformation connectedStage;

    [SerializeField]
    private int incrementBy, moveUpIndexBy;

    [SerializeField]
    private bool shouldIncrement, shouldMoveUp;

    private int stagesIndex;

    public StageInformation ConnectedStage => connectedStage;

    public int IncrementBy => incrementBy;

    public int MoveUpIndexBy => moveUpIndexBy;

    public bool ShouldIncrement => shouldIncrement;

    public bool ShouldMoveUp => shouldMoveUp; 

    public void SetStages(int si)
    {
        stagesIndex = si;

        worldMapMovement.LevelNodes = stages[stagesIndex].StageInformations;
    }
}