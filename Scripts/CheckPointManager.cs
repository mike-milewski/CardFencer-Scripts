using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] checkPointPositions;

    [SerializeField]
    private Transform checkPoint;

    private int checkPointIndex;

    public void SetCheckPointPosition(EnemyFieldAI enemyAI)
    {
        checkPointIndex = enemyAI.EnemyCheckPointIndex;

        checkPoint.position = checkPointPositions[checkPointIndex].position;
    }
}