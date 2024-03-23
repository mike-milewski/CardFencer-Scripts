using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleSystem;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Texture2D cursorTexture;

    private Ray ray;

    private RaycastHit hit;

    private void Update()
    {
        if(!battleSystem._InputManager.ControllerPluggedIn)
        {
            if (SteamOverlayPause.instance.IsPaused) return;

            if(battleSystem.CanHoverOverTargets)
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponent<EnemyDetection>())
                    {
                        if (battleSystem.CanOnlyHoverOverEnemies)
                        {
                            battleSystem.ConfirmedTarget = true;

                            battleSystem.TargetIndex = hit.collider.GetComponent<EnemyDetection>()._BattleEnemy.IndexedEnemy;

                            battleSystem.ChooseTarget(hit.collider.GetComponent<EnemyDetection>()._BattleEnemy.gameObject);
                        }
                        else if (battleSystem.HoverOverAllEnemies)
                        {
                            battleSystem.ConfirmedTarget = true;

                            battleSystem.ChooseAllEnemyTargets();
                        }
                    }
                    else if (hit.collider.GetComponent<BattlePlayer>())
                    {
                        if (!battleSystem.CanOnlyHoverOverEnemies && !battleSystem.HoverOverAllEnemies)
                        {
                            battleSystem.ConfirmedTarget = true;

                            battleSystem.ChooseTarget(hit.collider.GetComponent<BattlePlayer>().gameObject);
                        }
                    }
                    else
                    {
                        battleSystem.DeselectTargets(false);
                    }
                }
            }
        }
    }
}
