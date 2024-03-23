using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField]
    private SecretBossChecker secretBossChecker;

    [SerializeField]
    private Gate gate;

    private void Start()
    {
        if(secretBossChecker.BossDefeated)
        {
            gate.OpenGatesRoutine();
        }
    }
}