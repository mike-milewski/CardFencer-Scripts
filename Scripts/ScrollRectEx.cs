using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ScrollRectEx : ScrollRect
{
    private BattleSystem battleSystem = null;

    private bool routeToParent = false;


    /// <summary>
    /// Do action for all parents
    /// </summary>
    private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
    {
        Transform parent = transform.parent;
        while (parent != null)
        {
            foreach (var component in parent.GetComponents<Component>())
            {
                if (component is T)
                    action((T)(IEventSystemHandler)component);
            }
            parent = parent.parent;
        }
    }

    /// <summary>
    /// Always route initialize potential drag event to parents
    /// </summary>
    public override void OnInitializePotentialDrag(PointerEventData eventData)
    {
        DoForParents<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
        base.OnInitializePotentialDrag(eventData);
    }

    /// <summary>
    /// Drag event
    /// </summary>
    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (routeToParent)
            DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
        else
            base.OnDrag(eventData);
    }

    /// <summary>
    /// Begin drag event
    /// </summary>
    public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (!horizontal && Math.Abs(eventData.delta.x) > Math.Abs(eventData.delta.y))
            routeToParent = true;
        else if (!vertical && Math.Abs(eventData.delta.x) < Math.Abs(eventData.delta.y))
            routeToParent = true;
        else
            routeToParent = false;

        if (routeToParent)
            DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
        else
            base.OnBeginDrag(eventData);

        DisableDraggedItems();
    }

    /// <summary>
    /// End drag event
    /// </summary>
    public override void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (routeToParent)
            DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });
        else
            base.OnEndDrag(eventData);
        routeToParent = false;

        EnableDraggedObjects();
    }

    private void DisableDraggedItems()
    {
        foreach(Button button in transform.parent.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }
    }

    private void EnableDraggedObjects()
    {
        foreach (Button button in transform.parent.GetComponentsInChildren<Button>())
        {
            if(button.gameObject.GetComponent<Card>())
            {
                battleSystem = FindObjectOfType<BattleSystem>();
                if (battleSystem.UsingMysticCard)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
            else
            {
                button.interactable = true;
            }
        }
    }
}