using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerMenu : MonoBehaviour
{
    [SerializeField]
    private List<Sticker> stickerList;

    [SerializeField]
    private ScrollRectEx scrollRect;

    [SerializeField]
    private GameObject stickerBackArrow, stickerNextArrow;

    [SerializeField]
    private Transform stickerParent;

    [SerializeField]
    private Selectable exitButton;

    [SerializeField]
    private Selectable[] stickerArrows;

    [SerializeField]
    private Transform[] stickerParents;

    public List<Sticker> StickerList => stickerList;

    public Transform[] StickerParents => stickerParents;

    public Transform StickerParent => stickerParent;

    public GameObject StickerBackArrow => stickerBackArrow;

    public GameObject StickerNextArrow => stickerNextArrow;

    public void AdjustEquippedStickerNavigations()
    {
        if(stickerParent.childCount > 0)
        {
            for(int i = 0; i < stickerParent.childCount; i++)
            {
                Navigation nav = stickerParent.GetChild(i).GetComponent<Selectable>().navigation;

                nav.selectOnUp = null;
                nav.selectOnRight = null;
                nav.selectOnDown = null;
                nav.selectOnLeft = null;

                if (i == 0)
                {
                    nav.selectOnLeft = exitButton;

                    if(stickerParent.childCount > 1)
                       nav.selectOnRight = stickerParent.GetChild(i + 1).GetComponent<Selectable>();
                }
                else if(i >= stickerParent.childCount - 1)
                {
                    nav.selectOnRight = stickerParent.GetChild(0).GetComponent<Selectable>();
                    nav.selectOnLeft = stickerParent.GetChild(i - 1).GetComponent<Selectable>();
                }
                else
                {
                    nav.selectOnLeft = stickerParent.GetChild(i - 1).GetComponent<Selectable>();
                    nav.selectOnRight = stickerParent.GetChild(i + 1).GetComponent<Selectable>();
                }

                nav.selectOnDown = stickerBackArrow.GetComponent<Selectable>();

                stickerParent.GetChild(i).GetComponent<Selectable>().navigation = nav;
            }

            for(int j = 0; j < stickerArrows.Length; j++)
            {
                Navigation nav = stickerArrows[j].navigation;

                nav.selectOnUp = stickerParent.GetChild(0).GetComponent<Selectable>();

                stickerArrows[j].navigation = nav;
            }
        }
        else
        {
            for (int j = 0; j < stickerArrows.Length; j++)
            {
                Navigation nav = stickerArrows[j].navigation;

                nav.selectOnUp = null;

                stickerArrows[j].navigation = nav;
            }
        }
    }

    public void AjustStickerArrowNavigations()
    {
        if (stickerParent.childCount > 0)
        {
            for (int i = 0; i < stickerArrows.Length; i++)
            {
                Navigation nav = stickerArrows[i].navigation;

                nav.selectOnUp = stickerParent.GetChild(0).GetComponent<Selectable>();

                stickerArrows[i].navigation = nav;
            }

            Navigation stickerNav = stickerParent.GetChild(0).GetComponent<Selectable>().navigation;

            stickerNav.selectOnLeft = exitButton;

            stickerParent.GetChild(0).GetComponent<Selectable>().navigation = stickerNav;
        }
    }

    public void AdjustStickerNavigations()
    {
        stickerList.Clear();

        Navigation exitNav = exitButton.navigation;

        for (int i = 0; i < stickerParents.Length; i++)
        {
            if(stickerParents[i].gameObject.activeSelf)
            {
                if(stickerParents[i].childCount > 0)
                {
                    foreach(Sticker sticker in stickerParents[i].GetComponentsInChildren<Sticker>())
                    {
                        if(sticker.gameObject.activeSelf)
                        {
                            stickerList.Add(sticker);
                        }
                    }
                }
            }
        }

        if (stickerList.Count <= 0)
        {
            exitNav.selectOnRight = stickerBackArrow.GetComponent<Selectable>();

            exitButton.navigation = exitNav;

            return;
        }

        exitNav.selectOnRight = stickerParent.childCount > 0 ? stickerParent.GetChild(0).GetComponent<Selectable>() : stickerList[0].GetComponent<Selectable>();

        exitButton.navigation = exitNav;

        foreach (Selectable s in stickerArrows)
        {
            Navigation nav = s.navigation;

            nav.selectOnDown = stickerList[0].GetComponent<Selectable>();

            s.navigation = nav;
        }

        for (int i = 0; i < stickerList.Count; i++)
        {
            Navigation stickerNav = stickerList[i].GetComponent<Selectable>().navigation;

            stickerNav.selectOnLeft = null;
            stickerNav.selectOnRight = null;
            stickerNav.selectOnDown = null;
            stickerNav.selectOnUp = null;

            if (i == 0)
            {
                if (stickerList.Count > 1)
                {
                    stickerNav.selectOnLeft = stickerList[stickerList.Count - 1].GetComponent<Selectable>();
                    stickerNav.selectOnRight = stickerList[i + 1].GetComponent<Selectable>();
                }
            }
            else if (i >= stickerList.Count - 1)
            {
                stickerNav.selectOnRight = stickerList[0].GetComponent<Selectable>();
                stickerNav.selectOnLeft = stickerList[i - 1].GetComponent<Selectable>();
            }
            else
            {
                stickerNav.selectOnLeft = stickerList[i - 1].GetComponent<Selectable>();
                stickerNav.selectOnRight = stickerList[i + 1].GetComponent<Selectable>();
            }

            if (i <= 5)
            {
                stickerNav.selectOnUp = stickerArrows[0];
            }

            if (stickerList.Count > 6)
            {
                int downWardCardIndex = i + 6;
                stickerNav.selectOnDown = stickerList[downWardCardIndex >= stickerList.Count - 1 ? stickerList.Count - 1 : downWardCardIndex].GetComponent<Selectable>();
            }

            if (i >= 6)
            {
                int upWardCardIndex = i - 6;
                stickerNav.selectOnUp = stickerList[upWardCardIndex].GetComponent<Selectable>();
            }

            stickerList[i].GetComponent<Selectable>().navigation = stickerNav;
        }
    }

    public void ResetStickerArrowNavigations()
    {
        Navigation stickerArrowLeftNav = stickerArrows[0].navigation;
        Navigation stickerArrowRightNav = stickerArrows[1].navigation;

        stickerArrowLeftNav.selectOnUp = null;
        stickerArrowLeftNav.selectOnDown = null;

        stickerArrowRightNav.selectOnUp = null;
        stickerArrowRightNav.selectOnDown = null;

        stickerArrows[0].navigation = stickerArrowLeftNav;
        stickerArrows[1].navigation = stickerArrowRightNav;
    }

    public void FocusItem(RectTransform sticker)
    {
        if (InputManager.instance.ControllerPluggedIn)
            StartCoroutine(ScrollViewFocusFunctions.FocusOnItemCoroutine(sticker.GetComponent<Sticker>()._PropogateDrag.scrollView, sticker, 3f));
    }
}