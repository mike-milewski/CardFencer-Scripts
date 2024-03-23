using UnityEngine;

public class TreasureChestRaycast : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private RaycastHit hit;

    [SerializeField]
    private bool canSearch;

    public bool CanSearch
    {
        get
        {
            return canSearch;
        }
        set
        {
            canSearch = value;
        }
    }

    private void CheckTreasureChest()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<TreasureChest>())
            {
                TreasureChest treasureChest = hit.collider.GetComponent<TreasureChest>();

                treasureChest.OpenChest();

                canSearch = false;
            }
        }
    }
}