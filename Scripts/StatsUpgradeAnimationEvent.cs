using UnityEngine;
using UnityEngine.EventSystems;

public class StatsUpgradeAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject healthObject, cardPointsObject, stickerPointsObject;

    public void RemoveEventTriggers()
    {
        EventTrigger healthEventTrigger = healthObject.GetComponent<EventTrigger>();
        EventTrigger cardPointsEventTrigger = cardPointsObject.GetComponent<EventTrigger>();
        EventTrigger stickerPointsEventTrigger = stickerPointsObject.GetComponent<EventTrigger>();

        Destroy(healthEventTrigger);
        Destroy(cardPointsEventTrigger);
        Destroy(stickerPointsEventTrigger);
    }
}